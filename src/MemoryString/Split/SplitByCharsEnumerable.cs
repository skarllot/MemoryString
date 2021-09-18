using System;
using System.Collections.Generic;

namespace MemoryString.Split
{
    public readonly ref struct SplitByCharsEnumerable
    {
        private readonly ReadOnlySpan<char> textSpan;
        private readonly ReadOnlySpan<char> separators;
        private readonly int count;
        private readonly StringSplitOptions options;

        public SplitByCharsEnumerable(
            ReadOnlySpan<char> textSpan,
            ReadOnlySpan<char> separators,
            int count,
            StringSplitOptions options)
        {
            this.textSpan = textSpan;
            this.separators = separators;
            this.count = count;
            this.options = options;
        }

        public SplitByCharsEnumerator GetEnumerator() => new(textSpan, separators, count, options);

        public List<string> ToStringList()
        {
            var list = new List<string>();
            foreach (var span in this)
            {
                list.Add(span.ToString());
            }

            return list;
        }
    }
}