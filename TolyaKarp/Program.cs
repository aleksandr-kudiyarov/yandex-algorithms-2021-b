using System;
using System.Collections.Generic;
using System.Linq;

namespace TolyaKarp
{
    internal static class Program
    {
        private static void Main()
        {
            var dictionary = new Dictionary<long, long>();
            var n = int.Parse(Console.ReadLine().Trim());

            for (var i = 0; i < n; i++)
            {
                var line = Console.ReadLine().Trim().Split();

                var di = long.Parse(line[0]);
                var ai = long.Parse(line[1]);

                if (!dictionary.ContainsKey(di))
                {
                    dictionary[di] = 0;
                }

                dictionary[di] += ai;
            }

            var orderedKeys = dictionary.Keys
                .OrderBy(i => i);

            foreach (var key in orderedKeys)
            {
                Console.Write(key);
                Console.Write(' ');
                Console.WriteLine(dictionary[key]);
            }
        }
    }
}