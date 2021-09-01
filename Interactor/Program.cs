using System;

namespace Interactor
{
    internal static class Program
    {
        private static void Main()
        {
            var r = Console.ReadLine();
            var i = Console.ReadLine();
            var c = Console.ReadLine();

            var results = new Inputs
            {
                InteractorVerdict = int.Parse(i),
                CheckerVerdict = int.Parse(c),
                TaskCode = int.Parse(r)
            };

            var finalResult = InteractorWorker.GetResult(ref results);

            Console.WriteLine(finalResult);
        }
    }

    public static class InteractorWorker
    {
        public static int GetResult(ref Inputs inputs)
        {
            switch (inputs.InteractorVerdict)
            {
                case 0:
                    return inputs.TaskCode != 0
                        ? 3
                        : inputs.CheckerVerdict;
                case 1:
                    return inputs.CheckerVerdict;
                case 4:
                    return inputs.TaskCode != 0
                        ? 3
                        : 4;
                case 6:
                    return 0;
                case 7:
                    return 1;
                default:
                    return inputs.InteractorVerdict;
            }
        }
    }

    public struct Inputs
    {
        public int InteractorVerdict { get; set; }
        public int CheckerVerdict { get; set; }
        public int TaskCode { get; set; }
    }
}