using System;

namespace Dates
{
    internal static class Program
    {
        private static void Main()
        {
            var line = Console.ReadLine();
            var input = line.Split(' ');

            var inputs = new DatesInputs
            {
                A = int.Parse(input[0]),
                B = int.Parse(input[1]),
                C = int.Parse(input[2])
            };

            var result = DatesWorker.GetResult(ref inputs);
            Console.WriteLine(result ? 1 : 0);
        }
    }

    public static class DatesWorker
    {
        public static bool GetResult(ref DatesInputs inputs)
        {
            if (inputs.A >= 1970)
            {
                return GetResult(inputs.B, inputs.C);
            }

            if (inputs.B >= 1970)
            {
                return GetResult(inputs.A, inputs.C);
            }

            return GetResult(inputs.A, inputs.B);
        }

        private static bool GetResult(int a, int b)
        {
            return a == b
                   || a > 12
                   || b > 12;
        }
    }

    public struct DatesInputs
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }
}