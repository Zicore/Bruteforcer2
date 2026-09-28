using System.Security.Cryptography;
using System.Text;
using Bruteforcer2.Bruteforce;
using Bruteforcer2.Hash.MD5;

// Correctness checks only: these timings are not a performance benchmark.
HashAstraMD5 astra = args.Contains("--avx512") ? new HashAstraMD5Avx512() : new HashAstraMD5();
var bouncy = new HashMD5BouncyCastle();
var checks = 0;
void Check(bool condition, string description)
{
    if (!condition)
        throw new InvalidOperationException(description);
    checks++;
}

var vectors = new (string Text, string Digest)[]
{
    ("", "d41d8cd98f00b204e9800998ecf8427e"),
    ("a", "0cc175b9c0f1b6a831c399e269772661"),
    ("abc", "900150983cd24fb0d6963f7d28e17f72"),
    ("message digest", "f96b697d7cb7938d525a2f31aaf161d0"),
    ("abcdefghijklmnopqrstuvwxyz", "c3fcd3d76192e4007dfb496cca67e13b"),
    ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "d174ab98d277d9f5a5611c2c9f419d9f"),
    ("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "57edf4a22be3c955ac49da2e2107b67a"),
};
foreach (var (text, digest) in vectors)
{
    var bytes = Encoding.ASCII.GetBytes(text);
    Check(Convert.ToHexString(astra.Hash(bytes, 0, bytes.Length)).Equals(digest, StringComparison.OrdinalIgnoreCase),
        $"Known vector: {text}");
}

var random = new Random(1729);
for (var length = 0; length <= 256; length++)
{
    for (var sample = 0; sample < 16; sample++)
    {
        var bytes = new byte[length + 17];
        random.NextBytes(bytes);
        var expected = MD5.HashData(bytes.AsSpan(7, length));
        Check(astra.Hash(bytes, 7, length).AsSpan().SequenceEqual(expected), $"Scalar length {length}");
        Check(bouncy.Hash(bytes, 7, length).AsSpan().SequenceEqual(expected), $"BouncyCastle reference length {length}");
    }
}

// Exercise each SIMD lane, partial batches, byte/word boundaries and the >55-byte fallback.
for (var length = 0; length <= 80; length++)
{
    for (var count = 1; count <= astra.BatchSize; count++)
    {
        var batch = new byte[length * count];
        random.NextBytes(batch);
        var digests = new byte[count][];
        for (var lane = 0; lane < count; lane++)
            digests[lane] = MD5.HashData(batch.AsSpan(lane * length, length));
        for (var lane = 0; lane < count; lane++)
        {
            var target = digests[lane];
            var first = Array.FindIndex(digests, digest => digest.AsSpan().SequenceEqual(target));
            Check(astra.FindMatch(batch, length, count, target) == first, $"Batch length {length}, count {count}, lane {lane}");
        }
        var missing = MD5.HashData(Encoding.ASCII.GetBytes("not in this batch"));
        Check(astra.FindMatch(batch, length, count, missing) == -1, "No-match batch");
        Check(astra.FindMatch(batch, length, count, new byte[1]) == -1, "Wrong target length");
    }
}

var clone = (HashAstraMD5)astra.Clone();
var originalResult = astra.Hash(new byte[] { 1 }, 0, 1);
var snapshot = (byte[])originalResult.Clone();
clone.Hash(new byte[] { 2 }, 0, 1);
Check(originalResult.AsSpan().SequenceEqual(snapshot), "Clones must have independent buffers");

// Reused SIMD storage must survive changing lengths in either direction and changing payloads.
if (astra.BatchSize > 1)
{
    for (var sample = 0; sample < 1000; sample++)
    {
        var length = random.Next(56);
        var batch = new byte[length * astra.BatchSize];
        random.NextBytes(batch);
        var lane = length == 0 ? 0 : sample % astra.BatchSize;
        var target = MD5.HashData(batch.AsSpan(lane * length, length));
        var first = 0;
        while (!MD5.HashData(batch.AsSpan(first * length, length)).AsSpan().SequenceEqual(target))
            first++;
        Check(astra.FindMatch(batch, length, astra.BatchSize, target) == first, "SIMD buffer reuse across lengths");
    }
}

void Search(string charset, int min, int max, int threads, string targetText, string expectedResult)
{
    var manager = new BruteforceManager();
    var target = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(targetText)));
    manager.Start((HashAstraMD5)astra.Clone(), charset, min, max, threads, target);
    try
    {
        var deadline = DateTime.UtcNow.AddSeconds(10);
        while (manager.Tasks.Any(task => task.IsActive))
        {
            if (DateTime.UtcNow > deadline)
                throw new TimeoutException("Search did not complete");
            Thread.Sleep(1);
        }
        foreach (var task in manager.Tasks)
            task.WaitForCompletion();
        manager.Update();
        Check(manager.HasResult == (expectedResult != null), "Search result presence");
        if (expectedResult != null)
            Check(manager.Result == expectedResult, $"Search result: expected {expectedResult}, got {manager.Result}");
        else
            Check(manager.Tasks.Sum(task => task.Counter) == (long)StringHelper.CalculateSpace(charset, min, max),
                "Exhaustive search must count every candidate exactly once");
    }
    finally
    {
        manager.Stop();
    }
}

// Every candidate, including carries, SIMD lane positions and uneven worker partitions.
foreach (var threads in new[] { 1, 2, 4 })
{
    foreach (var a in "abc")
    foreach (var b in "abc")
    foreach (var c in "abc")
    {
        var text = new string(new[] { a, b, c });
        Search("abc", 3, 3, threads, text, text);
    }
    Search("abc", 1, 4, threads, "outside", null);
}
Search("abc", 1, 1, 32, "c", "c");
Search("abc", 1, 1, 32, "outside", null);
Search("abc", 3, 3, 1, "a", null); // Honor minimum length.

foreach (var index in new[] { 0, 7, 8, 15, 16, 31, 4095, 4096, 4097, 6560 })
{
    var position = index;
    var chars = new char[4];
    for (var i = 3; i >= 0; i--)
    {
        chars[i] = "abcdefghi"[position % 9];
        position /= 9;
    }
    var text = new string(chars);
    Search("abcdefghi", 4, 4, 1, text, text);
}
Search("abcdefghi", 4, 4, 3, "outside", null);
Search("a", 55, 56, 1, new string('a', 56), new string('a', 56));

// A stopped worker must not re-enter a subsequent run.
var restart = new BruteforceManager();
restart.Start(astra, "abcdefghi", 8, 8, 2, new string('0', 32));
restart.Stop();
Check(restart.Tasks.All(task => !task.IsActive), "Stop joins workers");
restart.Start(astra, "abc", 3, 3, 2, Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes("ccc"))));
foreach (var task in restart.Tasks)
    task.WaitForCompletion();
restart.Update();
Check(restart.HasResult && restart.Result == "ccc", "Restart result");
restart.Stop();

Console.WriteLine($"PASS: {checks} checks; {astra}; batch size {astra.BatchSize}");
