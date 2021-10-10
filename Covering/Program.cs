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
            var segments = int.Parse(lines[0]
                .Trim()
                .Split()
                .ElementAt(1));


            var points = lines[1]
                .Trim()
                .Split()
                .Select(long.Parse)
                .OrderBy(v => v)
                .ToList();

            var result = LeftBinarySearch(points, segments);

            return result.ToString();
        }

        private static int LeftBinarySearch(IReadOnlyList<long> points, int segments)
        {
            var l = 0;
            var r = points.Count - 1;

            while (l < r)
            {
                var m = (l + r) / 2;
                var current = points.TryCover(segments, m);
                
                if (current > 0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }

            return l;
        }

        private static int TryCover(this IReadOnlyList<long> points, int segments, int length)
        {
            checked
            {
                var sum = 0L;
                var firstInSegmentInd = 0;
                var lastInSegmentInd = length;
            
                for (var i = 0; i < segments; i++)
                {
                    if (lastInSegmentInd > points.Count - 1)
                    {
                        return 1;
                    }
                
                    var first = points[firstInSegmentInd];
                    var last = points[lastInSegmentInd];
                    var diff = last - first;
                    var increment = length + 1;

                    sum += diff;
                    firstInSegmentInd += increment;
                    lastInSegmentInd += increment;
                }

                var expected = segments * length;

                if (sum > expected)
                {
                    return 1;
                }
            
                if (sum < expected)
                {
                    return -1;
                }

                return 0;   
            }
        }
    }
}