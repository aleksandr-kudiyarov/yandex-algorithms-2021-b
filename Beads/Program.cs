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

            for (var i = 1; i < input.Length; i++)
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
            var max = 0;
            
            // todo bad moment
            var firsts = _dictionary.Values.Where(pair => pair.Links.Count == 1);

            foreach (var first in firsts)
            {
                GetLength(first, null, 1, ref max);    
            }

            return max;
        }

        private static void GetLength(Node<T> node, Node<T> previousNode, int length, ref int max)
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

                GetLength(link, node, len, ref max);
            }

            if (nextLinks == 0)
            {
                if (length > max)
                {
                    max = length;
                }
            }
        }

        private Node<T> GetOrCreate(T value)
        {
            Node<T> node;
            
            if (!_dictionary.TryGetValue(value, out node))
            {
                node = Node<T>.Create(value);
                
                _dictionary.Add(value, node);
            }

            return node;
        }
    }

    public class Node<T>
    {
        private Node(T value)
        {
            Value = value;
        }
        
        public T Value { get; }
        public IList<Node<T>> Links { get; } = new List<Node<T>>();

        public static Node<T> Create(T value) => new Node<T>(value);
    }
}