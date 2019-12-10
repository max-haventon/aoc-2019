using System;

namespace Day10
{
    public class Asteroid
    {
        public int x, y, otherAsteroidsInSight;

        public Asteroid(int _x, int _y)
        {
            x = _x;
            y = _y;
            otherAsteroidsInSight = 0;
        }

        public double DistanceTo(Asteroid otherAsteroid)
        {
            return Math.Sqrt(Math.Pow(x - otherAsteroid.x, 2) + Math.Pow(y - otherAsteroid.y, 2));
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
