using System;
using System.IO;
using System.Collections.Generic;
using Common;

namespace _7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==== Part 1 ====");
            part1();
            Console.WriteLine("==== Part 2 ====");
            Console.WriteLine("");
            part2();
        }

        static void part1() {
            int[] intCodes = file2intCodes("input");

            IntCodeComputer icc = new IntCodeComputer(intCodes);

            long[] outputA;
            long[] outputB;
            long[] outputC;
            long[] outputD;
            long[] outputE;
            
            for (long phaseA = 0; phaseA<5; phaseA++) {
                for (long phaseB = 0; phaseB<5; phaseB++) {
                    if (phaseB == phaseA) continue;
                    for (long phaseC = 0; phaseC<5; phaseC++) {
                        if (phaseC == phaseB) continue;
                        if (phaseC == phaseA) continue;
                        for (long phaseD = 0; phaseD<5; phaseD++) {
                            if (phaseD == phaseC) continue;
                            if (phaseD == phaseB) continue;
                            if (phaseD == phaseA) continue;
                            for (long phaseE = 0; phaseE<5; phaseE++) {
                                if (phaseE == phaseD) continue;
                                if (phaseE == phaseC) continue;
                                if (phaseE == phaseB) continue;
                                if (phaseE == phaseA) continue;

                                long[] inputA = {phaseA, 0};
                                outputA = icc.processIntCode(inputA);
                                icc.Reset();

                                outputB = icc.processIntCode(new long[] {phaseB, outputA[0]});
                                icc.Reset();

                                outputC = icc.processIntCode(new long[] {phaseC, outputB[0]});
                                icc.Reset();

                                outputD = icc.processIntCode(new long[] {phaseD, outputC[0]});
                                icc.Reset();

                                outputE = icc.processIntCode(new long[] {phaseE, outputD[0]});
                                icc.Reset();

                                Console.WriteLine($"OutputE: {outputE[0]} when phases are set as {phaseA}-{phaseB}-{phaseC}-{phaseD}-{phaseE}");
                            }
                        }
                    }
                }        
            }
        }

        static void part2() {
            int[] intCodes = file2intCodes("input");

            IntCodeComputer iccA = new IntCodeComputer(intCodes);
            IntCodeComputer iccB = new IntCodeComputer(intCodes);
            IntCodeComputer iccC = new IntCodeComputer(intCodes);
            IntCodeComputer iccD = new IntCodeComputer(intCodes);
            IntCodeComputer iccE = new IntCodeComputer(intCodes);

            List<long> outputA = new List<long>();
            List<long> outputB = new List<long>();
            List<long> outputC = new List<long>();
            List<long> outputD = new List<long>();
            List<long> outputE = new List<long>();
            
            List<long> outputs = new List<long>();

            for (long phaseA = 5; phaseA<10; phaseA++) {
                for (long phaseB = 5; phaseB<10; phaseB++) {
                    if (phaseB == phaseA) continue;
                    for (long phaseC = 5; phaseC<10; phaseC++) {
                        if (phaseC == phaseB) continue;
                        if (phaseC == phaseA) continue;
                        for (long phaseD = 5; phaseD<10; phaseD++) {
                            if (phaseD == phaseC) continue;
                            if (phaseD == phaseB) continue;
                            if (phaseD == phaseA) continue;
                            for (long phaseE = 5; phaseE<10; phaseE++) {
                                if (phaseE == phaseD) continue;
                                if (phaseE == phaseC) continue;
                                if (phaseE == phaseB) continue;
                                if (phaseE == phaseA) continue;

                                iccA.Reset();
                                iccB.Reset();
                                iccC.Reset();
                                iccD.Reset();
                                iccE.Reset();

                                outputA.Clear();
                                outputB.Clear();
                                outputC.Clear();
                                outputD.Clear();
                                outputE.Clear();
                                
                                outputE.Add(0);

                                Console.WriteLine($"Output sizes before processing: {outputA.Count}, {outputB.Count}, {outputC.Count}, {outputD.Count}, {outputE.Count}");

                                while (iccA.mode == "RUNNING"
                                    || iccB.mode == "RUNNING"
                                    || iccC.mode == "RUNNING"
                                    || iccD.mode == "RUNNING"
                                    || iccE.mode == "RUNNING"
                                ) {
                                    iccA.step(formInput(phaseA, outputE.ToArray()), outputA);
                                    
                                    iccB.step(formInput(phaseB, outputA.ToArray()), outputB);
                                    
                                    iccC.step(formInput(phaseC, outputB.ToArray()), outputC);
                                    
                                    iccD.step(formInput(phaseD, outputC.ToArray()), outputD);
                                    
                                    iccE.step(formInput(phaseE, outputD.ToArray()), outputE);
                                }

                                Console.WriteLine($"Output sizes after processing: {outputA.Count}, {outputB.Count}, {outputC.Count}, {outputD.Count}, {outputE.Count}");

                                Console.WriteLine($"OutputE: {outputE[outputE.Count - 1]} when phases are set as {phaseA}-{phaseB}-{phaseC}-{phaseD}-{phaseE}");
                            
                                outputs.Add(outputE[outputE.Count - 1]);
                            }
                        }
                    }
                }        
            }

            outputs.Sort();
            Console.WriteLine(outputs[0]);
            Console.WriteLine(outputs[outputs.Count - 1]);
        }

        static long[] formInput(long phase, long[] prevOutput) {
            long[] input = new long[prevOutput.Length + 1];

            input[0] = phase;

            for (int i = 0; i<prevOutput.Length; i++) {
                input[i+1] = prevOutput[i];
            }

            return input;
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
    }
}
