using NUnit.Framework;

namespace Interactor.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(0, 0, 0, 0)]
        [TestCase(-1, 0, 1, 3)]
        [TestCase(42, 1, 6, 6)]
        [TestCase(44, 7, 4, 1)]
        [TestCase(1, 4, 0, 3)]
        [TestCase(-3, 2, 4, 2)]
        public void Test1(int r, int i, int t, int expected)
        {
            var param = new Inputs
            {
                InteractorVerdict = i,
                CheckerVerdict = t,
                TaskCode = r
            };

            var actual = InteractorWorker.GetResult(ref param);
            Assert.AreEqual(expected, actual);
        }
    }
}