using NUnit.Framework;

namespace Subway.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(100, 5,6, 0)]
        [TestCase(100, 6,5, 0)]
        [TestCase(10, 1,9, 1)]
        [TestCase(10, 9,1, 1)]
        public void Test(int count, int @in, int @out, int expected)
        {
            var inputs = new Inputs
            {
                Count = count,
                In = @in,
                Out = @out
            };

            var actual = SubwayWorker.GetResult(ref inputs);
            Assert.AreEqual(actual, expected);
        }
    }
}