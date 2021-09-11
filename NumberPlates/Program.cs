using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPlates
{
    internal class Program
    {
        private static void Main()
        {
            var m = int.Parse(Console.ReadLine().Trim());
            var values = GetWitnessSets(m);
            var n = int.Parse(Console.ReadLine().Trim());
            var mostValues = GetMostValued(values, n);

            foreach (var value in mostValues)
            {
                Console.WriteLine(value);
            }
        }

        private static IReadOnlyList<HashSet<char>> GetWitnessSets(int count)
        {
            var array = new HashSet<char>[count];

            for (var i = 0; i < count; i++)
            {
                var line = Console.ReadLine().Trim();
                var set = new HashSet<char>(line);
                array[i] = set;
            }

            return array;
        }

        private static IEnumerable<string> GetMostValued(IReadOnlyList<HashSet<char>> values, int count)
        {
            var dictionary = new Dictionary<int, IList<string>>();

            for (var i = 0; i < count; i++)
            {
                var number = Console.ReadLine().Trim();
                var set = new HashSet<char>(number);
                var counter = values.Count(value => value.IsSubsetOf(set));

                IList<string> list;
                if (!dictionary.TryGetValue(counter, out list))
                {
                    list = new List<string>();
                    dictionary[counter] = list;
                }
                
                list.Add(number);
            }

            var max = dictionary.Keys.Max();
            return dictionary[max];
        }
    }
}