using System.IO;
using Xunit;

namespace KittensFullness.Tests
{
    public class UnitTest1
    {
        private const string Path = @"D:\Repos\Kudiyarov.YandexAlgorithms\KittensFullness.Tests\TestCases\";
        
        [Theory]
        [InlineData("1-input", "1-output")]
        public void Test1(string input, string output)
        {
            input = Path + input;
            output = Path + output;
            var lines = File.ReadAllLines(input);
            var expected = File.ReadAllText(output);
            var actual = Program.GetResult(lines);
            Assert.Equal(expected, actual);
        }
    }
}