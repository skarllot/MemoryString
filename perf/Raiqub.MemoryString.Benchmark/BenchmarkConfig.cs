using System.Collections.Generic;
using System.Globalization;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

namespace Raiqub.MemoryString.Benchmark
{
    internal class BenchmarkConfig
        : IConfig
    {
        private readonly IConfig defaultConfig = DefaultConfig.Instance;

        public IEnumerable<IColumnProvider> GetColumnProviders()
        {
            return defaultConfig.GetColumnProviders();
        }

        public IEnumerable<IExporter> GetExporters()
        {
            yield return AsciiDocExporter.Default;
        }

        public IEnumerable<ILogger> GetLoggers()
        {
            yield return ConsoleLogger.Default;
        }

        public IEnumerable<IDiagnoser> GetDiagnosers()
        {
            return defaultConfig.GetDiagnosers();
        }

        public IEnumerable<IAnalyser> GetAnalysers()
        {
            return defaultConfig.GetAnalysers();
        }

        public IEnumerable<Job> GetJobs()
        {
            return defaultConfig.GetJobs();
        }

        public IEnumerable<IValidator> GetValidators()
        {
            return defaultConfig.GetValidators();
        }

        public IEnumerable<HardwareCounter> GetHardwareCounters()
        {
            return defaultConfig.GetHardwareCounters();
        }

        public IEnumerable<IFilter> GetFilters()
        {
            return defaultConfig.GetFilters();
        }

        public IEnumerable<BenchmarkLogicalGroupRule> GetLogicalGroupRules()
        {
            return defaultConfig.GetLogicalGroupRules();
        }

        public IOrderer? Orderer => defaultConfig.Orderer;

        public SummaryStyle SummaryStyle => defaultConfig.SummaryStyle;

        public ConfigUnionRule UnionRule => defaultConfig.UnionRule;

        public string ArtifactsPath => defaultConfig.ArtifactsPath;

        public CultureInfo? CultureInfo => defaultConfig.CultureInfo;

        public ConfigOptions Options => defaultConfig.Options;
    }
}