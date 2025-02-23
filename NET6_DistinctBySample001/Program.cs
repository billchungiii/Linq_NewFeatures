namespace NET6_DistinctBySample001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] colors = { "red", "green", "blue", "green", "yellow", "blue" };

            // Get distinct elements from colors array 
            var distinctColors = colors.Distinct();
            Display(distinctColors);

            // Get distinct elements from colors array (.NET 6)
            distinctColors = colors.DistinctBy(c => c);
            Display(distinctColors);

            // Get distinct elements from colors array by length (before .NET6)
            var distinctColorsByLength = colors.Distinct(new StringLengthEqualityComparer());

            // Get distinct elements from colors array by length (.NET 6)
            distinctColorsByLength = colors.DistinctBy(c => c.Length);

        }

        static void Display<T>(IEnumerable<T> source)
        {
            Console.WriteLine($"{string.Join(",", source)}");
        }
    }

    public class StringLengthEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if (x== null && y == null) { return true; }
            if (x == null || y == null) { return false; }
            return x.Length == y.Length;
        }
        public int GetHashCode(string obj)
        {
            if (obj == null) { return -1; }
            return obj.Length;
        }
    }
}
