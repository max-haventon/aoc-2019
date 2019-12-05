using System;
using System.IO;
using System.Linq;

namespace _5_1
{
    class Program
    {
        static int INPUT = 5;
        static void Main(string[] args)
        {
            int[] intCodes = file2intCodes("input");

            int output = processIntCode(intCodes);

            //Console.WriteLine(processIntCode(string2intCodes("1002,4,3,4,33")));

        }

        static int[] file2intCodes(string fileName) {
            string[] lines = File.ReadAllLines(fileName);
            
            foreach(string line in lines) {
                return string2intCodes(line);
            }

            return null;
        }

        static int[] string2intCodes(string line) {
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

        
        static int processIntCode(int[] data) {
            int pos = 0;
            while (true) {
                //Console.WriteLine($"Pos: {pos}. Got instruction {data[pos]}. {data.Length}");
                
                char[] instr = data[pos].ToString().ToCharArray();
                int[] instruction = new int[instr.Length];

                for(int i = 0; i < instr.Length; i++) {
                    instruction[i] = Int32.Parse(instr[i].ToString());
                }

                //Console.WriteLine($"Converted instruction into an array with {instruction.Length} elements, first being: {instruction[0]}");
                
                int opCode = instruction[instruction.Length - 1];
                if (instruction.Length > 1) {
                    opCode += 10 * instruction[instruction.Length - 2];
                }
                //Console.WriteLine($"Determined opCode to be {opCode}");

                if (opCode == 99) {
                    Console.WriteLine($"Got opCode 99, quitting. Value at position 0 = {data[0]}");
                    return data[0];
                }
                
                int parameter1 = data[pos + 1];
                int parameter2 = data[pos + 2];

                bool isValueMode1 = true;
                bool isValueMode2 = true;

                if (instruction.Length > 2 && instruction[instruction.Length - 3] == 1) {
                    isValueMode1 = false;
                }

                if (instruction.Length > 3 && instruction[instruction.Length - 4] == 1) {
                    isValueMode2 = false;
                }
/*
                Console.WriteLine($"    parameter1: {parameter1}");
                Console.WriteLine($"    parameter2: {parameter2}");
                Console.WriteLine($"    isValueMode1: {isValueMode1}");
                Console.WriteLine($"    isValueMode2: {isValueMode2}");
                Console.WriteLine($"    destinationPosition: {data[pos + 3]}");
*/
                switch (opCode) {
                    case 1:
                        data[data[pos + 3]] = (isValueMode1 ? data[parameter1] : parameter1) + (isValueMode2 ? data[parameter2] : parameter2);
                        //Console.WriteLine($"Added {(isValueMode1 ? data[parameter1] : parameter1)} and {(isValueMode2 ? data[parameter2] : parameter2)} and write the result to position {data[pos + 3]}");
                        pos += 4;
                        break;
                    case 2:
                        data[data[pos + 3]] = (isValueMode1 ? data[parameter1] : parameter1) * (isValueMode2 ? data[parameter2] : parameter2);
                        //Console.WriteLine($"Multiplied {(isValueMode1 ? data[parameter1] : parameter1)} and {(isValueMode2 ? data[parameter2] : parameter2)} and write the result to position {data[pos + 3]}");
                        pos += 4;
                        break;
                    case 3:
                        //Console.WriteLine($"Setting input at position {data[pos + 1]}");
                        data[data[pos + 1]] = INPUT;
                        pos += 2;
                        break;
                    case 4:
                        Console.WriteLine($"Output requested: {data[data[pos + 1]]}");
                        pos += 2;
                        break;
                    case 5:
                        if ((isValueMode1 ? data[parameter1] : parameter1) != 0) {
                            pos = (isValueMode2 ? data[parameter2] : parameter2);
                        } else
                            pos += 3;
                        break;
                    case 6:
                        if ((isValueMode1 ? data[parameter1] : parameter1) == 0) {
                            pos = (isValueMode2 ? data[parameter2] : parameter2);
                        } else
                            pos += 3;
                        break;
                    case 7:
                        if ((isValueMode1 ? data[parameter1] : parameter1) < (isValueMode2 ? data[parameter2] : parameter2)) {
                            data[data[pos + 3]] = 1;
                        } else {
                            data[data[pos + 3]] = 0;
                        }
                        pos += 4;
                        break;
                    case 8:
                        if ((isValueMode1 ? data[parameter1] : parameter1) == (isValueMode2 ? data[parameter2] : parameter2)) {
                            data[data[pos + 3]] = 1;
                        } else {
                            data[data[pos + 3]] = 0;
                        }
                        pos += 4;
                        break;
                    default:
                        Console.WriteLine($"Bad opCode {opCode}, quitting");
                        Environment.Exit(-2);
                        break;
                }

                for (int i = 0; i < data.Length; i++) {
                    int str = data[i];
                    //Console.Write($"{str},");
                }
                //Console.WriteLine("");
                //Console.WriteLine($"Position updated to {pos}");
                //Console.WriteLine("");
            }
        }
    }
}
