using System.Collections.Generic;
using System.IO;

namespace FamilyTree
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
               
               var relations = GetRelations(tree, input, count);
               var output = string.Join(" ", relations);
               return output;
          }

          private static IEnumerable<string> GetRelations(FamilyTree tree, IReadOnlyList<string> input, int count)
          {
               for (var i = count; i < input.Count; i++)
               {
                    var splitted = input[i].Split();
                    var first = splitted[0];
                    var second = splitted[1];
                    var relation = tree.GetRelation(first, second);
                    yield return relation.ToString();
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
               var parentNode = GetParentNode(parentName);

               var childrenNode = new Node<string>
               {
                    Parent = parentNode,
                    Value = childrenName
               };

               _dictionary[childrenName] = childrenNode;
          }

          public int GetRelation(string first, string second)
          {
               var firstNode = _dictionary[first];

               if (TryGetParent(second, firstNode))
               {
                    return 2;
               }

               var secondNode = _dictionary[second];

               if (TryGetParent(first, secondNode))
               {
                    return 1;
               }

               return 0;
          }

          private static bool TryGetParent(string name, Node<string> node)
          {
               if (node == null)
               {
                    return false;
               }

               if (node.Value == name)
               {
                    return true;
               }

               return TryGetParent(name, node.Parent);
          }

          private Node<string> GetParentNode(string parentName)
          {
               Node<string> parentNode;
               
               if (!_dictionary.TryGetValue(parentName, out parentNode))
               {
                    parentNode = new Node<string> { Value = parentName };
                    _dictionary[parentName] = parentNode;
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