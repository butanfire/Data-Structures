namespace AvlTreeLab
{
    using System;
    using System.Collections.Generic;
    public class AvlTree<T> where T : IComparable<T>
    {
        private Node<T> root;

        public int Count { get; private set; }

        private int Depth(Node<T> n)
        {
            if (n == null)
            {
                return 0;
            }

            return Math.Max(Depth(n.LeftChild), Depth(n.RightChild)) + 1;
        }

        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }
            if (inserted)
            {
                this.Count++;
            }
        }

        private void ModifyParentCounts(Node<T> startNode)
        {
            while (startNode != null)
            {
                startNode.ChildrenCount++;
                startNode = startNode.Parent;
            }
        }

        private bool InsertInternal(Node<T> node, T item)
        {
            var currentNode = node;
            var newNode = new Node<T>(item);
            var shouldRetrace = false;
            while (true)
            {
                if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        currentNode.BalanceFactor--;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }


                    currentNode = currentNode.RightChild;
                }
                else
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        currentNode.BalanceFactor++;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }

            }

            this.ModifyParentCounts(currentNode);

            if (shouldRetrace)
            {
                this.RetraceInsert(currentNode);
            }

            return true;
        }

        private void RetraceInsert(Node<T> currentNode)
        {
            var parent = root.Parent;
            while (parent != null)
            {
                if (currentNode.isLeftChild)
                {
                    if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor++;
                        if (currentNode.BalanceFactor == -1)
                        {
                            this.RotateLeft(currentNode);
                        }

                        this.RotateRight(parent);
                        break;
                    }
                    if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }

                    parent.BalanceFactor = 1;

                    if (currentNode.isRightChild)
                    {
                        if (parent.BalanceFactor == -1)
                        {
                            parent.BalanceFactor--;
                            if (currentNode.BalanceFactor == 1)
                            {
                                this.RotateRight(currentNode);
                            }

                            this.RotateLeft(parent);
                            break;
                        }
                        if (parent.BalanceFactor == 1)
                        {
                            parent.BalanceFactor = 0;
                            break;
                        }

                        parent.BalanceFactor = -1;
                    }
                }

                currentNode = parent;
                parent = currentNode.Parent;
            }
        }

        private void RotateRight(Node<T> currentNode)
        {
            var parent = currentNode.Parent;
            var child = currentNode.RightChild;
            if (parent != null)
            {
                if (currentNode.isRightChild)
                {
                    parent.RightChild = child;
                }
                else
                {
                    parent.LeftChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            currentNode.LeftChild = child.RightChild;
            child.RightChild = currentNode;

            currentNode.UpdateCounts();
            child.UpdateCounts();

            currentNode.BalanceFactor -= 1 + Math.Max(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 - Math.Min(currentNode.BalanceFactor, 0);
        }

        private void RotateLeft(Node<T> currentNode)
        {
            var parent = currentNode.Parent;
            var child = currentNode.RightChild;
            if (parent != null)
            {
                if (currentNode.isLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            currentNode.RightChild = child.LeftChild;
            child.LeftChild = currentNode;

            currentNode.UpdateCounts();
            child.UpdateCounts();

            currentNode.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(currentNode.BalanceFactor, 0);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.CompareTo(item) == 0)
                {
                    return true;
                }
                if (node.Value.CompareTo(item) < 0)
                {
                    node = node.RightChild;
                }
                if (node.Value.CompareTo(item) > 0)
                {
                    node = node.LeftChild;
                }
            }

            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }

            this.InOrderDFS(this.root, 1, action);
        }

        private void InOrderDFS(Node<T> root, int depth, Action<int, T> action)
        {
            if (root == null)
            {
                return;
            }

            InOrderDFS(root.LeftChild, depth++, action);
            action.Invoke(depth, root.Value);
            InOrderDFS(root.RightChild, depth++, action);
        }

        private void InOrderDFS(Node<T> node, T from, T to, List<T> nodes)
        {
            if (node == null)
            {
                return;
            }
            InOrderDFS(node.LeftChild, from, to, nodes);
            if (node.Value.CompareTo(from) >= 0 && node.Value.CompareTo(to) <= 0)
            {
                nodes.Add(node.Value);
            }

            InOrderDFS(node.RightChild, from, to, nodes);
        }

        public List<T> InRange(T from, T to)
        {
            var node = this.root;
            List<T> nodeValues = new List<T>();
            InOrderDFS(node, from, to, nodeValues);

            return nodeValues;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException("Index must be in range [0.." + this.Count + "]");
                }

                var currentNode = this.root;
                while (true)
                {
                    var leftCount = currentNode.LeftChild == null ? 0 : currentNode.LeftChild.ChildrenCount;
                    if (leftCount == index)
                    {
                        return currentNode.Value;
                    }
                    if (leftCount > index)
                    {
                        currentNode = currentNode.LeftChild;
                    }
                    else
                    {
                        index -= leftCount + 1;
                        currentNode = currentNode.RightChild;
                    }
                }
            }
        }
    }
}

