using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NET6_NonEnumeratedCountSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            IEnumerable<int> range = Enumerable.Range(1, 1000);
            IEnumerable<Person> people = range.Select(i => new Person
            {
                Name = $"Name_{i}",
                Age = random.Next(10, 81)
            });
            Display(range);
            if (range is ICollection<int>)
            {
                Console.WriteLine("range is ICollection<int>");
            }
            Display(people);
           
            Display(people.ToList());
            Display(people.ToArray());
            Display(people.Where(p => p.Age > 20));
            Display(people.Select(p => p.Age));
            Display(people.Where(p => p.Age > 20).Select(p => p.Age));
            Display(people.GroupBy(x => x.Age / 10));
            Display(people.DistinctBy(x => x.Age));
            Display(people.Distinct(new PersonAgeComparer()));
            Display(people.OrderBy(x => x.Age));
            Display(people.Where(x => x.Age > 20).OrderBy(x => x.Age));

            Display(GetStrings());
            Display(GetStrings().Select(x => x));
            static IEnumerable<string> GetStrings()
            {
                yield return "One";
                yield return "Two";
                yield return "Three";
            }

            var repeat = Enumerable.Repeat(new Person { Name = "Name", Age = 30 }, 10);
            Display(repeat);
            if (repeat is ICollection<Person>)
            {
                Console.WriteLine("repeat is ICollection<Person>");
            }

            int[] array1 = { 1, 2, 3, 4, 5 };
            int[] array2 = { 1, 3, 4, 7, 9 };
            Display(array1.Intersect(array2));
            Display(array1.Union(array2));
            Display(array1.Except(array2));

            var teachers = Program.teachers;
            var students = Program.students;
            Display(teachers.Join(students, t => t.ClassName, s => s.ClassName, (t, s) => new { t.TeacherName, s.StudentName }));
        }


        static void Display<T>(IEnumerable<T> source, [CallerArgumentExpression(nameof(source))] string expression = null)
        {
            bool success = source.TryGetNonEnumeratedCount(out int count);
            if (success)
            {
                Console.WriteLine($"Source: {expression}, Try Result: {success}, Count: {count}");
            }
        }


        #region create data for Join
        static List<Teacher> teachers = new List<Teacher>
         {
             new Teacher { ClassName = "Math", TeacherName = "Teacher Esther" },
             new Teacher { ClassName = "English", TeacherName = "Teacher Alex" },
             new Teacher { ClassName = "History", TeacherName = "Teacher David" },
         };

        static List<Student> students = new List<Student>
         {
             new Student { ClassName = "Math", StudentName = "Student John" },
             new Student { ClassName = "English", StudentName = "Student Mary" },
             new Student { ClassName = "History", StudentName = "Student Tom" },
             new Student { ClassName = "Math", StudentName = "Student Jerry" },
             new Student { ClassName = "English", StudentName = "Student Alice" },
             new Student { ClassName = "History", StudentName = "Student Bob" },
             new Student { ClassName = "Math", StudentName = "Student Carol" },
             new Student { ClassName = "English", StudentName = "Student Bill" },
             new Student { ClassName = "History", StudentName = "Student Bob" },
             new Student { ClassName = "Math", StudentName = "Student Ada" },
         };
        #endregion



        static int GetCount<T>(IEnumerable<T> source)
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


    /// <summary>
    /// for Distinct() method
    /// </summary>
    public class PersonAgeComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            return x.Age.Equals(y.Age);
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.Age.GetHashCode();
        }
    }
    #region data classes
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Teacher
    {
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
    }

    public class Student
    {
        public string ClassName { get; set; }
        public string StudentName { get; set; }
    }
    #endregion
}
