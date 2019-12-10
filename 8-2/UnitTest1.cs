using NUnit.Framework;

namespace _8_2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test8_1()
        {
            Solver s = new Solver();
            s.Solve8_1();
            Assert.Pass();
        }
    }
}