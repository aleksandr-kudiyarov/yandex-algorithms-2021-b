using System;
using System.IO;
using System.Linq;

namespace ComputerForEveryone
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(string[] lines)
        {
            var groups =  lines[1].Trim().Split().Select((s, i) => new Group { Count = int.Parse(s) + 1, Index = i }).ToList();
            var audiences = lines[2].Trim().Split().Select((value, index) => new Audience {Size = int.Parse(value), Index = index + 1});

            var sortedGroups = groups.OrderByDescending(i => i.Count).ToList();
            var sortedAudiences = audiences.OrderByDescending(i => i.Size).ToList();

            var count = 0;
            
            foreach (var group in sortedGroups)
            {
                var audience = sortedAudiences[count];

                if (group.Count <= audience.Size)
                {
                    count++;
                    group.Audience = audience;
                }
            }
            
            var groupsByAudience = string.Join(" ", groups.Select(group => group.Audience.Index));
            var result = $"{count.ToString()}{Environment.NewLine}{groupsByAudience}";
            return result;
        }
    }

    public class Group
    {
        public int Count { get; set; }
        public int Index { get; set; }
        public Audience Audience { get; set; }
    }

    public struct Audience
    {
        public int Size { get; set; }
        public int Index { get; set; }
    }
}