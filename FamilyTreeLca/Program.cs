using System;
using System.Collections.Generic;
using System.IO;

namespace FamilyTreeLca
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(string[] input)
        {
            var count = int.Parse(input[0]);
            var tree = new FamilyTree(count - 1);
            FillTree(tree, input, count);
               
            var relations = GetLca(tree, input, count);
            var output = string.Join(Environment.NewLine, relations);
            return output;
        }

        private static IEnumerable<string> GetLca(FamilyTree tree, IReadOnlyList<string> input, int count)
        {
            for (var i = count; i < input.Count; i++)
            {
                var splitted = input[i].Split();
                var first = splitted[0];
                var second = splitted[1];
                var relation = tree.GetLca(first, second);
                yield return relation;
            }
        }

        private static void FillTree(FamilyTree tree, IReadOnlyList<string> input, int count)
        {
            for (var i = 1; i < count; i++)
            {
                var splitted = input[i].Split();
                var children = splitted[0];
                var parent = splitted[1];
                tree.Add(children, parent);
            }
        }
    }

    public class FamilyTree
    {
        private readonly Dictionary<string, Node<string>> _dictionary;

        public FamilyTree(int capacity)
        {
            _dictionary = new Dictionary<string, Node<string>>(capacity);
        }

        public void Add(string childrenName, string parentName)
        {
            var parentNode = GetNode(parentName);
            var childrenNode = GetNode(childrenName);
            childrenNode.Parent = parentNode;
        }

        public string GetLca(string first, string second)
        {
            var set = new HashSet<string>();

            var firstNode = _dictionary[first];
            var secondNode = _dictionary[second];
            
            AddValue(set, firstNode);
            var lca = GetLca(set, secondNode);
            return lca;
        }

        private static void AddValue<T>(ICollection<T> collection, Node<T> node)
        {
            while (true)
            {
                if (node == null)
                {
                    return;
                }

                collection.Add(node.Value);
                node = node.Parent;
            }
        }

        private static T GetLca<T>(ICollection<T> collection, Node<T> node)
        {
            while (true)
            {
                if (node == null)
                {
                    return default(T);
                }

                if (collection.Contains(node.Value))
                {
                    return node.Value;
                }

                node = node.Parent;
            }
        }

        private Node<string> GetNode(string name)
        {
            Node<string> parentNode;
               
            if (!_dictionary.TryGetValue(name, out parentNode))
            {
                parentNode = new Node<string> { Value = name };
                
                _dictionary[name] = parentNode;
            }

            return parentNode;
        }
    }

    public class Node<T>
    {
        public Node<T> Parent { get; set; }
        public T Value { get; set; }
    }
}