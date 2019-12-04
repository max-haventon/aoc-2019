using System;
using System.IO;
using System.Linq;

namespace _2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");
            
            foreach(string line in lines) {
                Console.WriteLine(line);

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

                data[1] = 12;
                data[2] = 2;

                int pos = 0;
                while (true) {
                    int opCode = data[pos];
                    
                    if (opCode == 99) {
                        Console.WriteLine("opCode 99 encountered, quitting!");
                            
                        Console.WriteLine(String.Join(",", data.Select(p=>p.ToString()).ToArray()));

                        Environment.Exit(0);
                    }
                    
                    int operand1 = data[data[pos + 1]];
                    int operand2 = data[data[pos + 2]];
                    int destinationPosition = data[pos + 3];

                    Console.WriteLine($"Processing from post: {pos}, with opCode: {opCode}, operand1: {operand1}, operand2: {operand2}, destinationPosition: {destinationPosition}");

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

                    Console.WriteLine("Result after processing");
                    Console.WriteLine(String.Join(",", data.Select(p=>p.ToString()).ToArray()));
                    Console.WriteLine();
                }
            }
        }
    }
}
