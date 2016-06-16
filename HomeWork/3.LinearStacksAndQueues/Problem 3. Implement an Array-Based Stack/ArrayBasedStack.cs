namespace Problem_3.Implement_an_Array_Based_Stack
{
    using System;

    public class ArrayBasedStack
    {
        public static void Main(string[] args)
        {
            ArrayStack<int> numbers = new ArrayStack<int>();

            for (int i = 0; i <= 18; i++)
            {
                numbers.Push(i);
            }

            Console.WriteLine("Popping :");

            for (int i = 0; i <= 18; i++)
            {
                Console.Write("{0} ", numbers.Pop());
            }

            for (int i = 0; i <= 18; i++)
            {
                numbers.Push(i);
            }

            Console.WriteLine();
            var arr = numbers.ToArray();
            Console.WriteLine("After converting to array :");
            for (int i = 0; i <= 18; i++)
            {
                Console.Write("{0} ", arr[i]);
            }
        }
    }
}

