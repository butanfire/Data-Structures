namespace Problem_1.Students_and_Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StudentsCourses
    {
        public static void Main(string[] args)
        {
            SortedDictionary<string, SortedSet<Person>> studentsAndCourses = new SortedDictionary<string, SortedSet<Person>>();

            var input = Console.ReadLine().Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            while(input.Count != 0)
            {
                var person = new Person(input[0], input[1]);
                string course = input[2];
                if (!studentsAndCourses.ContainsKey(input[2]))
                {
                    studentsAndCourses.Add(course, new SortedSet<Person>());
                }

                studentsAndCourses[course].Add(person);

                input = Console.ReadLine().Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            foreach(var course in studentsAndCourses)
            {
                Console.WriteLine("{0}: {1}",course.Key,string.Join(", ",course.Value));
            }
        }
    }
}
