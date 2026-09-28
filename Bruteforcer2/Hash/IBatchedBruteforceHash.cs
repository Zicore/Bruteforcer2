using System;

namespace Bruteforcer2.Hash
{
    public interface IBatchedBruteforceHash : IBruteforceHash
    {
        int BatchSize { get; }

        // Candidates are consecutive, equally sized byte sequences.
        // Return the first matching candidate index, or -1. All candidates are hashed.
        int FindMatch(ReadOnlySpan<byte> candidates, int length, int count, ReadOnlySpan<byte> target);
    }
}
