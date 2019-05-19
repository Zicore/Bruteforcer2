using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bruteforcer2.Libs.Hash;

// Managed

namespace Bruteforcer2.Hash.MD5
{
    public class HashMD5Mono : HashBase, IBruteforceHash
    {
        private readonly MD5CryptoServiceProvider _crypto = new MD5CryptoServiceProvider();

        public byte[] Hash(byte[] cArray, int offset, int count)
        {
            return HashHelper.GetHashBytes(cArray, _crypto, offset, count);
        }

        protected override string Name
        {
            get { return "MD5 Mono"; }
        }
    }
}
