using System;
using Xunit;

namespace SumOfThree.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(@"3
2 1 2
2 3 1
2 3 1", "0 1 1")]
        [InlineData(@"10
1 5
1 4
1 3", "-1")]
        [InlineData(@"5
4 1 2 3 4
3 5 2 1
4 5 3 2 2", "0 1 2")]
        public void Test1(string input, string expected)
        {
            var lines = input.Split(Environment.NewLine);
            var actual = Program.GetResult(lines);
            Assert.Equal(expected, actual);
        }
    }
}