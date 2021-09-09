using System.Collections.Generic;
using UniqueElements;
using Xunit;

namespace C_UniqueElements.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { 1, 2, 2, 3, 3, 3 }, new[] { 1 })]
        [InlineData(new[] { 4, 3, 5, 2, 5, 1, 3, 5 }, new[] { 4, 2, 1 })]
        public void Test1(IReadOnlyList<int> input, IReadOnlyList<int> expected)
        {
            var actual = UniqueElementsWorker.GetResult(input);
            Assert.Equal(expected, actual);
        }
    }
}