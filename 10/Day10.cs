using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public static class Day10Solver
    {
        static string[] input = new string[] {
            "#.#................#..............#......#......",
            ".......##..#..#....#.#.....##...#.........#.#...",
            ".#...............#....#.##......................",
            "......#..####.........#....#.......#..#.....#...",
            ".....#............#......#................#.#...",
            "....##...#.#.#.#.............#..#.#.......#.....",
            "..#.#.........#....#..#.#.........####..........",
            "....#...#.#...####..#..#..#.....#...............",
            ".............#......#..........#...........#....",
            "......#.#.........#...............#.............",
            "..#......#..#.....##...##.....#....#.#......#...",
            "...#.......##.........#.#..#......#........#.#..",
            "#.............#..........#....#.#.....#.........",
            "#......#.#................#.......#..#.#........",
            "#..#.#.....#.....###..#.................#..#....",
            "...............................#..........#.....",
            "###.#.....#.....#.............#.......#....#....",
            ".#.....#.........#.....#....#...................",
            "........#....................#..#...............",
            ".....#...#.##......#............#......#.....#..",
            "..#..#..............#..#..#.##........#.........",
            "..#.#...#.......#....##...#........#...#.#....#.",
            ".....#.#..####...........#.##....#....#......#..",
            ".....#..#..##...............................#...",
            ".#....#..#......#.#............#........##...#..",
            ".......#.....................#..#....#.....#....",
            "#......#..###...........#.#....#......#.........",
            "..............#..#.#...#.......#..#.#...#......#",
            ".......#...........#.....#...#.............#.#..",
            "..##..##.............#........#........#........",
            "......#.............##..#.........#...#.#.#.....",
            "#........#.........#...#.....#................#.",
            "...#.#...........#.....#.........#......##......",
            "..#..#...........#..........#...................",
            ".........#..#.......................#.#.........",
            "......#.#.#.....#...........#...............#...",
            "......#.##...........#....#............#........",
            "#...........##.#.#........##...........##.......",
            "......#....#..#.......#.....#.#.......#.##......",
            ".#....#......#..............#.......#...........",
            "......##.#..........#..................#........",
            "......##.##...#..#........#............#........",
            "..#.....#.................###...#.....###.#..#..",
            "....##...............#....#..................#..",
            ".....#................#.#.#.......#..........#..",
            "#........................#.##..........#....##..",
            ".#.........#.#.#...#...#....#........#..#.......",
            "...#..#.#......................#...............#"
        };
        public static int SolvePart1()
        {
            return FindMaximumOtherAsteroidsVisible(input);
        }

        public static int SolvePart2()
        {
            List<Asteroid> asteroids = ParseInput(input);

            SetOtherAsteroidsInSight(asteroids);

            Asteroid baseAsteroid = FindAsteroidWithMaximumOtherAsteroidsInSight(asteroids);

            Console.WriteLine($"Determined the base to be {baseAsteroid}, with {baseAsteroid.otherAsteroidsInSight} visible targets initially");

            List<Asteroid> vaporized = new List<Asteroid>();

            while(vaporized.Count < 200) {
                List<LineOfSight> unblockedLines = GetUnblockedLinesFrom(baseAsteroid, asteroids);

                List<LineOfSight> unblockedLinesOrderedByAngle = OrderLinesByAngle(unblockedLines);

                foreach (LineOfSight lazerBeam in unblockedLinesOrderedByAngle) {
                    vaporized.Add(lazerBeam.destination);
                    asteroids.Remove(lazerBeam.destination);

                    Console.WriteLine($"  Just vaporized {lazerBeam.destination}, with an angle of {lazerBeam.GetAngle()}");
                }

                Console.WriteLine($"The beam has now gone a full circle. {vaporized.Count} asteroids have been vaporized. {asteroids.Count} remain.");
            }

            return vaporized[199].x*100 + vaporized[199].y;
        }

        private static List<LineOfSight> OrderLinesByAngle(List<LineOfSight> unblockedLines)
        {
            return unblockedLines.OrderBy(l => l.GetAngle()).ToList();
        }

        private static List<LineOfSight> GetUnblockedLinesFrom(Asteroid baseAsteroid, List<Asteroid> asteroids)
        {
            List<LineOfSight> unblockedLines = new List<LineOfSight>();
            foreach (Asteroid targetAsteroid in asteroids)
            {
                if (targetAsteroid.x == baseAsteroid.x && targetAsteroid.y == baseAsteroid.y)
                {
                    continue;
                }

                LineOfSight lineOfSight = new LineOfSight(baseAsteroid, targetAsteroid);

                if (lineOfSight.IsNotBlockedByAny(asteroids)) {
                    unblockedLines.Add(lineOfSight);
                }
            }

            return unblockedLines;
        }

        public static int FindMaximumOtherAsteroidsVisible(string[] input)
        {
            List<Asteroid> asteroids = ParseInput(input);

            SetOtherAsteroidsInSight(asteroids);

            return GetMaximumOtherAsteroidsInSight(asteroids);
        }

        public static List<Asteroid> ParseInput(string[] input)
        {
            List<Asteroid> asteroids = new List<Asteroid>();

            for (int y = 0; y < input.Length; y++)
            {
                char[] line = input[y].ToCharArray();

                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        asteroids.Add(new Asteroid(x, y));
                    }
                }
            }

            return asteroids;
        }

        public static void SetOtherAsteroidsInSight(List<Asteroid> asteroids)
        {
            foreach (Asteroid baseCandidate in asteroids)
            {
                SetOtherAsteroidsInSightFrom(baseCandidate, asteroids);
            }
        }

        public static void SetOtherAsteroidsInSightFrom(Asteroid asteroid, List<Asteroid> asteroids)
        {
            foreach (Asteroid possiblyDetectableAsteroid in asteroids)
            {
                if (possiblyDetectableAsteroid.x == asteroid.x && possiblyDetectableAsteroid.y == asteroid.y)
                {
                    continue;
                }

                LineOfSight lineOfSight = new LineOfSight(asteroid, possiblyDetectableAsteroid);

                if (lineOfSight.IsNotBlockedByAny(asteroids))
                {
                    asteroid.otherAsteroidsInSight++;
                }
            }
        }

        public static int GetMaximumOtherAsteroidsInSight(List<Asteroid> asteroids) {
            return FindAsteroidWithMaximumOtherAsteroidsInSight(asteroids).otherAsteroidsInSight;
        }

        public static Asteroid FindAsteroidWithMaximumOtherAsteroidsInSight(List<Asteroid> asteroids) {
            IEnumerable<Asteroid> sortedAsteroids = asteroids.OrderBy(a => a.otherAsteroidsInSight);

            Console.WriteLine($"The asteroid with maximum visible other asteroids is {sortedAsteroids.Last()}, which has {sortedAsteroids.Last().otherAsteroidsInSight}");

            return sortedAsteroids.Last();
        }
    }
}
