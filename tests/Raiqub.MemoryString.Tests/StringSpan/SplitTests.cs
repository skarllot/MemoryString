using System;
using Xunit;

namespace Raiqub.MemoryString.Tests.StringSpan
{
    // Ref: https://github.com/dotnet/corefx/blob/release/3.1/src/System.Runtime/tests/System/String.SplitTests.cs
    public class SplitTests
    {
        [Fact]
        public static void SplitInvalidCount()
        {
            const string value = "a,b";
            const int count = -1;
            const StringSplitOptions options = StringSplitOptions.None;

            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByChar(',', count));
            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByChar(',', count, options));
            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByChar(new[] { ',' }, count));
            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByChar(new[] { ',' }, count, options));
            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByText(",", count));
            Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().SplitByText(",", count, options));
            //Assert.Throws<ArgumentOutOfRangeException>("count", () => value.AsSpan().Split(new[] { "," }, count, options));
        }

        [Fact]
        public static void SplitInvalidOptions()
        {
            const string value = "a,b";
            const int count = int.MaxValue;
            const StringSplitOptions optionsTooLow = StringSplitOptions.None - 1;
#if NET5_0_OR_GREATER
            const StringSplitOptions optionsTooHigh =
                (StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) + 1;
#else
            const StringSplitOptions optionsTooHigh = StringSplitOptions.RemoveEmptyEntries + 1;
#endif

            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(',', optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(',', optionsTooHigh));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(',', count, optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(',', count, optionsTooHigh));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(new[] { ',' }, optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(new[] { ',' }, optionsTooHigh));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(new[] { ',' }, count, optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByChar(new[] { ',' }, count, optionsTooHigh));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByText(",", optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByText(",", optionsTooHigh));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByText(",", count, optionsTooLow));
            Assert.ThrowsAny<ArgumentException>(() => value.AsSpan().SplitByText(",", count, optionsTooHigh));
            //Assert.Throws<ArgumentException>(null, () => value.AsSpan().Split(new[] { "," }, optionsTooLow));
            //Assert.Throws<ArgumentException>(null, () => value.AsSpan().Split(new[] { "," }, optionsTooHigh));
            //Assert.Throws<ArgumentException>(null, () => value.AsSpan().Split(new[] { "," }, count, optionsTooLow));
            //Assert.Throws<ArgumentException>(null, () => value.AsSpan().Split(new[] { "," }, count, optionsTooHigh));
        }

        [Fact]
        public static void SplitZeroCountEmptyResult()
        {
            ReadOnlySpan<char> value = "a,b";
            const int count = 0;
            const StringSplitOptions options = StringSplitOptions.None;

            string[] expected = new string[0];

            Assert.Equal(expected, value.SplitByChar(',', count).ToStringList());
            Assert.Equal(expected, value.SplitByChar(',', count, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }

        [Fact]
        public static void SplitEmptyValueWithRemoveEmptyEntriesOptionEmptyResult()
        {
            ReadOnlySpan<char> value = string.Empty;
            const int count = int.MaxValue;
            const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;

            string[] expected = new string[0];

            Assert.Equal(expected, value.SplitByChar(',', options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(',', count, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { "," }, options));
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }

        [Fact]
        public static void SplitOneCountSingleResult()
        {
            ReadOnlySpan<char> value = "a,b";
            const int count = 1;
            const StringSplitOptions options = StringSplitOptions.None;

            string[] expected = new[] { value.ToString() };

            Assert.Equal(expected, value.SplitByChar(',', count).ToStringList());
            Assert.Equal(expected, value.SplitByChar(',', count, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }

        [Fact]
        public static void SplitNoMatchSingleResult()
        {
            ReadOnlySpan<char> value = "a b";
            const int count = int.MaxValue;
            const StringSplitOptions options = StringSplitOptions.None;

            string[] expected = new[] { value.ToString() };

            Assert.Equal(expected, value.SplitByChar(',').ToStringList());
            Assert.Equal(expected, value.SplitByChar(',', options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(',', count, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { ',' }, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",").ToStringList());
            Assert.Equal(expected, value.SplitByText(",", options).ToStringList());
            Assert.Equal(expected, value.SplitByText(",", count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { "," }, options));
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }

        private const int M = int.MaxValue;

        [Theory]
        [InlineData("", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("", ',', 1, StringSplitOptions.None, new[] { "" })]
        [InlineData("", ',', 2, StringSplitOptions.None, new[] { "" })]
        [InlineData("", ',', 3, StringSplitOptions.None, new[] { "" })]
        [InlineData("", ',', 4, StringSplitOptions.None, new[] { "" })]
        [InlineData("", ',', M, StringSplitOptions.None, new[] { "" })]
        [InlineData("", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("", ',', 1, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("", ',', 2, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("", ',', 3, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("", ',', 4, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("", ',', M, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",", ',', 1, StringSplitOptions.None, new[] { "," })]
        [InlineData(",", ',', 2, StringSplitOptions.None, new[] { "", "" })]
        [InlineData(",", ',', 3, StringSplitOptions.None, new[] { "", "" })]
        [InlineData(",", ',', 4, StringSplitOptions.None, new[] { "", "" })]
        [InlineData(",", ',', M, StringSplitOptions.None, new[] { "", "" })]
        [InlineData(",", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "," })]
        [InlineData(",", ',', 2, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",", ',', 3, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",", ',', 4, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",", ',', M, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",,", ',', 1, StringSplitOptions.None, new[] { ",," })]
        [InlineData(",,", ',', 2, StringSplitOptions.None, new[] { "", ",", })]
        [InlineData(",,", ',', 3, StringSplitOptions.None, new[] { "", "", "" })]
        [InlineData(",,", ',', 4, StringSplitOptions.None, new[] { "", "", "" })]
        [InlineData(",,", ',', M, StringSplitOptions.None, new[] { "", "", "" })]
        [InlineData(",,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",," })]
        [InlineData(",,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",,", ',', M, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("ab", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("ab", ',', 1, StringSplitOptions.None, new[] { "ab" })]
        [InlineData("ab", ',', 2, StringSplitOptions.None, new[] { "ab" })]
        [InlineData("ab", ',', 3, StringSplitOptions.None, new[] { "ab" })]
        [InlineData("ab", ',', 4, StringSplitOptions.None, new[] { "ab" })]
        [InlineData("ab", ',', M, StringSplitOptions.None, new[] { "ab" })]
        [InlineData("ab", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("ab", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "ab" })]
        [InlineData("ab", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "ab" })]
        [InlineData("ab", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "ab" })]
        [InlineData("ab", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "ab" })]
        [InlineData("ab", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "ab" })]
        [InlineData("a,b", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,b", ',', 1, StringSplitOptions.None, new[] { "a,b" })]
        [InlineData("a,b", ',', 2, StringSplitOptions.None, new[] { "a", "b" })]
        [InlineData("a,b", ',', 3, StringSplitOptions.None, new[] { "a", "b" })]
        [InlineData("a,b", ',', 4, StringSplitOptions.None, new[] { "a", "b" })]
        [InlineData("a,b", ',', M, StringSplitOptions.None, new[] { "a", "b" })]
        [InlineData("a,b", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,b", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a,b" })]
        [InlineData("a,b", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,", ',', 1, StringSplitOptions.None, new[] { "a," })]
        [InlineData("a,", ',', 2, StringSplitOptions.None, new[] { "a", "" })]
        [InlineData("a,", ',', 3, StringSplitOptions.None, new[] { "a", "" })]
        [InlineData("a,", ',', 4, StringSplitOptions.None, new[] { "a", "" })]
        [InlineData("a,", ',', M, StringSplitOptions.None, new[] { "a", "" })]
        [InlineData("a,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a," })]
        [InlineData("a,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a" })]
        [InlineData("a,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a" })]
        [InlineData("a,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a" })]
        [InlineData("a,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a" })]
        [InlineData(",b", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",b", ',', 1, StringSplitOptions.None, new[] { ",b" })]
        [InlineData(",b", ',', 2, StringSplitOptions.None, new[] { "", "b" })]
        [InlineData(",b", ',', 3, StringSplitOptions.None, new[] { "", "b" })]
        [InlineData(",b", ',', 4, StringSplitOptions.None, new[] { "", "b" })]
        [InlineData(",b", ',', M, StringSplitOptions.None, new[] { "", "b" })]
        [InlineData(",b", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",b", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",b" })]
        [InlineData(",b", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "b" })]
        [InlineData(",b", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "b" })]
        [InlineData(",b", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "b" })]
        [InlineData(",b", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "b" })]
        [InlineData(",a,b", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",a,b", ',', 1, StringSplitOptions.None, new[] { ",a,b" })]
        [InlineData(",a,b", ',', 2, StringSplitOptions.None, new[] { "", "a,b" })]
        [InlineData(",a,b", ',', 3, StringSplitOptions.None, new[] { "", "a", "b" })]
        [InlineData(",a,b", ',', 4, StringSplitOptions.None, new[] { "", "a", "b" })]
        [InlineData(",a,b", ',', M, StringSplitOptions.None, new[] { "", "a", "b" })]
        [InlineData(",a,b", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",a,b", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",a,b" })]
        [InlineData(",a,b", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData(",a,b", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData(",a,b", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData(",a,b", ',', 5, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,b,", ',', 1, StringSplitOptions.None, new[] { "a,b," })]
        [InlineData("a,b,", ',', 2, StringSplitOptions.None, new[] { "a", "b,", })]
        [InlineData("a,b,", ',', 3, StringSplitOptions.None, new[] { "a", "b", "" })]
        [InlineData("a,b,", ',', 4, StringSplitOptions.None, new[] { "a", "b", "" })]
        [InlineData("a,b,", ',', M, StringSplitOptions.None, new[] { "a", "b", "" })]
        [InlineData("a,b,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,b,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a,b," })]
        [InlineData("a,b,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b," })]
        [InlineData("a,b,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" })]
        [InlineData("a,b,c", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,b,c", ',', 1, StringSplitOptions.None, new[] { "a,b,c" })]
        [InlineData("a,b,c", ',', 2, StringSplitOptions.None, new[] { "a", "b,c" })]
        [InlineData("a,b,c", ',', 3, StringSplitOptions.None, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", ',', 4, StringSplitOptions.None, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", ',', M, StringSplitOptions.None, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,b,c", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a,b,c" })]
        [InlineData("a,b,c", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b,c", })]
        [InlineData("a,b,c", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("a,,c", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,,c", ',', 1, StringSplitOptions.None, new[] { "a,,c" })]
        [InlineData("a,,c", ',', 2, StringSplitOptions.None, new[] { "a", ",c", })]
        [InlineData("a,,c", ',', 3, StringSplitOptions.None, new[] { "a", "", "c" })]
        [InlineData("a,,c", ',', 4, StringSplitOptions.None, new[] { "a", "", "c" })]
        [InlineData("a,,c", ',', M, StringSplitOptions.None, new[] { "a", "", "c" })]
        [InlineData("a,,c", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,,c", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a,,c" })]
        [InlineData("a,,c", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "c", })]
        [InlineData("a,,c", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "c" })]
        [InlineData("a,,c", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "c" })]
        [InlineData("a,,c", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "c" })]
        [InlineData(",a,b,c", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",a,b,c", ',', 1, StringSplitOptions.None, new[] { ",a,b,c" })]
        [InlineData(",a,b,c", ',', 2, StringSplitOptions.None, new[] { "", "a,b,c" })]
        [InlineData(",a,b,c", ',', 3, StringSplitOptions.None, new[] { "", "a", "b,c" })]
        [InlineData(",a,b,c", ',', 4, StringSplitOptions.None, new[] { "", "a", "b", "c" })]
        [InlineData(",a,b,c", ',', M, StringSplitOptions.None, new[] { "", "a", "b", "c" })]
        [InlineData(",a,b,c", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",a,b,c", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",a,b,c" })]
        [InlineData(",a,b,c", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b,c", })]
        [InlineData(",a,b,c", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData(",a,b,c", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData(",a,b,c", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("a,b,c,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("a,b,c,", ',', 1, StringSplitOptions.None, new[] { "a,b,c," })]
        [InlineData("a,b,c,", ',', 2, StringSplitOptions.None, new[] { "a", "b,c," })]
        [InlineData("a,b,c,", ',', 3, StringSplitOptions.None, new[] { "a", "b", "c,", })]
        [InlineData("a,b,c,", ',', 4, StringSplitOptions.None, new[] { "a", "b", "c", "" })]
        [InlineData("a,b,c,", ',', M, StringSplitOptions.None, new[] { "a", "b", "c", "" })]
        [InlineData("a,b,c,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("a,b,c,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "a,b,c," })]
        [InlineData("a,b,c,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b,c,", })]
        [InlineData("a,b,c,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c," })]
        [InlineData("a,b,c,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("a,b,c,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData(",a,b,c,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",a,b,c,", ',', 1, StringSplitOptions.None, new[] { ",a,b,c," })]
        [InlineData(",a,b,c,", ',', 2, StringSplitOptions.None, new[] { "", "a,b,c," })]
        [InlineData(",a,b,c,", ',', 3, StringSplitOptions.None, new[] { "", "a", "b,c," })]
        [InlineData(",a,b,c,", ',', 4, StringSplitOptions.None, new[] { "", "a", "b", "c," })]
        [InlineData(",a,b,c,", ',', M, StringSplitOptions.None, new[] { "", "a", "b", "c", "" })]
        [InlineData(",a,b,c,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",a,b,c,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",a,b,c," })]
        [InlineData(",a,b,c,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b,c," })]
        [InlineData(",a,b,c,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c," })]
        [InlineData(",a,b,c,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData(",a,b,c,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b", "c" })]
        [InlineData("first,second", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,second", ',', 1, StringSplitOptions.None, new[] { "first,second" })]
        [InlineData("first,second", ',', 2, StringSplitOptions.None, new[] { "first", "second" })]
        [InlineData("first,second", ',', 3, StringSplitOptions.None, new[] { "first", "second" })]
        [InlineData("first,second", ',', 4, StringSplitOptions.None, new[] { "first", "second" })]
        [InlineData("first,second", ',', M, StringSplitOptions.None, new[] { "first", "second" })]
        [InlineData("first,second", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,second", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first,second" })]
        [InlineData("first,second", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,", ',', 1, StringSplitOptions.None, new[] { "first," })]
        [InlineData("first,", ',', 2, StringSplitOptions.None, new[] { "first", "" })]
        [InlineData("first,", ',', 3, StringSplitOptions.None, new[] { "first", "" })]
        [InlineData("first,", ',', 4, StringSplitOptions.None, new[] { "first", "" })]
        [InlineData("first,", ',', M, StringSplitOptions.None, new[] { "first", "" })]
        [InlineData("first,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first," })]
        [InlineData("first,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first" })]
        [InlineData("first,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first" })]
        [InlineData("first,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first" })]
        [InlineData("first,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first" })]
        [InlineData(",second", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",second", ',', 1, StringSplitOptions.None, new[] { ",second" })]
        [InlineData(",second", ',', 2, StringSplitOptions.None, new[] { "", "second" })]
        [InlineData(",second", ',', 3, StringSplitOptions.None, new[] { "", "second" })]
        [InlineData(",second", ',', 4, StringSplitOptions.None, new[] { "", "second" })]
        [InlineData(",second", ',', M, StringSplitOptions.None, new[] { "", "second" })]
        [InlineData(",second", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",second", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",second" })]
        [InlineData(",second", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "second" })]
        [InlineData(",second", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "second" })]
        [InlineData(",second", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "second" })]
        [InlineData(",second", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "second" })]
        [InlineData(",first,second", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",first,second", ',', 1, StringSplitOptions.None, new[] { ",first,second" })]
        [InlineData(",first,second", ',', 2, StringSplitOptions.None, new[] { "", "first,second" })]
        [InlineData(",first,second", ',', 3, StringSplitOptions.None, new[] { "", "first", "second" })]
        [InlineData(",first,second", ',', 4, StringSplitOptions.None, new[] { "", "first", "second" })]
        [InlineData(",first,second", ',', M, StringSplitOptions.None, new[] { "", "first", "second" })]
        [InlineData(",first,second", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",first,second", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",first,second" })]
        [InlineData(",first,second", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData(",first,second", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData(",first,second", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData(",first,second", ',', 5, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,second,", ',', 1, StringSplitOptions.None, new[] { "first,second," })]
        [InlineData("first,second,", ',', 2, StringSplitOptions.None, new[] { "first", "second,", })]
        [InlineData("first,second,", ',', 3, StringSplitOptions.None, new[] { "first", "second", "" })]
        [InlineData("first,second,", ',', 4, StringSplitOptions.None, new[] { "first", "second", "" })]
        [InlineData("first,second,", ',', M, StringSplitOptions.None, new[] { "first", "second", "" })]
        [InlineData("first,second,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,second,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first,second," })]
        [InlineData("first,second,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second," })]
        [InlineData("first,second,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second" })]
        [InlineData("first,second,third", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,second,third", ',', 1, StringSplitOptions.None, new[] { "first,second,third" })]
        [InlineData("first,second,third", ',', 2, StringSplitOptions.None, new[] { "first", "second,third" })]
        [InlineData("first,second,third", ',', 3, StringSplitOptions.None, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ',', 4, StringSplitOptions.None, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ',', M, StringSplitOptions.None, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,second,third", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first,second,third" })]
        [InlineData("first,second,third", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second,third" })]
        [InlineData("first,second,third", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,,third", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,,third", ',', 1, StringSplitOptions.None, new[] { "first,,third" })]
        [InlineData("first,,third", ',', 2, StringSplitOptions.None, new[] { "first", ",third", })]
        [InlineData("first,,third", ',', 3, StringSplitOptions.None, new[] { "first", "", "third" })]
        [InlineData("first,,third", ',', 4, StringSplitOptions.None, new[] { "first", "", "third" })]
        [InlineData("first,,third", ',', M, StringSplitOptions.None, new[] { "first", "", "third" })]
        [InlineData("first,,third", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,,third", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first,,third" })]
        [InlineData("first,,third", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "third", })]
        [InlineData("first,,third", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "third" })]
        [InlineData("first,,third", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "third" })]
        [InlineData("first,,third", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "third" })]
        [InlineData(",first,second,third", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",first,second,third", ',', 1, StringSplitOptions.None, new[] { ",first,second,third" })]
        [InlineData(",first,second,third", ',', 2, StringSplitOptions.None, new[] { "", "first,second,third" })]
        [InlineData(",first,second,third", ',', 3, StringSplitOptions.None, new[] { "", "first", "second,third" })]
        [InlineData(",first,second,third", ',', 4, StringSplitOptions.None, new[] { "", "first", "second", "third" })]
        [InlineData(",first,second,third", ',', M, StringSplitOptions.None, new[] { "", "first", "second", "third" })]
        [InlineData(",first,second,third", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",first,second,third", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",first,second,third" })]
        [InlineData(",first,second,third", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second,third" })]
        [InlineData(",first,second,third", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData(",first,second,third", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData(",first,second,third", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,second,third,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData("first,second,third,", ',', 1, StringSplitOptions.None, new[] { "first,second,third," })]
        [InlineData("first,second,third,", ',', 2, StringSplitOptions.None, new[] { "first", "second,third," })]
        [InlineData("first,second,third,", ',', 3, StringSplitOptions.None, new[] { "first", "second", "third,", })]
        [InlineData("first,second,third,", ',', 4, StringSplitOptions.None, new[] { "first", "second", "third", "" })]
        [InlineData("first,second,third,", ',', M, StringSplitOptions.None, new[] { "first", "second", "third", "" })]
        [InlineData("first,second,third,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData("first,second,third,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { "first,second,third," })]
        [InlineData("first,second,third,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second,third," })]
        [InlineData("first,second,third,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third," })]
        [InlineData("first,second,third,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,second,third,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData(",first,second,third,", ',', 0, StringSplitOptions.None, new string[0])]
        [InlineData(",first,second,third,", ',', 1, StringSplitOptions.None, new[] { ",first,second,third," })]
        [InlineData(",first,second,third,", ',', 2, StringSplitOptions.None, new[] { "", "first,second,third," })]
        [InlineData(",first,second,third,", ',', 3, StringSplitOptions.None, new[] { "", "first", "second,third," })]
        [InlineData(",first,second,third,", ',', 4, StringSplitOptions.None, new[] { "", "first", "second", "third," })]
        [InlineData(",first,second,third,", ',', M, StringSplitOptions.None, new[] { "", "first", "second", "third", "" })]
        [InlineData(",first,second,third,", ',', 0, StringSplitOptions.RemoveEmptyEntries, new string[0])]
        [InlineData(",first,second,third,", ',', 1, StringSplitOptions.RemoveEmptyEntries, new[] { ",first,second,third," })]
        [InlineData(",first,second,third,", ',', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second,third," })]
        [InlineData(",first,second,third,", ',', 3, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third," })]
        [InlineData(",first,second,third,", ',', 4, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData(",first,second,third,", ',', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first", "second", "third" })]
        [InlineData("first,second,third", ' ', M, StringSplitOptions.None, new[] { "first,second,third" })]
        [InlineData("first,second,third", ' ', M, StringSplitOptions.RemoveEmptyEntries, new[] { "first,second,third" })]
        [InlineData("Foo Bar Baz", ' ', 2, StringSplitOptions.RemoveEmptyEntries, new[] { "Foo", "Bar Baz" })]
        [InlineData("Foo Bar Baz", ' ', M, StringSplitOptions.None, new[] { "Foo", "Bar", "Baz" })]
        public static void SplitCharSeparator(
            string valueString,
            char separator,
            int count,
            StringSplitOptions options,
            string[] expected)
        {
            ReadOnlySpan<char> value = valueString;
            Assert.Equal(expected, value.SplitByChar(separator, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByChar(new[] { separator }, count, options).ToStringList());
            Assert.Equal(expected, value.SplitByText(separator.ToString(), count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { separator.ToString() }, count, options));
            if (count == int.MaxValue)
            {
                Assert.Equal(expected, value.SplitByChar(separator, options).ToStringList());
                Assert.Equal(expected, value.SplitByChar(new[] { separator }, options).ToStringList());
                Assert.Equal(expected, value.SplitByText(separator.ToString(), options).ToStringList());
                //Assert.Equal(expected, value.Split(new[] { separator.ToString() }, options));
            }

            if (options == StringSplitOptions.None)
            {
                Assert.Equal(expected, value.SplitByChar(separator, count).ToStringList());
                Assert.Equal(expected, value.SplitByChar(new[] { separator }, count).ToStringList());
                Assert.Equal(expected, value.SplitByText(separator.ToString(), count).ToStringList());
            }

            if (count == int.MaxValue && options == StringSplitOptions.None)
            {
                Assert.Equal(expected, value.SplitByChar(separator).ToStringList());
                Assert.Equal(expected, value.SplitByChar(new[] { separator }).ToStringList());
                Assert.Equal(expected, value.SplitByText(separator.ToString()).ToStringList());
            }
        }

        [Theory]
        [InlineData("a,b,c", null, M, StringSplitOptions.None, new[] { "a,b,c" })]
        [InlineData("a,b,c", "", M, StringSplitOptions.None, new[] { "a,b,c" })]
        [InlineData("aaabaaabaaa", "aa", M, StringSplitOptions.None, new[] { "", "ab", "ab", "a" })]
        [InlineData("aaabaaabaaa", "aa", M, StringSplitOptions.RemoveEmptyEntries, new[] { "ab", "ab", "a" })]
        [InlineData("this, is, a, string, with some spaces", ", ", M, StringSplitOptions.None,
            new[] { "this", "is", "a", "string", "with some spaces" })]
        public static void SplitStringSeparator(
            string valueString,
            string separator,
            int count,
            StringSplitOptions options,
            string[] expected)
        {
            ReadOnlySpan<char> value = valueString;
            Assert.Equal(expected, value.SplitByText(separator, count, options).ToStringList());
            //Assert.Equal(expected, value.Split(new[] { separator }, count, options).ToStringList());
            if (count == int.MaxValue)
            {
                Assert.Equal(expected, value.SplitByText(separator, options).ToStringList());
                //Assert.Equal(expected, value.Split(new[] { separator }, options).ToStringList());
            }

            if (options == StringSplitOptions.None)
            {
                Assert.Equal(expected, value.SplitByText(separator, count).ToStringList());
            }

            if (count == int.MaxValue && options == StringSplitOptions.None)
            {
                Assert.Equal(expected, value.SplitByText(separator).ToStringList());
            }
        }

        [Fact]
        public static void SplitNullCharArraySeparator_BindsToCharArrayOverload()
        {
            ReadOnlySpan<char> value = "a b c";
            string[] expected = new[] { "a", "b", "c" };
            // Ensure Split(null) compiles successfully as a call to Split(char[])
            Assert.Equal(expected, value.SplitByChar(null).ToStringList());
        }

        [Theory]
        [InlineData("a b c", null, M, StringSplitOptions.None, new[] { "a", "b", "c" })]
        [InlineData("a b c", new char[0], M, StringSplitOptions.None, new[] { "a", "b", "c" })]
        [InlineData("a,b,c", null, M, StringSplitOptions.None, new[] { "a,b,c" })]
        [InlineData("a,b,c", new char[0], M, StringSplitOptions.None, new[] { "a,b,c" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ' ' }, M, StringSplitOptions.None, new[] { "this,", "is,", "a,", "string,", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ' ', ',' }, M, StringSplitOptions.None, new[] { "this", "", "is", "", "a", "", "string", "", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ' }, M, StringSplitOptions.None, new[] { "this", "", "is", "", "a", "", "string", "", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ', 's' }, M, StringSplitOptions.None, new[] { "thi", "", "", "i", "", "", "a", "", "", "tring", "", "with", "", "ome", "", "pace", "" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ', 's', 'a' }, M, StringSplitOptions.None, new[] { "thi", "", "", "i", "", "", "", "", "", "", "tring", "", "with", "", "ome", "", "p", "ce", "" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ' ' }, M, StringSplitOptions.RemoveEmptyEntries, new[] { "this,", "is,", "a,", "string,", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ' ', ',' }, M, StringSplitOptions.RemoveEmptyEntries, new[] { "this", "is", "a", "string", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ' }, M, StringSplitOptions.RemoveEmptyEntries, new[] { "this", "is", "a", "string", "with", "some", "spaces" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ', 's' }, M, StringSplitOptions.RemoveEmptyEntries, new[] { "thi", "i", "a", "tring", "with", "ome", "pace" })]
        [InlineData("this, is, a, string, with some spaces", new[] { ',', ' ', 's', 'a' }, M, StringSplitOptions.RemoveEmptyEntries, new[] { "thi", "i", "tring", "with", "ome", "p", "ce" })]
        public static void SplitCharArraySeparator(string valueString, char[] separators, int count, StringSplitOptions options, string[] expected)
        {
            ReadOnlySpan<char> value = valueString;
            Assert.Equal(expected, value.SplitByChar(separators, count, options).ToStringList());
            //Assert.Equal(expected, value.Split(ToStringArray(separators), count, options));
        }
    }
}