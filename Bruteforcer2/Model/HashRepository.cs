using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bruteforcer2.Hash;
using Bruteforcer2.Hash.MD5;
using Bruteforcer2.Libs.Hash;

namespace Bruteforcer2.Model
{
    public class HashRepository
    {
        public static ObservableCollection<IBruteforceHash> GetHashes()
        {
            var hashes = new ObservableCollection<IBruteforceHash>
            {
                new HashMD5BouncyCastle(),
                new HashMD5HashLib(),
                new HashMD5Mono(),
                new HashMD5Managed(),
                new HashMD5Unmanaged(),
                new NullHash()
            };

            return hashes;
        }
    }
}
