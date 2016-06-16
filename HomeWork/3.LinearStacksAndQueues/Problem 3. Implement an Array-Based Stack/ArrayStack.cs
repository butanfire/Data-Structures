namespace Problem_3.Implement_an_Array_Based_Stack
{
    using System;

    public class ArrayStack<T>
    {
        private const int InitCapacity = 16;
        private T[] elements;

        public ArrayStack(int capacity = InitCapacity)
        {
            this.elements = new T[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Push(T element)
        {
            if(this.elements.Length == this.Count)
            {
                this.elements = Grow();
            }

            this.elements[this.Count++] = element;            
        }

        public T Pop()
        {
            return this.elements[--this.Count];
        }

        public T[] ToArray()
        {
            var newElements = new T[this.Count];
            Array.Copy(this.elements, newElements, this.Count);
            return newElements;
        }

        private T[] Grow()
        {
            var newElements = new T[this.elements.Length * 2];
            Array.Copy(this.elements, newElements, this.elements.Length);
            return newElements;
        }
    }
}
