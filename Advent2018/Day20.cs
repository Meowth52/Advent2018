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
        Grid Grid;
        Coordinate CurrentPosition;
        public Day20(string _input) : base(_input)
        {
            Input = _input;
        }
        public override Tuple<string, string> getResult()
        {
            Grid = new Grid(1000, 1000, 2.0f);
            //for (int x = 0; x<1000;x++)
            //{
            //    for (int y = 0; y<1000;y++)
            //    {
            //        if ()
            //    }
            //}
            CurrentPosition = new Coordinate(500, 500);
            Coordinate E = new Coordinate(0,1);
            Coordinate W = new Coordinate(0, -1);
            Coordinate N = new Coordinate(1, 0);
            Coordinate S = new Coordinate(-1, 0);
            foreach (char c in Input)
            {
                switch (c)
                {
                    case 'E':
                        CurrentPosition.AddTo(E);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        CurrentPosition.AddTo(E);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        break;
                    case 'W':
                        CurrentPosition.AddTo(W);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        CurrentPosition.AddTo(W);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        break;
                    case 'N':
                        CurrentPosition.AddTo(N);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        CurrentPosition.AddTo(N);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        break;
                    case 'S':
                        CurrentPosition.AddTo(S);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        CurrentPosition.AddTo(S);
                        Grid.SetCellCost(CurrentPosition.GetPosition(), 1);
                        break;
                    case '(':
                        ;
                        break;
                    case ')':
                        ;
                        break;
                    case '|':
                        ;
                        break;
                    default:
                        break;
                }
            }
                int Sum = 0;
            int Sum2 = 0;
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
            }
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
