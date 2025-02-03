using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NET6_NonEnumeratedCountBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
          var summary = BenchmarkRunner.Run<NonEnumeratedCountBenchmark>();
          
        }
    }

    public class NonEnumeratedCountBenchmark
    {

        private IEnumerable<Person> _people;
        private IEnumerable<int> _people_selectAge;
        private IEnumerable<Person> _people_orderby;
        private IEnumerable<Person> _people_where_orderby;
        private IEnumerable<int> _people_selectAge_orderby;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            List<Person> list = new List<Person>(1000);
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new Person
                {
                    Name = $"Name_{i}",
                    Age = random.Next(10, 81)
                });
            }
            _people = list;
            _people_selectAge = _people.Select(p => p.Age);
            _people_orderby = _people.OrderBy(p => p.Age);
            _people_where_orderby = _people.Where(p => p.Age > 9).OrderBy(p => p.Age);
            _people_selectAge_orderby = _people_selectAge.OrderBy(age => age);
        }

        [Benchmark]
        public void CallCustomCount_People()
        {
            var count = CustomCount(_people);
        }

        [Benchmark]
        public void CallCount_People()
        {
            var count = _people.Count();
        }


        [Benchmark]
        public void CallCustomCount_SelectAge()
        {
            var count = CustomCount(_people_selectAge);
        }

        [Benchmark]
        public void CallCount_SelectAge()
        {
            var count = _people_selectAge.Count();
        }

        [Benchmark]
        public void CallCustomCount_OrderBy()
        {
            var count = CustomCount(_people_orderby);
        }

        [Benchmark]
        public void CallCount_OrderBy()
        {
            var count = _people_orderby.Count();
        }

        [Benchmark]
        public void CallCustomCount_WhereOrderBy()
        {
            var count = CustomCount(_people_where_orderby);
        }

        [Benchmark]
        public void CallCount_WhereOrderBy()
        {
            var count = _people_where_orderby.Count();
        }

        [Benchmark]
        public void CallCustomCount_SelectAgeOrderBy()
        {
            var count = CustomCount(_people_selectAge_orderby);
        }
        [Benchmark]
        public void CallCount_SelectAgeOrderBy()
        {
            var count = _people_selectAge_orderby.Count();
        }

        static int CustomCount<T>(IEnumerable<T> source)
        {
            if (source.TryGetNonEnumeratedCount(out int count))
            {
                return count;
            }
            else
            {
                return source.Count();
            }
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
