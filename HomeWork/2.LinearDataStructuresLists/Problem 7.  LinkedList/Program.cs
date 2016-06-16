namespace Problem_7.LinkedList
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var list = new LinkedList<int>();
            Console.WriteLine("Count before adding :" + list.Count());
            list.Add(10);
            list.Add(15);
            list.Add(20);
            list.Add(25);
            list.Add(15);
            list.Add(10);

            Console.WriteLine("Count after adding :" + list.Count());
            Console.WriteLine();
            Console.WriteLine("First index of 15:" + list.FirstIndexOf(15));
            Console.WriteLine();
            Console.WriteLine("Last index of 15:" + list.LastIndexOf(15));

            Console.WriteLine("Remove :");
            Console.WriteLine("Before remove : " + list.Count());
            list.Remove(0);
            Console.WriteLine("After remove : " + list.Count());
        }
    }
}
