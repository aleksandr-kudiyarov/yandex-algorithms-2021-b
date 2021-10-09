using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CubicEquation
{
    public static class Program
    {
        private const double Tolerance = 0.000001;
        
        private static void Main()
        {
            var input = File.ReadAllText("cubroot.in");
            var result = GetResult(input);
            File.WriteAllText("cubroot.out", result);
        }

        public static string GetResult(string input)
        {
            var values = input
                .Trim()
                .Split()
                .Select(int.Parse)
                .ToList();

            var worker = new CubicEquationWorker(values);
            
            var result = worker.A > 0
                ? LeftBinarySearch(worker)
                : RightBinarySearch(worker);
            
            return result.ToString(CultureInfo.InvariantCulture);
        }

        private static double LeftBinarySearch(CubicEquationWorker worker)
        {
            var l = -10000d;
            var r = 10000d;
            
            while (l.LessThan(r))
            {
                var m = (l + r) / 2;

                var calcResult = worker.Calc(m);

                if (calcResult < 0)
                {
                    l = m + Tolerance;
                }
                else
                {
                    r = m;
                }
            }

            return l;
        }
        
        private static double RightBinarySearch(CubicEquationWorker worker)
        {
            var l = -1000d;
            var r = 1000d;
            
            while (l.LessThan(r))
            {
                var m = (l + r + Tolerance) / 2;

                var calcResult = worker.Calc(m);

                if (calcResult < 0)
                {
                    r = m - Tolerance;
                }
                else
                {
                    l = m;
                }
            }

            return l;
        }
    }

    public class CubicEquationWorker
    {
        public int A { get; }
        private readonly int _b;
        private readonly int _c;
        private readonly int _d;
        
        
        public CubicEquationWorker(IReadOnlyList<int> values)
        {
            A = values[0];
            _b = values[1];
            _c = values[2];
            _d = values[3];
        }
        
        public double Calc(double x)
        {
            var result = 0
                + A * Math.Pow(x, 3)
                + _b * Math.Pow(x, 2)
                + _c * Math.Pow(x, 1)
                + _d * Math.Pow(x, 0);
            
            return result;
        }
    }
    
    public static class DoubleExtensions
    {
        private const double Tolerance = 0.000000001;

        public static bool LessThan(this double x, double y)
        {
            var diff = x - y;
            var result = diff < 0 && !LessThanTolerance(diff);
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