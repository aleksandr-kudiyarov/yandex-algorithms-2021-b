﻿using System.IO;
using System.Linq;

namespace Deforestation
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllText("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(string input)
        {
            var worker = new DeforestationWorker(input);
            var result = LeftBinarySearch(worker);
            return result.ToString();
        }

        private static int LeftBinarySearch(DeforestationWorker worker)
        {
            var l = 0;
            var r = 1000000;
            
            while (l < r)
            {
                var m = (l + r) / 2;
                var value = worker.Calc(m);

                if (value > 0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }

            return l;
        }
    }

    public class DeforestationWorker
    {
        private readonly long _trees;
        private readonly long _dmitryTreesPerDay;
        private readonly long _dmitryRestDay;
        private readonly long _fedorTreesPerDay;
        private readonly long _fedorRestDay;

        public DeforestationWorker(string input)
        {
            var values = input
                .Trim()
                .Split()
                .Select(long.Parse)
                .ToList();
            
            _dmitryTreesPerDay = values[0];
            _dmitryRestDay = values[1];
            _fedorTreesPerDay = values[2];
            _fedorRestDay = values[3];
            _trees = values[4];
        }

        public long Calc(long day)
        {
            var daysDmitryWorks = day - day / _dmitryRestDay;
            var daysFedorWorks = day - day / _fedorRestDay;

            var result = daysDmitryWorks * _dmitryTreesPerDay
                         + daysFedorWorks * _fedorTreesPerDay;

            return _trees - result;
        }
    }
}