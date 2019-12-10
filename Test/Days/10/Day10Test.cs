using System;
using System.Collections.Generic;
using NUnit.Framework;
using Day10;

namespace Test.Days
{
    public class Day10Test
    {
        [Test]
        public void FindMaximumOtherAsteroidsVisible_Sample1()
        {
            string[] input = {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };

            Assert.AreEqual(8, Day10Solver.FindMaximumOtherAsteroidsVisible(input));
        }

        [Test]
        public void FindMaximumOtherAsteroidsVisible_Sample2()
        {
            string[] input = {
                "......#.#.",
                "#..#.#....",
                "..#######.",
                ".#.#.###..",
                ".#..#.....",
                "..#....#.#",
                "#..#....#.",
                ".##.#..###",
                "##...#..#.",
                ".#....####"
            };

            Assert.AreEqual(33, Day10Solver.FindMaximumOtherAsteroidsVisible(input));
        }

        [Test]
        public void FindMaximumOtherAsteroidsVisible_Sample3()
        {
            Console.WriteLine("Writing shit to console");
            string[] input = {
                "#.#...#.#.",
                ".###....#.",
                ".#....#...",
                "##.#.#.#.#",
                "....#.#.#.",
                ".##..###.#",
                "..#...##..",
                "..##....##",
                "......#...",
                ".####.###."
            };

            Assert.AreEqual(35, Day10Solver.FindMaximumOtherAsteroidsVisible(input));
        }

        [Test]
        public void FindMaximumOtherAsteroidsVisible_Sample4()
        {
            string[] input = {
                ".#..#..###",
                "####.###.#",
                "....###.#.",
                "..###.##.#",
                "##.##.#.#.",
                "....###..#",
                "..#.#..#.#",
                "#..#.#.###",
                ".##...##.#",
                ".....#.#.."
            };

            Assert.AreEqual(41, Day10Solver.FindMaximumOtherAsteroidsVisible(input));
        }

        [Test]
        public void FindMaximumOtherAsteroidsVisible_Sample5()
        {
            string[] input = {
                ".#..##.###...#######",
                "##.############..##.",
                ".#.######.########.#",
                ".###.#######.####.#.",
                "#####.##.#.##.###.##",
                "..#####..#.#########",
                "####################",
                "#.####....###.#.#.##",
                "##.#################",
                "#####.##.###..####..",
                "..######..##.#######",
                "####.##.####...##..#",
                ".#####..#.######.###",
                "##...#.##########...",
                "#.##########.#######",
                ".####.#.###.###.#.##",
                "....##.##.###..#####",
                ".#.#.###########.###",
                "#.#.#.#####.####.###",
                "###.##.####.##.#..##"
            };

            Assert.AreEqual(210, Day10Solver.FindMaximumOtherAsteroidsVisible(input));
        }

        [Test]
        public void GetMaximumOtherAsteroidsInSight() {
            var asteroid1 = new Asteroid(1, 0);
            var asteroid2 = new Asteroid(1, 2);
            var asteroid3 = new Asteroid(18, 74);

            asteroid1.otherAsteroidsInSight = 87;
            asteroid2.otherAsteroidsInSight = 0;
            asteroid3.otherAsteroidsInSight = 13;
            
            List<Asteroid> asteroids = new List<Asteroid>()
            {
                asteroid1,
                asteroid2,
                asteroid3
            };

            Assert.AreEqual(87, Day10Solver.GetMaximumOtherAsteroidsInSight(asteroids));
        }

