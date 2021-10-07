using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeftAndRightIndexes
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
            var array = input[1]
                .Trim()
                .Split()
                .Select(long.Parse)
                .ToList();
            
            var numbers = input[3]
                .Trim()
                .Split()
                .Select(n => array.GetIndexes(n));

            var result = string.Join(Environment.NewLine, numbers);
            return result;
        }

        private static string GetIndexes(this IReadOnlyList<long> source, string s)
        {
            var value = long.Parse(s);
            var l = source.LeftBinarySearch(value) + 1 ?? 0;
            var r = source.RightBinarySearch(value) + 1 ?? 0;

            return $"{l} {r}";
        }
        
        private static int? LeftBinarySearch(this IReadOnlyList<long> source, long param)
        {
            var l = 0;
            var r = source.Count - 1;
            
            while (l < r)
            {
                var m = (l + r) / 2;
                var value = source[m];

                if (value >= param)
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }

            return source[l] == param
                ? l
                : (int?)null;
        }
        
        private static int? RightBinarySearch(this IReadOnlyList<long> source, long param)
        {
            var l = 0;
            var r = source.Count - 1;
            
            while (l < r)
            {
                var m = (l + r + 1) / 2;
                var value = source[m];

                if (value <= param)
                {
                    l = m;
                }
                else
                {
                    r = m - 1;
                }
            }
            
            return source[l] == param
                ? l
                : (int?)null;
        }
    }
}