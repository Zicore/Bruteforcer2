using System;
using System.Reflection;

namespace Bruteforcer2.Hash
{
    public class HashBase : ICloneable
    {
        protected virtual string Name { get; set; } = "";

        public object Clone()
        {
            return Assembly.GetExecutingAssembly().CreateInstance(GetType().FullName);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}