using System;
using System.IO;
using System.Linq;

namespace _2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int desiredOutput = 19690720;

            for(int noun = 0; noun <= 99; noun++) {
                for(int verb = 0; verb <= 99; verb++) {
                    int[] intCodes = file2intCodes("input");

                    intCodes[1] = noun;
                    intCodes[2] = verb;

                    int output = processIntCode(intCodes);

                    if (desiredOutput == output) {
                        Console.WriteLine($"Got {output} = {desiredOutput} using input1: {noun}, input2: {verb}; {100 * noun + verb}");
                    }
                }
            }
        }

        static int[] file2intCodes(string fileName) {
            string[] lines = File.ReadAllLines(fileName);
            
            foreach(string line in lines) {
                string[] stringData = line.Split(',');
                int[] data = new int[stringData.Length];

                for(int i = 0; i < stringData.Length; i ++) {
                    try {
                        data[i] = Int32.Parse(stringData[i]);
                    }
                    catch (FormatException) {
                        Console.WriteLine("Expected integer inputs, quitting!");
                        Environment.Exit(-1);
                    }
                }

                return data;
            }

            return null;
        }

        static int processIntCode(int[] data) {
            int pos = 0;
            while (true) {
                int opCode = data[pos];
                
                if (opCode == 99) {
                    return data[0];
                }
                
                int operand1 = data[data[pos + 1]];
                int operand2 = data[data[pos + 2]];
                int destinationPosition = data[pos + 3];

                switch (opCode) {
                    case 1:
                        data[destinationPosition] = operand1 + operand2;
                        break;
                    case 2:
                        data[destinationPosition] = operand1 * operand2;
                        break;
                    default:
                        Console.WriteLine("Bad opCode, quitting");
                        Environment.Exit(-2);
                        break;
                }
                pos += 4;
            }
        }
    }
}
