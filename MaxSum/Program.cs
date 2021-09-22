using System;
using System.Collections.Generic;
using System.IO;

namespace MaxSum
{
    internal static class Program
    {
        private static void Main()
        {
            var result = GetResult("input.txt");
            Console.WriteLine(result);
        }

        private static long GetResult(string path)
        {
            var lines = File.ReadAllLines(path);
            var array = lines[1].Trim().Split().ToInts();
            var prefixSums = array.GetPrefixSums();
            var result = prefixSums.GetMaxSum();

            return result;
        }

        private static IReadOnlyList<long> ToInts(this IReadOnlyCollection<string> source)
        {
            var list = new List<long>(source.Count);
            
            foreach (var s in source)
            {
                var l = long.Parse(s);
                list.Add(l);
            }

            return list;
        }

        private static IReadOnlyList<long> GetPrefixSums(this IReadOnlyList<long> source)
        {
            var sums = new long[source.Count + 1];

            for (var i = 0; i < source.Count; i++)
            {
                var sum = source[i] + sums[i];
                sums[i + 1] = sum;
            }

            return sums;
        }

        private static long GetMaxSum(this IReadOnlyList<long> source)
        {
            var l = source[0];
            var maxDiff = long.MinValue;

            for (var i = 1; i < source.Count; i++)
            {
                var sum = source[i];
                var diff = sum - l;

                if (sum < l)
                {
                    l = sum;
                }

                if (diff > maxDiff)
                {
                    maxDiff = diff;
                }
            }

            return maxDiff;
        }
    }
}