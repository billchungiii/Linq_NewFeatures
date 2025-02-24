namespace NET6_UnionBySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] colors = { "red", "green", "blue", "green", "yellow", "pink" };
            string[] insects = { "ant", "bee", "beetle", "fly", "butterfly", "grasshopper" };
            var result = colors.Union(insects, new StringLengthEqualityComparer());
            Display(result);
            result = colors.UnionBy(insects, c => c.Length);
            Display(result);
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
            if (x == y) { return true; }
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
