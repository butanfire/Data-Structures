namespace Problem_6.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private T[] array;
        private int Index { get; set; }
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public ReversedList(int capacity)
        {
            this.Capacity = capacity;
            this.array = new T[capacity];
            this.Index = -1; //we start from -1 to avoid the problem when accessing the 0 element, where we have only 1 element in the array
            this.Count = 0;
        }

        public void Add(T item)
        {
            if (this.Count == this.Capacity)
            {
                this.array = ExtendArray();
            }

            this.Index++;
            this.Count++;

            this.array[Index] = item;
        }

        public T this[int index]
        {
            get
            {
                return this.array[this.Index - index];
            }
        }

        public void Remove(int index)
        {
            var newArray = new T[this.Count - 1];
            Array.Copy(array, newArray, index + 1);
            Array.Copy(array, index, newArray, index, this.Count - index - 1);
            this.array = newArray;
            this.Index--;
            this.Count--;
        }

        private T[] ExtendArray()
        {
            var newArray = new T[this.Capacity * 2];
            Array.Copy(this.array, newArray, this.Count);
            return newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var tempIndex = 0;
            while (tempIndex < this.Count)
            {
                yield return this.array[tempIndex++];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
