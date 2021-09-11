using System;

namespace MemoryString.Split
{
    public readonly ref struct SplitEnumerable
    {
        private readonly ReadOnlySpan<char> textSpan;
        private readonly ReadOnlySpan<char> separator;
        private readonly int count;
        private readonly StringSplitOptions options;

        public SplitEnumerable(
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

        public SplitEnumerator GetEnumerator() => new(textSpan, separator, count, options);
    }
}