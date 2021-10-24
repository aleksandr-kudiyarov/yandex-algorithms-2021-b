using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KittensFullness
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
            var catEvents = GetCatEvents(lines[1]);
            var segmentEvents = GetSegmentEvents(lines);
            
            var events = catEvents
                .Concat(segmentEvents)
                .OrderBy(e => e, Event.DefaultComparer)
                .ToList();

            var currentSegments = new List<Segment>(events.Count / 2);

            foreach (var @event in events)
            {
                switch (@event.Type)
                {
                    case EventType.Begin:
                        currentSegments.Add(@event.Parent);
                        break;
                    case EventType.End:
                        currentSegments.Remove(@event.Parent);
                        break;
                    case EventType.CatFound:
                        foreach (var segment in currentSegments)
                        {
                            segment.Cats++;
                        }
                        break;
                }
            }

            var cats = segmentEvents
                .Where(e => e.Type == EventType.Begin)
                .Select(e => e.Parent.Cats);

            var result = string.Join(" ", cats);
            return result;
        }

        private static IReadOnlyList<Event> GetSegmentEvents(IReadOnlyList<string> lines)
        {
            var segmentEvents = new List<Event>(lines.Count - 2);

            for (var i = 2; i < lines.Count; i++)
            {
                var line = lines[i].Split();

                var segment = new Segment();

                var begin = new Event
                {
                    Parent = segment,
                    Coordinate = int.Parse(line.First()),
                    Type = EventType.Begin
                };

                var end = new Event
                {
                    Parent = segment,
                    Coordinate = int.Parse(line.Last()),
                    Type = EventType.End
                };

                segmentEvents.Add(begin);
                segmentEvents.Add(end);
            }

            return segmentEvents;
        }

        private static IReadOnlyList<Event> GetCatEvents(string line)
        {
            return line
                .Split()
                .Select(GetCatEvent)
                .ToList();
        }

        private static Event GetCatEvent(string value)
        {
            return new Event
            {
                Coordinate = int.Parse(value),
                Type = EventType.CatFound
            };
        }
    }

    public class Segment
    {
        public int Cats { get; set; }
    }

    public class Event
    {
        public static IComparer<Event> DefaultComparer { get; } = new Comparer();

        public Segment Parent { get; set; }
        public int Coordinate { get; set; }
        public EventType Type { get; set; }

        private class Comparer : IComparer<Event>
        {
            public int Compare(Event x, Event y)
            {
                var coordinateComparison = x.Coordinate.CompareTo(y.Coordinate);
                
                return coordinateComparison != 0
                    ? coordinateComparison
                    : x.Type.CompareTo(y.Type);
            }
        }
    }

    public enum EventType
    {
        Begin = 0,
        CatFound = 1,
        End = 2
    }
}