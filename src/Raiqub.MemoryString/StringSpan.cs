using System;
using System.ComponentModel;
using Raiqub.MemoryString.Split;
using Validation;

namespace Raiqub.MemoryString;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class StringSpan
{
    public static SplitByCharsEnumerable SplitByChar(
        this ReadOnlySpan<char> text,
        char separator,
        StringSplitOptions options = StringSplitOptions.None)
    {
        return SplitByChar(text, separator, int.MaxValue, options);
    }

    public static SplitByCharsEnumerable SplitByChar(
        this ReadOnlySpan<char> text,
        char separator,
        int count,
        StringSplitOptions options = StringSplitOptions.None)
    {
        var separators = separator == ' '
            ? ReadOnlySpan<char>.Empty
            : new ReadOnlySpan<char>(new[] { separator });

        return SplitByChar(text, separators, count, options);
    }

    public static SplitByCharsEnumerable SplitByChar(
        this ReadOnlySpan<char> text,
        ReadOnlySpan<char> separators,
        StringSplitOptions options = StringSplitOptions.None)
    {
        return SplitByChar(text, separators, int.MaxValue, options);
    }

    public static SplitByCharsEnumerable SplitByChar(
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