using NUnit.Framework;
using _8_2;

namespace Test.Days
{
    public class Day8
    {
        [Test]
        public void Part1()
        {
            Solver s = new Solver();

            Assert.AreEqual(1950, s.Solve8_1());
        }

        [Test]
        public void Part2()
        {
            Solver s = new Solver();

            s.Solve8_2();
        }
    }
}
