using System;
using System.IO;
using System.Collections.Generic;

namespace _6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");
            
            (string center, string orbiter) parsedLine;

            List<string> planets = new List<string>();
            
            Dictionary<string, List<string>> childrenByParent = new Dictionary<string, List<string>>();

            Dictionary<string, int> orbitCounts = new Dictionary<string, int>();
            
            foreach(string line in lines) {
                parsedLine = parseLine(line);
                Console.WriteLine($"Parsed [{line}] into center: {parsedLine.center}, orbiter: {parsedLine.orbiter}");

                if(!childrenByParent.ContainsKey(parsedLine.center)) {
                    childrenByParent.Add(parsedLine.center, new List<string>());
                }

                childrenByParent[parsedLine.center].Add(parsedLine.orbiter);

                if (!planets.Exists(x => x == parsedLine.center)) {
                    planets.Add(parsedLine.center);
                }

                if (!planets.Exists(x => x == parsedLine.orbiter)) {
                    planets.Add(parsedLine.orbiter);
                }
            }

            foreach (KeyValuePair<string, List<string>> entry in childrenByParent) {
                Console.WriteLine($"Parent is {entry.Key}");
                foreach (string child in entry.Value) {
                    Console.WriteLine($"  child: {child}");
                    if (childrenByParent.ContainsKey(child)) {
                        Console.WriteLine($"    {child} has {childrenByParent[child].Count} children");
                    }
                }
            }

            // TODO
            Console.WriteLine("Printing all planets:");
            int orbits = 0;
            int totalOrbits = 0;
            foreach (string planet in planets) {
                orbits = countOrbits(childrenByParent, planet);
                //Console.WriteLine($"  {planet}, orbits: {orbits}");
                totalOrbits += orbits;
                if(planet == "SAN" || planet == "YOU") {
                    Console.WriteLine($"{planet}: {listParents(childrenByParent, planet)}");
                }
            }
            Console.WriteLine($"Total number of orbits {totalOrbits}");
        }

        static int countOrbits(Dictionary<string, List<string>> childrenByParent, string planet) {
            foreach (KeyValuePair<string, List<string>> entry in childrenByParent) {
                foreach (string child in entry.Value) {
                    if (child == planet) {
                        return 1 + countOrbits(childrenByParent, entry.Key);
                    }
                }
            }

            return 0;
        }

        static string listParents(Dictionary<string, List<string>> childrenByParent, string planet) {
            foreach (KeyValuePair<string, List<string>> entry in childrenByParent) {
                foreach (string child in entry.Value) {
                    if (child == planet) {
                        return entry.Key + "," + listParents(childrenByParent, entry.Key);
                    }
                }
            }

            return "";
        }

        static (string center, string orbiter) parseLine(string line) {
            string[] split = line.Split(')');

            if(split.Length != 2) {
                Console.WriteLine($"Got bad input line: [{line}], quitting!");
                Environment.Exit(-1);
            }

            return (center: split[0], orbiter: split[1]);
        } 
    }
}
