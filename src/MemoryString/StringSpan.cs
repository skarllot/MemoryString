using System;
using System.ComponentModel;
using MemoryString.Split;
using Validation;

namespace MemoryString
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringSpan
    {
        public static SplitByTextEnumerable Split(
            this ReadOnlySpan<char> text,
            char separator,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return Split(text, separator, int.MaxValue, options);
        }

        public static SplitByTextEnumerable Split(
            this ReadOnlySpan<char> text,
            char separator,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            var textSeparator = separator == ' '
                ? ReadOnlySpan<char>.Empty
                : new ReadOnlySpan<char>(new[] { separator });

            return SplitByText(text, textSeparator, count, options);
        }

        public static SplitByCharsEnumerable SplitByChars(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separators,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitByChars(text, separators, int.MaxValue, options);
        }

        public static SplitByCharsEnumerable SplitByChars(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separators,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            Requires.Range(count >= 0, nameof(count));
            CheckStringSplitOptions(options);

            return new SplitByCharsEnumerable(text, separators, count, options);
        }

        public static SplitByTextEnumerable SplitByText(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separator,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitByText(text, separator, int.MaxValue, options);
        }

        public static SplitByTextEnumerable SplitByText(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            Requires.Range(count >= 0, nameof(count));
            CheckStringSplitOptions(options);

            return new SplitByTextEnumerable(text, separator, count, options);
        }

        private static void CheckStringSplitOptions(StringSplitOptions options)
        {
#if NET5_0_OR_GREATER
            const StringSplitOptions allValidFlags =
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
#else
            const StringSplitOptions allValidFlags = StringSplitOptions.RemoveEmptyEntries;
#endif

            if ((options & ~allValidFlags) != 0)
            {
                Throws.InvalidEnum(nameof(options), options);
            }
        }
    }
}