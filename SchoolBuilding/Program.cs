using System;
using System.Collections.Generic;

namespace SchoolBuilding
{
    internal static class Program
    {
        private static void Main()
        {
            Console.ReadLine();
            var coordinatesLine = Console.ReadLine();
            var coordinatesSplitted = coordinatesLine.Split(' ');

            var coordinates = new List<double>(coordinatesSplitted.Length);

            foreach (var value in coordinatesSplitted)
            {
                var coordinate = double.Parse(value);
                coordinates.Add(coordinate);
            }

            var result = SchoolBuildingWorker.GetMedian(coordinates);
            Console.WriteLine(result);
        }
    }

    public static class SchoolBuildingWorker
    {
        public static double GetMedian(IReadOnlyList<double> sourceNumbers)
        {
            var size = sourceNumbers.Count;
            var mid = size / 2;
            var isCountOdd = size % 2 != 0;

            var median = isCountOdd
                ? sourceNumbers[mid]
                : (sourceNumbers[mid] + sourceNumbers[mid - 1]) / 2;

            return Math.Round(median, MidpointRounding.AwayFromZero);
        }
    }
}