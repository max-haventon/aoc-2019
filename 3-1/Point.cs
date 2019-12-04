using System;
using System.IO;

namespace _3_1
{
    public class Point
    {
        public int x, y, distance;

        public Point() : this(0, 0, 0)
        { }

        public Point(int x, int y, int distance)
        {
            this.x = x;
            this.y = y;
            this.distance = distance;
        }
        
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType())) 
            {
                return false;
            }
            else { 
                Point p = (Point) obj; 
                return (x == p.x) && (y == p.y);
            }   
        }

        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }

        public int GetDistance() {
            return distance;
        }

        public Point Step(string direction) {
            switch (direction){
                case "L":
                    return new Point(x - 1, y, distance + 1);
                case "R":
                    return new Point(x + 1, y, distance + 1);
                case "D":
                    return new Point(x, y - 1, distance + 1);
                case "U":
                    return new Point(x, y + 1, distance + 1);
                default:
                    Console.WriteLine($"Unsupported direction {direction}");
                    Environment.Exit(-1);
                    break;
            }
            
            return null;
        }

        /*public int CompareTo(object obj) {
            if (obj == null) return 1;
            
            Point otherPoint = obj as Point;
            if (otherPoint == null)
            {
                throw new ArgumentException("Object is not a Point");
            }
            else
            {
                if (getX() == otherPoint.getX() && getY() == otherPoint.getY())
                {
                    return 0;
                }
                else
                {
                    return (getX() + getY()).CompareTo((otherPoint.getX() + otherPoint.getY()));
                }
            }
        }*/

    }
}