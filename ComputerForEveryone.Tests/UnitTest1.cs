using System;
using Xunit;

namespace ComputerForEveryone.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(@"1 1
1
2", @"1
1")]
        [InlineData(@"1 1
1
1", @"0
0")]        [InlineData(@"2 2
1 2
2 3", @"2
1 2")]
        public void Test1(string input, string expected)
        {
            var lines = input.Split(Environment.NewLine);
            var actual = Program.GetResult(lines);
            Assert.Equal(expected, actual);
        }
    }
}