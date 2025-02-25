namespace NET9_LinqSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var persons = GetPersons();
            #region CountBy
            // before .NET9
            var oldCountByCity = persons.GroupBy(p => p.City)
                                        .Select(g => new { City = g.Key, Count = g.Count() });
            var oldCountResult = string.Join(", ", oldCountByCity.Select(c => $"{c.City}: {c.Count} persons"));
          
            // after .NET9
            var newCountByCity = persons.CountBy(p => p.City);
            var newCountResult = string.Join(", ", newCountByCity.Select(c => $"{c.Key}: {c.Value} persons"));

            Console.WriteLine($"before .NET9-- {oldCountResult}{Environment.NewLine}after  .NET9-- {newCountResult}");

            #endregion CountBy

            #region AgrerateBy
            // before .NET9
            var oldAggregateByCity = persons.GroupBy(p => p.City)
                                     .Select(g => new { City = g.Key, Age = g.Sum(p => p.Age) });
            var oldAggregateResult1 = string.Join(", ", oldAggregateByCity.Select(c => $"{c.City}: {c.Age} years old in total"));
            oldAggregateByCity = persons.GroupBy(p => p.City)
                                            .Select(g => new { City = g.Key, Age = g.Aggregate(0, (acc, p) => acc + p.Age) });
            var oldAggregateResult2 = string.Join(", ", oldAggregateByCity.Select(c => $"{c.City}: {c.Age} years old in total"));

            // after .NET9
            var newAggregateByCity = persons.AggregateBy(p => p.City, 0, (acc, p) => acc + p.Age);
            var newAggregateResult = string.Join(", ", newAggregateByCity.Select(c => $"{c.Key}: {c.Value} years old in total"));
            Console.WriteLine($"before .NET9-- {oldAggregateResult2}{Environment.NewLine}after  .NET9-- {newAggregateResult}");
            #endregion AgrerateBy

            #region Index
            // before .NET9
            IEnumerable<(int Index, Person Item)> oldIndex = persons.Select((p, index) => (Index: index, Item: p));
            foreach (var item in oldIndex)
            {
                Console.WriteLine($"{item.Item.Name} is at index {item.Index}");
            }
            Console.WriteLine("**************");

            // after .NET9
            IEnumerable<(int Index, Person Item)> newIndex = persons.Index();
            foreach (var item in newIndex)
            {
                Console.WriteLine($"{item.Item.Name} is at index {item.Index}");
            }
            #endregion Index
        }

        static IEnumerable<Person> GetPersons()
        {
            yield return new Person { Name = "Bill", City = "Seattle", Age = 18 };
            yield return new Person { Name = "Mark", City = "Taipei", Age = 21 };
            yield return new Person { Name = "Steve", City = "New York", Age = 32 };
            yield return new Person { Name = "James", City = "Taipei", Age = 16 };
            yield return new Person { Name = "John", City = "Seattle", Age = 52 };
            yield return new Person { Name = "Tom", City = "Taipei", Age = 37 };
            yield return new Person { Name = "David", City = "New York", Age = 26 };
            yield return new Person { Name = "Peter", City = "Seattle", Age = 23 };
            yield return new Person { Name = "Paul", City = "Taipei", Age = 45 };
            yield return new Person { Name = "Mary", City = "New York", Age = 38 };
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string City { get; set; }

        public int Age { get; set; }
    }
}
