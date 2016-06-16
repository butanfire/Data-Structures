using System;

namespace Problem_1.Students_and_Courses
{
    public class Person : IComparable<Person>
    {
        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int CompareTo(Person other)
        {
            return this.LastName.CompareTo(other.LastName);
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
