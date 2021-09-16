using System;
using System.Collections.Generic;
using System.Linq;

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
        public static int? GetResult(IList<int> buildings)
        {
            var dict = new Dictionary<int, int?>();
            
            int? rightShop = null;
            
            for (var i = 0; i < buildings.Count; i++)
            {
                switch (buildings[i])
                {
                    case 2:
                        rightShop = i;
                        break;
                    case 1:
                    {
                        dict.Add(i, i - rightShop);
                        break;
                    }
                }
            }

            int? leftShop = null;

            for (var i = buildings.Count - 1; i >= 0; i--)
            {
                switch (buildings[i])
                {
                    case 2:
                        leftShop = i;
                        break;
                    case 1:
                    {
                        var oldValue = dict[i];
                        var diff = leftShop - i;

                        var newValue = oldValue == null
                            ? diff
                            : diff < oldValue
                                ? diff
                                : oldValue;

                        dict[i] = newValue;
                        break;
                    }
                }
            }

            var result = dict.Values.Max();
            return result;
        }
    }
}