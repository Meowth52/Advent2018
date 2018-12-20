using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day16 : Day
    {
        List<List<List<int>>> Instructions1;
        List<List<int>> Instructions2;
        public Day16(string _input) : base(_input)
        {
            string Input = _input.Replace("\r\n\r\n", "_");
            string[] SplitString = Input.Split('_');
            Instructions1 = new List<List<List<int>>>();
            foreach (string s in SplitString)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    if (s[0] == 'B')
                    {
                        Instructions1.Add(this.parseListOfIntegerLists(s));
                    }
                    else
                    {
                        Instructions2 = this.parseListOfIntegerLists(s);
                    }
                }
            }
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            Dictionary<string, List<int>> OpCodes = new Dictionary<string, List<int>>
            {
                {"addr", new List<int>()},
                {"addi", new List<int>()},
                {"mulr", new List<int>()},
                {"muli", new List<int>()},
                {"banr", new List<int>()},
                {"bani", new List<int>()},
                {"borr", new List<int>()},
                {"bori", new List<int>()},
                {"setr", new List<int>()},
                {"seti", new List<int>()},
                {"gtir", new List<int>()},
                {"gtri", new List<int>()},
                {"gtrr", new List<int>()},
                {"eqir", new List<int>()},
                {"eqri", new List<int>()},
                {"eqrr", new List<int>()},
            };
            //Part 1
            foreach (List<List<int>> ListList in Instructions1)
            {
                int SuccessCounter = 0;
                foreach (KeyValuePair<string, List<int>> OpCode in OpCodes)
                {
                    if (ListList[2].SequenceEqual(Operate(OpCode.Key, ListList[1], ListList[0])))
                    {
                        SuccessCounter++;
                    }
                }
                if (SuccessCounter >= 3)
                    Sum++;
            }
            //Part 2
            foreach (List<List<int>> ListList in Instructions1)
            {
                List<string> Edits = new List<string>();
                foreach (KeyValuePair<string, List<int>> OpCode in OpCodes)
                {
                    if (!OpCode.Value.Contains(ListList[1][0]))
                    {
                        if (ListList[2].SequenceEqual(Operate(OpCode.Key, ListList[1], ListList[0])))
                        {
                            ;
                        }
                        else
                        {
                            Edits.Add(OpCode.Key);
                        }

                    }
                }
                foreach (string s in Edits)
                {
                    if (!OpCodes[s].Contains(ListList[1][0]))
                        OpCodes[s].Add(ListList[1][0]);
                }
            }
            Dictionary<int,string> DecodedCodes = new Dictionary<int,string>();
            while (DecodedCodes.Count < 16)
            {
                int RemovedInt = -1;
                string RemoveThis = "";

                foreach (KeyValuePair<string, List<int>> k in OpCodes)
                {
                    if (!DecodedCodes.ContainsValue(k.Key) && k.Value.Count == 15)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            if (!k.Value.Contains(i))
                            {
                                DecodedCodes.Add(i, k.Key);
                                RemovedInt = i;
                                RemoveThis = k.Key;
                                break;
                            }
                        }
                        break;
                    }
                }
                OpCodes.Remove(RemoveThis);
                foreach (KeyValuePair<string, List<int>> k in OpCodes)
                {
                    if (!k.Value.Contains(RemovedInt))
                    {
                        k.Value.Add(RemovedInt);
                    }
                }
            }
            List<int> Registers = new List<int>() {0,0,0,0};
            foreach (List<int> l in Instructions2)
            {
                Registers = Operate(DecodedCodes[l[0]], l, Registers);
            }
            Sum2 = Registers[0];
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
        public List<int> Operate(string OpCode, List<int> OpNumbers, List<int> _registers)
        {
            List<int> Registers = new List<int>(_registers);
            int A = OpNumbers[1];
            int B = OpNumbers[2];
            int C = OpNumbers[3];
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