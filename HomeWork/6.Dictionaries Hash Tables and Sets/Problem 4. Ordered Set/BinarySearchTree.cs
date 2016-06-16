namespace Problem_4.Ordered_Set
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public T Value { get; private set; }
        public BinarySearchTree<T> LeftChild { get; set; }
        public BinarySearchTree<T> RightChild { get; set; }
        public BinarySearchTree<T> Root { get; set; }


        public BinarySearchTree(T value, BinarySearchTree<T> root = null, BinarySearchTree<T> leftChild = null, BinarySearchTree<T> rightChild = null)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            this.Root = root;
        }

        public bool Add(T element)
        {
            if(this.Value.CompareTo(element) < 0)
            {
                if(this.RightChild == null)
                {
                    this.RightChild = new BinarySearchTree<T>(element);
                    this.RightChild.Root = this;
                    return true;                 
                }

                return this.RightChild.Add(element);
            }
           
            if(this.Value.CompareTo(element) > 0)
            {
                if(this.LeftChild == null)
                {
                    this.LeftChild = new BinarySearchTree<T>(element);
                    this.LeftChild.Root = this;
                    return true;
                }

                return this.LeftChild.Add(element);
            }

            return false;
        }

        public BinarySearchTree<T> Contains(T element)
        {
            if(this.Value.CompareTo(element) == 0)
            {
                return this;
            }
            
            if(this.Value.CompareTo(element) > 0)
            {
                return this.LeftChild.Contains(element);
            }

            return this.Value.CompareTo(element) < 0 ? this.RightChild.Contains(element) : null;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftChild != null)
            {
                foreach (var child in this.LeftChild)
                {
                    yield return child;
                }
            }

            yield return this.Value;

            if (this.RightChild != null)
            {
                foreach (var child in this.RightChild)
                {
                    yield return child;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool IsLeftChild()
        {
            return this.Root != null && this.Root.LeftChild == this;
        }

        public void Print(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent * 2) + this.Value);

            this.LeftChild?.Print(indent + 1);

            this.RightChild?.Print(indent + 1);
        }
    }
}
