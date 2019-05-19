using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bruteforcer2.Hash
{
    public interface IBruteforceHash : ICloneable
    {
        byte[] Hash(byte[] buffer, int offset, int count);
    }
}
