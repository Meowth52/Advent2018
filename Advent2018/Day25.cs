using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day25 : Day
    {
        List<List<int>> Instructions;
        public Day25(string _input) : base(_input)
        {
            Instructions = this.parseListOfIntegerLists(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
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
    public class Coordinate4D : Coordinate
    {
        public int z;
        public int t;
        public Coordinate4D (int x, int y, int z, int t) : base(x, y)
        {
            this.z = z;
            this.t = t;
        }
    }
}