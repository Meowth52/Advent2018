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
            foreach(string s in SplitString)
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
            List<int> Registers = new List<int>();
            foreach(List<List<int>> ListList in Instructions1)
            {
                Registers = ListList[0];
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
        public int[] GetOperationResult(int OpCode,int[] Registers)
        {
            return Registers;
        }
    }
}