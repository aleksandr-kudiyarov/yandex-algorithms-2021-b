using NUnit.Framework;

namespace SchoolBuilding.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(new double[] { 1, 2, 3, 4 }, 3)]
        [TestCase(new double[] { -1, 0, 1 }, 0)]
        [TestCase(new double[] { -9, -1, 8, 9, 10 }, 8)]
        [TestCase(new double[] { 0, 10, 11 }, 10)]
        public void Test1(double[] coordinates, int expected)
        {
            var actual = SchoolBuildingWorker.GetMedian(coordinates);
            Assert.AreEqual(expected, actual);
        }
    }
}