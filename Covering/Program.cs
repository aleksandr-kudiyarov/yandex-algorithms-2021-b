using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Covering
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }
        
        public static string GetResult(IReadOnlyList<string> lines)
        {
            var segments = long.Parse(lines[0]
                .Trim()
                .Split()
                .ElementAt(1));


            var points = lines[1]
                .Trim()
                .Split()
                .Select(long.Parse)
                .OrderBy(v => v)
                .ToList();

            var result = points.GetYandexResult(segments);

            return result.ToString();
        }

        private static long GetYandexResult(this IReadOnlyList<long> points, long segments)
        {
            var l = 0L;
            var r = points[points.Count - 1] - points[0];

            while (l < r)
            {
                var count = 0;
                var m = (l + r) / 2;
                var maxright = points[0] - 1;

                foreach (var point in points)
                {
                    if (point > maxright)
                    {
                        count++;
                        maxright = point + m;
                    }
                }

                if (count <= segments)
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
    }
}