using System;

namespace Raiqub.MemoryString.Split;

public ref struct SplitByCharsEnumerator
{
    private readonly ReadOnlySpan<char> _separators;
    private readonly StringSplitOptions _options;
    private readonly bool _isSingleResult;
    private ReadOnlySpan<char> _remaining;
    private int _count;
    private ReadOnlySpan<char> _current;

    public SplitByCharsEnumerator(
        ReadOnlySpan<char> textSpan,
        ReadOnlySpan<char> separators,
        int count,
        StringSplitOptions options)
    {
        _remaining = textSpan;
        _separators = separators;
        _count = count;
        _options = options;
        _isSingleResult = count <= 1 || textSpan.Length == 0;
        _current = default;
    }

    public ReadOnlySpan<char> Current => _current;

    public bool MoveNext()
    {
        if (_count <= 0)
        {
            return false;
        }

        return _isSingleResult ? SingleResultMoveNext() : MultiResultMoveNext();
    }

    private bool SingleResultMoveNext()
    {
        _count = 0;
        var candidate = _remaining;
        if (_options.HasTrimEntries() && _remaining.Length > 0)
        {
            candidate = candidate.Trim();
        }

        if (_options.HasRemoveEmptyEntries() && candidate.Length == 0)
        {
            _current = default;
            return false;
        }
        else
        {
            _current = candidate;
            return true;
        }
    }

    private bool MultiResultMoveNext()
    {
        var actualSeparators = _separators.IsEmpty ? stackalloc char[] { ' ' } : _separators;

        while (true)
        {
            var candidate = _remaining;
            int index = candidate.IndexOfAny(actualSeparators);

            if (index == -1)
            {
                _remaining = default;
                _current = candidate;
                _count = 0;
            }
            else
            {
                _remaining = candidate.Slice(index + 1);
                _current = candidate.Slice(0, index);
            }

            if (_options.HasTrimEntries())
            {
                _current = _current.Trim();
            }

            if (_options.HasRemoveEmptyEntries() && _current.Length == 0)
            {
                if (_count == 0)
                    return false;
            }
            else
            {
                if (_count == 1)
                {
                    _remaining = default;
                    _current = candidate;
                }

                break;
            }
        }

        _count--;
        return true;
    }
}