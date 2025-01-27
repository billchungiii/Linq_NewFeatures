using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_NET6
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

            #endregion
        }
    }



    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
