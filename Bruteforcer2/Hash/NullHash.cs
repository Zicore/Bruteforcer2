using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bruteforcer2.Hash
{
    class NullHash : HashBase, IBruteforceHash
    {
        private readonly byte[] _result = new byte[] { 0 };

        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            return _result;
        }

        protected override string Name
        {
            get { return "Null Hash"; }
        }
    }
}
