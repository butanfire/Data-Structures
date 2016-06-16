namespace Problem_2.Calculate_Sequence_with_a_Queue
{
    using System;
    using System.Collections.Generic;

    public class CalcSequence
    {
        public static void Main(string[] args)
        {
            Queue<int> numbers = new Queue<int>();
            int input = int.Parse(Console.ReadLine());
            Console.Write(input + " ");

            numbers.Enqueue(input);

            for (int i = 0; i < 50; i++)
            {
                int s = numbers.Dequeue();

                int s2 = s + 1;
                int s3 = 2 * s + 1;
                int s4 = s + 2;
                Console.Write("{0} {1} {2} ", s2, s3, s4);

                numbers.Enqueue(s2);
                numbers.Enqueue(s3);
                numbers.Enqueue(s4);
            }
        }
    }
}
