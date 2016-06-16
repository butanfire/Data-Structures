namespace Problem_3.Tree_Indexing
{
    using AvlTreeLab;
    using System;
    using System.Linq;

    public class TreeIndexing
    {
        public static void Main(string[] args)
        {
            var inputNumbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            AvlTree<int> example = new AvlTree<int>();
            foreach (var number in inputNumbers)
            {
                example.Add(number);
            }

            while (true)
            {
                var input = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(example[input]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Invalid index");
                }
             
            }
        }
    }
}
