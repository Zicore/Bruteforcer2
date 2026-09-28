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
        private readonly byte[] _result = new byte[16];

        // The returned buffer belongs to this instance and is overwritten by the next call.
        public byte[] Hash(byte[] buffer, int offset, int count)
        {
            _crypto.BlockUpdate(buffer, offset, count);
            _crypto.DoFinal(_result, 0);
            return _result;
        }

        protected override string Name
        {
            get { return "MD5 BouncyCastle"; }
        }
    }
}
