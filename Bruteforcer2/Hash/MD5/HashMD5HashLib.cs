using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HashLib;

namespace Bruteforcer2.Hash.MD5
{
    public class HashMD5HashLib : HashBase, IBruteforceHash
    {
        private readonly IHash _crypto = HashFactory.Crypto.CreateMD5();

        public byte[] Hash(byte[] cArray, int offset, int count)
        {
            return HashHelper.GetHashBytes(cArray, _crypto, offset, count);
        }

        protected override string Name
        {
            get { return "MD5 HashLib"; }
        }
    }
}
