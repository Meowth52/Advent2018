using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day14 : Day
    {
        string Instruction;
        List<int> Recipes;
        int TargetIteration;
        public Day14(string _input) : base(_input)
        {
            Instruction = this.parseJustOneLine(_input);
        }
        public override Tuple<string, string> getResult()
        {
            string Sum = "";
            int Sum2 = 0;
            TargetIteration = 0;
            Int32.TryParse(Instruction, out TargetIteration);
            int[] Elves= {0,1};
            Recipes = new List<int>();
            Recipes.Add(3);
            Recipes.Add(7);
            bool Part1 = true;
            bool Part2 = false;
            StringBuilder TestStringBuilder = new StringBuilder();
            while (!(Part1&&Part2))
            {
                int NewRecipe = Recipes[Elves[0]] + Recipes[Elves[1]];
                if (NewRecipe > 9)
                {
                    int FirstDigit = NewRecipe / 10;
                    Recipes.Add(FirstDigit);
                    NewRecipe = NewRecipe % 10;
                }
                Recipes.Add(NewRecipe);
                Elves[0] = (Elves[0] + Recipes[Elves[0]] + 1) % (Recipes.Count);
                Elves[1] = (Elves[1] + Recipes[Elves[1]] + 1) % (Recipes.Count);
                if (Recipes.Count >= TargetIteration + 10)
                {
                    Sum = getPartOne();
                    Part1 = true;
                }
                if (Recipes.Count>100)
                {
                    for (int i = Recipes.Count-(Instruction.Length+1); i < Recipes.Count; i++)
                    {
                        TestStringBuilder.Append(Recipes[i].ToString());
                    }
                    string TestString = TestStringBuilder.ToString();
                    if (TestString.Contains(Instruction))
                    {
                        TestStringBuilder.Clear();
                        foreach(int i in Recipes)
                        {
                            TestStringBuilder.Append(i.ToString());
                        }
                        TestString = TestStringBuilder.ToString();
                        Sum2 = TestString.IndexOf(Instruction);
                        Part2 = true;
                    }
                    TestStringBuilder.Clear();
                }
            }
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            string Sum = "";
            List<int> ResultList = Recipes.GetRange(TargetIteration, 10);
            foreach (int i in ResultList)
            {
                Sum += i.ToString();
            }
            return Sum;
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
    }
}