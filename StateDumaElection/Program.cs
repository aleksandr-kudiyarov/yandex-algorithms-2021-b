using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StateDumaElection
{
    internal static class Program
    {
        private static void Main()
        {
            const int totalSeats = 450;
            
            var lines = File.ReadAllLines("input.txt");
            var parties = new List<Party>(lines.Length);
            var votesSum = 0;

            foreach (var line in lines)
            {
                var party = GetParty(line);
                parties.Add(party);
                votesSum += party.Votes;
            }

            var votesForSeat = (double)votesSum / totalSeats;
            var seatsSum = 0;

            foreach (var party in parties)
            {
                var remainder = party.Votes % votesForSeat;
                var seatsForParty = (int)(party.Votes / votesForSeat);
                party.Seats = seatsForParty;
                party.Remainder = remainder;
                seatsSum += seatsForParty;
            }

            var diff = totalSeats - seatsSum;

            if (diff > 0)
            {
                var orderedParties = GetDescendingOrderedList(parties);
                
                foreach (var party in orderedParties.RepeatIndefinitely())
                {
                    party.Seats++;

                    if (--diff == 0)
                    {
                        break;
                    }
                }
            }

            var results = parties.Select(party => $"{party.Name} {party.Seats}");
            File.WriteAllLines("output.txt", results);
        }

        private static Party GetParty(string line)
        {
            line = line.Trim();
            
            var separator = line.LastIndexOf(' ');
            var name = line.Substring(0, separator);
            var votes = line.Substring(separator + 1, line.Length - name.Length - 1);
            
            var party = new Party
            {
                Name = name,
                Votes = int.Parse(votes)
            };

            return party;
        }

        private static IReadOnlyList<T> GetDescendingOrderedList<T>(IReadOnlyCollection<T> parties)
        {
            var orderedParties = parties.OrderByDescending(item => item);
            var list = new List<T>(parties.Count);

            foreach (var party in orderedParties)
            {
                list.Add(party);
            }

            return list;
        }

        private static IEnumerable<T> RepeatIndefinitely<T>(this IReadOnlyCollection<T> source)
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }
    }

    public class Party : IComparable<Party>
    {
        public string Name { get; set; }
        public int Votes { get; set; }
        public int Seats { get; set; }
        public double Remainder { get; set; }

        public int CompareTo(Party other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            var result = Remainder.CompareTo(other.Remainder);

            if (result == 0)
            {
                result = Votes.CompareTo(other.Votes);
            }

            return result;
        }
    }
}