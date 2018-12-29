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
            foreach (List<int> l in Instructions)
            {
                Coordinates.Add(new Coordinate4D(l[0], l[1], l[2], l[3]));
            }
            while (Coordinates.Count > 0)
            {
                List<Coordinate4D> Current = new List<Coordinate4D>();
                Current.Add(Coordinates[0]);
                Coordinates.RemoveAt(0);
                if (Coordinates.Count > 0)
                {
                    List<int> RemoveIndex = new List<int>();
                    bool JoinUs = true;
                    while (JoinUs)
                    {
                        foreach (Coordinate4D c in Coordinates)
                        {
                            JoinUs = false;
                            foreach (Coordinate4D coo in Current)
                            {
                                if (c.GetManhattan(coo) <= 3 & !Current.Contains(c))
                                {
                                    JoinUs = true;
                                    break;
                                }
                            }
                            if (JoinUs)
                            {
                                RemoveIndex.Add(Coordinates.IndexOf(c));
                                Current.Add(new Coordinate4D(c));
                                break;
                            }
                        }
                    }
                    RemoveIndex = RemoveIndex.OrderByDescending(s => s).ToList();
                    foreach (int i in RemoveIndex)
                    {
                        Coordinates.RemoveAt(i);
                    }
                    if (Coordinates.Count == 0)
                        ;
                }
                else
                    ;
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
        public Coordinate4D(int x, int y, int z, int t) : base(x, y)
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
            return Math.Abs(x - c.x) + Math.Abs(y - c.y) + Math.Abs(z - c.z) + Math.Abs(t - c.t);
        }
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString() + "," + z.ToString() + "," + t.ToString();
        }
        public override int GetHashCode()
        {
            int hCode = x ^ y ^ z ^ t;
            return hCode.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate4D);
        }
        public bool Equals(Coordinate4D obj)
        {
            return obj != null && obj.x == x && obj.y == y && obj.z == z && obj.t == t;
        }
        class Coordinate4DEqualityComparer : IEqualityComparer<Coordinate4D>
        {
            public bool Equals(Coordinate4D b1, Coordinate4D b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null | b2 == null)
                    return false;
                else if (b1.x == b2.x && b1.y == b2.y && b1.z == b2.z && b1.t == b2.t)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(Coordinate4D bx)
            {
                int hCode = bx.x ^ bx.y ^ bx.z ^ bx.t;
                return hCode.GetHashCode();
            }
        }
    }
}