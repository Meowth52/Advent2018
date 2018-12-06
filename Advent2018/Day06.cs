using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day06 : Day
    {
        List<string[]> Instructions = new List<string[]>();
        List<Coordinate> Targets = new List<Coordinate>();
        List<TargetCoordinate> TheGrid = new List<TargetCoordinate>();
        public Day06(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            foreach(string[] s in Instructions)
            {
                int x = 0;
                int y = 0;
                Int32.TryParse(s[0].Replace(",", ""), out x);
                Int32.TryParse(s[1], out y);
                Targets.Add(new Coordinate(x, y));
            }
            List<TargetCoordinate> TheEdge = new List<TargetCoordinate>();
            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    if (x==0 || y == 0 || x == 499 || y == 499)
                        TheEdge.Add(new TargetCoordinate(x, y));
                    else
                        TheGrid.Add(new TargetCoordinate(x, y));
                }
            }
            Dictionary<Coordinate, int> Result = new Dictionary<Coordinate, int>();
            List<Coordinate> ClosestToTheEdge = new List<Coordinate>();
            foreach(TargetCoordinate t in TheEdge)
            {
                t.FindTargets(Targets);
                if (!ClosestToTheEdge.Contains(t.getBestTarget())) // eh comparer
                    ClosestToTheEdge.Add(t.getBestTarget());
            }
            foreach(Coordinate c in Targets)
            {
                if (!ClosestToTheEdge.Contains(c))
                    Result.Add(c, 0);
            }
            foreach(TargetCoordinate t in TheGrid)
            {
                t.FindTargets(Targets);
                if (Result.ContainsKey(t.getBestTarget()))
                    Result[t.getBestTarget()]++;
                int TotalDistance = 0;
                foreach (Coordinate c in Targets)
                {
                    TotalDistance += t.getAbsoluteDifferance(c);
                }
                if (TotalDistance < 10000)
                    Sum2++;
            }
            foreach(KeyValuePair<Coordinate,int> k in Result)
            {
                if (k.Value > Sum)
                    Sum = k.Value;
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
    public class TargetCoordinate : Coordinate
    {
        Coordinate BestTarget;
        public TargetCoordinate(int x, int y) : base (x, y)
        {
           
        }
        public void FindTargets(List<Coordinate> Targets)
        {
            int BestDistance = 10000000;
            bool IsEqual = false;
            foreach(Coordinate c in Targets)
            {
                if (this.getAbsoluteDifferance(c) < BestDistance)
                {
                    BestDistance = this.getAbsoluteDifferance(c);
                    BestTarget = new Coordinate(c);
                    IsEqual = false;
                }
                else if (this.getAbsoluteDifferance(c) == BestDistance)
                {
                    IsEqual = true;
                }
            }
            if (IsEqual)
            {
                BestTarget = new Coordinate(0, 0);
            }
        }
        public int getAbsoluteDifferance(Coordinate c)
        {
            int x2 = Math.Abs(x - c.x);
            int y2 = Math.Abs(y - c.y);
            return x2 + y2;
        }
        public Coordinate getBestTarget()
        {
            return BestTarget;
        }
    }
}
