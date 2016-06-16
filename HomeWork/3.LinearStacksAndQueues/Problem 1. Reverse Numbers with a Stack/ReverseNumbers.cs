namespace Problem_1.Reverse_Numbers_with_a_Stack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseNumbers
    {
        public static void Main(string[] args)
        {
            Stack<int> numbers = new Stack<int>();
            var input = Console.ReadLine().Split(' ').ToArray();

            foreach (string number in input)
            {
                if (number != string.Empty)
                {
                    numbers.Push(int.Parse(number));
                }
            }

            int length = numbers.Count;
            for (int i = 0; i < length; i++)
            {
                Console.Write(numbers.Pop() + " ");
            }
        }
    }
}
