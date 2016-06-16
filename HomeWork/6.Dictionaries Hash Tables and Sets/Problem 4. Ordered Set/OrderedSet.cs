namespace Problem_4.Ordered_Set
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinarySearchTree<T> root;

        private T Value { get; set; }

        public int Count { get; set; }

        public void Add(T element)
        {
            if (this.root == null)
            {
                this.root = new BinarySearchTree<T>(element);
                this.Count++;
            }
            else
            {
                if (this.root.Add(element))
                {
                    this.Count++;
                }
            }
        }

        public bool Contains(T element)
        {
            return this.root.Contains(element) != null;
        }

        public bool Remove(T element)
        {
            var currentNode = this.root.Contains(element);
            if (currentNode == null)
            {
                return false;
            }

            if (currentNode.RightChild != null && currentNode.LeftChild != null)
            {
                var nodeToBeMoved = currentNode.RightChild;

                while (nodeToBeMoved.LeftChild != null)
                {
                    nodeToBeMoved = nodeToBeMoved.LeftChild;
                }
                
                if (nodeToBeMoved.RightChild != null)
                {
                    nodeToBeMoved.RightChild = currentNode.RightChild;
                    nodeToBeMoved.RightChild.Root = nodeToBeMoved;
                }

                if (currentNode.LeftChild != null)
                {
                    nodeToBeMoved.LeftChild = currentNode.LeftChild;
                    nodeToBeMoved.LeftChild.Root = nodeToBeMoved;
                }

                if (currentNode.Root == null)
                {
                    nodeToBeMoved.Root = null;
                    this.root = nodeToBeMoved;
                }
                else
                {
                    if (currentNode.IsLeftChild())
                    {
                        currentNode.Root.LeftChild = nodeToBeMoved;
                    }
                    else
                    {
                        currentNode.Root.RightChild = nodeToBeMoved;
                    }

                    nodeToBeMoved.Root = currentNode.Root;
                }
            }
            else if (currentNode.RightChild != null)
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Root.LeftChild = currentNode.RightChild;
                }
                else
                {
                    currentNode.Root.RightChild = currentNode.RightChild;
                }

                currentNode.RightChild.Root = currentNode.Root;
            }
            else if (currentNode.LeftChild != null)
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Root.LeftChild = currentNode.LeftChild;
                }
                else
                {
                    currentNode.Root.RightChild = currentNode.LeftChild;
                }

                currentNode.LeftChild.Root = currentNode.Root;
            }
            else
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Root.LeftChild = null;
                }
                else
                {
                    currentNode.Root.RightChild = null;
                }
            }

            this.Count--;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void PrintTree()
        {
            this.root.Print();
        }

    }
}
