using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day01 : Day
    {
        string[] Instructions;
        public Day01(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public override string getPartOne()
        {
            List<int> Numbers = new List<int>();
            foreach (string s in Instructions)
            {
                int i = 0;
                Int32.TryParse(s, out i);
                Numbers.Add(i);
            }
            int Frequency = 0;
            foreach (int i in Numbers)
            {
                Frequency += i;
            }
            return Frequency.ToString();
        }
        public override string getPartTwo()
        {
            List<int> Numbers = new List<int>();
            foreach (string s in Instructions)
            {
                int i = 0;
                Int32.TryParse(s, out i);
                Numbers.Add(i);
            }
            List<int> Frequencies = new List<int>();
            Frequencies.Add(0);
            int Frequency = 0;
            bool FoundIt = false;
            while (!FoundIt)
            {
                foreach (int i in Numbers)
                {
                    Frequency += i;
                    if (Frequencies.Contains(Frequency))
                    {
                        FoundIt = true;
                        break;
                    }
                    Frequencies.Add(Frequency);
                }
            }
            return Frequency.ToString();
        }
    }
}
