namespace Raiqub.MemoryString.Split;

public readonly ref struct SplitByCharsEnumerable
{
    private readonly ReadOnlySpan<char> _textSpan;
    private readonly ReadOnlySpan<char> _separators;
    private readonly int _count;
    private readonly StringSplitOptions _options;

    public SplitByCharsEnumerable(
        ReadOnlySpan<char> textSpan,
        ReadOnlySpan<char> separators,
        int count,
        StringSplitOptions options)
    {
        _textSpan = textSpan;
        _separators = separators;
        _count = count;
        _options = options;
    }

    public SplitByCharsEnumerator GetEnumerator() => new(_textSpan, _separators, _count, _options);

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