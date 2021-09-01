using NUnit.Framework;

namespace Dates.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(1,2,2003, false)]
        [TestCase(1,2003,2, false)]
        [TestCase(2003,1,2, false)]
        [TestCase(2,29,2008, true)]
        [TestCase(2,2008, 29, true)]
        [TestCase(2008,29, 2, true)]
        [TestCase(3,3, 2067, true)]
        public void Test1(int a, int b, int c, bool expected)
        {
            var inputs = new DatesInputs
            {
                A = a,
                B = b,
                C = c
            };

            var actual = DatesWorker.GetResult(ref inputs);
            Assert.AreEqual(expected, actual);
        }
    }
}