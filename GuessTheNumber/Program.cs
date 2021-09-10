using System;
using System.Collections.Generic;

namespace GuessTheNumber
{
    internal static class Program
    {
        public static void Main()
        {
            var range = int.Parse(Console.ReadLine().Trim());
            var set = new HashSet<int>();
            
            for (var i = 1; i <= range; i++)
            {
                set.Add(i);
            }

            string input;
            
            while ((input = Console.ReadLine()) != "HELP")
            {
                var questions = input.Trim().Split().ToInt();
                var answer = Console.ReadLine();

                if (answer == "NO")
                {
                    foreach (var question in questions)
                    {
                        set.Remove(question);
                    }
                }
                else
                {
                    set.IntersectWith(questions);
                }
            }

            Console.WriteLine(string.Join(" ", set));
        }
        
        private static IEnumerable<int> ToInt(this IReadOnlyCollection<string> source)
        {
            var list = new List<int>(source.Count);

            foreach (var s in source)
            {
                var i = int.Parse(s);
                list.Add(i);
            }

            return list;
        }
    }
}