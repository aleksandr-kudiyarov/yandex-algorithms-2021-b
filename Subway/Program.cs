using System;

namespace Subway
{
    internal static class Program
    {
        private static void Main()
        {
            var line = Console.ReadLine();
            var input = line.Split(' ');

            var inputs = new Inputs
            {
                Count = int.Parse(input[0]),
                In = int.Parse(input[1]),
                Out = int.Parse(input[2])
            };

            var result = SubwayWorker.GetResult(ref inputs);
            Console.WriteLine(result);
        }
    }

    public static class SubwayWorker
    {
        public static int GetResult(ref Inputs inputs)
        {
            var diff = Math.Abs(inputs.Out - inputs.In);
            var otherDiff = inputs.Count - diff;
            var i = Math.Min(diff, otherDiff);

            return i - 1;
        }
    }

    public struct Inputs
    {
        public int Count { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
    }
}