using System;
using System.Collections.Generic;

namespace MemoryString.Split
{
    public readonly ref struct SplitByTextEnumerable
    {
        private readonly ReadOnlySpan<char> textSpan;
        private readonly ReadOnlySpan<char> separator;
        private readonly int count;
        private readonly StringSplitOptions options;

        public SplitByTextEnumerable(
            ReadOnlySpan<char> textSpan,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options)
        {
            this.textSpan = textSpan;
            this.separator = separator;
            this.count = count;
            this.options = options;
        }

        public SplitByTextEnumerator GetEnumerator() => new(textSpan, separator, count, options);

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