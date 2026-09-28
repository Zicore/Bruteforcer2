using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Bruteforcer2.Hash.MD5
{
    // One instance per worker. Result buffers may be overwritten by the next call.
    public partial class HashAstraMD5 : HashBase, IBatchedBruteforceHash
    {
        private readonly byte[] _result = new byte[16];
        private readonly uint[] _transposed = new uint[16 * 8];
        private int _preparedLength = -1;
        public virtual int BatchSize => Avx2.IsSupported ? 8 : 1;
        protected override string Name => Avx2.IsSupported ? "AstraMD5 (AVX2 x8)" : "AstraMD5 (Scalar)";

        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            HashSingle(buffer.AsSpan(offset, count), _result);
            return _result;
        }

        public virtual int FindMatch(ReadOnlySpan<byte> candidates, int length, int count, ReadOnlySpan<byte> target)
        {
            if (length < 0 || count < 1 || count > BatchSize || candidates.Length != (long)length * count)
                throw new ArgumentException("Invalid candidate batch.");

            if (Avx2.IsSupported && length <= 55 && count == 8)
                return FindMatchAvx2(candidates, length, target);

            // Partial batches and CPUs without AVX2 use the same scalar implementation as Hash().
            var match = -1;
            for (var lane = 0; lane < count; lane++)
            {
                HashSingle(candidates.Slice(lane * length, length), _result);
                if (_result.AsSpan().SequenceEqual(target) && match < 0)
                    match = lane;
            }
            return match;
        }

        private static void HashSingle(ReadOnlySpan<byte> input, Span<byte> output)
        {
            if (input.Length > 55)
            {
                // General multi-block hashing is outside this specialized kernel.
                System.Security.Cryptography.MD5.HashData(input, output);
                return;
            }

            Span<uint> words = stackalloc uint[16];
            words.Clear();
            var fullWords = input.Length / 4;
            for (var i = 0; i < fullWords; i++)
                words[i] = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(i * 4, 4));
            for (var i = fullWords * 4; i < input.Length; i++)
                words[i / 4] |= (uint)input[i] << ((i & 3) * 8);
            words[input.Length / 4] |= 0x80u << ((input.Length & 3) * 8);
            words[14] = (uint)input.Length * 8;

            CompressScalar(words, out var a, out var b, out var c, out var d);
            BinaryPrimitives.WriteUInt32LittleEndian(output, a);
            BinaryPrimitives.WriteUInt32LittleEndian(output.Slice(4), b);
            BinaryPrimitives.WriteUInt32LittleEndian(output.Slice(8), c);
            BinaryPrimitives.WriteUInt32LittleEndian(output.Slice(12), d);
        }

        private int FindMatchAvx2(ReadOnlySpan<byte> candidates, int length, ReadOnlySpan<byte> target)
        {
            // Structure of arrays: each vector contains the same MD5 word from eight messages.
            var transposed = _transposed.AsSpan();
            if (_preparedLength != length)
            {
                transposed.Clear();
                for (var lane = 0; lane < 8; lane++)
                    transposed[14 * 8 + lane] = (uint)length * 8;
                _preparedLength = length;
            }
            var fullWords = length / 4;
            for (var lane = 0; lane < 8; lane++)
            {
                var input = candidates.Slice(lane * length, length);
                for (var word = 0; word < fullWords; word++)
                    transposed[word * 8 + lane] = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(word * 4, 4));
                var lastWord = 0x80u << ((length & 3) * 8);
                for (var i = fullWords * 4; i < length; i++)
                    lastWord |= (uint)input[i] << ((i & 3) * 8);
                transposed[fullWords * 8 + lane] = lastWord;
            }

            CompressAvx2(MemoryMarshal.Cast<uint, Vector256<uint>>(transposed), out var a, out var b, out var c, out var d);
            if (target.Length != 16)
                return -1;

            var equal = Avx2.CompareEqual(a, Vector256.Create(BinaryPrimitives.ReadUInt32LittleEndian(target)))
                & Avx2.CompareEqual(b, Vector256.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(4))))
                & Avx2.CompareEqual(c, Vector256.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(8))))
                & Avx2.CompareEqual(d, Vector256.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(12))));
            var mask = (uint)Avx2.MoveMask(equal.AsByte());
            return mask == 0 ? -1 : BitOperations.TrailingZeroCount(mask) / 4;
        }
    }
}
