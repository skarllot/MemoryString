using System;
using System.Collections.Generic;
using MemoryString.Split;
using Xunit;

namespace MemoryString.Tests.StringSpan
{
    public class SplitTests
    {
        [Fact]
        public static void SplitZeroCountEmptyResult()
        {
            ReadOnlySpan<char> value = "a,b";
            const int count = 0;
            const StringSplitOptions options = StringSplitOptions.None;

            string[] expected = new string[0];

            Assert.Equal(expected, value.Split(',', count));
            Assert.Equal(expected, value.Split(',', count, options));
            Assert.Equal(expected, value.Split(new[] { ',' }, count));
            Assert.Equal(expected, value.Split(new[] { ',' }, count, options));
            Assert.Equal(expected, value.Split(",", count).ToList());
            Assert.Equal(expected, value.Split(",", count, options).ToList());
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }

        [Fact]
        public static void SplitEmptyValueWithRemoveEmptyEntriesOptionEmptyResult()
        {
            ReadOnlySpan<char> value = string.Empty;
            const int count = int.MaxValue;
            const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;

            string[] expected = new string[0];

            Assert.Equal(expected, value.Split(',', options));
            Assert.Equal(expected, value.Split(',', count, options));
            Assert.Equal(expected, value.Split(new[] { ',' }, options));
            Assert.Equal(expected, value.Split(new[] { ',' }, count, options));
            Assert.Equal(expected, value.Split(",", options).ToList());
            Assert.Equal(expected, value.Split(",", count, options).ToList());
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

            Assert.Equal(expected, value.Split(',', count));
            Assert.Equal(expected, value.Split(',', count, options));
            Assert.Equal(expected, value.Split(new[] { ',' }, count));
            Assert.Equal(expected, value.Split(new[] { ',' }, count, options));
            Assert.Equal(expected, value.Split(",", count).ToList());
            Assert.Equal(expected, value.Split(",", count, options).ToList());
            //Assert.Equal(expected, value.Split(new[] { "," }, count, options));
        }
    }
}