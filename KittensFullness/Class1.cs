using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KittensFullness
{
    public static class Program
    {
        private static readonly char[] Separators = { ' ' }; 
        
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(IReadOnlyList<string> lines)
        {
            var catsDictionary = GetCatDictionary(lines[1]);
            var catEvents = GetCatEvents(catsDictionary);
            var segmentEvents = GetSegmentEvents(lines);
            
            var events = catEvents
                .Concat(segmentEvents)
                .OrderBy(e => e, Event.DefaultComparer);

            var catsNow = 0;

            foreach (var @event in events)
            {
                switch (@event.Type)
                {
                    case EventType.Begin:
                        @event.Parent.CatsAtBegin = catsNow;
                        break;
                    case EventType.End:
                        @event.Parent.CatsAtEnd = catsNow;
                        break;
                    case EventType.CatFound:
                        catsNow = catsDictionary[@event.Coordinate];
                        break;
                }
            }

            var cats = segmentEvents
                .Where(e => e.Type == EventType.Begin)
                .Select(e => e.Parent.Cats);

            var result = string.Join(" ", cats);
            return result;
        }

        private static IEnumerable<Event> GetCatEvents(IReadOnlyDictionary<int, int> catsDictionary)
        {
            var events = catsDictionary.Keys
                .Select(key =>
                    new Event
                    {
                        Coordinate = key,
                        Type = EventType.CatFound
                    });
            
            return events;
        }

        private static IReadOnlyList<Event> GetSegmentEvents(IReadOnlyList<string> lines)
        {
            var segmentEvents = new List<Event>(lines.Count - 2);

            for (var i = 2; i < lines.Count; i++)
            {
                var line = lines[i].Split(Separators, StringSplitOptions.RemoveEmptyEntries);

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

        private static IReadOnlyDictionary<int, int> GetCatDictionary(string line)
        {
            var count = 0;
            var splittedLine = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
                
            var coordinates = splittedLine
                .Select(int.Parse)
                .OrderBy(i => i);
            
            var dictionary = new Dictionary<int, int>(splittedLine.Length);

            foreach (var coordinate in coordinates)
            {
                count++;
                dictionary[coordinate] = count;
            }

            return dictionary;
        }
    }

    public class Segment
    {
        public int Cats => CatsAtEnd - CatsAtBegin;

        public int CatsAtBegin { get; set; }
        public int CatsAtEnd { get; set; }
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