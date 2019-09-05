using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RoyT.AStar;

namespace Advent2018
{
    public class Day20 : Day
    {
        string Input;
        Grid TheGrid;
        List<Coordinate> Rooms;
        int Max;
        public Day20(string _input) : base(_input)
        {
            Input = _input;
        }
        public override Tuple<string, string> getResult()
        {
            Max = 500;
            TheGrid = new Grid(Max, Max, 2.0f);
            Stack<Coordinate> NestStack = new Stack<Coordinate>();
            Rooms = new List<Coordinate>();
            Coordinate E = new Coordinate(0, 1);
            Coordinate W = new Coordinate(0, -1);
            Coordinate N = new Coordinate(1, 0);
            Coordinate S = new Coordinate(-1, 0);
            List<Coordinate> CurrentPositions = new List<Coordinate>();
            List<List<Coordinate>> NextPositions = new List<List<Coordinate>>();
            CurrentPositions.Add(new Coordinate(Max / 2, Max / 2));
            char LastChar = ' ';
            foreach (char c in Input)
            {
                bool Poppipop = false;
                bool UselessBranch = false;
                bool PushiPsush = false;
                Coordinate NextPosition = new Coordinate(0,0);
                foreach (Coordinate CurrentPosition in CurrentPositions)
                {
                    //Coordinate NextPosition = new Coordinate(CurrentPosition);
                    switch (c)
                    {
                        case 'E':
                            CurrentPosition.AddTo(E);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            CurrentPosition.AddTo(E);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            if (!Rooms.Contains(CurrentPosition))
                                Rooms.Add(new Coordinate(CurrentPosition));
                            break;
                        case 'W':
                            CurrentPosition.AddTo(W);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            CurrentPosition.AddTo(W);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            if (!Rooms.Contains(CurrentPosition))
                                Rooms.Add(new Coordinate(CurrentPosition));
                            break;
                        case 'N':
                            CurrentPosition.AddTo(N);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            CurrentPosition.AddTo(N);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            if (!Rooms.Contains(CurrentPosition))
                                Rooms.Add(new Coordinate(CurrentPosition));
                            break;
                        case 'S':
                            CurrentPosition.AddTo(S);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            CurrentPosition.AddTo(S);
                            TheGrid.SetCellCost(CurrentPosition.GetPosition(), 1);
                            if (!Rooms.Contains(CurrentPosition))
                                Rooms.Add(new Coordinate(CurrentPosition));
                            break;
                        case '(':
                            PushiPsush = true;
                            NextPosition = new Coordinate(CurrentPosition);
                            break;
                        case ')':
                            Poppipop = true;
                            UselessBranch = LastChar == '|';
                            NextPositions.Last().Add(new Coordinate(CurrentPosition));
                            CurrentPosition.Assimilate(NestStack.Peek());
                            //if (LastChar != '|')
                            //    NextPosition = new Coordinate( NestStack.Peek());
                            //else
                            //    NextPositions.Add(new Coordinate( NestStack.Peek()));
                            break;
                        case '|':
                            NextPositions.Last().Add(new Coordinate(CurrentPosition));
                            CurrentPosition.Assimilate(NestStack.Peek());
                            break;
                        default:
                            break;

                    }
                    LastChar = c;
                }
                if (PushiPsush)
                {
                    NextPositions.Add(new List<Coordinate>());
                    NestStack.Push(new Coordinate(NextPosition));
                }
                if (Poppipop)
                {
                    if (!UselessBranch)
                        CurrentPositions.AddRange(new List<Coordinate>(NextPositions.Last()));
                    NextPositions.Remove(NextPositions.Last());
                    NestStack.Pop();
                }
            }
            StringBuilder OutString = new StringBuilder();
            for (int x = 0; x < Max; x++)
            {
                for (int y = 0; y < Max; y++)
                {
                    if (TheGrid.GetCellCost(new Position(x, y)) != 1)
                        TheGrid.BlockCell(new Position(x, y));
                }
            }
            for (int x = Max / 2 + 50; x > Max / 2 - 50; x--)
            {
                for (int y = Max / 2 - 50; y < Max / 2 + 50; y++)
                {
                    if (x == Max / 2 && y == Max / 2)
                    {
                        OutString.Append("X");
                    }
                    else
                    if (TheGrid.GetCellCost(new Position(x, y)) == 1)
                    {
                        OutString.Append(".");
                    }
                    else
                    {
                        OutString.Append("#");
                    }
                }
                OutString.Append("\r\n");
            }
            int Sum = 0;
            int Sum2 = 0;
            //return Tuple.Create(OutString.ToString(),"");
            return Tuple.Create(getPartOne(), Sum2.ToString() + "\n" + OutString.ToString());
        }

        public override string getPartOne()
        {
            int MaximumLenght = 0;
            foreach (Coordinate r in Rooms)
            {
                Position[] Path = TheGrid.GetPath(new Position(Max / 2, Max / 2), r.GetPosition(), MovementPatterns.LateralOnly);
                if (Path.Count() > MaximumLenght)
                    MaximumLenght = Path.Count();
            }
            return ((MaximumLenght) / 2).ToString();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
    }
}
