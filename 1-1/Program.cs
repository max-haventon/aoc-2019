using System;
using System.IO;

namespace _1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");
            
            int totalFuelRequirement = 0;

            foreach(string line in lines) {
                Console.WriteLine(line);

                try {
                    int mass = Int32.Parse(line);

                    int fuel = mass2fuel(mass);

                    totalFuelRequirement += fuel;
                }
                catch (FormatException) {
                    Console.WriteLine("Failed parsing of line");
                    Environment.Exit(-1);
                }
            }

            Console.WriteLine(String.Concat("Total fuel needed: ", totalFuelRequirement));
        }

        static int mass2fuel(int mass) {
            return mass / 3 - 2;
        }

   }
}
