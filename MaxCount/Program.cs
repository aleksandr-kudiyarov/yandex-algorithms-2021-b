using System;
using System.Collections.Generic;

namespace MaxCount
{
    internal static class Program
    {
        private static void Main()
        {
            var input = GetInput();
            var result = MaxCountWorker.GetResult(input);
            Console.WriteLine(result);
        }

        private static IEnumerable<int> GetInput()
        {
            string input;
            
            while ((input = Console.ReadLine()) != "0")
            {
                yield return int.Parse(input);
            }
        }
    }

    public static class MaxCountWorker
    {
        public static int GetResult(IEnumerable<int> enumerable)
        {
            int? currentMax = null;
            var count = 0;

            foreach (var value in enumerable)
            {
                if (currentMax == null || value > currentMax)
                {
                    currentMax = value;
                    count = 1;
                }
                else if (value == currentMax)
                {
                    count++;
                }
            }

            return count;
        }
    }
}