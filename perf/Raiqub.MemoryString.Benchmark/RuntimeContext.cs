using System.IO;
using System.Linq;
using System.Reflection;

namespace Raiqub.MemoryString.Benchmark
{
    internal static class RuntimeContext
    {
        static RuntimeContext()
        {
            var assemblyFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
            for (var currDir = assemblyFolder; currDir != null; currDir = currDir.Parent)
            {
                if (!currDir.EnumerateFiles("*.sln", SearchOption.TopDirectoryOnly).Any())
                    continue;

                SolutionDirectory = currDir.FullName;
                break;
            }
        }

        public static string SolutionDirectory { get; } = string.Empty;
    }
}