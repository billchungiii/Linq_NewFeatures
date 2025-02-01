using System.Runtime.InteropServices;

namespace NET6_ChunkSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
                {
                    new Person { Name = "John", Age = 21 },
                    new Person { Name = "Alex", Age = 34 },
                    new Person { Name = "Mary", Age = 29 },
                    new Person { Name = "Sophia", Age = 24 },
                    new Person { Name = "Michael", Age = 40 },
                    new Person { Name = "Emma", Age = 26 },
                    new Person { Name = "Daniel", Age = 33 },
                    new Person { Name = "Olivia", Age = 28 },
                    new Person { Name = "James", Age = 45 },
                    new Person { Name = "Isabella", Age = 30 },
                    new Person { Name = "Benjamin", Age = 31 },
                    new Person { Name = "Mia", Age = 32 },
                    new Person { Name = "Lucas", Age = 27 }
                };
            IEnumerable<Person[]> chunkedPeople = people.Chunk(3);
            int index = 1;
            foreach (var chunk in chunkedPeople)
            {
                Console.WriteLine($"Chunk {index}: {string.Join(", ", chunk.Select(p => p.Name))}");
                index++;
            }


            IEnumerable<Person[]> chunkedPeople2 = CustomChunk(people, 3);
            index = 1;
            foreach (var chunk in chunkedPeople2)
            {
                Console.WriteLine($"CustomChunk {index}: {string.Join(", ", chunk.Select(p => p.Name))}");
                index++;
            }

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
