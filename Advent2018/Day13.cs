using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day13 : Day
    {
        string[] Instructions;
        public Day13(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            Coordinate Sum = new Coordinate(0,0);
            Wagon Sum2;
            char[,] TheGrid = new char[Instructions[0].Length, Instructions.Length];
            List<Wagon> Wagons = new List<Wagon>();
            for (int x = 0; x < Instructions[0].Length; x++)
            {
                for (int y = 0; y < Instructions.Length; y++)
                {
                    char c = Instructions[y][x];
                    TheGrid[x, y] = c;
                    if (c == '<' || c == '>' || c == '^' || c == 'v')
                    {
                        Wagons.Add(new Wagon(x, y, c));
                        TheGrid[x, y] = '-';
                    }
                }
            }
            bool Safe = true;
            int DebugIterator = 0;
            List<Wagon> CrashWagons = new List<Wagon>();
            while (Wagons.Count>1)
            {
                DebugIterator++;
                Wagons = Wagons.OrderBy(s => s.y).ThenBy(s => s.x).ToList();
                foreach (Wagon w in Wagons)
                {
                    if (Wagons.Contains(w.PeekPush()))
                    {
                        Sum = w.PeekPush();
                        CrashWagons.Add(Wagons.Find(w.PeekPush().Equals));
                        CrashWagons.Add(w);
                    }
                    w.Push();
                    char c = TheGrid[w.x, w.y];
                    switch (c)
                    {
                        case '+':
                            w.Choice();
                            break;
                        case '-':
                        case '|':
                            ;
                            break;
                        case ' ':
                            ;//for debug
                            break;
                        default: //I guess corner then
                            w.Turn(c);
                            break;
                    }
                }
                foreach (Wagon w in CrashWagons)
                {
                    Wagons.Remove(w);
                }
                CrashWagons.Clear();
            }
            Sum2 = Wagons.First();
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
        public class Wagon : Coordinate
        {
            Coordinate Direction;
            int TurnNumber;
            public Wagon(int x, int y, char direction) : base(x, y)
            {
                switch (direction)
                {
                    case '<':
                        Direction = new Coordinate(-1, 0);
                        break;
                    case '^':
                        Direction = new Coordinate(0, -1);
                        break;
                    case '>':
                        Direction = new Coordinate(1, 0);
                        break;
                    case 'v':
                        Direction = new Coordinate(0, 1);
                        break;
                    default:
                        break;
                }
                TurnNumber = 1;
            }
            public void Push()
            {
                this.AddTo(Direction);
            }
            public Coordinate PeekPush()
            {
                return this.GetSum(Direction);
            }
            public void Choice()
            {
                switch (TurnNumber)
                {
                    case 1: // left
                        int LeftX = Direction.y;
                        int LeftY = Direction.x * -1;
                        Direction = new Coordinate(LeftX, LeftY);
                        break;
                    case 2: // straight
                        ;
                        break;
                    case 3: // right
                        int RightX = Direction.y * -1;
                        int RightY = Direction.x;
                        Direction = new Coordinate(RightX, RightY);
                        break;
                    default:
                        break;
                }
                TurnNumber++;
                if (TurnNumber > 3)
                    TurnNumber = 1;
            }
            public void Turn(char c)
            {
                switch (c)
                {
                    case '\\':
                        Direction = new Coordinate(Direction.y, Direction.x);
                        break;
                    case '/':
                        int NewX = Direction.y * -1;
                        int NewY = Direction.x * -1;
                        Direction = new Coordinate(NewX, NewY);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
