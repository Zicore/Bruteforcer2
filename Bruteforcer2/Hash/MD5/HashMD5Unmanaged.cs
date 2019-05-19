using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // Unmanaged
using System.Text;

namespace Bruteforcer2.Hash.MD5
{
    public class HashMD5Unmanaged : HashBase, IBruteforceHash
    {
        // Unmanaged
        private readonly MD5CryptoServiceProvider _crypto = new MD5CryptoServiceProvider();

        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            return HashHelper.GetHashBytes(buffer, _crypto, offset, count);
        }

        protected override string Name
        {
            get { return "MD5 Unmanaged"; }
        }
    }
}
