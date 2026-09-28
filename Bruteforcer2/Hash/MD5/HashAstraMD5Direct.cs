using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Bruteforcer2.Hash.MD5
{
    public sealed class HashAstraMD5Direct : HashAstraMD5Avx512, IPreparedBruteforceHash
    {
        protected override string Name => Avx512F.IsSupported ? "AstraMD5 (AVX-512 Direct)" : base.Name;

        public IPreparedBruteforceSearch CreateSearch(byte[] charset, int length, long start, long count, byte[] target)
        {
            if (!Avx512F.IsSupported || length < 1 || length > 55)
                return null;
            return new DirectSearch(charset, length, start, count, target);
        }

        private sealed class DirectSearch : IPreparedBruteforceSearch
        {
            private const int Width = 16;
            private readonly uint[] _words = new uint[16 * Width];
            private readonly byte[] _charset;
            private readonly int[] _indices;
            private readonly int[] _step;
            private readonly int _length;
            private readonly int _firstStep;
            private readonly int _lastStep;
            private readonly bool _validTarget;
            private readonly Vector512<uint> _targetA, _targetB, _targetC, _targetD;
            private long _remaining;

            public DirectSearch(byte[] charset, int length, long start, long count, byte[] target)
            {
                ArgumentNullException.ThrowIfNull(charset);
                ArgumentNullException.ThrowIfNull(target);
                if (charset.Length == 0 || start < 0 || count < 0)
                    throw new ArgumentException("Invalid search range.");
                long total = 1;
                for (var i = 0; i < length; i++)
                    total = checked(total * charset.Length);
                if (start > total || count > total - start)
                    throw new ArgumentException("Search range exceeds the candidate space.");

                _charset = (byte[])charset.Clone();
                _length = length;
                _remaining = count;
                _indices = new int[length * Width];
                _step = new int[length];
                _firstStep = length;
                _lastStep = -1;
                var stride = Width;
                for (var position = length - 1; position >= 0 && stride > 0; position--)
                {
                    _step[position] = stride % charset.Length;
                    stride /= charset.Length;
                    if (_step[position] != 0)
                    {
                        _firstStep = position;
                        if (_lastStep < 0)
                            _lastStep = position;
                    }
                }

                // Lane k begins at start+k and advances by 16 after each compression.
                // Padding and length stay in the same transposed words for the entire range.
                for (var lane = 0; lane < Math.Min(Width, count); lane++)
                {
                    var ordinal = start + lane;
                    for (var position = length - 1; position >= 0; position--)
                    {
                        var index = (int)(ordinal % charset.Length);
                        ordinal /= charset.Length;
                        _indices[position * Width + lane] = index;
                        _words[(position / 4) * Width + lane] |= (uint)charset[index] << ((position & 3) * 8);
                    }
                    _words[(length / 4) * Width + lane] |= 0x80u << ((length & 3) * 8);
                    _words[14 * Width + lane] = (uint)length * 8;
                }

                _validTarget = target.Length == 16;
                if (_validTarget)
                {
                    _targetA = Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target));
                    _targetB = Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.AsSpan(4)));
                    _targetC = Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.AsSpan(8)));
                    _targetD = Vector512.Create(BinaryPrimitives.ReadUInt32LittleEndian(target.AsSpan(12)));
                }
            }

            public int Search(int budget, out byte[] result)
            {
                if (budget <= 0 || (budget & (Width - 1)) != 0)
                    throw new ArgumentOutOfRangeException(nameof(budget));
                result = null;
                var processed = 0;
                while (_remaining > 0 && processed < budget)
                {
                    var lanes = (int)Math.Min(Width, _remaining);
                    CompressAvx512(MemoryMarshal.Cast<uint, Vector512<uint>>(_words.AsSpan()),
                        out var a, out var b, out var c, out var d);
                    processed += lanes;
                    _remaining -= lanes;
                    if (_validTarget)
                    {
                        var equal = Avx512F.CompareEqual(a, _targetA) & Avx512F.CompareEqual(b, _targetB)
                            & Avx512F.CompareEqual(c, _targetC) & Avx512F.CompareEqual(d, _targetD);
                        // Incomplete batches may have stale lanes: never report those as hits.
                        var mask = Vector512.ExtractMostSignificantBits(equal) & ((1UL << lanes) - 1);
                        if (mask != 0)
                        {
                            var lane = BitOperations.TrailingZeroCount(mask);
                            result = new byte[_length];
                            for (var position = 0; position < _length; position++)
                                result[position] = (byte)(_words[(position / 4) * Width + lane] >> ((position & 3) * 8));
                            _remaining = 0;
                            return processed;
                        }
                    }
                    if (_remaining > 0)
                        Advance((int)Math.Min(Width, _remaining));
                }
                return processed;
            }

            private void Advance(int lanes)
            {
                // Add the precomputed base-N representation of 16, touching only changed bytes.
                // No division, candidate byte array, batch copy or word transposition here.
                for (var lane = 0; lane < lanes; lane++)
                {
                    var carry = 0;
                    for (var position = _lastStep; position >= 0 && (position >= _firstStep || carry != 0); position--)
                    {
                        var slot = position * Width + lane;
                        var next = _indices[slot] + _step[position] + carry;
                        carry = next >= _charset.Length ? 1 : 0;
                        if (carry != 0)
                            next -= _charset.Length;
                        _indices[slot] = next;
                        var shift = (position & 3) * 8;
                        var word = (position / 4) * Width + lane;
                        _words[word] = (_words[word] & ~(0xffu << shift)) | ((uint)_charset[next] << shift);
                    }
                }
            }
        }
    }
}
