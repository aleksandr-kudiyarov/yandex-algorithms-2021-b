using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrefixSum
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var sums = GetResult(lines);
            var result = sums.Select(i => i.ToString());
            File.WriteAllLines("output.txt", result);
        }

        private static IEnumerable<long> GetResult(IReadOnlyList<string> input)
        {
            var array = input[1].ToLongs();
            var prefixSums = array.GetPrefixSumArray();

            for (var i = 2; i < input.Count; i++)
            {
                var line = input[i].ToInts();
                var l = line[0];
                var r = line[1];
                var sum = prefixSums.GetSum(l, r);
                yield return sum;
            }
        }

        private static IReadOnlyList<long> ToLongs(this string line)
        {
            var parts = line.Trim().Split();
            var list = new List<long>(parts.Length);

            foreach (var part in parts)
            {
                var i = long.Parse(part);
                list.Add(i);
            }

            return list;
        }
        
        private static IReadOnlyList<int> ToInts(this string line)
        {
            var parts = line.Trim().Split();
            var list = new List<int>(parts.Length);

            foreach (var part in parts)
            {
                var i = int.Parse(part);
                list.Add(i);
            }

            return list;
        }

        private static IReadOnlyList<long> GetPrefixSumArray(this IReadOnlyList<long> source)
        {
            var list = new long[source.Count + 1];

            for (var i = 0; i < source.Count; i++)
            {
                var sum = source[i] + list[i];
                list[i + 1] = sum;
            }

            return list;
        }

        private static long GetSum(this IReadOnlyList<long> source, int l, int r)
        {
            var rValue = source[r];
            var lValue = source[l - 1];
            var result = rValue - lValue;
            return result;
        }
    }
}