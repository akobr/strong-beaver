using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongBeaver.Core.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum flag)
        {
            return Enum.GetValues(flag.GetType()).Cast<Enum>().Where(flag.HasFlag);
        }
    }
}