using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bruteforcer2.Hash
{
    class NullHash : HashBase, IBruteforceHash
    {
        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            return new byte[]{0};
        }

        protected override string Name
        {
            get { return "Null Hash"; }
        }
    }
}
