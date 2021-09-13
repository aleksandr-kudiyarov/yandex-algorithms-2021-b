using System;
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

            var dictionary = new Dictionary<string, CountedEntity>();

            foreach (var word in words)
            {
                if (!dictionary.ContainsKey(word))
                {
                    dictionary[word] = new CountedEntity
                    {
                        Value = word
                    };
                }

                dictionary[word].Count++;
            }

            var ordered = dictionary
                .OrderBy(pair => pair.Value)
                .Select(pair => pair.Key);

            File.WriteAllLines("output.txt", ordered);
        }
    }

    public class CountedEntity : IComparable<CountedEntity>
    {
        public string Value { get; set; }
        public int Count { get; set; }

        public int CompareTo(CountedEntity other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            var countCompare = Count.CompareTo(other.Count);

            if (countCompare != 0)
            {
                return countCompare * -1;
            }

            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}