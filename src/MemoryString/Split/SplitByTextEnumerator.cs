using System;

namespace MemoryString.Split
{
    public ref struct SplitByTextEnumerator
    {
        private readonly ReadOnlySpan<char> separator;
        private readonly StringSplitOptions options;
        private readonly bool isSingleResult;
        private ReadOnlySpan<char> textSpan;
        private int count;

        public SplitByTextEnumerator(
            ReadOnlySpan<char> textSpan,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options)
        {
            this.textSpan = textSpan;
            this.separator = separator;
            this.count = count;
            this.options = options;
            this.isSingleResult = count <= 1 || textSpan.Length == 0;
            this.Current = ReadOnlySpan<char>.Empty;
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public bool MoveNext()
        {
            if (count <= 0)
            {
                return false;
            }

            if (isSingleResult)
            {
                count = 0;
                var candidate = textSpan;
                if (options.HasTrimEntries() && textSpan.Length > 0)
                {
                    candidate = candidate.Trim();
                }

                if (options.HasRemoveEmptyEntries() && candidate.Length == 0)
                {
                    Current = ReadOnlySpan<char>.Empty;
                    return false;
                }
                else
                {
                    Current = candidate;
                    return true;
                }
            }

            var finalSeparator = separator.IsEmpty ? stackalloc char[] { ' ' } : separator;

            while (true)
            {
                var candidate = textSpan;
                var index = candidate.IndexOf(finalSeparator);
                if (index == -1)
                {
                    textSpan = ReadOnlySpan<char>.Empty;
                    Current = candidate;
                    count = 0;
                }
                else
                {
                    textSpan = candidate.Slice(index + finalSeparator.Length);
                    Current = candidate.Slice(0, index);
                }

                if (options.HasTrimEntries())
                {
                    Current = Current.Trim();
                }

                if (options.HasRemoveEmptyEntries() && Current.Length == 0)
                {
                    if (count == 0)
                        return false;
                }
                else
                {
                    if (count == 1)
                    {
                        textSpan = ReadOnlySpan<char>.Empty;
                        Current = candidate;
                    }

                    break;
                }
            }

            count--;
            return true;
        }
    }
}