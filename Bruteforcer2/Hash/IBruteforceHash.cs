using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bruteforcer2.Hash
{
    public interface IBruteforceHash : ICloneable
    {
        // Implementations may reuse the result buffer; consume or copy it before the next call.
        byte[] Hash(byte[] buffer, int offset, int count);
    }
}
