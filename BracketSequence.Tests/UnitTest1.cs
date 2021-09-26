using System;
using Xunit;

namespace BracketSequence.Tests
{
    public class UnitTest1
    {
        private const string Yes = "YES";
        private const string No = "NO";

        [Theory]
        [InlineData("(())()", Yes)]
        [InlineData("(()))()", No)]
        [InlineData("((())()", No)]
        public void Test1(string input, string expected)
        {
            var actual = Program.GetResult(input);
            Assert.Equal(expected, actual);
        }
    }
}