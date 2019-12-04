using System;
using System.IO;
using System.Collections.Generic;

namespace _3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");

            string[] movements1 = lines[0].Split(',');
            string[] movements2 = lines[1].Split(',');

            Console.WriteLine($"{movements1.Length} movements for line 1");
            Console.WriteLine($"{movements2.Length} movements for line 2");
            
            List<Point> points1 = movements2points(movements1);
            List<Point> points2 = movements2points(movements2);

            Console.WriteLine($"Wire 1 touched {points1.Count} points");
            Console.WriteLine($"Wire 2 touched {points2.Count} points");

            Dictionary<string, Point> hash = new Dictionary<string, Point>();
            foreach(Point point in points2) {
                if (!hash.ContainsKey(point.ToString())) {
                    hash.Add(point.ToString(), point);
                } 
            }
            /*foreach(Point point in pointArray1) {
                Console.WriteLine($"  ({point.x}, {point.y})");
            }   
            foreach(Point point in pointArray2) {
                Console.WriteLine($"  ({point.x}, {point.y})");
            }*/ 

            List<Point> intersections = new List<Point>();
            foreach(Point point in points1) {
                if (hash.ContainsKey(point.ToString())) {
                    intersections.Add(new Point(point.x, point.y, point.distance + hash[point.ToString()].distance));
                }
            }

            Console.WriteLine($"Found {intersections.Count} intersections");

            int[] distances = new int [intersections.Count];

            for(int i = 0; i < intersections.Count; i++) {
                distances[i] = intersections[i].GetDistance();
                //Console.WriteLine($"Intersection at ({intersections[i].x}, {intersections[i].y}): distance {intersections[i].GetDistance()}");
            }
            
            Array.Sort(distances);

            Console.WriteLine($"{distances[0]}, {distances[distances.Length - 1]}");
            
        }

        private static List<Point> movements2points(string[] movements) {
            Point position = new Point(0, 0, 0);
            List<Point> points = new List<Point>();
            int totalDistance = 0;
            foreach(string movement in movements) {
                string direction = movement.Substring(0, 1);
                int distance = Int32.Parse(movement.Substring(1, movement.Length - 1));
                
                for(int i = 0; i < distance; i++) {
                    position = position.Step(direction);
                    
                    points.Add(position);
                }

                totalDistance += distance;
            }

            Console.WriteLine($"Created a list with {points.Count} elements. TotalDistance: {totalDistance}");

            return points;
        }
    }
}
