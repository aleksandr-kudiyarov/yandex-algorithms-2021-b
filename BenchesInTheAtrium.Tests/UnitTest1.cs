using System.Collections.Generic;
using Xunit;

namespace BenchesInTheAtrium.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(5, new[] { 0, 2 }, new[] { 2 })]
        [InlineData(13, new[] { 1, 4, 8, 11 }, new[] { 4, 8 })]
        [InlineData(14, new[] { 1, 6, 8, 11, 12, 13 }, new[] { 6, 8 })]
        [InlineData(4, new[] { 0, 1, 2, 3 }, new[] { 1, 2 })]
        public void Test1(int benchLength, IReadOnlyList<int> coordinates, IReadOnlyList<int> expected)
        {
            var input = new Input
            {
                BenchLength = benchLength,
                CubesCoordinates = coordinates
            };

            var actual = BenchesInTheAtriumWorker.GetResult(ref input);
            Assert.Equal(expected, actual);
        }
    }
}