using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Beads
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(string[] input)
        {
            var tree = new Tree<int>();

            for (int i = 1; i < input.Length; i++)
            {
                var line = input[i];
                var splittedLine = line.Split();
                var a = int.Parse(splittedLine[0]);
                var b = int.Parse(splittedLine[1]);
                
                tree.Add(a, b);
            }

            var longest = tree.FindLongest();
            return longest.ToString();
        }
    }

    public class Tree<T>
    {
        private readonly Dictionary<T, Node<T>> _dictionary;

        public Tree()
        {
            _dictionary = new Dictionary<T, Node<T>>();
        }

        public void Add(T a, T b)
        {
            var nodeA = GetOrCreate(a);
            var nodeB = GetOrCreate(b);
            
            nodeA.Links.Add(nodeB);
            nodeB.Links.Add(nodeA);
        }

        public int FindLongest()
        {
            var values = new List<int>();
            
            var first = _dictionary.Values.First(pair => pair.Links.Count == 1);
            GetLength(first, null, 1, values);
            return values.Max();
        }

        private static void GetLength(Node<T> node, Node<T> previousNode, int length, List<int> values)
        {
            var nextLinks = 0;
            var len = length + 1;
            
            foreach (var link in node.Links)
            {
                if (ReferenceEquals(link, previousNode))
                {
                    continue;
                }

                nextLinks++;

                GetLength(link, node, len, values);
            }

            if (nextLinks == 0)
            {
                values.Add(length);
            }
        }

        private Node<T> GetOrCreate(T value)
        {
            Node<T> node;
            
            if (!_dictionary.TryGetValue(value, out node))
            {
                node = new Node<T>
                {
                    Value = value
                };
                
                _dictionary.Add(value, node);
            }

            return node;
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public IList<Node<T>> Links { get; set; } = new List<Node<T>>();
    }
}