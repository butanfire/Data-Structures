namespace Problem_7.LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedList<T> : IEnumerable<T>
    {
        private int ElementsCount { get; set; }

        public ListNode<T> head;        

        public void Add(T item) //add first
        {
            if (this.ElementsCount == 0)
            {
                this.head = new ListNode<T>(item);
            }
            else
            {
                var newHead = new ListNode<T>(item);
                newHead.NextNode = this.head;
                this.head = newHead;
            }

            this.ElementsCount++;
        }

        public void Remove(int index)
        {
            if (this.ElementsCount == 0)
            {
                throw new InvalidOperationException("Empty list");
            }

            if (index > ElementsCount)
            {
                throw new IndexOutOfRangeException("Index does not exist");
            }
            

            for (int i = 0; i < index - 1; i++)
            {
                this.head = this.head.NextNode;
            }

            this.head.NextNode = this.head.NextNode.NextNode;
            this.head = this.head.NextNode;
            this.ElementsCount--;
        }

        public int Count()
        {
            return this.ElementsCount;
        }

        public int FirstIndexOf(T item)
        {
            int index = 0;

            var currentElement = this.head;
            while (!currentElement.Value.Equals(item) && currentElement.NextNode != null)
            {
                index++;
                currentElement = currentElement.NextNode;
            }

            return index;
        }

        public int LastIndexOf(T item)
        {
            int index = 0;
            int tempIndex = 0;
            var currentElement = this.head;
            for (int i = 0; i < this.ElementsCount; i++, currentElement = currentElement.NextNode)
            {
                if (currentElement.Value.Equals(item))
                {
                    tempIndex = i;
                }

                index = tempIndex;
            }

            return index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
