namespace Problem_3.Phonebook
{
    using Problem_1.Dictionary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PhoneBook
    {
        static void Main(string[] args)
        {
            //+Bonus , it can keep multiple phone numbers for the same person :)
            HashTable<string, string> phoneBook = new HashTable<string, string>();

            string userInput = Console.ReadLine();
            while (userInput != "search")
            {
                var phoneDetails = userInput.Split('-').ToList();

                if (!phoneBook.ContainsKey(phoneDetails[0] + phoneDetails[1])) //like in the HQC exam - the key is the - key+value
                {
                    phoneBook.Add(phoneDetails[0] + phoneDetails[1], phoneDetails[1]);
                }

                userInput = Console.ReadLine();
            }

            while (true)
            {
                var searchInput = Console.ReadLine();
                var multiple = phoneBook.Where(
                    s => s.Key.Substring(0, searchInput.Length) == searchInput)
                    .ToList(); //fetching the respective name from the Dictionary and extracting all numbers to a list

                if (multiple.Count > 0)
                {
                    foreach (var element in multiple)
                    {
                        try
                        {
                            Console.WriteLine("{0} -> {1}", searchInput, phoneBook[searchInput + element.Value]);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Contact " + searchInput + " does not exist.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Contact " + searchInput + " does not exist.");
                }
            }
        }
    }
}
