using System;
using System.Linq;

namespace TraingleAndPoint
{
    internal static class Program
    {
        private static void Main()
        {
            var dLine = Console.ReadLine();
            var pointLine = Console.ReadLine();
            var pointSplitted = pointLine.Split();
            
            var d = int.Parse(dLine);
            
            var point = new Point
            {
                X = int.Parse(pointSplitted[0]),
                Y = int.Parse(pointSplitted[1])
            };

            var worker = new TraingleAndPointWorker(ref d);
            var result = worker.GetResult(ref point);
            Console.WriteLine(result);
        }
    }

    public class TraingleAndPointWorker
    {
        private readonly Traingle _traingle;
        private readonly double _cathetus;

        public TraingleAndPointWorker(ref int cathetus)
        {
            var a = new Point { X = 0, Y = 0 };
            var b = new Point { X = cathetus, Y = 0 };
            var c = new Point { X = 0, Y = cathetus };

            _traingle = new Traingle
            {
                A = a,
                B = b,
                C = c
            };

            _cathetus = cathetus;
        }

        public int GetResult(ref Point point)
        {
            var diffA = new Diff { Id = 1, Value = GetDiff(_traingle.A, point) };
            var diffB = new Diff { Id = 2, Value = GetDiff(_traingle.B, point) };
            var diffC = new Diff { Id = 3, Value = GetDiff(_traingle.C, point) };

            var sum = diffA.Value + diffB.Value + diffC.Value;

            if (IsInside(ref sum))
            {
                return 0;
            }

            var diffs = new[] { diffA, diffB, diffC };
            var min = diffs.Min();
            return min.Id;
        }

        private static double GetDiff(Point x, Point y)
        {
            var z = new Point
            {
                X = Math.Abs(x.X - y.X),
                Y = Math.Abs(x.Y - y.Y)
            };

            var d = z.X * z.X + z.Y * z.Y;
            var result = Math.Sqrt(d);
            return result;
        }

        private bool IsInside(ref double sum)
        {
            var maxSum = GetMaxSum();
            return sum.LessThanOrEqual(maxSum);
        }

        private double GetMaxSum()
        {
            return _cathetus
                   + Math.Sqrt(_cathetus * _cathetus + _cathetus * _cathetus);
        }
    }

    public struct Traingle
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public struct Diff : IComparable<Diff>
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public int CompareTo(Diff other)
        {
            var result = MathHelper.CompareTo(Value, other.Value);

            if (result == 0)
            {
                result = Id.CompareTo(other.Id);
            }

            return result;
        }
    }
    
    public static class MathHelper
    {
        private const double Tolerance = 0.000000001;
        
        public static int CompareTo(double x, double y)
        {
            if (x.LessThan(y))
            {
                return -1;
            }

            if (x.MoreThan(y))
            {
                return 1;
            }
            
            return 0;
        }
        
        public static bool LessThanOrEqual(this double x, double y)
        {
            var diff = x - y;
            var result = LessThanTolerance(diff) || diff < 0;
            return result;
        }

        private static bool LessThan(this double x, double y)
        {
            var diff = x - y;
            var result = !LessThanTolerance(diff) && diff < 0;
            return result;
        }

        private static bool MoreThan(this double x, double y)
        {
            var diff = x - y;
            var result = !LessThanTolerance(diff) && diff > 0;
            return result;
        }

        private static bool LessThanTolerance(double value)
        {
            var abs = Math.Abs(value);
            var result = abs < Tolerance;
            return result;
        }
    }
}