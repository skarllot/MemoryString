using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace MemoryString.Benchmark.StringSpan
{
    [AsciiDocExporter]
    [SimpleJob(RuntimeMoniker.Net50)]
    [MemoryDiagnoser]
    public class SplitLines
    {
        private string Data = Strings.LoremIpsum100Lines;

        [Benchmark]
        public void StringReader()
        {
            var reader = new StringReader(Data);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
            }
        }

        [Benchmark(Baseline = true)]
        public void Split()
        {
            foreach (var line in Data.Split(Environment.NewLine))
            {
            }
        }

        [Benchmark]
        public void Span()
        {
            foreach (var line in Data.AsSpan().SplitByText(Environment.NewLine))
            {
            }
        }

        [Benchmark]
        public void SpanToList()
        {
            foreach (var line in Data.AsSpan().SplitByText(Environment.NewLine).ToStringList())
            {
            }
        }
    }
}