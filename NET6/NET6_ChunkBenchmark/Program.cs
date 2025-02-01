using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NET6_ChunkBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ChunkBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class  ChunkBenchmark
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
        [Arguments(3)]
        [Arguments(7)]
        [Arguments(43)]
        public void CallChunk(int size)
        {
           var result = _people.Chunk(size);
        }

        [Benchmark]
        [Arguments(3)]
        [Arguments(7)]
        [Arguments(43)]
        public void CallCustomChunk(int size)
        {
            var result = CustomChunk(_people, size);
        }

        static IEnumerable<Person[]> CustomChunk(IEnumerable<Person> source, int size)
        {            
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var queue = new Queue<Person>(source);
            while (queue.Count > 0)
            {
                var chunk = new Person[Math.Min(size, queue.Count)];
                for (int i = 0; i < chunk.Length; i++)
                {
                    chunk[i] = queue.Dequeue();
                }
                yield return chunk;
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
