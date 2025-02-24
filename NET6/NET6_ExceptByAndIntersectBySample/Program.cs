namespace NET6_ExceptByAndIntersectBySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ids = { 1, 4, 6 };
            Student[] students = CreateStudents();
            var result = students.ExceptBy(ids, s => s.Id);
            Display(result);
            Console.WriteLine("--------------");
            result = students.IntersectBy(ids, s => s.Id);
            Display(result);

        }

        static Student[] CreateStudents()
        {
            return new Student[]
            {
                new Student { Id = 1, Name = "Bill" },
                new Student { Id = 2, Name = "Steve" },
                new Student { Id = 3, Name = "Ram" },
                new Student { Id = 4, Name = "Alex" },
                new Student { Id = 5, Name = "Mary" },
                new Student { Id = 6, Name = "David" },
                new Student { Id = 7, Name = "Steve" }
            };            
        }
        static void Display(IEnumerable<Student> source)
        {
            foreach (var student in source)
            {
                Console.WriteLine($"{student.Id} - {student.Name}");
            }
        }
    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
