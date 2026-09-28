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
            decimal words = 1;
            for (int i = 1; i <= maxWordLength; i++)
            {
                words *= charset.Length;
                if (i >= minWordLength)
                    space += words;
            }
            return space;
        }
    }
}
