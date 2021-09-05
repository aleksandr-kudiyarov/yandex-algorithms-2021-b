using System;

namespace Palindrome
{
    internal static class Program
    {
        private static void Main()
        {
            var line = Console.ReadLine().Trim();
            var result = PalindromeWorker.GetResult(line);
            Console.WriteLine(result);
        }
    }

    public static class PalindromeWorker
    {
        public static int GetResult(string input)
        {
            var count = 0;
            
            for (var i = 0; i < input.Length / 2; i++)
            {
                if (input[i] != input[input.Length - 1 - i])
                {
                    count++;
                }
            }

            return count;
        }
    }
}