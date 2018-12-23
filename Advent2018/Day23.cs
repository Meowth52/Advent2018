using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{    
    public class Day23 : Day
    {
        List<List<int>> Instructions;
        public Day23(string _input) : base(_input)
        {
            Instructions = this.parseListOfIntegerLists(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            List<NanoBot> NanoBots = new List<NanoBot>();
            NanoBot BiggestBot = new NanoBot(1,1,1,1);
            int BiggestRange = 0;
            foreach(List<int> l in Instructions)
            {
                NanoBots.Add(new NanoBot(l[0], l[1], l[2], l[3]));
                if (l[3] > BiggestRange)
                {
                    BiggestRange = l[3];
                    BiggestBot = new NanoBot(l[0], l[1], l[2], l[3]);
                }
            }
            int MostestNumber = 0;
            NanoBot MostestBot = new NanoBot(0, 0, 0, 0);
            foreach (NanoBot n in NanoBots)
            {
                int Number = 0;
                if (BiggestBot.isInRange(n))
                {
                    Sum++;
                }
                foreach(NanoBot m in NanoBots)
                {
                    if (m.isInRange(n))
                    {
                        Number++;
                    }
                }
                if (Number > MostestNumber)
                {
                    MostestNumber = Number;
                    MostestBot = n;
                }
            }
            MostestNumber = 0;
            int TN = 50;
            for(int x = MostestBot.x-TN;x<MostestBot.x+TN;x++)
                for (int y = MostestBot.y - TN; y < MostestBot.y + TN; x++)
                    for (int z = MostestBot.z - TN; z < MostestBot.z + TN; x++)
                    {
                        NanoBot TestBot = new NanoBot(x, y, z, 0);
                        int Number = 0;
                        foreach(NanoBot n in NanoBots)
                        {
                            if (n.isInRange(TestBot))
                            {
                                Number++;
                            }
                        }
                        if (Number > MostestNumber)
                        {
                            MostestNumber = Number;
                            Sum2 = TestBot.x + TestBot.y + TestBot.z;
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
    }
    public class NanoBot : Coordinate
    {
        public int z;
        public int Range;
        public NanoBot(int x, int y, int z, int _range) : base(x, y)
        {
            this.z = z;
            Range = _range;
        }
        public bool isInRange(NanoBot n)
        {
            return Math.Abs(this.x - n.x) + Math.Abs(this.y - n.y) + Math.Abs(this.z - n.z) <= Range;
        }
    }
}