        [Test]
        public void ParseInput_Sample1()
        {
            List<Asteroid> actual = Day10Solver.ParseInput(new string[] {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            });

            List<Asteroid> expected = new List<Asteroid>()
            {
                new Asteroid(1, 0),
                new Asteroid(4, 0),
                new Asteroid(0, 2),
                new Asteroid(1, 2),
                new Asteroid(2, 2),
                new Asteroid(3, 2),
                new Asteroid(4, 2),
                new Asteroid(4, 3),
                new Asteroid(3, 4),
                new Asteroid(4, 4)
            };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].ToString(), actual[i].ToString());
            }
        }

        [Test]
        public void ParseInput()
        {
            List<Asteroid> actual = Day10Solver.ParseInput(new string[] { ".#.", "#.#", "..#" });

            List<Asteroid> expected = new List<Asteroid>()
            {
                new Asteroid(1, 0),
                new Asteroid(0, 1),
                new Asteroid(2, 1),
                new Asteroid(2, 2)
            };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].ToString(), actual[i].ToString());
            }
        }

        [Test]
        public void SetOtherAsteroidsInSight()
        {
            List<Asteroid> asteroids = new List<Asteroid>()
            {
                new Asteroid(0, 0),
                new Asteroid(1, 1),
                new Asteroid(2, 2),
                new Asteroid(7, 4)
            };

            Day10Solver.SetOtherAsteroidsInSight(asteroids);

            Assert.AreEqual(2, asteroids[0].otherAsteroidsInSight);
            Assert.AreEqual(3, asteroids[1].otherAsteroidsInSight);
            Assert.AreEqual(2, asteroids[2].otherAsteroidsInSight);
            Assert.AreEqual(3, asteroids[3].otherAsteroidsInSight);

        }

        [Test]
        public void SetOtherAsteroidsInSightFrom()
        {
            var asteroid1 = new Asteroid(0, 0);

            List<Asteroid> asteroids = new List<Asteroid>()
            {
                asteroid1,
                new Asteroid(1, 1),
                new Asteroid(2, 2),
                new Asteroid(7, 4)
            };

            Day10Solver.SetOtherAsteroidsInSightFrom(asteroid1, asteroids);

            Assert.AreEqual(2, asteroids[0].otherAsteroidsInSight);
        }

        [Test]
        public void SetOtherAsteroidsInSight_Sample1()
        {
            List<Asteroid> asteroids = new List<Asteroid>()
            {
                new Asteroid(1, 0),
                new Asteroid(4, 0),
                new Asteroid(0, 2),
                new Asteroid(1, 2),
                new Asteroid(2, 2),
                new Asteroid(3, 2),
                new Asteroid(4, 2),
                new Asteroid(4, 3),
                new Asteroid(3, 4),
                new Asteroid(4, 4)
            };

            Day10Solver.SetOtherAsteroidsInSight(asteroids);

            Assert.AreEqual(7, asteroids[0].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[1].otherAsteroidsInSight);
            Assert.AreEqual(6, asteroids[2].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[3].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[4].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[5].otherAsteroidsInSight);
            Assert.AreEqual(5, asteroids[6].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[7].otherAsteroidsInSight);
            Assert.AreEqual(8, asteroids[8].otherAsteroidsInSight);
            Assert.AreEqual(7, asteroids[9].otherAsteroidsInSight);

        }

        [Test]
        public void SetOtherAsteroidsInSightFrom_Sample1()
        {
            List<Asteroid> asteroids = new List<Asteroid>()
            {
                new Asteroid(1, 0),
                new Asteroid(4, 0),
                new Asteroid(0, 2),
                new Asteroid(1, 2),
                new Asteroid(2, 2),
                new Asteroid(3, 2),
                new Asteroid(4, 2),
                new Asteroid(4, 3),
                new Asteroid(3, 4),
                new Asteroid(4, 4)
            };

            //Day10Solver.SetOtherAsteroidsInSightFrom(asteroids[8], asteroids);

            //Assert.AreEqual(8, asteroids[8].otherAsteroidsInSight);

            Day10Solver.SetOtherAsteroidsInSightFrom(asteroids[5], asteroids);

            Assert.AreEqual(7, asteroids[5].otherAsteroidsInSight);
        }

        [Test]
        public void SolvePart1()
        {
            Assert.AreEqual(309, Day10Solver.SolvePart1());
        }

        [Test]
        public void SolvePart2()
        {
            Assert.AreEqual(416, Day10Solver.SolvePart2());
        }
    }
}