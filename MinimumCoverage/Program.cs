using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinimumCoverage
{
    public static class Program
    {
        private const string NoSolution = "No solution";
        
        private static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var result = GetResult(input);
            File.WriteAllText("output.txt", result);
        }

        public static string GetResult(IReadOnlyList<string> lines)
        {
            var m = int.Parse(lines[0].Trim());
            var events = GetEvents(lines, m);
            
            var orderedEvents = events
                .OrderBy(segment => segment, Event.DefaultComparer)
                .ToList();
            
            if (orderedEvents.Count == 0 || orderedEvents[0].Coordinate > 0)
            {
                return NoSolution;
            }

            var currentSegment = orderedEvents[0].Parent;
            var longestSegment = currentSegment;
            
            var resultList = new List<Segment>(); 

            foreach (var @event in orderedEvents)
            {
                var segment = @event.Parent;

                if (@event.Type == EventType.Begin)
                {
                    if (segment.Begin.Coordinate > currentSegment.End.Coordinate)
                    {
                        return NoSolution;
                    }

                    if (segment.End.Coordinate > longestSegment.End.Coordinate)
                    {
                        longestSegment = segment;
                    }
                }

                else if (@event.Type == EventType.End)
                {
                    if (ReferenceEquals(segment, currentSegment))
                    {
                        resultList.Add(segment);
                        currentSegment = longestSegment;
                        
                        if (longestSegment.End.Coordinate >= m)
                        {
                            if (!ReferenceEquals(segment, longestSegment))
                            {
                                resultList.Add(longestSegment);   
                            }
                            
                            break;
                        }
                    }
                }
            }

            var lastEvent = resultList.LastOrDefault();

            if (lastEvent != null && lastEvent.End.Coordinate < m)
            {
                return NoSolution;
            }

            return $"{resultList.Count}" +
                   $"{Environment.NewLine}" +
                   $"{string.Join(Environment.NewLine, resultList.Select(segment => $"{segment.Begin.Coordinate} {segment.End.Coordinate}"))}";
        }

        private static IReadOnlyList<Event> GetEvents(IReadOnlyList<string> lines, int m)
        {
            var events = new List<Event>(lines.Count * 2);
            var zeroSegments = new List<Segment>();

            for (var i = 1; i < lines.Count; i++)
            {
                var line = lines[i];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                
                var coordinates = line.Split();

                var beginCoordinate = int.Parse(coordinates[0]);

                if (beginCoordinate >= m)
                {
                    continue;
                }
                
                var endCoordinate = int.Parse(coordinates[coordinates.Length - 1]);

                if (endCoordinate <= 0)
                {
                    continue;
                }

                var segment = new Segment();

                segment.Begin = new Event
                {
                    Parent = segment,
                    Coordinate = beginCoordinate,
                    Type = EventType.Begin
                };
                
                segment.End = new Event
                {
                    Parent = segment,
                    Coordinate = endCoordinate,
                    Type = EventType.End
                };
                
                if (segment.Begin.Coordinate <= 0)
                {
                    zeroSegments.Add(segment);
                }
                else
                {
                    events.Add(segment.Begin);
                    events.Add(segment.End);  
                }
            }
            
            var longestZeroSegment = zeroSegments.FirstOrDefault();

            if (longestZeroSegment != null)
            {
                foreach (var segment in zeroSegments.Where(segment => segment.End.Coordinate > longestZeroSegment.End.Coordinate))
                {
                    longestZeroSegment = segment;
                }

                events.Add(longestZeroSegment.Begin);
                events.Add(longestZeroSegment.End);
            }

            return events;
        }
    }

    public class Segment
    {
        public Event Begin { get; set; }
        public Event End { get; set; }
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
                var result = x.Coordinate.CompareTo(y.Coordinate);

                if (result == 0)
                {
                    result = x.Type.CompareTo(y.Type);
                }

                return result;
            }
        }
    }

    public enum EventType
    {
        Begin = 0,
        End = 1
    }
}