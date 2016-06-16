namespace Problem_1.Dictionary
{
    using System;
    using System.Linq;

    public class Dictionary
    {
        public static void Main(string[] args)
        {
            HashTable<string, int> dictionary = new HashTable<string, int>();
            dictionary.Add("Number 0", 15);
            dictionary.Add("Number 1", 115);
            dictionary.Add("Number 2", 215);
            dictionary.Add("Number 3", 315);

            var ordered = dictionary.OrderBy(s => s.Key).ToList();

            foreach(var element in ordered)
            {
                Console.WriteLine("{0} -> {1}", element.Key, element.Value);
            }
        }
    }
}
