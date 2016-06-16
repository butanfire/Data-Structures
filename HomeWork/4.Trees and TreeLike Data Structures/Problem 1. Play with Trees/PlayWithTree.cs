using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_1.Play_with_Trees
{
    public class PlayWithTree
    {
        private static int numberOfNodes;
        private static IDictionary<int, Tree> nodeByValue;
        private static int longestPath;
        private static Tree longestPathLeaf;
        private static int pathSumWanted;
        private static int subTreeSumWanted;

        public static void Main(string[] args)
        {
            numberOfNodes = int.Parse(Console.ReadLine());
            nodeByValue = new Dictionary<int, Tree>(numberOfNodes);

            for (int i = 1; i < numberOfNodes; i++)
            {
                var parentChildPair = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var parentNode = GetTreeNodeByValue(parentChildPair[0]);
                var childNode = GetTreeNodeByValue(parentChildPair[1]);

                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            pathSumWanted = int.Parse(Console.ReadLine());
            subTreeSumWanted = int.Parse(Console.ReadLine());

            var rootNode = FindRootNode();
            Console.WriteLine($"Root node: {rootNode.Value}");

            // Find leafs
            var allLeafNodes = FindLeafNodes();
            Console.WriteLine($"Leaf nodes: {string.Join(", ", allLeafNodes)}");

            // Find middle nodes
            var allMiddleNodes = FindMiddleNodes();
            Console.WriteLine($"Leaf nodes: {string.Join(", ", allMiddleNodes)}");

            // Find longest path
            FindLongestPath(rootNode);
            var longest = BackTrackPath(longestPathLeaf);
            Console.WriteLine($"Longest path:{Environment.NewLine}{longest} (length = {longestPath})");

            // Find all paths of sum
            Console.WriteLine($"Paths of sum {pathSumWanted}:");
            FindAllPathsWithSum(rootNode, rootNode.Value);

            // Find all sub trees of sum
            Console.WriteLine($"Subtrees of sum {subTreeSumWanted}:");
            FindAllSubTreesOfSum(rootNode);
        }

        private static Tree GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree(value);
            }

            return nodeByValue[value];
        }

        private static void FindLongestPath(Tree tree, int depth = 1)
        {
            if (depth > longestPath)
            {
                longestPath = depth;
                longestPathLeaf = tree;
            }

            foreach (var child in tree.Children)
            {
                FindLongestPath(child, depth + 1);
            }
        }

        private static void FindAllPathsWithSum(Tree tree, int sum)
        {
            if (sum == pathSumWanted)
            {
                Console.WriteLine(BackTrackPath(tree));
            }

            foreach (var child in tree.Children)
            {
                FindAllPathsWithSum(child, sum + child.Value);
            }
        }

        private static string BackTrackPath(Tree tree, string separator = " -> ")
        {
            var result = new LinkedList<int>();
            while (tree != null)
            {
                result.AddFirst(tree.Value);
                tree = tree.Parent;
            }

            return string.Join(separator, result);
        }

        private static void DfsTravers(Tree tree, IList<int> subTree)
        {
            subTree.Add(tree.Value);

            foreach (var child in tree.Children)
            {
                DfsTravers(child, subTree);
            }
        }

        private static void FindAllSubTreesOfSum(Tree tree)
        {
            if (tree.SubTreeSum == subTreeSumWanted)
            {
                var subTree = new List<int>();
                DfsTravers(tree, subTree);
                Console.WriteLine(string.Join(" + ", subTree));
            }

            foreach (var child in tree.Children)
            {
                FindAllSubTreesOfSum(child);
            }
        }

        private static Tree FindRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(node => node.Parent == null);            
        }

        private static IEnumerable<int> FindMiddleNodes()
        {
            return nodeByValue.Values.Where(node => node.Parent != null && node.Children.Count > 0).ToList().Select(node => node.Value);            
        }

        private static IEnumerable<int> FindLeafNodes()
        {
            return nodeByValue.Values
                .Where(node => node.Parent != null && node.Children.Count == 0)
                .OrderBy(node => node.Value)
                .Select(node => node.Value);
        }
    }
}
