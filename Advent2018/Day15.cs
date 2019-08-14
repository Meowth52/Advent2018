using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day15 : Day
    {
        string[] Instructions;
        string[,] Map;
        public Day15(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            Map = new string[Instructions.Length,Instructions[0].Length];
            int x = 0;
            int y = 0;
            foreach (string s in Instructions)
            {
                foreach (char c in s)
                {
                    Map[x, y] = c.ToString();
                    y += 1;
                }
                y = 0;
                x += 1;
            }
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            string Returnstring = "";
            foreach (string s in Map)
                Returnstring += s;
            return Tuple.Create(Returnstring, Sum2.ToString());
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
