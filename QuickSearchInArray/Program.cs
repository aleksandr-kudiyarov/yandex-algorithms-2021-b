using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickSearchInArray
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(string[] lines)
        {
            var array = lines[1]
                .Trim()
                .Split()
                .Select(long.Parse)
                .OrderBy(number => number)
                .ToList();

            var requestsCount = int.Parse(lines[2].Trim());
            var responses = new List<int>(requestsCount);

            for (var i = 0; i < requestsCount; i++)
            {
                var request = lines[3 + i]
                    .Trim()
                    .Split()
                    .Select(int.Parse)
                    .ToList();

                var a = request[0];
                var b = request[1];
                
                var isOutOfRange =
                    a < array[0] && b < array[0] ||
                    a > array[array.Count - 1] && b > array[array.Count - 1];

                if (isOutOfRange)
                {
                    responses.Add(0);
                    continue;
                }
                
                var min = array.LeftBinarySearch(a);
                var max = array.RightBinarySearch(b);
                var response = max - min + 1;
                responses.Add(response);
            }

            var result = string.Join(" ", responses);
            return result;
        }

        private static int LeftBinarySearch(this IReadOnlyList<long> source, long param)
        {
            var l = 0;
            var r = source.Count - 1;
            
            while (l < r)
            {
                var m = (l + r) / 2;
                var value = source[m];

                if (value >= param)
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }
            
            return l;
        }
        
        private static int RightBinarySearch(this IReadOnlyList<long> source, long param)
        {
            var l = 0;
            var r = source.Count - 1;
            
            while (l < r)
            {
                var m = (l + r + 1) / 2;
                var value = source[m];

                if (value <= param)
                {
                    l = m;
                }
                else
                {
                    r = m - 1;
                }
            }
            
            return l;
        }
    }
}