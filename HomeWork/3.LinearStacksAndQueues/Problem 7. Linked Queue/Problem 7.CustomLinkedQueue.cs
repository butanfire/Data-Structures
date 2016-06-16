using System;

namespace Problem_7.Linked_Queue
{
    public class CustomLinkedQueue
    {
        public static void Main(string[] args)
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();
            Console.WriteLine("Enqueue 5 elements :");
            linkedQueue.Enqueue(10);
            linkedQueue.Enqueue(9);
            linkedQueue.Enqueue(8);
            linkedQueue.Enqueue(7);
            linkedQueue.Enqueue(6);
            Console.WriteLine("Count of the queue :" + linkedQueue.Count);
            
            var arr = linkedQueue.ToArray();
            Console.WriteLine("Output from the array :");
            Console.WriteLine(string.Join(", ",arr));

            Console.WriteLine("Dequeue all elements form the linkedQueue :");
            var elementsCount = linkedQueue.Count;
            for (int i = 0; i < elementsCount; i++)
            {
                var element = linkedQueue.Dequeue();
                Console.Write(element + " ");
            }
        }
    }
}
