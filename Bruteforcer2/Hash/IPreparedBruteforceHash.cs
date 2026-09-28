namespace Bruteforcer2.Hash
{
    public interface IPreparedBruteforceHash : IBruteforceHash
    {
        // Null means that this length or CPU must use the existing search path.
        IPreparedBruteforceSearch CreateSearch(byte[] charset, int length, long start, long count, byte[] target);
    }

    public interface IPreparedBruteforceSearch
    {
        // Budget must be a positive multiple of 16. Returns the number of valid candidates
        // examined, including the complete final batch on a hit. Result is allocated only on a hit.
        int Search(int budget, out byte[] result);
    }
}
