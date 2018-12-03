using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day03 : Day
    {
        List<string[]> Instructions = new List<string[]>();
        public Day03(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            Dictionary<int,Field> Fields = new Dictionary<int, Field>();
            foreach(string[] s in Instructions)
            {
                int ID = 0;
                Int32.TryParse(s[0].Replace("#", ""), out ID);
                Fields.Add(ID, new Field(s));
            }
            int[,] Fabric = new int[1000, 1000];
            for (int x = 0; x < 1000; x++)
                for (int y = 0; y < 1000; y++)
                    Fabric[x, y] = 0;
            foreach(KeyValuePair<int,Field> k in Fields)
            {
                Coordinate TopLeft = k.Value.getTopLeft();
                Coordinate BottomRight = k.Value.getBottomRight();
                for (int x = TopLeft.x; x < BottomRight.x; x++)
                {
                    for (int y = TopLeft.y; y < BottomRight.y; y++)
                    {
                        Fabric[x, y]++;
                    }
                }

            }
            foreach (KeyValuePair<int, Field> k in Fields)
            {
                Coordinate TopLeft = k.Value.getTopLeft();
                Coordinate BottomRight = k.Value.getBottomRight();
                bool IsThisIt = true;
                for (int x = TopLeft.x; x < BottomRight.x; x++)
                {
                    for (int y = TopLeft.y; y < BottomRight.y; y++)
                    {
                        if (Fabric[x, y] > 1)
                            IsThisIt = false;
                    }
                }
                if(IsThisIt)
                {
                    Sum2 = k.Key;
                    break;
                }
            }
            for (int x = 0; x < 1000; x++)
                for (int y = 0; y < 1000; y++)
                    if (Fabric[x, y] > 1)
                        Sum++;
            // part 2

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
    public class Field
    {
        Coordinate Position;
        Coordinate Size;
        public Field(string[] s)
        {
            int x = 0;
            int y = 0;
            string[] ParseCoordinate = s[2].Replace(":", "").Split(',');
            Int32.TryParse(ParseCoordinate[0], out x);
            Int32.TryParse(ParseCoordinate[1], out y);
            Position = new Coordinate(x, y);
            x = 0;
            y = 0;
            ParseCoordinate = s[3].Split('x');
            Int32.TryParse(ParseCoordinate[0], out x);
            Int32.TryParse(ParseCoordinate[1], out y);
            Size = new Coordinate(x, y);
        }
        public Coordinate getTopLeft()
        {
            return new Coordinate(Position);
        }
        public Coordinate getBottomRight()
        {
            return Position.GetSum(Size);
        }
    }
}
