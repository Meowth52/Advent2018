using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day11 : Day
    {
        string Instruction;
        public Day11(string _input) : base(_input)
        {
            Instruction = this.parseJustOneLine(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            int SerialNumber = 0;
            Int32.TryParse(Instruction, out SerialNumber);
            int[,] TheGrid = new int[301,301];
            for(int x = 1; x <= 300; x++)
            {
                for(int y= 1; y <= 300; y++)
                {
                    if (x == 122 && y == 79)
                        ;
                    int Number = ((x + 10) * y + SerialNumber) * (x + 10);
                    Number = Number % 1000;
                    Number = Number / 100;
                    Number -= 5;
                    TheGrid[x, y] = Number;
                }
            }
            Coordinate TopLeft = GetSum(ref TheGrid,ref Sum, 3);
            Coordinate PartOne = new Coordinate(TopLeft);
            Coordinate ToppestLeft = new Coordinate(0,0);
            int ToppestInt = 0;
            int LargestSum = 0;
            for(int i = 1; i <= 50; i++)
            {
                 TopLeft = GetSum(ref TheGrid, ref Sum, i);
                if (Sum > LargestSum)
                {
                    LargestSum = Sum;
                    ToppestLeft = new Coordinate(TopLeft);
                    ToppestInt = i;
                }
            }
            return Tuple.Create(PartOne.ToString(), ToppestLeft.ToString()+","+ToppestInt.ToString());
        }
        public Coordinate GetSum(ref int[,] TheGrid,ref int Sum, int Size)
        {
            Coordinate TopLeft = new Coordinate(0, 0);
            for (int x = 1; x <= 301-Size; x++)
            {
                for (int y = 1; y <= 301-Size; y++)
                {
                    int PowerSum = 0;
                    for (int x2 = 0; x2 < Size; x2++)
                    {
                        for (int y2 = 0; y2 < Size; y2++)
                        {
                            PowerSum += TheGrid[x + x2, y + y2];
                        }
                    }
                    if (PowerSum > Sum)
                    {
                        Sum = PowerSum;
                        TopLeft = new Coordinate(x, y);
                    }

                }
            }
            return TopLeft;
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
