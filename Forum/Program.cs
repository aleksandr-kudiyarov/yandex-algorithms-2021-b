using System.Collections.Generic;
using System.IO;

namespace Forum
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var linksToTopics = new List<Topic>();
            Topic result = null;

            for (var i = 1; i < lines.Length;)
            {
                var line = lines[i];
                
                if (line == "0")
                {
                    var topic = new Topic
                    {
                        Name = lines[i + 1],
                        Messages = 1
                    };
                    
                    linksToTopics.Add(topic);

                    if (result == null)
                    {
                        result = topic;
                    }
                    
                    i += 3;
                }
                else
                {
                    var id = int.Parse(line);
                    var topic = linksToTopics[id - 1];

                    topic.Messages++;
                    linksToTopics.Add(topic);

                    if (topic.Messages > result.Messages)
                    {
                        result = topic;
                    }
                    
                    i += 2;
                }
            }

            File.WriteAllText("output.txt", result.Name);
        }
    }

    public class Topic
    {
        public string Name { get; set; }
        public int Messages { get; set; }
    }
}