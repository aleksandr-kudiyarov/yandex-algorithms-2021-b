using System.IO;
using Xunit;

namespace CubicEquation.Tests
{
    public class UnitTest1
    {
        private const string Path = @"D:\Repos\Kudiyarov.YandexAlgorithms\CubicEquation.Tests\TestCases\";
        
        [Theory]
        [InlineData("1-input", "1-output")]
        [InlineData("2-input", "2-output")]
        public void Test1(string input, string output)
        {
            input = Path + input;
            output = Path + output;
            var line = File.ReadAllText(input);
            var expectedText = File.ReadAllText(output);
            var actualText = Program.GetResult(line);

            var expected = expectedText[..GetLength(expectedText)];
            var actual = actualText[..GetLength(actualText)];
            
            Assert.Equal(expected, actual);
        }

        private static int GetLength(string input)
        {
            var dotIndex = input.IndexOf('.');
            var length = dotIndex + 5;
            return length;
        }
    }
}