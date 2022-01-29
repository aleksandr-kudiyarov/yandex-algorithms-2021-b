using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseUnitTest;

namespace BinaryTree
{
    public class Program : IYandexProgram
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        private static string GetResult(string[] input)
        {
            var controller = new TreeController();
            var result = string.Join(Environment.NewLine,
                input.Select(command => controller.Execute(command)));
            return result;
        }

        string IYandexProgram.GetResult(string[] input)
        {
            return GetResult(input);
        }
    }

    public class TreeController
    {
        private readonly Tree<int> _tree;

        public TreeController()
        {
            _tree = new Tree<int>();
        }

        public string Execute(string command)
        {
            if (command.Equals("PRINTTREE"))
            {
                return _tree.Print();
            }

            var parameters = command.Split();
            var commandName = parameters[0];
            var argument = int.Parse(parameters[1]);

            switch (commandName)
            {
                case "ADD":
                    return _tree.Add(argument)
                        ? "DONE"
                        : "ALREADY";
                case "SEARCH":
                    return _tree.Search(argument)
                        ? "YES"
                        : "NO";
            }

            throw new NotImplementedException();
        }
    }

    public class Tree<T>
        where T : IComparable<T>
    {
        private Node<T> Root { get; set; }

        public bool Add(T value)
        {
            if (Root != null)
            {
                return Add(value, Root);
            }

            Root = Node<T>.Create(value);

            return true;
        }

        private static bool Add(T value, Node<T> node)
        {
            var compare = value.CompareTo(node.Value);
            
            switch (compare)
            {
                case -1:
                    if (node.Left == null)
                    {
                        node.Left = Node<T>.Create(value);
                        return true;
                    }
                    else
                    {
                        return Add(value, node.Left);
                    }
                case 1:
                    if (node.Right == null)
                    {
                        node.Right = Node<T>.Create(value);
                        return true;
                    }
                    else
                    {
                        return Add(value, node.Right);
                    }
            }

            return false;
        }

        public bool Search(T value)
        {
            return Search(value, Root);
        }

        private static bool Search(T value, Node<T> node)
        {
            if (node == null)
            {
                return false;
            }
            
            var compare = value.CompareTo(node.Value);

            switch (compare)
            {
                case -1:
                    return Search(value, node.Left);
                case 1:
                    return Search(value, node.Right);
            }

            return true;
        }

        public string Print()
        {
            var presentations = GetNodePresentations();
            var strings = presentations.Select(presentation => presentation.GetString());
            var result = string.Join(Environment.NewLine, strings);
            return result;
        }

        private IReadOnlyList<NodePresentation<T>> GetNodePresentations()
        {
            var list = new List<NodePresentation<T>>();
            AddNodePresentation(Root, 0, list);
            return list;
        }

        private static void AddNodePresentation(Node<T> node, int level, IList<NodePresentation<T>> list)
        {
            if (node == null)
            {
                return;
            }

            var nextLevel = level + 1;
            AddNodePresentation(node.Left, nextLevel, list);
            
            var presentation = new NodePresentation<T>(node.Value, level);
            list.Add(presentation);
            
            AddNodePresentation(node.Right, nextLevel, list);
        }
    }

    public struct NodePresentation<T>
    {
        public NodePresentation(
            T value,
            int level)
        {
            _value = value;
            _level = level;
        }

        private readonly T _value;
        private readonly int _level;

        public string GetString()
        {
            var dots = new string('.', _level);
            return $"{dots}{_value}";
        }
    }

    public class Node<T>
    {
        private Node()
        {
            
        }

        public T Value { get; private set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public static Node<T> Create(T value)
        {
            return new Node<T> { Value = value };
        }
    }
}