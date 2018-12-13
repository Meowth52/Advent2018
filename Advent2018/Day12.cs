using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day12 : Day
    {
        string[] Instructions;
        public Day12(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            long Sum2 = 0;
            long NumberOfIterations = 200;
            StringBuilder TestString = new StringBuilder();
            Instructions[0] = Instructions[0].Replace("initial state: ", "");
            bool[] Pots = new bool[Instructions[0].Length +NumberOfIterations*4];
            Pots.Initialize();
            for(int i =0;i<Instructions[0].Length;i++)
            {
                if (Instructions[0][i] == '#')
                {
                    Pots[i+NumberOfIterations*2] = true;
                }
            }
            List<bool[]> GrowthPatterns = new List<bool[]>();
            int NrOfSame = 0;
            int NrOfPlants = 0;
            int LastNrOfPlants = 0;
            int LastIteration = 0;
            int LastI = 0;
            int PointsPerStable = 0;
            foreach(string s in Instructions)
            {
                if (s.Length == 10 && s[9]=='#')
                {
                    bool[] NewPattern = new bool[5];
                    NewPattern.Initialize();
                    for(int i = 0; i < 5; i++)
                    {
                        if (s[i] == '#')
                            NewPattern[i] = true;
                    }
                    GrowthPatterns.Add(NewPattern);
                }
            }
            for (int i = 1; i <= NumberOfIterations; i++)
            {
                bool[] NextPots = new bool[Instructions[0].Length + NumberOfIterations*4];
                for(int Pot = 2; Pot < Pots.Length - 2; Pot++)
                {
                    foreach(bool[] Pattern in GrowthPatterns)
                    {
                        bool[] PatternToTest = new bool[5];
                        Array.ConstrainedCopy(Pots, Pot - 2, PatternToTest, 0, 5);
                        if (Pattern.SequenceEqual(PatternToTest))
                        {
                            NextPots[Pot] = true;
                            break;
                        }
                    }
                }
                Pots = NextPots;
                if (i == 20)
                {
                    Sum = getPoints(Pots, NumberOfIterations);
                }
                for (int i2 = 0; i2 < Pots.Length; i2++)
                {
                    if (Pots[i2])
                    {
                        NrOfPlants++;
                    }
                }
                if (LastNrOfPlants == NrOfPlants)
                    NrOfSame++;
                else
                    NrOfSame = 0;
                LastNrOfPlants = NrOfPlants;
                NrOfPlants = 0;
                if (NrOfSame == 4)
                {
                    PointsPerStable = getPoints(Pots, NumberOfIterations);
                }
                if (NrOfSame > 4)
                {
                    LastIteration = getPoints(Pots, NumberOfIterations);
                    PointsPerStable = LastIteration - PointsPerStable;
                    LastI = i;
                    break;
                }
            }
            for(int i = 0; i < Pots.Length; i++)
            {
                if (Pots[i])
                {
                    Sum2 = LastIteration + (50000000000-LastI)*PointsPerStable;
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
        public int getPoints(bool[]  Pots, long NumberOfIterations)
        {
            int ReturnThis =0;
            for (int i2 = 0; i2 < Pots.Length; i2++)
            {
                if (Pots[i2])
                {
                    ReturnThis += i2 - (int)NumberOfIterations * 2;
                }
            }
            return ReturnThis;
        }
    }
}
