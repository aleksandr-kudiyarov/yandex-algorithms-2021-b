using Xunit;

namespace Palindrome.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("a", 0)]
        [InlineData("ab", 1)]
        [InlineData("aa", 0)]
        [InlineData("cognitive", 4)]
        public void Test1(string input, int expected)
        {
            var actual = PalindromeWorker.GetResult(input);
            Assert.Equal(expected, actual);
        }
    }
}