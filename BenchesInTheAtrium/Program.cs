using System;
using System.Collections.Generic;
using System.Linq;

namespace BenchesInTheAtrium
{
    internal static class Program
    {
        private static void Main()
        {
            var firstLine = Console.ReadLine().Trim().Split();
            var secondLine = Console.ReadLine().Trim().Split();

            var coordinates = new List<int>(secondLine.Length);

            foreach (var value in secondLine)
            {
                var intValue = int.Parse(value);
                coordinates.Add(intValue);
            }

            var input = new Input
            {
                BenchLength = int.Parse(firstLine[0]),
                CubesCoordinates = coordinates
            };

            var result = BenchesInTheAtriumWorker.GetResult(ref input);
            var resultAsString = result.Select(i => i.ToString());
            var consoleResult = string.Join(" ", resultAsString);

            Console.WriteLine(consoleResult);
        }
    }

    public static class BenchesInTheAtriumWorker
    {
        public static IReadOnlyList<int> GetResult(ref Input input)
        {
            var list = new List<int>(input.CubesCoordinates.Count);

            var middle = input.BenchLength / 2;
            var isBenchEven = input.BenchLength % 2 == 0;

            int? left = null;

            foreach (var coordinate in input.CubesCoordinates)
            {
                if (coordinate == middle && !isBenchEven)
                {
                    list.Add(coordinate);
                    break;
                }

                if (coordinate < middle)
                {
                    left = coordinate;
                }
                else if (coordinate >= middle)
                {
                    if (left.HasValue)
                    {
                        list.Add(left.Value);
                    }

                    list.Add(coordinate);
                    break;
                }
            }

            return list;
        }
    }

    public struct Input
    {
        public int BenchLength { get; set; }
        public IReadOnlyList<int> CubesCoordinates { get; set; }
    }
}