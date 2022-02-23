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
                var splitted = line.Split();
                var a = int.Parse(splitted[0]);
                var b = int.Parse(splitted[1]);
                
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
            
            LinkNodes(nodeA, nodeB);
        }
        
        public int FindLongest()
        {
            var further = default(Node<T>); 
            var longest = default(int);
            
            var first = _dictionary.Values.First(pair => pair.Links.Count == 1);

            GetLength(first, null, 1, ref longest, ref further);
            GetLength(further, null, 1, ref longest, ref further);

            return longest;
        }
        
        private Node<T> GetOrCreate(T value)
        {
            Node<T> node;
            
            if (!_dictionary.TryGetValue(value, out node))
            {
                node = Node<T>.Create();
                _dictionary.Add(value, node);
            }

            return node;
        }
        
        private static void LinkNodes(Node<T> nodeA, Node<T> nodeB)
        {
            nodeA.Links.Add(nodeB);
            nodeB.Links.Add(nodeA);
        }

        private static void GetLength(Node<T> node, Node<T> previousNode, int length, ref int longest, ref Node<T> further)
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

                GetLength(link, node, len, ref longest, ref further);
            }

            if (nextLinks == 0 && length > longest)
            {
                longest = length;
                further = node;
            }
        }
    }

    public class Node<T>
    {
        public IList<Node<T>> Links { get; } = new List<Node<T>>();
        public static Node<T> Create() => new Node<T>();
    }
}