using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SumOfThree
{
    public static class Program
    {
        private static readonly char[] Separators = { ' ' };
        
        private static void Main()
        {
            var input = File.ReadAllLines("threesum.in");
            var result = GetResult(input);
            Console.WriteLine(result);
        }

        public static string GetResult(string[] input)
        {
            var s = int.Parse(input[0].Trim());
            var list1 = GetIndexedValues(input[1], s);
            var list2 = GetIndexedValues(input[2], s).ToList();
            var list3 = GetIndexedValues(input[3], s).ToDistinctDictionary();

            foreach (var i in list1)
            {
                foreach (var j in list2)
                {
                    var sum = i.Value + j.Value;
                    var diff = s - sum;
                    Indexed<int> k;

                    if (list3.TryGetValue(diff, out k))
                    {
                        return $"{i.Index} {j.Index} {k.Index}";
                    }
                }
            }

            return "-1";
        }

        private static IEnumerable<Indexed<int>> GetIndexedValues(string line, int maxValue)
        {
            var result = line
                .Trim()
                .Split(Separators)
                .Skip(1)
                .Select((value, index) => new Indexed<int> { Value = int.Parse(value), Index = index })
                .Where(i => i.Value <= maxValue);

            return result;
        }

        private static IReadOnlyDictionary<T, Indexed<T>> ToDistinctDictionary<T>(this IEnumerable<Indexed<T>> source)
        {
            var result = new Dictionary<T, Indexed<T>>();

            foreach (var element in source)
            {
                if (!result.ContainsKey(element.Value))
                {
                    result.Add(element.Value, element);
                }
            }

            return result;
        }
    }

    public class Indexed<T>
    {
        public T Value { get; set; }
        public int Index { get; set; }
    }
}