using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day05 : Day
    {
        string Instruction;
        int RemoveThis = 0;
        public Day05(string _input) : base(_input)
        {
            Instruction = this.parseJustOneLine(_input);
        }
        public override Tuple<string, string> getResult()
        {
            string Sum = getPartOne();
            string Sum2 = getPartTwo();
            return Tuple.Create(Sum, Sum2);
        }
        public override string getPartOne()
        {
            int Difference = 32;
            List<int> IntegerPolymers = new List<int>();
            foreach (char c in Instruction)
            {
                int NewInt = (int)c;
                if (NewInt != RemoveThis && NewInt != RemoveThis + Difference)
                {
                    IntegerPolymers.Add(NewInt);
                }
            }
            bool AnyHit = true;
            while (AnyHit)
            {
                AnyHit = false;
                int LastPoly = 0;
                int DestructionIndex = -1;
                foreach (int i in IntegerPolymers)
                {
                    DestructionIndex++;
                    if (Math.Abs(i - LastPoly) == Difference)
                    {
                        AnyHit = true;
                        break;
                    }
                    else
                    {

                    }
                    LastPoly = i;
                }
                if (AnyHit)
                {
                    IntegerPolymers.RemoveRange(DestructionIndex - 1, 2);
                }
            }
            return IntegerPolymers.Count.ToString();
            
        }
        public override string getPartTwo()
        {
            int SavedChar = 0;
            int SavedResult = 10000000;
            for(int i = 41; i <= 90; i++)
            {
                RemoveThis = i;
                int IntResult = 10000000;
                Int32.TryParse(getPartOne(), out IntResult);
                if (IntResult < SavedResult)
                {
                    SavedResult = IntResult;
                    SavedChar = i;
                }
            }
            return SavedResult.ToString();
        }
    }
}