using System.Collections.Generic;
using Xunit;

namespace A_Intersect.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { 1, 3, 2 }, new[] { 4, 3, 2 }, 2)]
        [InlineData(new[] { 1, 2, 6, 4, 5, 7 }, new[] { 10, 2, 3, 4, 8 }, 2)]
        [InlineData(new[] { 1, 7, 3, 8, 10, 2, 5 }, new[] { 6, 5, 2, 8, 4, 3, 7 }, 5)]
        public void Test1(IEnumerable<int> e1, IEnumerable<int> e2, int expected)
        {
            var actual = IntersectWorker.GetResult(e1, e2);
            Assert.Equal(expected, actual);
        }
    }
}