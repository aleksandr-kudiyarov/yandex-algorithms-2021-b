using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var max = long.MinValue;

            if (array.All(n => n >= 0)
                || array.All(n => n <= 0))
            {
                max = prefixSums.Skip(1).Max();
            }
            else
            {
                var borders = new HashSet<int>();
                
                borders.Add(0);
                
                for (var i = 0; i < array.Count; i++)
                {
                    if (array[i] < 0)
                    {
                        borders.Add(i);
                    }
                }

                borders.Add(array.Count - 1);
                var bordersList = borders.ToList();

                for (var i = 0; i < borders.Count - 1; i++)
                {
                    for (var j = i + 1; j < borders.Count; j++)
                    {
                        var l = bordersList[i];
                        var r = bordersList[j];
                        var sum = prefixSums.GetSum(l, r - 1);

                        if (sum > max)
                        {
                            max = sum;
                        }
                    }
                }
            }
            
            return max;
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

        private static long GetSum(this IReadOnlyList<long> source, int l, int r)
        {
            return source[r + 1] - source[l];
        }
    }
}