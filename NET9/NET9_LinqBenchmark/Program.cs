using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NET9_LinqBenchmark
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<NET9LinqBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class NET9LinqBenchmark
    {
        private List<Person> _people;
        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            _people = Enumerable.Range(1, 2000).Select(i => new Person
            {
                Name = $"Name_{i}",
                Age = random.Next(10, 81),
                City = GetCity(random)
            }).ToList();
        }

        private string GetCity(Random random)
        {
           switch(random.Next(1, 6))
            {
                case 1:
                    return "Seattle";
                case 2:
                    return "Taipei";
                case 3:
                    return "New York";
                case 4:
                    return "Tokyo";                
                default:
                    return "Paris";
            }
        }

        [Benchmark]
        public void CountBy()
        {
            IEnumerable<KeyValuePair<string, int>> result = _people.CountBy(p => p.City);
        }
        [Benchmark]
        public void GroupByAndCount()
        {
            IEnumerable<KeyValuePair<string, int>> result = _people.GroupBy(p => p.City)
                                .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()));
        }

        [Benchmark]
        public void AggregateBy()
        {
            IEnumerable<KeyValuePair<string, int>> result = _people.AggregateBy(p => p.City, 0, (acc, p) => acc + p.Age);
        }

        [Benchmark]
        public void GroupByAndSum()
        {
            IEnumerable<KeyValuePair<string, int>> result = _people.GroupBy(p => p.City)
                                .Select(g => new KeyValuePair<string, int>(g.Key, g.Sum(p => p.Age)));
        }
        [Benchmark]
        public void GroupByAndAggregate()
        {
            IEnumerable<KeyValuePair<string, int>> result = _people.GroupBy(p => p.City)
                                .Select(g => new KeyValuePair<string, int>(g.Key, g.Aggregate(0, (acc, p) => acc + p.Age)));
        }
    }
    

    public class Person
    {
        public string Name { get; set; }

        public string City { get; set; }
        public int Age { get; set; }
    }
}
