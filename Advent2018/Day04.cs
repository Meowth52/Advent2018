using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day04 : Day
    {
        string[] Instructions;
        public Day04(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            SortedDictionary<DateTime, string> SortedInstructions = new SortedDictionary<DateTime, string>();
            foreach(string s in Instructions)
            {
                string[] SplitInstruction = s.Replace("[", "").Split(']');
                SortedInstructions.Add(DateTime.Parse(SplitInstruction[0]), SplitInstruction[1]);
            }
            Dictionary<int, Guard> Guards = new Dictionary<int, Guard>();
            int CurrentGuard = 0;
            DateTime FeelAsleep= new DateTime(1, 1, 1, 0, 0, 0);
            foreach(KeyValuePair<DateTime,string> k in SortedInstructions)
            {
                char Switcheroo = k.Value[1];
                switch (Switcheroo)
                {
                    case 'G':
                        string[] SplitGuard = k.Value.Replace("#", "").Split(' ');
                        int TryInt = 0;
                        Int32.TryParse(SplitGuard[2], out TryInt);
                        CurrentGuard = TryInt;
                        if (!Guards.ContainsKey(CurrentGuard))
                            Guards.Add(CurrentGuard, new Guard());
                        break;
                    case 'w':
                        Guards[CurrentGuard].fillMinutes(FeelAsleep, k.Key);
                        break;
                    case 'f':
                        FeelAsleep = k.Key;
                        break;
                    default:
                        break;
                }             
            }
            int WinningGuard = 0;
            int WinningSleep = 0;
            int Part2WinningSleep = 0;
            int Part2WinningGuard = 0;
            foreach (KeyValuePair<int,Guard> k in Guards)
            {
                if (k.Value.getTotalTime() > WinningSleep)
                {
                    WinningSleep = k.Value.getTotalTime();
                    WinningGuard = k.Key;
                }
                if (k.Value.getMostestMinuteAmount() > Part2WinningSleep)
                {
                    Part2WinningSleep = k.Value.getMostestMinuteAmount();
                    Part2WinningGuard = k.Key;
                }
            }

            Sum = WinningGuard * Guards[WinningGuard].getMostestMinute();
            Sum2 = Part2WinningGuard * Guards[Part2WinningGuard].getMostestMinute();
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
    public class Guard
    {
        Dictionary<int, int> MinuteList = new Dictionary<int, int>();
        int TotalTime = 0;
        public Guard()
        {
            for (int i = 0; i < 60; i++)
            {
                MinuteList.Add(i, 0);
            }
        }
        public void fillMinutes(DateTime from, DateTime to)
        {
            for(int i = from.Minute; i <= to.Minute; i++)
            {
                MinuteList[i]++;
                TotalTime++;
            }
        }
        public int getMostestMinute()
        {
            int MostesMinute = 0;
            int minute = 0;
            foreach(KeyValuePair<int,int> k in MinuteList)
            {
                if (k.Value > MostesMinute)
                {
                    MostesMinute = k.Value;
                    minute = k.Key;
                }
            }
            return minute;
        }
        public int getMostestMinuteAmount()
        {
            int MostesMinute = 0;
            int minute = 0;
            foreach (KeyValuePair<int, int> k in MinuteList)
            {
                if (k.Value > MostesMinute)
                {
                    MostesMinute = k.Value;
                    minute = k.Key;
                }
            }
            return MostesMinute;

        }
        public int getTotalTime()
        {
            return TotalTime;
        }

    }
}
