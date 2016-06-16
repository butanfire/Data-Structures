namespace Collection_of_Persons
{
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCollectionSlow : IPersonCollection
    {
        List<Person> personList = new List<Person>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            if(this.FindPerson(email) != null)
            { return false; }
            personList.Add(new Person(email, name, age, town));
            return true;
        }

        public int Count => this.personList.Count;

        public Person FindPerson(string email)
        {
            return personList.FirstOrDefault(s => s.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);
            if (person == null)
            { return false; }

            personList.Remove(person);
            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return personList.Where(s => s.Email.EndsWith("@" + emailDomain)).OrderBy(s => s.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return personList.Where(s => s.Name == name && s.Town == town).OrderBy(s => s.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return personList.Where(s => s.Age >= startAge && s.Age <= endAge).OrderBy(s => s.Age).ThenBy(s => s.Email);
        }

        public IEnumerable<Person> FindPersons(
            int startAge, int endAge, string town)
        {
            return personList.Where(s => s.Age >= startAge && s.Age <= endAge).Where(s => s.Town == town).OrderBy(s => s.Age).ThenBy(s => s.Email);
        }
    }
}
