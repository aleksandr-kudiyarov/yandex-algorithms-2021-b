using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ColoringLine
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
            var events = new List<YEvent>((lines.Length - 1) * 2);
            
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim().Split();

                var begin = new YEvent
                {
                    Coordinate = int.Parse(line[0]),
                    Type = YEventType.Begin
                };
                
                var end = new YEvent
                {
                    Coordinate = int.Parse(line[1]),
                    Type = YEventType.End
                };
                
                events.Add(begin);
                events.Add(end);
            }

            var sortedEvents = events.OrderBy(value => value);

            var counter = 0;
            var length = 0;
            var l = 0;

            foreach (var yEvent in sortedEvents)
            {
                if (counter == 0)
                {
                    l = yEvent.Coordinate;
                }
                
                if (yEvent.Type == YEventType.Begin)
                {
                    counter++;
                }
                else
                {
                    counter--;
                }

                if (counter == 0)
                {
                    length += yEvent.Coordinate - l;
                }
            }
            
            return length.ToString();
        }
    }

    public struct YEvent : IComparable<YEvent>
    {
        public int Coordinate { get; set; }
        public YEventType Type { get; set; }
        
        public int CompareTo(YEvent other)
        {
            var coordinateComparison = Coordinate.CompareTo(other.Coordinate);
            
            return coordinateComparison != 0
                ? coordinateComparison
                : Type.CompareTo(other.Type);
        }
    }

    public enum YEventType
    {
        Begin = 0,
        End = 1
    }
}