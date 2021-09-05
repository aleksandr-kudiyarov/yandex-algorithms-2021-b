using System.Collections.Generic;
using Xunit;

namespace MaxCount.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { 1, 7, 9, 0 }, 1)]
        [InlineData(new[] { 1, 3, 3, 1, 0 }, 2)]
        [InlineData(new[] { -1 }, 1)]
        [InlineData(new int[] {  }, 0)]
        public void Test1(IEnumerable<int> enumerable, int expected)
        {
            var actual = MaxCountWorker.GetResult(enumerable);
            Assert.Equal(expected, actual);
        }
    }
}