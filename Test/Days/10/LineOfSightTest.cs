using NUnit.Framework;
using System.Collections.Generic;
using Day10;

namespace Test.Days
{
    public class LineOfSightTest
    {
        [Test]
        public void IsBlockedBy_1()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 1);
            Asteroid asteroid3 = new Asteroid(2, 2);
            Asteroid asteroid4 = new Asteroid(7, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsTrue(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsBlockedBy_2()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 1);
            Asteroid asteroid3 = new Asteroid(2, 2);
            Asteroid asteroid4 = new Asteroid(7, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsBlockedBy_3()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 1);
            Asteroid asteroid3 = new Asteroid(2, 2);
            Asteroid asteroid4 = new Asteroid(7, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid3, asteroid1);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsTrue(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsBlockedBy_4()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 1);
            Asteroid asteroid3 = new Asteroid(2, 2);
            Asteroid asteroid4 = new Asteroid(7, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid4);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsBlockedBy_5()
        {
            Asteroid asteroid1 = new Asteroid(1, 0);
            Asteroid asteroid2 = new Asteroid(3, 2);
            Asteroid asteroid3 = new Asteroid(4, 3);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsTrue(lineOfSight.IsBlockedBy(asteroid2));
        }

        [Test]
        public void IsBlockedBy_6()
        {
            Asteroid asteroid1 = new Asteroid(3, 4);
            Asteroid asteroid2 = new Asteroid(4, 4);
            Asteroid asteroid3 = new Asteroid(3, 2);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid2));
        }

        [Test]
        public void IsBlockedBy_Horizontal()
        {
            Asteroid asteroid1 = new Asteroid(1, 4);
            Asteroid asteroid2 = new Asteroid(2, 4);
            Asteroid asteroid3 = new Asteroid(3, 4);
            Asteroid asteroid4 = new Asteroid(4, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsTrue(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsBlockedBy_Vertical()
        {
            Asteroid asteroid1 = new Asteroid(1, 1);
            Asteroid asteroid2 = new Asteroid(1, 2);
            Asteroid asteroid3 = new Asteroid(1, 3);
            Asteroid asteroid4 = new Asteroid(1, 4);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid1));
            Assert.IsTrue(lineOfSight.IsBlockedBy(asteroid2));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid3));
            Assert.IsFalse(lineOfSight.IsBlockedBy(asteroid4));
        }

        [Test]
        public void IsNotBlockedByAny()
        {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 1);
            Asteroid asteroid3 = new Asteroid(2, 2);
            Asteroid asteroid4 = new Asteroid(7, 12);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid4);

            Assert.IsTrue(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid2, asteroid3 }));
            Assert.IsTrue(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid1, asteroid2, asteroid3, asteroid4 }));

            lineOfSight = new LineOfSight(asteroid1, asteroid3);

            Assert.IsFalse(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid2 }));
            Assert.IsFalse(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid1, asteroid2, asteroid3, asteroid4 }));
            Assert.IsTrue(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid4 }));
            Assert.IsTrue(lineOfSight.IsNotBlockedByAny(new List<Asteroid>() { asteroid1, asteroid3, asteroid4 }));

        }

        [Test]
        public void GetAngle_0_up() {
            Asteroid asteroid1 = new Asteroid(0, 4);
            Asteroid asteroid2 = new Asteroid(0, 0);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(0.0, lineOfSight.GetAngle());
        }

        [Test]
        public void GetAngle_45_up_right() {
            Asteroid asteroid1 = new Asteroid(0, 4);
            Asteroid asteroid2 = new Asteroid(4, 0);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(45.0, lineOfSight.GetAngle());
        }

        [Test]
        public void GetAngle_90() {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(1, 0);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(90.0, lineOfSight.GetAngle());
        }

        [Test]
        public void GetAngle_180_down() {
            Asteroid asteroid1 = new Asteroid(0, 0);
            Asteroid asteroid2 = new Asteroid(0, 1);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(180.0, lineOfSight.GetAngle());
        }

        [Test]
        public void GetAngle_270_left() {
            Asteroid asteroid1 = new Asteroid(1, 0);
            Asteroid asteroid2 = new Asteroid(0, 0);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(270.0, lineOfSight.GetAngle());
        }

        [Test]
        public void GetAngle_280_up_left() {
            Asteroid asteroid1 = new Asteroid(30, 30);
            Asteroid asteroid2 = new Asteroid(0, 25);

            LineOfSight lineOfSight = new LineOfSight(asteroid1, asteroid2);

            Assert.AreEqual(280.0, lineOfSight.GetAngle());
        }
    }
}
