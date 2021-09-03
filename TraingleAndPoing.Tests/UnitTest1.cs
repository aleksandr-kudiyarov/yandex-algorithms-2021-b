using NUnit.Framework;
using TraingleAndPoint;

namespace TraingleAndPoing.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(5,1,1,0)]
        [TestCase(3,-1,-1,1)]
        [TestCase(4,4,4,2)]
        [TestCase(4,2,2,0)]
        [TestCase(1,0,1,0)]
        public void Test1(int d, int x, int y, int expected)
        {
            var point = new Point
            {
                X = x,
                Y = y
            };

            var worker = new TraingleAndPointWorker(ref d); 
            var actual = worker.GetResult(ref point);
            Assert.AreEqual(expected, actual);
        }
    }
}