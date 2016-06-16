using System;

namespace Problem_4.Ordered_Set
{
    public class OrderedSetShowcase
    {
        static void Main(string[] args)
        {
            var OrderedSet = new OrderedSet<int> {17, 9, 12, 19, 6, 25};
            foreach (var item in OrderedSet)
            {
                Console.WriteLine(item);
            }

            //play around with the tree
            OrderedSet.PrintTree();

            OrderedSet.Remove(9);
            OrderedSet.PrintTree();

            OrderedSet.Add(9);
            OrderedSet.Add(4);
            OrderedSet.PrintTree();

            OrderedSet.Remove(12);
            OrderedSet.PrintTree();

            OrderedSet.Remove(6);
            OrderedSet.PrintTree();

            //print in ascending order
            foreach (var item in OrderedSet)
            {
                Console.WriteLine(item);
            }
        }
    }
}
