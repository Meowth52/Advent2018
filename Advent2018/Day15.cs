using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day15 : Day
    {
        string[] Instructions;
        string[,] Map;
        Dictionary<string, Fighter> Elves;
        Dictionary<string, Fighter> Goblins;
        public Day15(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            Map = new string[Instructions.Length,Instructions[0].Length];
            int x = 0;
            int y = 0;
            foreach (string s in Instructions)
            {
                foreach (char c in s)
                {
                    Map[x, y] = c.ToString();
                    y += 1;
                }
                y = 0;
                x += 1;
            }
            Elves = new Dictionary<string, Fighter>();
            Goblins = new Dictionary<string, Fighter>();
            int DictionaryIndex = 0;
            for(x = 0; x < Map.GetLength(0); x++)
            {
                for (y = 0; y < Map.GetLength(0); y++)
                {
                    if (Map[x,y] == "E")
                    {
                        Map[x, y] = Map[x, y] + DictionaryIndex.ToString();
                        Elves.Add(Map[x, y], new Fighter(3, 200,x,y));
                    }
                    else if (Map[x, y] == "G")
                    {
                        Map[x, y] = Map[x, y] + DictionaryIndex.ToString();
                        Goblins.Add(Map[x, y], new Fighter(3, 200,x,y));
                    }
                    DictionaryIndex++;
                }
            }
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            string Returnstring = "";
            foreach (string s in Map)
                Returnstring += s;
            return Tuple.Create(getPartOne(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            while (Elves.Count > 0 && Goblins.Count > 0)
            {
                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    for (int y = 0; y < Map.GetLength(0); y++)
                    {
                        if (Map[x, y][0] == 'E')
                        {

                        }
                        else if (Map[x, y][0] == 'G')
                        {

                        }
                    }
                }

            }
            throw new NotImplementedException();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
    }
    public class Fighter
    {
        int AttackPower;
        int Hp;
        Coordinate Position;
        public Fighter(int attackPower, int hp, int x, int y)
        {
            AttackPower = attackPower;
            Hp = hp;
            Position = new Coordinate(x, y);
        }
        public Coordinate GetClosestTarget()
        {            
            return ;
        }
        public Coordinate GetNextSquare()
        {
            return;
        }
        public List<Coordinate> GetFreeNeighbours(string[,] map)
        {
            List<Coordinate> directions = new List<Coordinate>();
            directions.Add(new Coordinate(1,0));
            directions.Add(new Coordinate(-1, 0));
            directions.Add(new Coordinate(0, 1));
            directions.Add(new Coordinate(0, -1));
            List<Coordinate> returnList = new List<Coordinate>();

            return;
        }

    }
}
