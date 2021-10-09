using System.IO;
using Xunit;

namespace Deforestation.Tests
{
    public class UnitTest1
    {
        private const string Path = @"D:\Repos\Kudiyarov.YandexAlgorithms\Deforestation.Tests\TestCases\";
        
        [Theory]
        [InlineData("1-input", "1-output")]
        [InlineData("2-input", "2-output")]
        [InlineData("3-input", "3-output")]
        [InlineData("4-input", "4-output")]
        public void Test1(string input, string output)
        {
            input = Path + input;
            output = Path + output;
            var lines = File.ReadAllText(input);
            var expected = File.ReadAllText(output);
            var actual = Program.GetResult(lines);
            Assert.Equal(expected, actual);
        }
    }
}