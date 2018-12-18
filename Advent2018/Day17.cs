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
            int LargeNumber = 0;
            int LargestNumber = 0;
            int SmallestNumber = 100;
            foreach (List<int> l in InstructionsValues)
            {
                foreach (int i in l)
                {
                    if (i > LargeNumber)
                        LargeNumber = i;
                }
            }
            char[,] TheGrid = new char[1000, LargeNumber + 1];
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y <= LargeNumber; y++)
                {
                    TheGrid[x, y] = '.';
                }
            }
            for (int i = 0; i < InstructionsValues.Count; i++)
            {
                if (InstructionIndex[i][0] == 'x')
                {
                    for (int i2 = InstructionsValues[i][1]; i2 <= InstructionsValues[i][2]; i2++)
                    {
                        TheGrid[InstructionsValues[i][0], i2] = '#';
                        if (i2 > LargestNumber)
                            LargestNumber = i2;
                        if (i2 < SmallestNumber)
                            SmallestNumber = i2;
                    }
                }
                else
                {
                    for (int i2 = InstructionsValues[i][1]; i2 <= InstructionsValues[i][2]; i2++)
                    {
                        TheGrid[i2, InstructionsValues[i][0]] = '#';
                        if (InstructionsValues[i][0] > LargestNumber)
                            LargestNumber = InstructionsValues[i][0];
                        if (InstructionsValues[i][0] < SmallestNumber)
                            SmallestNumber = InstructionsValues[i][0];
                    }
                }
            }
            Dictionary<Coordinate, bool> Water = new Dictionary<Coordinate, bool>();
            Water.Add(new Coordinate(500, 1), true);
            TheGrid[500, 1] = '|';
            int PreviousWaterCount = 0;
            Dictionary<Coordinate, bool> NextWater = new Dictionary<Coordinate, bool>(Water);
            StringBuilder TestOutput = new StringBuilder();
            int ToleratedStandstill = 0;
            while (ToleratedStandstill < 100)
            {
                if (Water.Count == PreviousWaterCount)
                    ToleratedStandstill++;
                else
                    ToleratedStandstill = 0;
                PreviousWaterCount = Water.Count;
                foreach (KeyValuePair<Coordinate, bool> w in Water)
                {
                    if (w.Value && w.Key.y < LargestNumber)
                    {
                        if (TheGrid[w.Key.x, w.Key.y + 1] == '.')
                        {
                            NextWater.Add(new Coordinate(w.Key.x, w.Key.y + 1), true);
                            TheGrid[w.Key.x, w.Key.y + 1] = '|';
                        }
                        else if ((TheGrid[w.Key.x, w.Key.y + 1] == '#' || TheGrid[w.Key.x, w.Key.y + 1] == '~') && TheGrid[w.Key.x + 1, w.Key.y] == '#' && TheGrid[w.Key.x - 1, w.Key.y] == '#')
                        {
                            NextWater[w.Key] = false;
                            TheGrid[w.Key.x, w.Key.y] = '~';
                        }
                        else if (TheGrid[w.Key.x, w.Key.y + 1] == '#' || TheGrid[w.Key.x, w.Key.y + 1] == '~')
                        {
                            bool RightStillGoing = true;
                            bool LeftStillGoing = true;
                            bool RowStillGoing = true;
                            int ThisIsGettingMessy = 1;
                            Dictionary<Coordinate, bool> TestRow = new Dictionary<Coordinate, bool>();
                            while (RowStillGoing)
                            {
                                if (RightStillGoing && TheGrid[w.Key.x + ThisIsGettingMessy, w.Key.y] == '.')
                                {
                                    TestRow.Add(new Coordinate(w.Key.x + ThisIsGettingMessy, w.Key.y), true);
                                    if (TheGrid[w.Key.x + ThisIsGettingMessy, w.Key.y + 1] == '.')
                                    {
                                        RightStillGoing = false;
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
                                    }
                                }
                                else
                                {
                                    LeftStillGoing = false;
                                }
                                RowStillGoing = RightStillGoing || LeftStillGoing;
                                ThisIsGettingMessy++;
                            }
                            bool FullRow = true;
                            foreach (KeyValuePair<Coordinate, bool> t in TestRow)
                            {
                                TheGrid[t.Key.x, t.Key.y] = '|';
                            }
                            if (true)
                            {
                                int x = w.Key.x;
                                int y = w.Key.y;
                                while (TheGrid[x, y] == '~' || TheGrid[x, y] == '|')
                                {
                                    if (TheGrid[x, y + 1] == '.' || TheGrid[x + 1, y] == '.' || TheGrid[x - 1, y] == '.')
                                        FullRow = false;
                                    x++;
                                }
                                x = w.Key.x;
                                while (TheGrid[x, y] == '~' || TheGrid[x, y] == '|')
                                {
                                    if (TheGrid[x, y + 1] == '.' || TheGrid[x + 1, y] == '.' || TheGrid[x - 1, y] == '.')
                                        FullRow = false;
                                    x--;
                                }

                            }
                            if ( FullRow)
                            {
                                NextWater[w.Key] = false;
                                TheGrid[w.Key.x, w.Key.y] = '~';
                            }
                            foreach (KeyValuePair<Coordinate, bool> t in TestRow)
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
                //TestOutput = new StringBuilder();
                //for (int y = 0; y < 50; y++)
                //{
                //    for (int x = 475; x < 525; x++)
                //    {
                //        TestOutput.Append(TheGrid[x, y]);
                //    }
                //    TestOutput.Append("\r\n");
                //}
                //_mainView.OutText = TestOutput.ToString();
            }
            Sum = Water.Count()-(SmallestNumber-1);
            foreach(KeyValuePair<Coordinate,bool> w in Water)
            {
                if (!w.Value)
                    Sum2++;
            }
            TestOutput = new StringBuilder();
            for (int y = 0; y <= LargestNumber; y++)
            {
                for (int x = 400; x < 600; x++)
                {
                    TestOutput.Append(TheGrid[x, y]);
                }
                TestOutput.Append("\r\n");
            }
            return Tuple.Create(Sum.ToString() + TestOutput.ToString(), Sum2.ToString());
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