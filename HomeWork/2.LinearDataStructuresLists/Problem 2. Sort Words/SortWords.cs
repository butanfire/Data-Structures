namespace Problem_2.Sort_Words
{
    using System;
    using System.Linq;

    public class SortWords
    {
        public static void Main(string[] args)
        {
            var inputWords = Console.ReadLine().Split(' ').ToList();
            inputWords.Sort();
            Console.WriteLine(string.Join(" ", inputWords));
        }
    }
}
