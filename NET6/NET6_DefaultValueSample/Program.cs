using System.ComponentModel.DataAnnotations;

namespace NET6_DefaultValueSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region default value 
            var source = Enumerable.Range(1, 10);
            var first = source.FirstOrDefault(x => x > 10, int.MinValue);
            var last = source.LastOrDefault(x => x > 10, int.MinValue);
            var single = source.SingleOrDefault(x => x == 11, int.MinValue);
            Console.WriteLine($"First: {first}, Last: {last}, Single: {single}");
            #endregion

            #region Zip
            var s1 = new string[] { "A", "B", "C", "D" };
            var s2 = new string[] { "甲", "乙", "丙", "丁" };
            var numbers = new int[] { 1, 2, 3, 4 };
            var zip1 = s1.Zip(numbers, (a, b) => $"{a}-{b}");
            var zip2 = s1.Zip(numbers);
            var zip3 = s1.Zip(numbers, s2);
            Display(zip1);
            Display(zip2);
            Display(zip3);
            #endregion

            #region Index struct in ElementAt
            // before .NET 6
            var element1b = source.ElementAt(source.Count() - 1);
            var element2b = source.ElementAtOrDefault(source.Count() - 2);

            // after .NET 6
            var element1a = source.ElementAt(^1);            
            var element2a = source.ElementAtOrDefault(^2);

            Console.WriteLine($"ElementAt(^1): {element1a}, ElementAt(source.Count() - 1): {element1b}");
            Console.WriteLine($"ElementAtOrDefault(^2): {element2a}, ElementAtOrDefault(source.Count() - 2): {element2b}");
            #endregion

            #region Range struct in Take
            // before .NET 6
             var take1 = source.Skip(2).Take(3);
            // after .NET 6
            var take2 = source.Take(2..5);

            Display(take1);
            Display(take2);
            #endregion

        }

        static void Display<T>(IEnumerable<T> source)
        {
            Console.WriteLine(string.Join(", ", source));
        }
    }
}
