using System;
using System.Collections.Generic;

namespace Day10
{
    public class LineOfSight
    {
        public Asteroid source, destination;
        private int dx, dy;
        
        public LineOfSight(Asteroid from, Asteroid to)
        {
            source = from;
            destination = to;

            dx = destination.x - source.x;
            dy = destination.y - source.y;
        }

        public bool IsBlockedBy(Asteroid asteroid)
        {
            bool onLine;

            if (destination.x == source.x)
            {
                onLine = (asteroid.x == source.x);
            }
            else
            {
                int expectedYdx = dx * source.y + dy * (asteroid.x - source.x);
                int actualYdx = asteroid.y * dx;

                onLine = actualYdx == expectedYdx;
            }
            
            bool closerThanDestination = (asteroid.DistanceTo(source) < destination.DistanceTo(source));

            bool closerThanSource = (asteroid.DistanceTo(destination) < source.DistanceTo(destination));

            bool isSourceOrDestination = (asteroid.x == source.x && asteroid.y == source.y) || (asteroid.x == destination.x && asteroid.y == destination.y);

            bool response = onLine && closerThanDestination && closerThanSource && !isSourceOrDestination;

            //if (response) Console.WriteLine($"The line of sight between {source} and {destination} is blocked by {asteroid}!");

            return response;
        }

        public bool IsNotBlockedByAny(List<Asteroid> asteroids)
        {
            foreach (Asteroid asteroid in asteroids)
            {
                if (IsBlockedBy(asteroid))
                {
                    return false;
                }
            }

            return true;
        }

        public double GetAngle() {
            return (360 + Math.Atan2((double)dy, (double)dx)*180/Math.PI + 90) % 360;
        }
    }
}
