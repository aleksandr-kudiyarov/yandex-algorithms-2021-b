using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Customs
{
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(IReadOnlyList<string> lines)
        {
            var size = (lines.Count - 1) * 2;
            var events = new List<Event>(size);
            
            for (var i = 1; i < lines.Count; i++)
            {
                var line = lines[i].Trim().Split();
                var beginTime = int.Parse(line[0]);
                var endTime = beginTime + int.Parse(line[1]);  

                var begin = new Event
                {
                    Time = beginTime,
                    Type = EventType.Begin
                };
                
                var end = new Event
                {
                    Time = endTime,
                    Type = EventType.End
                };
                
                events.Add(begin);
                events.Add(end);
            }

            var sortedEvents = events.OrderBy(i => i, Event.TimeTypeComparer);

            var counter = 0;
            var max = 0;

            foreach (var @event in sortedEvents)
            {
                if (@event.Type == EventType.Begin)
                {
                    counter++;
                }
                else
                {
                    counter--;
                }

                if (counter > max)
                {
                    max = counter;
                }
            }

            return max.ToString();
        }
    }

    public struct Event
    {
        public static IComparer<Event> TimeTypeComparer { get; } = new EventComparer();

        public int Time { get; set; }
        public EventType Type { get; set; }
        
        private sealed class EventComparer : IComparer<Event>
        {
            public int Compare(Event x, Event y)
            {
                var timeComparison = x.Time.CompareTo(y.Time);
                
                return timeComparison != 0
                    ? timeComparison
                    : x.Type.CompareTo(y.Type);
            }
        }
    }

    public enum EventType
    {
        End = 0,
        Begin = 1
    }
}