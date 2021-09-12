using System;
using System.ComponentModel;
using MemoryString.Split;

namespace MemoryString
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringSpan
    {
        public static SplitEnumerable Split(
            this ReadOnlySpan<char> text,
            char separator,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return Split(text, new ReadOnlySpan<char>(new[] { separator }), int.MaxValue, options);
        }

        public static SplitEnumerable Split(
            this ReadOnlySpan<char> text,
            char separator,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return Split(text, new ReadOnlySpan<char>(new[] { separator }), count, options);
        }

        public static string[] Split(
            this ReadOnlySpan<char> text,
            char[] separators,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return text.ToString().Split(separators, int.MaxValue, options);
        }

        public static string[] Split(
            this ReadOnlySpan<char> text,
            char[] separators,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return text.ToString().Split(separators, count, options);
        }

        public static SplitEnumerable Split(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separator,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return Split(text, separator, int.MaxValue, options);
        }

        public static SplitEnumerable Split(
            this ReadOnlySpan<char> text,
            ReadOnlySpan<char> separator,
            int count,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return new SplitEnumerable(text, separator, count, options);
        }
    }
}