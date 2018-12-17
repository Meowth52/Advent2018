using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day17 : Day
    {
        List<List<int>> InstructionsValues;
        string[] InstructionIndex;
        public Day17(string _input) : base(_input)
        {
            InstructionsValues = this.parseListOfIntegerLists(_input);
            InstructionIndex = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            int LargestNumber = 0;
            foreach(List<int> l in InstructionsValues)
            {
                foreach(int i in l)
                {
                    if (i > LargestNumber)
                        LargestNumber = i;
                }
            }
            char[,] TheGrid = new char[1000, LargestNumber+1];
            for(int x = 0;x < 1000; x++)
            {
                for(int y = 0; y <= LargestNumber; y++)
                {
                    TheGrid[x, y] = '.';
                }
            }
            for(int i = 0; i<InstructionsValues.Count; i++)
            {
                if (InstructionIndex[i][0] == 'x')
                {
                    for (int i2 = InstructionsValues[i][1]; i2 <= InstructionsValues[i][2]; i2++)
                    {
                        TheGrid[InstructionsValues[i][0], i2] = '#';
                    }
                }
                else
                {
                    for (int i2 = InstructionsValues[i][1]; i2 <= InstructionsValues[i][2]; i2++)
                    {
                        TheGrid[i2,InstructionsValues[i][0]] = '#';
                    }
                }
            }
            Dictionary<Coordinate, bool> Water = new Dictionary<Coordinate, bool>();
            Water.Add(new Coordinate(500, 1), true);
            TheGrid[500, 1] ='|';
            int PreviousWaterCount = 0;
            Dictionary<Coordinate, bool> NextWater = new Dictionary<Coordinate, bool>(Water);
            while (Water.Count>PreviousWaterCount)
            {
                PreviousWaterCount = Water.Count;
                foreach(KeyValuePair<Coordinate,bool> w in Water)
                {
                    if (w.Value && w.Key.y<LargestNumber)
                    {
                        if (TheGrid[w.Key.x, w.Key.y+1] == '.')
                        {
                            NextWater.Add(new Coordinate(w.Key.x, w.Key.y + 1), true);
                            TheGrid[w.Key.x, w.Key.y + 1] = '|';
                        }
                        else if (TheGrid[w.Key.x, w.Key.y + 1] == '#' || TheGrid[w.Key.x, w.Key.y + 1] == '~')
                        {
                            bool RightStillGoing = true;
                            bool LeftStillGoing = true;
                            bool RowStillGoing = true;
                            bool FullRow = true;
                            int ThisIsGettingMessy = 1;
                            Dictionary<Coordinate, bool> TestRow = new Dictionary<Coordinate, bool>();
                            while (RowStillGoing)
                            {
                                if (RightStillGoing && TheGrid[ w.Key.x + ThisIsGettingMessy, w.Key.y] == '.')
                                {
                                    TestRow.Add(new Coordinate(w.Key.x + ThisIsGettingMessy, w.Key.y), true);
                                    if (TheGrid[w.Key.x + ThisIsGettingMessy, w.Key.y + 1] == '.')
                                    {
                                        RightStillGoing = false;
                                        FullRow = false;
                                    }

                                }
                                else
                                {
                                    RightStillGoing = false;
                                }
                                if (LeftStillGoing && TheGrid[w.Key.x - ThisIsGettingMessy, w.Key.y] == '.')
                                {
                                    TestRow.Add(new Coordinate(w.Key.x - ThisIsGettingMessy, w.Key.y), true);
                                    if (TheGrid[w.Key.x - ThisIsGettingMessy, w.Key.y + 1] == '.')
                                    {
                                        LeftStillGoing = false;
                                        FullRow = false;
                                    }
                                }
                                else
                                {
                                    LeftStillGoing = false;
                                }
                                RowStillGoing = RightStillGoing || LeftStillGoing;
                                ThisIsGettingMessy++;
                            }
                            if (FullRow)
                            {
                                NextWater[w.Key] = false;
                                TheGrid[w.Key.x, w.Key.y] = '~';
                            }
                            foreach(KeyValuePair<Coordinate,bool> t in TestRow)
                            {
                                NextWater.Add(t.Key, !FullRow);
                                char Charmander = '|';
                                if (FullRow)
                                {
                                    Charmander = '~';
                                }
                                TheGrid[t.Key.x, t.Key.y] = Charmander;
                            }
                        }
                    }
                }
                Water = new Dictionary<Coordinate, bool>(NextWater);
            }
            Sum = Water.Count();
            StringBuilder TestOutput = new StringBuilder();
            for (int y = 0; y < 50; y++) 
            {
                for (int x = 475; x < 525; x++)
                {
                    TestOutput.Append(TheGrid[x, y]);
                }
                TestOutput.Append("\r\n");

            }
            return Tuple.Create(TestOutput.ToString(), Sum2.ToString());
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
}