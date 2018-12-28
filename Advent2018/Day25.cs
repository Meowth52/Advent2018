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
            List<Coordinate4D> Coordinates = new List<Coordinate4D>();
            List<List<Coordinate4D>> Groups = new List<List<Coordinate4D>>();
            foreach(List<int> l in Instructions)
            {
                Coordinates.Add(new Coordinate4D(l[0], l[1], l[2], l[3]));
            }
            while(Coordinates.Count > 0)
            {
                List<Coordinate4D> Current = new List<Coordinate4D>();
                Current.Add(Coordinates[0]);
                Coordinates.RemoveAt(0);
                List<int> RemoveIndex = new List<int>();
                bool JoinUs = true;
                while (JoinUs)
                {
                    foreach (Coordinate4D c in Coordinates)
                {
                        JoinUs = false;
                        foreach (Coordinate4D coo in Current)
                        {
                            if (c.GetManhattan(coo) <= 3 &! Current.Contains(c))
                            {
                                JoinUs = true;
                                break;
                            }
                        }
                        if (JoinUs)
                        {
                            RemoveIndex.Add(Coordinates.IndexOf(c));
                            Current.Add(new Coordinate4D( c));
                        }
                    }
                }
                RemoveIndex.OrderByDescending(s => s);
                foreach(int i in RemoveIndex)
                {
                    Coordinates.RemoveAt(i);
                }
                Groups.Add(Current);
            }
            Sum = Groups.Count();
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
        public Coordinate4D(Coordinate4D c)
        {
            x = c.x;
            y = c.y;
            z = c.z;
            t = c.t;
        }
        public int GetManhattan(Coordinate4D c)
        {
            return Math.Abs(x-c.x) + Math.Abs(y-c.y) + Math.Abs(z-c.z) + Math.Abs(t-c.t);
        }
    }
}