using System.Collections.Generic;

namespace Statr.Extensions
{
    public static class StringExtensions
    {
        public static string Join(this IEnumerable<string> items, string seperator = ",")
        {
            return string.Join(seperator, items);
        }
    }
}
