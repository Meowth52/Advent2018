using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day18 : Day
    {
        string[] Instructions;
        public Day18(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            int MaxX = Instructions[0].Length - 1;
            int MaxY = Instructions.Length - 1;
            Dictionary<Coordinate, char> TheGrid = new Dictionary<Coordinate, char>();
            for (int x = 0; x < Instructions[0].Length; x++)
            {
                for (int y = 0; y < Instructions.Length; y++)
                {
                    TheGrid.Add(new Coordinate(x, y), Instructions[x][y]);
                }
            }
            List<Coordinate> Adjant = new List<Coordinate>();
            Adjant.Add(new Coordinate(1, 1));
            Adjant.Add(new Coordinate(1, 0));
            Adjant.Add(new Coordinate(1, -1));
            Adjant.Add(new Coordinate(0, 1));
            Adjant.Add(new Coordinate(0, -1));
            Adjant.Add(new Coordinate(-1, 0));
            Adjant.Add(new Coordinate(-1, 1));
            Adjant.Add(new Coordinate(-1, -1));
            int Minute = 0;
            Dictionary<Coordinate, char> NextGrid = new Dictionary<Coordinate, char>();
            Dictionary<int, int> Values = new Dictionary<int, int>();
            int PeakValue = 0;
            int LastPeak = 0;
            int PeakInterval = 0;
            while (Minute < 1000)
            {
                Minute++;
                foreach (KeyValuePair<Coordinate, char> Acre in TheGrid)
                {
                    int SquareCounter = 0;
                    char NextChar = ' ';
                    switch (Acre.Value)
                    {
                        case '.': //Open
                            foreach (Coordinate c in Adjant)
                            {
                                if (Acre.Key.GetSum(c).IsInPositiveBounds(MaxX, MaxY))
                                {
                                    if (TheGrid[Acre.Key.GetSum(c)] == '|')
                                    {
                                        SquareCounter++;
                                    }
                                }
                            }
                            if (SquareCounter >= 3)
                                NextChar = '|';
                            else
                                NextChar = '.';
                            NextGrid.Add(new Coordinate(Acre.Key), NextChar);
                            break;
                        case '|': //Tree
                            foreach (Coordinate c in Adjant)
                            {
                                if (Acre.Key.GetSum(c).IsInPositiveBounds(MaxX, MaxY))
                                {
                                    if (TheGrid[Acre.Key.GetSum(c)] == '#')
                                    {
                                        SquareCounter++;
                                    }
                                }

                            }
                            if (SquareCounter >= 3)
                                NextChar = '#';
                            else
                                NextChar = '|';
                            NextGrid.Add(new Coordinate(Acre.Key), NextChar);
                            break;
                        case '#': //Lumberyard
                            bool Lumberyard = false;
                            bool Tree = false;
                            foreach (Coordinate c in Adjant)
                            {

                                if (Acre.Key.GetSum(c).IsInPositiveBounds(MaxX, MaxY))
                                {
                                    if (TheGrid[Acre.Key.GetSum(c)] == '#')
                                    {
                                        Lumberyard = true;
                                    }
                                    if (TheGrid[Acre.Key.GetSum(c)] == '|')
                                    {
                                        Tree = true;
                                    }
                                }
                            }
                            if (Lumberyard && Tree)
                                NextChar = '#';
                            else
                                NextChar = '.';
                            NextGrid.Add(new Coordinate(Acre.Key), NextChar);
                            break;
                        default:
                            break;
                    }
                }
                TheGrid = new Dictionary<Coordinate, char>(NextGrid);
                NextGrid.Clear();
                int Lumberyards = 0;
                int Wooded = 0;
                foreach (KeyValuePair<Coordinate, char> Acre in TheGrid)
                {
                    if (Acre.Value == '#')
                        Lumberyards++;
                    if (Acre.Value == '|')
                        Wooded++;
                }
                int LandValue = Lumberyards * Wooded;
                if(Minute==10)
                    Sum = LandValue;
                Values.Add(Minute, LandValue);
                if (Minute>500 && LandValue > PeakValue)
                {
                    PeakValue = LandValue;
                }
                if(Minute>750 && LandValue == PeakValue)
                {
                    PeakInterval = Minute - LastPeak;
                    LastPeak = Minute;
                }
            }
            Sum2 = Values[500 + 999999500 % PeakInterval];
            StringBuilder TestOutput = new StringBuilder();
            for (int y = 0; y <= MaxY; y++)
            {
                for (int x = 0; x <= MaxX; x++)
                {
                    TestOutput.Append(TheGrid[new Coordinate(x, y)]);
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
