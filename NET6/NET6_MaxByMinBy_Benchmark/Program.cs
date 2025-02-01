using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NET6_MaxByMinBySample;

namespace NET6_MaxByMinBy_Benchmark
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    [MemoryDiagnoser]
    public class MaxByMinByBenchmark
    {
        private List<Person> _people;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            _people = Enumerable.Range(1, 1000).Select(i => new Person
            {
                Name = $"Name_{i}",
                Age = random.Next(10, 81)
            }).ToList();
        }

        [Benchmark]
        public Person CallOrderByDescThenFirst()
        {
            return _people.CallOrderByDescThenFirst(p => p.Age);
        }

        [Benchmark]
        public Person CallMaxThenFirst()
        {
            return _people.CallMaxThenFirst(p => p.Age);
        }

        [Benchmark]
        public Person CallMaxBy()
        {
            return _people.CallMaxBy(p => p.Age);
        }

        [Benchmark]
        public Person CallOrderByThenFirst()
        {
            return _people.CallOrderByThenFirst(p => p.Age);
        }

        [Benchmark]
        public Person CallMinThenFirst()
        {
            return _people.CallMinThenFirst(p => p.Age);
        }

        [Benchmark]
        public Person CallMinBy()
        {
            return _people.CallMinBy(p => p.Age);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MaxByMinByBenchmark>();
        }
    }
}
