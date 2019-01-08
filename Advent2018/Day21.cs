using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day21 : Day
    {
        List<string[]> Instructions;
        public Day21(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 1000000;
            int Sum2 = 0;
            List<Operation> Operations = new List<Operation>();
            int IPP = 0;
            Int32.TryParse(Instructions[0][1], out IPP);
            foreach(string[] s in Instructions)
            {
                if (s.Length == 4)
                {
                    Operations.Add(new Operation(s));
                }
            }
            int Breaker;
            int BreakPoint = 10000;
            bool Broke;
            int Stop = 1000000;
            for (int i = 0; i < Stop; i++)
            {
                Broke = false;
                Breaker = 0;
                int[] Registers = { 0, 0, 0, 0, 0, 0 };
                int IP = Registers[IPP];
                while (IP >= 0 && IP < Operations.Count)
                {
                    Breaker++;
                    Registers[IPP] = IP;
                    Registers = Operations[IP].Operate(Registers);
                    IP = Registers[IPP];
                    IP++;
                    if (Breaker >= BreakPoint)
                    {
                        Broke = true;
                        break;
                    }
                }
                if (!Broke)
                {
                    Sum = i;
                    break;
                }
            }

            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            throw new NotImplementedException();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
        
    }
    public class Operation        
    {
        string OpCode;
        List<int> OpNumbers;
        public Operation(string[] s)
        {
            OpCode = s[0];
            OpNumbers = new List<int>();
            for(int i = 1; i <= 3; i++)
            {
                int ParseInt = 0;
                Int32.TryParse(s[i], out ParseInt);
                OpNumbers.Add(ParseInt);
            }
        }

        public int[] Operate(int[] _registers)
        {
            int[] Registers = _registers;
            int A = OpNumbers[0];
            int B = OpNumbers[1];
            int C = OpNumbers[2];
            switch (OpCode)
            {
                case "addr":
                    Registers[C] = Registers[A] + Registers[B];
                    break;
                case "addi":
                    Registers[C] = Registers[A] + B;
                    break;
                case "mulr":
                    Registers[C] = Registers[A] * Registers[B];
                    break;
                case "muli":
                    Registers[C] = Registers[A] * B;
                    break;
                case "banr":
                    Registers[C] = Registers[A] & Registers[B];
                    break;
                case "bani":
                    Registers[C] = Registers[A] & B;
                    break;
                case "borr":
                    Registers[C] = Registers[A] | Registers[B];
                    break;
                case "bori":
                    Registers[C] = Registers[A] | B;
                    break;
                case "setr":
                    Registers[C] = Registers[A];
                    break;
                case "seti":
                    Registers[C] = A;
                    break;
                case "gtir":
                    if (A > Registers[B])
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                case "gtri":
                    if (Registers[A] > B)
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                case "gtrr":
                    if (Registers[A] > Registers[B])
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                case "eqir":
                    if (A == Registers[B])
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                case "eqri":
                    if (Registers[A] == B)
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                case "eqrr":
                    if (Registers[A] == Registers[B])
                        Registers[C] = 1;
                    else
                        Registers[C] = 0;
                    break;
                default:
                    break;
            }
            return Registers;
        }
    }
}
