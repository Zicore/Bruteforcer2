using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Bruteforcer2.Hash.MD5
{
    // Separate selection so the existing AVX2 implementation remains a direct comparison.
    public sealed class HashAstraMD5Avx512 : HashAstraMD5
    {
        private readonly uint[] _words = new uint[16 * 16];
        private int _preparedLength = -1;
        public override int BatchSize => Avx512F.IsSupported ? 16 : base.BatchSize;
        protected override string Name => Avx512F.IsSupported ? "AstraMD5 (AVX-512 x16)" : base.Name;

        public override int FindMatch(ReadOnlySpan<byte> candidates, int length, int count, ReadOnlySpan<byte> target)
        {
            if (!Avx512F.IsSupported || length > 55 || count != 16)
                return base.FindMatch(candidates, length, count, target);
            if (length < 0 || candidates.Length != (long)length * count)
                throw new ArgumentException("Invalid candidate batch.");

            var words = _words.AsSpan();
            if (_preparedLength != length)
            {
                words.Clear();
                for (var lane = 0; lane < 16; lane++)
                    words[14 * 16 + lane] = (uint)length * 8;
                _preparedLength = length;
            }
            var fullWords = length / 4;
            for (var lane = 0; lane < 16; lane++)
            {
                var input = candidates.Slice(lane * length, length);
                for (var word = 0; word < fullWords; word++)
                    words[word * 16 + lane] = BinaryPrimitives.ReadUInt32LittleEndian(input.Slice(word * 4, 4));
                var lastWord = 0x80u << ((length & 3) * 8);
                for (var i = fullWords * 4; i < length; i++)
                    lastWord |= (uint)input[i] << ((i & 3) * 8);
                words[fullWords * 16 + lane] = lastWord;
            }

            CompressAvx512(MemoryMarshal.Cast<uint, Vector512<uint>>(words), out var a, out var b, out var c, out var d);
            if (target.Length != 16)
                return -1;

            var equal = Avx512F.CompareEqual(a, Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target)))
                & Avx512F.CompareEqual(b, Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(4))))
                & Avx512F.CompareEqual(c, Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(8))))
                & Avx512F.CompareEqual(d, Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.Slice(12))));
            var mask = Vector512.ExtractMostSignificantBits(equal);
            return mask == 0 ? -1 : BitOperations.TrailingZeroCount(mask);
        }
    }
}
