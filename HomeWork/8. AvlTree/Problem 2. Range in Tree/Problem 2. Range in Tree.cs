namespace Problem_2.Range_in_Tree
{
    using AvlTreeLab;
    using System;
    using System.Linq;

    public class RangeInTree
    {
        static void Main(string[] args)
        {
            var inputNumbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            AvlTree<int> example = new AvlTree<int>();
            foreach(var number in inputNumbers)
            {
                example.Add(number);
            }

            var rangeFromTo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var output = example.InRange(rangeFromTo[0], rangeFromTo[1]);
            if (output.Any())
            {
                foreach (var number in output)
                {
                    Console.Write(number + " ");
                }
            }
            else
            {
                Console.WriteLine("(empty)");
            }
        }
    }
}
