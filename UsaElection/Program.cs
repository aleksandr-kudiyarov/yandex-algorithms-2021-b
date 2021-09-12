using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UsaElection
{
    internal static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var dictionary = GetDictionary(input);

            var sortedKeys = dictionary.Keys
                .OrderBy(key => key)
                .Select(key => $"{key} {dictionary[key].ToString()}");
            
            File.WriteAllLines("output.txt", sortedKeys);
        }

        private static IReadOnlyDictionary<string, int> GetDictionary(IEnumerable<string> input)
        {
            var splitter = new[] { ' ' };
            var dictionary = new Dictionary<string, int>();

            foreach (var value in input)
            {
                var splitted = value.Split(splitter);
                var name = splitted[0];
                var votes = int.Parse(splitted[1]);

                if (!dictionary.ContainsKey(name))
                {
                    dictionary[name] = 0;
                }

                dictionary[name] += votes;
            }

            return dictionary;
        }
    }
}