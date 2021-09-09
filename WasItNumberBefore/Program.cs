using System;
using System.Collections.Generic;

namespace WasItNumberBefore
{
    internal static class Program
    {
        private static void Main()
        {
            var input = Console.ReadLine().Trim().Split().ToInt();
            var result = WasItNumberBeforeWorker.GetResult(input);
            
            foreach (var value in result)
            {
                Console.WriteLine(value);
            }
        }
        
        private static IReadOnlyList<int> ToInt(this IReadOnlyCollection<string> source)
        {
            var list = new List<int>(source.Count);

            foreach (var s in source)
            {
                var i = int.Parse(s);
                list.Add(i);
            }

            return list;
        }
    }

    public static class WasItNumberBeforeWorker
    {
        public static IEnumerable<string> GetResult(IReadOnlyCollection<int> ints)
        {
            var set = new HashSet<int>();
            var result = new List<string>(ints.Count);

            foreach (var i in ints)
            {
                string verdict;
                
                if (set.Contains(i))
                {
                    verdict = "YES";
                }
                else
                {
                    set.Add(i);
                    verdict = "NO";
                }

                result.Add(verdict);
            }

            return result;
        }
    }
}