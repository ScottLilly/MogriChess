using BenchmarkDotNet.Running;

namespace Benchmark.MogriChess
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<MemoryBenchmarker>();
        }
    }
}