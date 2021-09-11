using System.Collections.Generic;
using MemoryString.Split;

namespace MemoryString.Tests
{
    public static class EnumerableExtensions
    {
        internal static IReadOnlyList<string> ToList(in this SplitEnumerable enumerable)
        {
            var result = new List<string>();
            foreach (var item in enumerable)
            {
                result.Add(item.ToString());
            }

            return result;
        }
    }
}