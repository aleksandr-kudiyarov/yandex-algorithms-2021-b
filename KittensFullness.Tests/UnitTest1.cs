using System;
using System.Diagnostics;
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
        
        [Theory]
        [InlineData("1-input")]
        public void Test2(string input)
        {
            input = Path + input;
            var lines = File.ReadAllLines(input);
            var stopwatch = Stopwatch.StartNew();
            _ = Program.GetResult(lines);
            stopwatch.Stop();
            var isInTime = stopwatch.Elapsed < TimeSpan.FromSeconds(2);
            Assert.True(isInTime);
        }
    }
}