using System;
using System.Collections.Generic;

namespace Common {

    public class IntCodeComputer {
        private long[] prestineData, data;
        public string mode;

        private long pos, inputPos, relativeBase;

        public IntCodeComputer(int[] intCodes)
        {
            prestineData = new long[intCodes.Length];

            for (int i = 0; i < intCodes.Length; i++)
            {
                prestineData[i] = (long)intCodes[i];
            }

            Reset();
        }

        public IntCodeComputer(long[] intCodes)
        {
            prestineData = new long[intCodes.Length];

            for (int i = 0; i < intCodes.Length; i++)
            {
                prestineData[i] = intCodes[i];
            }

            Reset();
        }

        public void Reset() {
            data = new long[10000];
            
            for (int i = 0; i < prestineData.Length; i++) {
                data[i] = prestineData[i];
            }

            pos = 0;
            inputPos = 0;
            relativeBase = 0;
            mode = "RUNNING";
        }

        public long[] processIntCode(long[] input) {
            List<long> output = new List<long>();
            int i = 0;
            while (true) {
                if (mode == "STOPPED") return output.ToArray();
                step(input, output);
                i++;
                if (i > 1000000)
                {
                    throw new Exception("IntCodeComputer seems to be stuck!");
                }
            }
        }

        public void step(long[] input, List<long>output) {
            if (mode == "STOPPED") {
                return;
            }

            char[] instr = data[pos].ToString().ToCharArray();
            long[] instruction = new long[instr.Length];

            for(long i = 0; i < instr.Length; i++) {
                try
                {
                    if (instr[i] == '-')
                    {
                        instruction[i] = Int64.Parse(instr[i].ToString() + instr[++i].ToString());
                    } else
                    {
                        instruction[i] = Int64.Parse(instr[i].ToString());
                    }
                } catch (FormatException) {
                    throw new Exception($"Parse of {instr[i].ToString()} to Int failed");
                }
            }

            long opCode = instruction[instruction.Length - 1];
            if (instruction.Length > 1) {
                opCode += 10 * instruction[instruction.Length - 2];
            }

            if (opCode == 99) {
                mode = "STOPPED";
                return;
            }

            (long parameter1, long parameter2, long parameter3, ParameterMode mode1, ParameterMode mode2, ParameterMode mode3) = GetParameters(instruction, data, pos);
            
            switch (opCode) {
                case 1:
                    data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = GetValue(parameter1, mode1) + GetValue(parameter2, mode2);
                    pos += 4;
                    break;
                case 2:
                    data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = GetValue(parameter1, mode1) * GetValue(parameter2, mode2);
                    pos += 4;
                    break;
                case 3:
                    if (inputPos < input.Length) {
                        data[(mode1 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 1]] = input[inputPos++];
                        pos += 2;
                    }
                    break;
                case 4:
                    output.Add(GetValue(parameter1, mode1));
                    pos += 2;
                    break;
                case 5:
                    if (GetValue(parameter1, mode1) != 0) {
                        pos = GetValue(parameter2, mode2);
                    } else
                        pos += 3;
                    break;
                case 6:
                    if (GetValue(parameter1, mode1) == 0) {
                        pos = GetValue(parameter2, mode2);
                    } else
                        pos += 3;
                    break;
                case 7:
                    if (GetValue(parameter1, mode1) < GetValue(parameter2, mode2)) {
                        data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = 1;
                    } else {
                        data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = 0;
                    }
                    pos += 4;
                    break;
                case 8:
                    if (GetValue(parameter1, mode1) == GetValue(parameter2, mode2))
                    {
                        data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = 1;
                    }
                    else
                    {
                        data[(mode3 == ParameterMode.Relative ? relativeBase : 0) + data[pos + 3]] = 0;
                    }
                    pos += 4;
                    break;
                case 9:
                    relativeBase += GetValue(parameter1, mode1);
                    pos += 2;
                    break;
                default:
                    Console.WriteLine($"Bad opCode {opCode}, quitting. pos was {pos}. data[pos] was {data[pos]}. data[pos+1] was {data[pos + 1]}. data[pos+2] was {data[pos + 2]}. data[pos+3] was {data[pos + 3]}");
                    throw new Exception($"Bad opCode {0}");
            }
        
        }

        private (long, long, long, ParameterMode, ParameterMode, ParameterMode) GetParameters(long[] instruction, long[] d, long p)
        {
            long parameter1 = d[p + 1];
            long parameter2 = d[p + 2];
            long parameter3 = d[p + 3];

            ParameterMode mode1 = ParameterMode.Position;
            ParameterMode mode2 = ParameterMode.Position;
            ParameterMode mode3 = ParameterMode.Position;

            if (instruction.Length > 2)
            {
                mode1 = (ParameterMode)instruction[instruction.Length - 3];
            }

            if (instruction.Length > 3)
            {
                mode2 = (ParameterMode)instruction[instruction.Length - 4];
            }

            if (instruction.Length > 4)
            {
                mode3 = (ParameterMode)instruction[instruction.Length - 5];
            }

            return (parameter1, parameter2, parameter3, mode1, mode2, mode3);
        }

        private long GetValue(long parameter, ParameterMode parameterMode)
        {
            if (parameterMode == ParameterMode.Position)
            {
                return data[parameter];
            } else if (parameterMode == ParameterMode.Immediate) {
                return parameter;

            } else if (parameterMode == ParameterMode.Relative)
            {
                return data[relativeBase + parameter];
            } else
            {
                throw new Exception("Unsupported parameter mode");
            }
        }

        public long GetDataAt(long pos)
        {
            return data[pos];
        }
    }
}