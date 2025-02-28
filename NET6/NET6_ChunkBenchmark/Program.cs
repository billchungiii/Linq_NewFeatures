﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

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
    public class ChunkBenchmark
    {
        private List<Person> _people;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            _people = Enumerable.Range(1, 500000).Select(i => new Person
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
            var result = _people.Chunk(size).ToList();
        }

        [Benchmark]
        [Arguments(3)]
        [Arguments(7)]
        [Arguments(43)]
        public void CallCustomChunk(int size)
        {
            var result = CustomChunk(_people, size).ToList();
        }


        [Benchmark]
        [Arguments(3)]
        [Arguments(7)]
        [Arguments(43)]
        public void CallCustomChunkV2(int size)
        {
            var result = CustomChunkV2(_people, size).ToList();
        }



        [MethodImpl(MethodImplOptions.NoInlining)]

        static IEnumerable<T[]> CustomChunk<T>(IEnumerable<T> source, int size)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var queue = new Queue<T>(source);
            while (queue.Count > 0)
            {
                var chunk = new T[Math.Min(size, queue.Count)];
                for (int i = 0; i < chunk.Length; i++)
                {
                    chunk[i] = queue.Dequeue();
                }
                yield return chunk;
            }
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<T[]> CustomChunkV2<T>(IEnumerable<T> source, int size)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var chunk = new T[size];
                for (int i = 0; i < size; i++)
                {
                    chunk[i] = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        Array.Resize(ref chunk, i + 1);
                        yield return chunk;
                        yield break;
                    }
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
