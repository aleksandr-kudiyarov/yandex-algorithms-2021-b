using System.Collections.Generic;
using Xunit;

namespace WasItNumberBefore.Tests
{
    public class UnitTest1
    {
        private const string Yes = "YES";
        private const string No = "NO";

        [Theory]
        [InlineData(new[] { 1, 2, 3, 2, 3, 4 }, new[] { No, No, No, Yes, Yes, No })]
        public void Test1(IReadOnlyCollection<int> collection, IReadOnlyCollection<string> expected)
        {
            var actual = WasItNumberBeforeWorker.GetResult(collection);
            Assert.Equal(expected, actual);
        }
    }
}