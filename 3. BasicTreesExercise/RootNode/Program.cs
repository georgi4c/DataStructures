using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    class Program
    {
        static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        static void Main(string[] args)
        {
            ReadTree();
            var rootNode = GetRootNode();
            //Console.WriteLine($"Root node: {rootNode.Value}");
            //rootNode.PrintTree();
            //Console.WriteLine($"Leaf nodes: {string.Join(" ", LeafNodes())}");
            //Console.WriteLine($"Middle nodes: {string.Join(" ", MiddleNodes())}");
            //Console.WriteLine($"Deepest node: {DeepestNode().Value}");
            //Console.WriteLine($"Longest path: {string.Join(" ", LongestPath())}");
            //AllPathsWithSum();
            AllSubtreesWithSum();


        }

        private static void AllSubtreesWithSum()
        {
            var nodesWithSumSubpaths = new List<Tree<int>>();
            var target = int.Parse(Console.ReadLine());
            var root = GetRootNode();
            var rootSum = SumDFS(root);
            if (rootSum == target)
            {
                nodesWithSumSubpaths.Add(root);
            }

            GetSubtreeSum(root, nodesWithSumSubpaths, target);
            
            Console.WriteLine($"Subtrees of sum {target}:");
            foreach (var node in nodesWithSumSubpaths)
            {
                var elements = new List<int>();
                ValuesDFS(node, elements);
                Console.WriteLine(string.Join(" ", elements));
            }

        }

        private static void GetSubtreeSum(Tree<int> root, List<Tree<int>> nodesWithSumSubpaths, int target)
        {
            foreach (var child in root.Children)
            {
                
                var currentSum = SumDFS(child);
                if (currentSum == target)
                {
                    nodesWithSumSubpaths.Add(child);
                }
                
                GetSubtreeSum(child, nodesWithSumSubpaths, target);
            }
        }

        private static void ValuesDFS(Tree<int> currentElement, List<int> values)
        {
            values.Add(currentElement.Value);
            foreach (var child in currentElement.Children)
            {
                ValuesDFS(child, values);
            }
            
        }

        private static int SumDFS(Tree<int> currentElement)
        {
            int sum = 0;
            foreach (var child in currentElement.Children)
            {
                sum += SumDFS(child);
            }
            return sum + currentElement.Value;
        }
        static void AllPathsWithSum()
        {
            var desiredSum = int.Parse(Console.ReadLine());
            var root = GetRootNode();
            var nodesWithDesiredSum = new List<Tree<int>>();
            FindPathsWithSum(root, nodesWithDesiredSum, desiredSum);
            List<List<int>> result = new List<List<int>>();
            foreach (var node in nodesWithDesiredSum)
            {
                result.Add(GetPath(node));
            }
            Console.WriteLine($"Paths of sum {desiredSum}:");
            foreach (var path in result)
            {
                Console.WriteLine(string.Join(" ", path));
            }
        }

        private static void FindPathsWithSum(Tree<int> currentElement, List<Tree<int>> resultNodes, int target, int sum = 0)
        {
            sum += currentElement.Value;
            foreach (var childElement in currentElement.Children)
            {
                FindPathsWithSum(childElement, resultNodes, target, sum);
            }
            if (currentElement.Children.Count == 0 && sum == target)
            {
                resultNodes.Add(currentElement);
            }
        }

        static List<int> LongestPath()
        {
            Tree<int> deepestElement = DeepestNode();
            var elements = GetPath(deepestElement);
            return elements;
        }

        private static List<int> GetPath(Tree<int> leaf)
        {
            List<int> elements = new List<int>();
            while (true)
            {
                elements.Add(leaf.Value);
                if (leaf.Parent == null)
                {
                    break;
                }
                leaf = leaf.Parent;
            }
            elements.Reverse();
            return elements;
        }

        static Tree<int> DeepestNode()
        {
            List<Tree<int>> leafNodes = new List<Tree<int>>();
            Tree<int> root = GetRootNode();
            GetLeafsWithDepth(root, leafNodes);
            int maxElement = leafNodes.Select(x => x.Depth).Max();
            var deepestElement = leafNodes.FirstOrDefault(x => x.Depth == maxElement);
            return deepestElement;

        }

        private static void GetLeafsWithDepth(Tree<int> currentElement, List<Tree<int>> leafNodes, int depth = 0)
        {
            currentElement.Depth = depth;
            foreach (var currentElementChild in currentElement.Children)
            {
                GetLeafsWithDepth(currentElementChild, leafNodes, depth + 1);
                if (currentElementChild.Children.Count == 0)
                {
                    leafNodes.Add(currentElementChild);
                }
            }
        }

        static List<int> MiddleNodes()
        {
            var result = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count > 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();
            return result;
        }

        static List<int> LeafNodes()
        {
            var result = nodeByValue.Values
                .Where(x => x.Children.Count == 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();
            return result;

        }

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }
            return nodeByValue[value];
        }

        static void AddEdge(int parent, int child)
        {
            var parentNode = GetTreeNodeByValue(parent);
            var childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodeCount; i++)
            {
                int[] edge = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                AddEdge(edge[0], edge[1]);
            }
        }

        static Tree<int> GetRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }
    }
}
