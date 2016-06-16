namespace Problem01.QuadTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuadTree<T> where T : IBoundable
    {
        public const int DefaultMaxDepth = 5;

        public readonly int MaxDepth;

        private Node<T> root;

        public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
        {
            this.root = new Node<T>(0, 0, width, height);
            this.Bounds = this.root.Bounds;
            this.MaxDepth = maxDepth;
        }

        public int Count { get; private set; }

        public Rectangle Bounds { get; private set; }

        public bool Insert(T item)
        {
            if (!item.Bounds.IsInside(this.Bounds))
            {
                return false;
            }

            int depth = 1;
            var currentNode = this.root;
            while (currentNode.Children != null)
            {
                int quadrant = this.GetQuadrant(currentNode, item.Bounds);
                if (quadrant != -1)
                {
                    currentNode = currentNode.Children[quadrant];
                    depth++;
                }
                else
                {
                    break;
                }
            }

            currentNode.Items.Add(item);
            this.Split(currentNode, depth);
            this.Count++;

            return true;
        }

        public List<T> Report(Rectangle bounds)
        {
            var collisionCandidates = new List<T>();

            this.GetCollisionCandidates(this.root, bounds, collisionCandidates);

            return collisionCandidates;
        }

        public void ForEachDfs(Action<List<T>, int, int> action)
        {
            this.ForEachDfs(this.root, action);
        }

        private void GetCollisionCandidates(Node<T> node, Rectangle bounds, List<T> results)
        {
            int quadrant = this.GetQuadrant(node, bounds);
            if (quadrant == -1)
            {
                this.GetSubtreeContents(node, bounds, results);
            }
            else
            {
                if (node.Children != null)
                {
                    foreach (var child in node.Children)
                    {
                        if (bounds.IsInside(child.Bounds))
                        {
                            this.GetCollisionCandidates(child, bounds, results);
                        }
                    }
                }

                results.AddRange(node.Items);
            }
        }

        private void GetSubtreeContents(Node<T> node, Rectangle bounds, List<T> results)
        {
            if (node.Children != null)
            {
                foreach (var child in node.Children.Where(child => child.Bounds.Intersects(bounds)))
                {
                    this.GetSubtreeContents(child, bounds, results);
                }
            }

            results.AddRange(node.Items);
        }

        private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
        {
            if (node == null)
            {
                return;
            }

            if (node.Items.Any())
            {
                action(node.Items, depth, quadrant);
            }

            if (node.Children != null)
            {
                for (int i = 0; i < node.Children.Length; i++)
                {
                    this.ForEachDfs(node.Children[i], action, depth + 1, i);
                }
            }
        }

        private void Split(Node<T> currentNode, int depth)
        {
            if (!(currentNode.ShouldSplit && depth < MaxDepth))
            {
                return;
            }

            int leftWidth = currentNode.Bounds.Width / 2;
            int rightWidth = currentNode.Bounds.Width - leftWidth;
            int topHeight = currentNode.Bounds.Height / 2;
            int bottomHeight = currentNode.Bounds.Height - topHeight;

            currentNode.Children = new Node<T>[4];
            currentNode.Children[0] = new Node<T>(currentNode.Bounds.MidX, currentNode.Bounds.MidY, rightWidth, topHeight);
            currentNode.Children[1] = new Node<T>(currentNode.Bounds.X1, currentNode.Bounds.MidY, leftWidth, topHeight);
            currentNode.Children[2] = new Node<T>(currentNode.Bounds.X1, currentNode.Bounds.Y1, leftWidth, bottomHeight);
            currentNode.Children[3] = new Node<T>(currentNode.Bounds.MidX, currentNode.Bounds.Y1, rightWidth, bottomHeight);

            for (int i = 0; i < currentNode.Items.Count;)
            {
                var item = currentNode.Items[i];
                int quadrant = this.GetQuadrant(currentNode, item.Bounds);
                if (quadrant != -1)
                {
                    currentNode.Items.Remove(item);
                    currentNode.Children[quadrant].Items.Add(item);
                }
                else
                {
                    i++;
                }
            }

            foreach (var item in currentNode.Children)
            {
                this.Split(item, depth + 1);
            }
        }

        private int GetQuadrant(Node<T> node, Rectangle bounds)
        {
            int horizontalMidpoint = node.Bounds.MidY;
            int verticalMidpoint = node.Bounds.MidX;

            bool InTopQuadrant = bounds.Y2 <= node.Bounds.Y2 && bounds.Y1 >= horizontalMidpoint;
            bool InBottomQuadrant = horizontalMidpoint >= bounds.Y2 && node.Bounds.Y1 <= bounds.Y1;
            bool InRightQuadrant = verticalMidpoint <= bounds.X1 && bounds.X2 <= node.Bounds.X2;
            bool InLeftQuadrant = node.Bounds.X1 <= bounds.X1 && bounds.X2 <= verticalMidpoint;

            if (InTopQuadrant)
            {
                if (InRightQuadrant)
                {
                    return 0;
                }
                if (InLeftQuadrant)
                {
                    return 1;
                }
            }
            else if (InBottomQuadrant)
            {
                if (InLeftQuadrant)
                {
                    return 2;
                }
                if (InRightQuadrant)
                {
                    return 3;
                }
            }

            return -1;
        }
    }
}