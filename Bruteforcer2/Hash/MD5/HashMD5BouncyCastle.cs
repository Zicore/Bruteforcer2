using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace Bruteforcer2.Hash.MD5
{
    public class HashMD5BouncyCastle : HashBase, IBruteforceHash
    {
        private readonly MD5Digest _crypto = new MD5Digest();
        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            _crypto.BlockUpdate(buffer, offset, count);
            int size = _crypto.GetDigestSize();
            buffer = new byte[size];
            _crypto.DoFinal(buffer, 0);
            return buffer;
        }

        protected override string Name
        {
            get { return "MD5 BouncyCastle"; }
        }
    }
}
