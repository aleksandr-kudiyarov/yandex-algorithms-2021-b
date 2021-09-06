using System.Collections.Generic;
using Xunit;

namespace DiplomasInFolders.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] {2, 1}, 1)]
        public void Test1(IReadOnlyList<int> input, int expected)
        {
            var actual = DiplomasInFoldersWorker.GetResult(input);
            Assert.Equal(expected, actual);
        }
    }
}