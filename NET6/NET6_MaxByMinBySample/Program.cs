namespace NET6_MaxByMinBySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // fake data
            var people = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 25 },
                new Person { Name = "Charlie", Age = 35 }
            };

            #region MaxBy
            // before .NET 6
            // var oldestPerson1 = people.OrderByDescending(p => p.Age).First();
            var oldestPerson1 = people.CallOrderByDescThenFirst(p => p.Age);
            Console.WriteLine($"[before NET6 (1)] Oldest person: {oldestPerson1.Name}, Age: {oldestPerson1.Age}");

            // before .NET 6
            //int maxAge = people.Max(p => p.Age);
            //var oldestPerson2 = people.First(p => p.Age == maxAge);
            var oldestPerson2 = people.CallMaxThenFirst(p => p.Age);
            Console.WriteLine($"[before NET6 (2)] Oldest person: {oldestPerson2.Name}, Age: {oldestPerson2.Age}");

            // after .NET 6
            //  var oldestPerson3 = people.MaxBy(p => p.Age);
            var oldestPerson3 = people.CallMaxBy(p => p.Age);
            Console.WriteLine($"[after NET6] Oldest person: {oldestPerson3.Name}, Age: {oldestPerson3.Age}");
            #endregion

            #region MinBy
            // before .NET 6
            // var youngestPerson1 = people.OrderBy(p => p.Age).First();
            var youngestPerson1 = people.CallOrderByThenFirst(p => p.Age);
            Console.WriteLine($"[before NET6 (1)] Youngest person: {youngestPerson1.Name}, Age: {youngestPerson1.Age}");

            // before .NET 6
            //int minAge = people.Min(p => p.Age);
            //var youngestPerson2 = people.First(p => p.Age == minAge);
            var youngestPerson2 = people.CallMinThenFirst(p => p.Age);

            Console.WriteLine($"[before NET6 (2)] Youngest person: {youngestPerson2.Name}, Age: {youngestPerson2.Age}");

            // after .NET 6
            //var youngestPerson3 = people.MinBy(p => p.Age);
            var youngestPerson3 = people.CallMinBy(p => p.Age);
            Console.WriteLine($"[after NET6] Youngest person: {youngestPerson3.Name}, Age: {youngestPerson3.Age}");
            #endregion
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
