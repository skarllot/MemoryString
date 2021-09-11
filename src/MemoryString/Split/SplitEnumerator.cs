using System;

namespace MemoryString.Split
{
    public ref struct SplitEnumerator
    {
        private readonly ReadOnlySpan<char> separator;
        private readonly StringSplitOptions options;
        private ReadOnlySpan<char> textSpan;
        private int count;

        public SplitEnumerator(
            ReadOnlySpan<char> textSpan,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options)
        {
            this.textSpan = textSpan;
            this.separator = separator;
            this.count = count;
            this.options = options;
            this.Current = ReadOnlySpan<char>.Empty;
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public bool MoveNext()
        {
            if (count == 0)
            {
                return false;
            }

            do
            {
                var lastSpan = textSpan;
                if (lastSpan.Length == 0)
                    return false;

                var index = lastSpan.IndexOf(separator);
                if (index == -1)
                {
                    textSpan = ReadOnlySpan<char>.Empty;
                    Current = lastSpan;
                }
                else
                {
                    textSpan = lastSpan.Slice(index + separator.Length);
                    Current = lastSpan.Slice(0, index);
                }
            } while (options.HasRemoveEmptyEntries() && Current.Length == 0);

            if (options.HasTrimEntries())
            {
                Current = Current.Trim(' ');
            }

            count--;
            return true;
        }
    }
}