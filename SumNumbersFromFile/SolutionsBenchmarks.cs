using BenchmarkDotNet.Attributes;

namespace SumNumbersFromFile
{
    [MemoryDiagnoser]
    public class SolutionsBenchmarks
    {
        private readonly string _filename = "datas.txt";

        [Benchmark(Baseline = true)]
        public void FirstSolution()
        {
            string[] dataFromFile = Solutions.ReadDataFromFileIntoArray(_filename);
            int[] processedData = Solutions.GetIntegerDataParts(dataFromFile);
        }

        [Benchmark]
        public void SimpleSolution()
        {
            int[] result = Solutions.SimpleSolution(_filename);
        }

        [Benchmark]
        public void SimpleSolution2()
        {
            int[] result = Solutions.SimpleSolution2(_filename);
        }

        [Benchmark]
        public void SimpleSolutionEnumerable()
        {
            var result = Solutions.SimpleSolutionEnumerable(_filename);
        }

        [Benchmark]
        public void SimpleSolutionEnumerableToList()
        {
            var result = Solutions.SimpleSolutionEnumerable(_filename).ToList();
        }

        [Benchmark]
        public void EnumerableSolution1()
        {
            var result = Solutions.EnumerableSolution1(_filename);
        }

        [Benchmark]
        public void EnumerableSolution1ToList()
        {
            var result = Solutions.EnumerableSolution1(_filename).ToList();
        }

        [Benchmark]
        public void EnumerableSolution2()
        {
            var result = Solutions.EnumerableSolution2(_filename);
        }

        [Benchmark]
        public void EnumerableSolution2ToList()
        {
            var result = Solutions.EnumerableSolution2(_filename).ToList();
        }

        [Benchmark]
        public void LINQSolution1()
        {
            var result = Solutions.LINQSolution1(_filename);
        }

        [Benchmark]
        public void LINQSolution2()
        {
            var result = Solutions.LINQSolution2(_filename);
        }

        [Benchmark]
        public void LINQSolution3()
        {
            var result = Solutions.LINQSolution3(_filename);
        }

        [Benchmark]
        public void LINQSolution4()
        {
            var result = Solutions.LINQSolution4(_filename);
        }

        [Benchmark]
        public void LINQSolution4WithExceptionHandling()
        {
            var result = Solutions.LINQSolution4WithExceptionHandling(_filename);
        }
    }
}