using System;
using System.Collections.Generic;
using System.Linq;

namespace A_Intersect
{
    internal static class Program
    {
        private static void Main()
        {
            var array1 = Console.ReadLine().Trim().Split().ToInt();
            var array2 = Console.ReadLine().Trim().Split().ToInt();

            var intersect = IntersectWorker.GetResult(array1, array2);
            Console.WriteLine(intersect);
        }

        private static IEnumerable<int> ToInt(this IReadOnlyCollection<string> source)
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

    public static class IntersectWorker
    {
        public static int GetResult(IEnumerable<int> e1, IEnumerable<int> e2)
        {
            return e1.Intersect(e2).Count();
        }
    }
}