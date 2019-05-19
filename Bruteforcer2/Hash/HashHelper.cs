using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HashLib;

namespace Bruteforcer2.Hash
{
    public class HashHelper
    {
        /// <summary>
        /// Gibt einen Hash als String zurück
        /// </summary>
        /// <param name="textToHash">string der Gehasht werden soll.</param>
        /// <param name="crypto">Crypto algorithm</param>
        /// <returns>Hash als string.</returns>
        public static string GetHash(string textToHash, HashAlgorithm crypto)
        {
            //Prüfen ob Daten übergeben wurden.
            if (string.IsNullOrEmpty(textToHash))
            {
                return string.Empty;
            }

            //Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
            //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.

            byte[] textToHashBytes = Encoding.Default.GetBytes(textToHash);
            byte[] result = crypto.ComputeHash(textToHashBytes);

            return System.BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
        }

        public static byte[] GetHashBytes(byte[] t, IHash crypto, int offset, int count)
        {
            crypto.TransformBytes(t, offset, count);
            var r = crypto.TransformFinal();
            return r.GetBytes();
        }

        public static byte[] GetHashBytes(byte[] t, HashAlgorithm crypto, int offset, int count)
        {
            return crypto.ComputeHash(t, offset, count);
        }

        public static string Prepare(String str)
        {
            var sb = new StringBuilder();
            int counter = 1;
            bool fillLast = str.Length % 2 != 0;

            foreach (char c in str)
            {
                sb.Append(c);

                if (counter % 2 == 0 && counter < str.Length)
                {
                    sb.Append("-");
                }

                if (counter == str.Length - 1 && fillLast)
                {
                    sb.Append("0");
                }

                counter++;
            }
            return sb.ToString().ToUpper();
        }

        public static byte[] ToBuffer(String str)
        {
            str = Prepare(str);
            String[] arr = str.Split('-');
            byte[] array = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++) array[i] = Convert.ToByte(arr[i], 16);
            return array;
        }

        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        public static String ToHexString(byte[] bytes)
        {
            return System.BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        }
    }
}
