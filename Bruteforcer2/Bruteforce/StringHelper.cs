using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bruteforcer2.Bruteforce
{
    public class StringHelper
    {
        public static decimal CalculateSpace(String charset, int minWordLength, int maxWordLength)
        {
            decimal space = 0;
            for (int i = minWordLength; i <= maxWordLength; i++)
            {
                space += (decimal)Math.Pow(charset.Length, i);
            }
            return space;
        }
    }
}
