using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrequencyAnalysis
{
    internal static class Program
    {
        private static void Main()
        {
            var words = File.ReadAllLines("input.txt")
                .SelectMany(i => i.Split());

            var dictionary = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!dictionary.ContainsKey(word))
                {
                    dictionary[word] = 0;
                }

                dictionary[word]++;
            }

            var ordered = dictionary
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Select(pair => pair.Key);

            File.WriteAllLines("output.txt", ordered);
        }
    }
}