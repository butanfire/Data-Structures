using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    Dictionary<string, Person> addCollection = new Dictionary<string, Person>();
    Dictionary<string, OrderedDictionary<string, Person>> findByEmail = new Dictionary<string, OrderedDictionary<string, Person>>();
    Dictionary<string, OrderedDictionary<string, Person>> peoples = new Dictionary<string, OrderedDictionary<string, Person>>();
    OrderedDictionary<int, OrderedDictionary<string, Person>> peopleByAge = new OrderedDictionary<int, OrderedDictionary<string, Person>>();
    OrderedDictionary<string, OrderedDictionary<int, OrderedDictionary<string, Person>>> peopleByAgeTown = new OrderedDictionary<string, OrderedDictionary<int, OrderedDictionary<string, Person>>>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (addCollection.ContainsKey(email))
        {
            return false;
        }

        var person = new Person(email, name, age, town);
        addCollection.Add(email, person);

        //by email domain
        var domain = email.Substring(email.IndexOf('@'));
        if (!findByEmail.ContainsKey(domain))
        {
            findByEmail.Add(domain, new OrderedDictionary<string, Person>());
        }

        findByEmail[domain].Add(email, person);

        //by name+town
        if (!peoples.ContainsKey(name + town))
        {
            peoples.Add(name + town, new OrderedDictionary<string, Person>());
        }

        peoples[name + town].Add(email, person);

        //by age
        if (!peopleByAge.ContainsKey(age))
        {
            peopleByAge.Add(age, new OrderedDictionary<string, Person>());
        }

        peopleByAge[age].Add(email, person);

        //by age and town
        if(!peopleByAgeTown.ContainsKey(town))
        {
            peopleByAgeTown.Add(town, new OrderedDictionary<int, OrderedDictionary<string, Person>>());
        }
        if (!peopleByAgeTown[town].ContainsKey(age))
        {
            peopleByAgeTown[town].Add(age, new OrderedDictionary<string, Person>());
        }
        peopleByAgeTown[town][age].Add(email, person);

        return true;
    }

    public int Count
    {
        get
        {
            return addCollection.Count;
        }
    }

    public Person FindPerson(string email)
    {
        if (addCollection.ContainsKey(email))
        {
            return addCollection[email];
        }

        return null;
    }

    public bool DeletePerson(string email)
    {
        var result = this.FindPerson(email);
        if (result == null) return false;
        addCollection.Remove(email);

        //by domain
        var domain = email.Substring(email.IndexOf('@'));
        findByEmail[domain].Remove(email);

        //by name and town
        peoples.Remove(result.Name + result.Town);

        //by age
        peopleByAge[result.Age].Remove(email);

        //by age and town
        peopleByAgeTown[result.Town][result.Age].Remove(email);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        string mail = "@" + emailDomain;
        if (findByEmail.ContainsKey(mail))
        {
            return findByEmail[mail].Values;
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        if (peoples.ContainsKey(name + town))
        {
            return peoples[name + town].Values;
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = peopleByAge.Range(startAge, true, endAge, true);
        foreach (var persons in personsInRange)
        {
            foreach (var person in persons.Value)
            {
                yield return person.Value;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (peopleByAgeTown.ContainsKey(town))
        {
            var personsInRange = peopleByAgeTown[town].Range(startAge, true, endAge, true);
            foreach (var persons in personsInRange)
            {
                foreach (var person in persons.Value)
                {
                    yield return person.Value;
                }
            }
        }

        yield break;
    }
}
