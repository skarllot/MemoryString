using System;
using System.Collections.Generic;

namespace Raiqub.MemoryString.Split
{
    public readonly ref struct SplitByTextEnumerable
    {
        private readonly ReadOnlySpan<char> _textSpan;
        private readonly ReadOnlySpan<char> _separator;
        private readonly int _count;
        private readonly StringSplitOptions _options;

        public SplitByTextEnumerable(
            ReadOnlySpan<char> textSpan,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options)
        {
            _textSpan = textSpan;
            _separator = separator;
            _count = count;
            _options = options;
        }

        public SplitByTextEnumerator GetEnumerator() => new(_textSpan, _separator, _count, _options);

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