using System;
using System.Collections.Generic;

namespace HousesAndShops
{
    internal static class Program
    {
        private static void Main()
        {
            var line = Console.ReadLine();
            var splittedLine = line.Split(' ');
            var intValues = new List<int>(splittedLine.Length);
            
            foreach (var value in splittedLine)
            {
                int parsed;
                if (int.TryParse(value, out parsed))
                {
                    intValues.Add(int.Parse(value));   
                }
            }

            var result = HousesAndShopsWorker.GetResult(intValues);
            Console.WriteLine(result);
        }
    }

    public static class HousesAndShopsWorker
    {
        public static int GetResult(IList<int> buildings)
        {
            int? min = null;
            
            for (var i = 0; i < buildings.Count; i++)
            {
                if (buildings[i] != 1) continue;
                
                var next = GetNext(buildings, ref i);
                var prev = GetPrev(buildings, ref i);
                var currMin = GetMinForHouse(ref prev, ref next, ref i);

                if (min == null || currMin > min)
                {
                    min = currMin;
                }
            }

            return min.Value;
        }

        private static int GetMinForHouse(ref int? prev, ref int? next, ref int i)
        {
            if (prev == null)
            {
                return next.Value - i;
            }

            if (next == null)
            {
                return i - prev.Value;
            }

            return Math.Min(next.Value - i, i - prev.Value);
        }
        
        private static int? GetNext(IList<int> buildings, ref int index)
        {
            for (var i = index; i < buildings.Count; i++)
            {
                if (buildings[i] != 2) continue;
                return i;
            }

            return null;
        }

        private static int? GetPrev(IList<int> buildings, ref int index)
        {
            for (var i = index - 1; i >= 0; i--)
            {
                if (buildings[i] != 2) continue;
                return i;
            }

            return null;
        }
    }
}