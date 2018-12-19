using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day19 : Day
    {
        List<string[]> Instructions;
        public Day19(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            int InstructionPointer = 0;
            Int32.TryParse(Instructions[0][1], out InstructionPointer);
            List<Instruction> ProperInstructions = new List<Instruction>();
            foreach(string[] s in Instructions)
            {
                if (s.Length == 4)
                    ProperInstructions.Add(new Instruction(s));
            }
            int[] Registers = new int[6] {0,0,0,0,0,0};
            int PointyMcPointFace = Registers[InstructionPointer];
            while (PointyMcPointFace > 0 && PointyMcPointFace < ProperInstructions.Count)
            {
                string SwitchString = ProperInstructions[PointyMcPointFace].Operation;
                int A = ProperInstructions[PointyMcPointFace].A;
                int B = ProperInstructions[PointyMcPointFace].B;
                int C = ProperInstructions[PointyMcPointFace].C;
                switch (SwitchString)
                {
                    case "addr":
                        Registers[C]=Registers[A]+Registers[B];
                        break;
                    case "addi":
                        Registers[C]= Registers[A] + B;
                        break;
                    case "mulr":
                        ;
                        break;
                    case "muli":
                        ;
                        break;
                    case "banr":
                        ;
                        break;
                    case "bani":
                        ;
                        break;
                    case "borr":
                        ;
                        break;
                    case "bori":
                        ;
                        break;
                    case "setr":
                        ;
                        break;
                    case "seti":
                        ;
                        break;
                    case "gtir":
                        ;
                        break;
                    case "gtri":
                        ;
                        break;
                    case "gtrr":
                        ;
                        break;
                    case "eqir":
                        ;
                        break;
                    case "eqri":
                        ;
                        break;
                    case "eqrr":
                        ;
                        break;
                    default:
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
    class Instruction
    {
        public string Operation;
        public int A;
        public int B;
        public int C;
        public Instruction(string[] s)
        {
            Operation = s[0];
            Int32.TryParse(s[1], out A);
            Int32.TryParse(s[2], out B);
            Int32.TryParse(s[3], out C);
        }
    }
}
