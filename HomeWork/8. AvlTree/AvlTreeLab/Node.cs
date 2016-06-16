using System;

namespace AvlTreeLab
{
    public class Node<T> where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild;
        private int count;

        public Node(T Value)
        {
            this.Value = Value;
            this.ChildrenCount = 1;
        }

        public T Value { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Parent { get; set; }

        public int BalanceFactor { get; set; }

        public bool isLeftChild
        {
            get
            {
                return this.Parent != null && this.Parent.leftChild == this;
            }
        }

        public bool isRightChild
        {
            get
            {
                return this.Parent != null && this.Parent.rightChild == this;
            }
        }

        public int ChildrenCount
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public void UpdateCounts()
        {
            this.ChildrenCount = 1;
            if(this.LeftChild != null)
            {
                this.ChildrenCount += this.leftChild.ChildrenCount;
            }

            if(this.RightChild != null)
            {
                this.ChildrenCount += this.RightChild.ChildrenCount;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

    }
}



