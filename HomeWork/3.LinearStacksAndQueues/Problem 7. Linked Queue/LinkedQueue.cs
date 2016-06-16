namespace Problem_7.Linked_Queue
{
    using System;

    public class LinkedQueue<T>
    {
        private QueueNode<T> startNode;

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            if (this.Count == 0)
            {
                this.startNode = new QueueNode<T>(element);
            }
            else
            {
                var newHead = new QueueNode<T>(element);
                newHead.NextNode = this.startNode;
                this.startNode.PrevNode = newHead;
                this.startNode = newHead;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }

            var lastElement = this.startNode.Value;
            if (this.startNode.NextNode == null)
            {
                this.startNode.PrevNode = null;
            }
            else
            {
                this.startNode = this.startNode.NextNode;
            }

            this.startNode.PrevNode = null;
            this.Count--;

            return lastElement;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            int index = 0;
            var currentNode = this.startNode;
            while (currentNode != null)
            {
                array[index++] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return array;
        }

        private class QueueNode<T>
        {
            public T Value { get; set; }
            public QueueNode<T> NextNode { get; set; }
            public QueueNode<T> PrevNode { get; set; }

            public QueueNode(T element)
            {
                this.Value = element;
            }
        }
    }
}
