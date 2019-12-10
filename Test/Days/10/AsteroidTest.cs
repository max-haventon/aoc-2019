using NUnit.Framework;
using Day10;

namespace Test.Days
{
    public class AsteroidTest
    {
        [Test]
        public void DistanceTo()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(3, 4);

            Assert.AreEqual(5, asteroid1.DistanceTo(asteroid2));
        }
    }
}
