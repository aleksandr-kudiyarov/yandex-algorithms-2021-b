using System.Collections.Generic;
using System.IO;

namespace BracketSequence
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt").Trim();
            var result = GetResult(input);
            File.WriteAllText("output.txt",result);
        }

        public static string GetResult(string input)
        {
            var stack = new Stack<char>();
            
            foreach (var bracket in input)
            {
                if (bracket == '(')
                {
                    stack.Push(bracket);
                }
                else
                {
                    if (stack.Count == 0 || stack.Pop() != '(')
                    {
                        return "NO";
                    }
                }
            }

            return stack.Count == 0
                ? "YES"
                : "NO";
        }
    }
}