namespace Problem_5.Linked_Stack
{
    using System;
    using System.Collections.Generic;

    public class LinkedStack<T>
    {
        private Node<T> firstNode;

        public int Count { get; set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }

        public T Pop()
        {
            if (!(this.Count > 0))
            {
                throw new IndexOutOfRangeException("No elements exist");
            }

            var element = this.firstNode.Value;                                  
            this.firstNode = this.firstNode.NextNode;
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            int elements = this.Count;
            var tempNode = this.firstNode;
            Stack<T> dataHolder = new Stack<T>();

            for (int i = 0; i < elements; i++)
            {
                dataHolder.Push(tempNode.Value);
                tempNode = tempNode.NextNode;
            }

            return dataHolder.ToArray();
        }
        
        private class Node<T>
        {
            public T Value { get; private set; }
            public Node<T> NextNode { get; private set; }
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }
        }
    }
}
