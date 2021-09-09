using System;
using System.Collections.Generic;

namespace UniqueElements
{
    internal static class Program
    {
        private static void Main()
        {
            var input = Console.ReadLine().Trim().Split().ToInt();
            var result = UniqueElementsWorker.GetResult(input);
            var output = string.Join(" ", result);
            Console.WriteLine(output);
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

    public static class UniqueElementsWorker
    {
        public static IEnumerable<int> GetResult(IReadOnlyList<int> input)
        {
            var dictionary = new Dictionary<int, int>();

            foreach (var i in input)
            {
                if (dictionary.ContainsKey(i))
                {
                    dictionary[i]++;
                }
                else
                {
                    dictionary[i] = 1;
                }
            }

            foreach (var pair in dictionary)
            {
                if (pair.Value == 1)
                {
                    yield return pair.Key;
                }
            }
        }
    }
}