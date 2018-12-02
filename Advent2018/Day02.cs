using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day02 : Day
    {
        string[] Instructions;
        public Day02(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public override string getPartOne()
        {
            int Doubles = 0;
            int Triples = 0;
            foreach (string s in Instructions)
            {
                List<char> ICanHasSort = new List<char>();
                foreach (char c in s)
                {
                    ICanHasSort.Add(c);
                }
                ICanHasSort.Sort();
                ICanHasSort.Add('_');
                char LastChar = '_';
                int Consecutive = 1;
                bool Double = false;
                bool Triple = false;
                foreach (char c in ICanHasSort)
                {
                    if (c == LastChar)
                    {
                        Consecutive++;
                    }
                    else
                    {
                        switch (Consecutive)
                        {
                            case 2:
                                Double = true;
                                break;
                            case 3:
                                Triple = true;
                                break;
                            default:
                                ;
                                break;
                        }
                        Consecutive = 1;
                    }
                    LastChar = c;
                }
                if (Double)
                    Doubles++;
                if (Triple)
                    Triples++;
            }
            int Sum = Doubles * Triples;
            return Sum.ToString();
        }
        public override string getPartTwo()
        {
            List<string> MatchTestList = new List<string>();
            for (int i = 0; i < 25; i++)
            {
                foreach(string s in Instructions)
                {
                    string TestString = s.Remove(i,1);
                    if (MatchTestList.Contains(TestString))
                    {
                        return TestString;
                    }
                    else
                    {
                        MatchTestList.Add(TestString);
                    }
                }
                MatchTestList.Clear();
            }
            return "";
        }
    }
}
