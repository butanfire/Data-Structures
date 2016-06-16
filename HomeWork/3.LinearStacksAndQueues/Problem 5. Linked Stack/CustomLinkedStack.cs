using System;

namespace Problem_5.Linked_Stack
{
   public class CustomLinkedStack
    {
        public static void Main(string[] args)
        {
            LinkedStack<int> linkedStack = new LinkedStack<int>();
            Console.WriteLine("Push all elements :");
            linkedStack.Push(10);
            linkedStack.Push(1);
            linkedStack.Push(5);
            linkedStack.Push(4);
            linkedStack.Push(3);
            Console.WriteLine("Count of the stack is : {0}",linkedStack.Count);

            Console.WriteLine("Change to array :");
            var tempArray = linkedStack.ToArray();
            Console.WriteLine(string.Join(", ",tempArray));


            Console.WriteLine("Pop out all elements :");
            for (int i = 0; i < 5; i++)
            {
                int element = linkedStack.Pop();
                Console.Write(element + " ");
            }
        }
    }
}
