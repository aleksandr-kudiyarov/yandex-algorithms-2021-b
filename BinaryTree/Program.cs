using System;
using System.IO;
using System.Linq;

namespace BinaryTree
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
            var controller = new TreeController();
            var result = string.Join(Environment.NewLine,
                input.Select(command => controller.Execute(command)));
            return result;
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

        private int GetArgument(string command)
        {
            var parameters = command.Split();
            var stringArgument = parameters[1];
            var argument = int.Parse(stringArgument);
            return argument;
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

        private bool Add(T value, Node<T> node)
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

        private bool Search(T value, Node<T> node)
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
            return null;
        }
    }

    public class Node<T>
    {
        private Node()
        {
            
        }

        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public static Node<T> Create(T value)
        {
            return new Node<T> { Value = value };
        }
    }
}