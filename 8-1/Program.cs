using System;
using System.IO;
using System.Collections.Generic;
namespace _8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");
            
            char[] input = lines[0].ToCharArray();

            int WIDTH = 25;
            int HEIGHT = 6;
            int LAYER_SIZE = WIDTH * HEIGHT;
            
            Layer[] layers = new Layer[input.Length / LAYER_SIZE];

            for (int i=0; i<layers.Length; i++) {
                var startIndex = i * LAYER_SIZE;
                var endIndex = (i + 1) * LAYER_SIZE;

                layers[i] = new Layer(input[startIndex..endIndex], WIDTH, HEIGHT);

                Console.WriteLine($"Created a layer with 0:{layers[i].count(0)}, 1:{layers[i].count(1)}, 2:{layers[i].count(2)}, {startIndex}..{endIndex}");
            }

            Console.WriteLine($"input was {input.Length} chars long. Created {layers.Length} layers.");
        }
    }
}
