using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Forum
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var messagesCounter = 0;
            var topics = new List<Topic>();
            var messages = new Dictionary<int, Message>();

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
                    
                    var message = new Message
                    {
                        Id = ++messagesCounter,
                        Topic = topic
                    };
                    
                    topics.Add(topic);
                    messages.Add(message.Id, message);
                    
                    i += 3;
                }
                else
                {
                    var id = int.Parse(line);
                    var topic = messages[id].Topic;
                    
                    var message = new Message
                    {
                        Id = ++messagesCounter,
                        Topic = topic
                    };
                    
                    topic.Messages++;
                    messages.Add(message.Id, message);
                    
                    i += 2;
                }
            }

            var result = topics.First();

            for (var i = 1; i < topics.Count; i++)
            {
                var topic = topics[i];

                if (topic.Messages > result.Messages)
                {
                    result = topic;
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

    public class Message
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
    }
}