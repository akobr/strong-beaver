using System.Collections.Generic;

namespace StrongBeaver.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToLongString(this IEnumerable<string> stringList, string separator)
        {
            return string.Join(separator, stringList);
        }
    }
}
