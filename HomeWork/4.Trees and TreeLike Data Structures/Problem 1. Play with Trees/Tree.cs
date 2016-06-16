using System.Collections.Generic;

namespace Problem_1.Play_with_Trees
{
    public class Tree
    {
        private long? subTreeSum;

        public int Value { get; set; }
        public Tree Parent { get; set; }
        public IList<Tree> Children { get; private set; }
        public Tree(int value, Tree parent = null)
        {
            this.Value = value;
            this.Children = new List<Tree>();
            this.Parent = parent;
        }

        public long SubTreeSum
        {
            get
            {
                if (this.subTreeSum == null)
                {
                    this.CalculateSubTreeSum();
                }

                return this.subTreeSum.Value;
            }
        }

        private void CalculateSubTreeSum()
        {
            this.subTreeSum = 0L;
            this.subTreeSum += this.Value;

            foreach (var child in this.Children)
            {
                this.subTreeSum += child.SubTreeSum;
            }
        }
    }
}
