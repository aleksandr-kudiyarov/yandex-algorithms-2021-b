using System.IO;
using System.Text;
using Xunit;

namespace MinimumCoverage.Tests
{
    public class UnitTest1
    {
        private const string Path = @"D:\Repos\Kudiyarov.YandexAlgorithms\MinimumCoverage.Tests\TestCases\";
        
        [Theory]
        [InlineData("1-input", "1-output")]
        [InlineData("2-input", "2-output")]
        [InlineData("4-input", "4-output")]
        [InlineData("5-input", "5-output")]
        [InlineData("6-input", "6-output")]
        public void Test1(string input, string output)
        {
            input = Path + input;
            output = Path + output;
            var lines = File.ReadAllLines(input);
            var expected = File.ReadAllText(output);
            var actual = Program.GetResult(lines);
            expected = NormalizeLineBreaks(expected);
            
            Assert.Equal(expected, actual);
        }

        private static string NormalizeLineBreaks(string input)
        {
            var builder = new StringBuilder((int) (input.Length * 1.1));

            var lastWasCr = false;

            foreach (var c in input)
            {
                if (lastWasCr)
                {
                    lastWasCr = false;
                    
                    if (c == '\n')
                    {
                        continue;
                    }
                }
                switch (c)
                {
                    case '\r':
                        builder.Append("\r\n");
                        lastWasCr = true;
                        break;
                    case '\n':
                        builder.Append("\r\n");
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }
            
            return builder.ToString();
        }
    }
}