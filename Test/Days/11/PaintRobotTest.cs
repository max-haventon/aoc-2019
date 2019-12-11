using NUnit.Framework;
using Day11;
using Common;

namespace Test.Days
{
    public class PaintRobotTest {
        [Test]
        public void UpdateDirection() {
            IntCodeComputer icc = new IntCodeComputer(new long[]{});
            PaintRobot paintRobot = new PaintRobot(icc);

            Assert.AreEqual(Direction.Up, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnLeft);
            Assert.AreEqual(Direction.Left, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnLeft);
            Assert.AreEqual(Direction.Down, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnLeft);
            Assert.AreEqual(Direction.Right, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnLeft);
            Assert.AreEqual(Direction.Up, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnRight);
            Assert.AreEqual(Direction.Right, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnRight);
            Assert.AreEqual(Direction.Down, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnRight);
            Assert.AreEqual(Direction.Left, paintRobot.direction);

            paintRobot.UpdateDirection(DirectionChange.TurnRight);
            Assert.AreEqual(Direction.Up, paintRobot.direction);
        }

        [Test]
        public void UpdatePosition() {
            IntCodeComputer icc = new IntCodeComputer(new long[]{});
            PaintRobot paintRobot = new PaintRobot(icc);

            Assert.AreEqual(Direction.Up, paintRobot.direction);
            Assert.AreEqual(0, paintRobot.x);
            Assert.AreEqual(0, paintRobot.y);
            
            paintRobot.UpdatePosition();

            Assert.AreEqual(0, paintRobot.x);
            Assert.AreEqual(1, paintRobot.y);
        }
    }
}