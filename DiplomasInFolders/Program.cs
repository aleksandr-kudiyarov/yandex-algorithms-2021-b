using System;
using System.Collections.Generic;

namespace DiplomasInFolders
{
    internal static class Program
    {
        private static void Main()
        {
            Console.ReadLine();
            var line = Console.ReadLine().Trim().Split(' ');
            var folders = new List<int>(line.Length);
            
            foreach (var value in line)
            {
                var intValue = int.Parse(value);
                folders.Add(intValue);
            }

            var result = DiplomasInFoldersWorker.GetResult(folders);
            Console.WriteLine(result);
        }
    }

    public static class DiplomasInFoldersWorker
    {
        public static int GetResult(IReadOnlyList<int> folders)
        {
            var max = 0;
            var count = 0;
            
            foreach (var folder in folders)
            {
                if (folder > max)
                {
                    max = folder;
                }

                count += folder;
            }

            count -= max;

            return count;
        }
    }
}