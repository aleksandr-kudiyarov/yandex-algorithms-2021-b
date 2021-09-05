using System.Collections.Generic;
using Xunit;

namespace HousesAndShops.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { 2, 0, 1, 1, 0, 1, 0, 2, 1, 2 }, 3)]
        public void Test1(IList<int> enumerable, int expected)
        {
            var actual = HousesAndShopsWorker.GetResult(enumerable);
            Assert.Equal(expected, actual);
        }
    }
}