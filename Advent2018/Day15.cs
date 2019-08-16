using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RoyT.AStar;

namespace Advent2018
{
    public class Day15 : Day
    {
        string[] Instructions;
        string[,] Map;
        Dictionary<string, Fighter>[] Fighters;
        Grid grid;
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
            grid = new Grid(Map.GetLength(0), Map.GetLength(1), 1.0f);
            Fighters = new Dictionary<string, Fighter>[2];
            Fighters[0] = new Dictionary<string, Fighter>();
            Fighters[1] = new Dictionary<string, Fighter>();
            int DictionaryIndex = 0;
            for(x = 0; x < Map.GetLength(0); x++)
            {
                for (y = 0; y < Map.GetLength(1); y++)
                {
                    if (Map[x, y] != ".")
                    {
                        grid.BlockCell(new Position(x, y));
                        if (Map[x, y] == "E")
                        {
                            Map[x, y] = Map[x, y] + DictionaryIndex.ToString();
                            Fighters[0].Add(Map[x, y], new Fighter(3, 200, x, y));
                        }
                        else if (Map[x, y] == "G")
                        {
                            Map[x, y] = Map[x, y] + DictionaryIndex.ToString();
                            Fighters[1].Add(Map[x, y], new Fighter(3, 200, x, y));
                        }
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
            int Rounds = 0;
            while (Fighters[0].Count > 0 && Fighters[1].Count > 0)
            {
                Rounds++;
                List<string> MovedPieces = new List<string>();
                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    for (int y = 0; y < Map.GetLength(0); y++)
                    {
                        if (!MovedPieces.Contains(Map[x, y]))
                        {
                            if (Map[x, y][0] == 'E')
                            {
                                Act(0, x, y);
                                MovedPieces.Add(Map[x, y]);
                            }
                            else if (Map[x, y][0] == 'G')
                            {
                                Act(1, x, y);
                                MovedPieces.Add(Map[x, y]);
                            }
                        }
                    }
                }

            }
            int WinningTeam = 0;
            if (Fighters[0].Count == 0)
                WinningTeam = 1;
            int RemainingHp = 0;
            foreach(KeyValuePair<string, Fighter> winner in Fighters[WinningTeam])
            {
                RemainingHp += winner.Value.Hp;
            }
            return (Rounds * RemainingHp).ToString();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
        public void Act(int FighterType, int x, int y)
        {
            Fighter CurrentFighter = Fighters[FighterType][Map[x, y]];
            List<Coordinate> Neighbours = CurrentFighter.GetAllNeighbours(Map);
            Dictionary<string, Fighter> Enemies = new Dictionary<string, Fighter>();
            char EnemyLetter = 'G';
            if (FighterType == 1)
                EnemyLetter = 'E';
            int EnemyType = (FighterType + 1) % 2;
            foreach (Coordinate neighbour in Neighbours)
            {
                if (Map[neighbour.x, neighbour.y][0] == EnemyLetter) //find neighbour enemy
                {
                    Enemies.Add(Map[neighbour.x, neighbour.y], Fighters[EnemyType][Map[neighbour.x, neighbour.y]]);
                }
            }
            if (Enemies.Count >= 1)
            {
                //Fight
                Enemies.OrderBy(s => s.Value.Hp).ThenBy(s => s.Value.LocalPosition.x).ThenBy(s => s.Value.LocalPosition.y);
                if (!Fighters[EnemyType][Enemies.First().Key].TakeHit(CurrentFighter.AttackPower)) //if the target doesnt survive
                {
                    Coordinate RemoveThis = new Coordinate(Fighters[EnemyType][Enemies.First().Key].LocalPosition.x, Fighters[EnemyType][Enemies.First().Key].LocalPosition.y);
                    Map[RemoveThis.x, RemoveThis.y] = ".";
                    grid.UnblockCell(new Position(RemoveThis.x, RemoveThis.y));
                    Fighters[EnemyType].Remove(Enemies.First().Key); //Arghh!!
                }
            }
            else
            {
                //walk
                if (CurrentFighter.HasFreeNeighbours(Map))  //can we walk
                {
                    //to what position do we want to walk
                    List<Coordinate> EnemyPositions = new List<Coordinate>();
                    foreach (KeyValuePair<string, Fighter> target in Fighters[(FighterType+1)%2])
                    {
                        EnemyPositions.Add(new Coordinate(target.Value.LocalPosition.x, target.Value.LocalPosition.y));
                    }
                    Coordinate ClosestTarget = CurrentFighter.GetClosestTarget(EnemyPositions, grid);
                    if (!(ClosestTarget.x == -1))
                    {
                        //what square is next
                        Coordinate NextSquare = CurrentFighter.GetNextSquare(ClosestTarget, grid, Map);
                        //walk
                        UpdatePosition(CurrentFighter.LocalPosition, NextSquare, FighterType);
                    }
                }
                else
                {
                    //Fuck it
                }
            }

        }
        public void UpdatePosition(Coordinate from, Coordinate to, int fighterType)
        {
            string Current = Map[from.x, from.y];
            Map[to.x, to.y] = Current;
            Map[from.x, from.y] = ".";
            Fighters[fighterType][Current].LocalPosition = to;
            grid.BlockCell(new Position(to.x, to.y));
            grid.UnblockCell(new Position(from.x,from.y));
        }
    }
    public class Fighter
    {
        public int AttackPower;
        public int Hp;
        public Coordinate LocalPosition;
        public Fighter(int attackPower, int hp, int x, int y)
        {
            AttackPower = attackPower;
            Hp = hp;
            LocalPosition = new Coordinate(x, y);
        }
        public bool TakeHit(int attackPower)
        {
            Hp -= attackPower;
            return Hp > 0;
        }
        public Coordinate GetClosestTarget(List<Coordinate> targets, Grid grid)
        {
            List<Position[]> Resultset = new List<Position[]>();
            foreach (Coordinate target in targets)
            {
                grid.UnblockCell(new Position(LocalPosition.x, LocalPosition.y));
                grid.UnblockCell(new Position(target.x, target.y));
                Position[] _path = grid.GetPath(new Position(LocalPosition.x, LocalPosition.y), new Position(target.x, target.y), MovementPatterns.LateralOnly);
                if (_path.Length >0)
                    Resultset.Add(_path);
                grid.BlockCell(new Position(LocalPosition.x, LocalPosition.y));
                grid.BlockCell(new Position(target.x, target.y));
                ;
            }
            Resultset.Sort((x, y) => x.Length.CompareTo(y.Length));
            if (Resultset.Count == 0)
                return new Coordinate(-1, -1);
            return new Coordinate(Resultset[0].Last().X, Resultset[0].Last().Y);
        }
        public Coordinate GetNextSquare(Coordinate target, Grid grid, string[,] map)
        {
            List<Coordinate> Alternatives = this.GetFreeNeighbours(map);
            List<Position[]> Paths = new List<Position[]>();
            List<Coordinate> GoodAlternatives = new List<Coordinate>();
            int BestestDistance = 1000000;
            foreach (Coordinate alternative in Alternatives)
            {
                grid.UnblockCell(new Position(LocalPosition.x, LocalPosition.y));
                grid.UnblockCell(new Position(target.x, target.y));
                Position[] Path = grid.GetPath(new Position(alternative.x, alternative.y), new Position(target.x, target.y), MovementPatterns.LateralOnly);
                if (Path.Length < BestestDistance)
                {
                    BestestDistance = Path.Length;
                }
                Paths.Add(Path);
                grid.BlockCell(new Position(LocalPosition.x, LocalPosition.y));
                grid.BlockCell(new Position(target.x, target.y));
            }
            foreach (Position[] p in Paths)
            {
                if (p.Length == BestestDistance && p.Length > 0)
                {
                    GoodAlternatives.Add(new Coordinate(p[0].X, p[0].Y));
                }
            }
            GoodAlternatives.OrderBy(s => s.x).ThenBy(s => s.y);
            return GoodAlternatives[0];
        }
        public bool HasFreeNeighbours(string[,] map)
        {
            return GetFreeNeighbours(map).Count != 0;
        }
        public List<Coordinate> GetAllNeighbours(string[,] map)
        {
            List<Coordinate> directions = new List<Coordinate>();
            directions.Add(new Coordinate(1, 0));
            directions.Add(new Coordinate(-1, 0));
            directions.Add(new Coordinate(0, 1));
            directions.Add(new Coordinate(0, -1));
            List<Coordinate> returnList = new List<Coordinate>();
            foreach (Coordinate d in directions)
            {
                Coordinate neighbour = LocalPosition.GetSum(d);
                if (neighbour.IsInPositiveBounds(map.GetLength(0),map.GetLength(1)))
                    returnList.Add(neighbour);
            }
            return returnList;
        }
        public List<Coordinate> GetFreeNeighbours(string[,] map)
        {
            List<Coordinate> returnList = new List<Coordinate>();
            List<Coordinate> Neighbours = this.GetAllNeighbours(map);
            foreach(Coordinate neighbour in Neighbours)
            {
                if (map[neighbour.x, neighbour.y] == ".")
                    returnList.Add(neighbour);
            }
            return returnList;
        }

    }
}
//using RoyT.AStar;

//// Create a new grid and let each cell have a default traversal cost of 1.0
//var grid = new Grid(100, 100, 1.0f);

//// Block some cells (for example walls)
//grid.BlockCell(new Position(5, 5))

//// Make other cells harder to traverse (for example water)
//grid.SetCellCost(new Position(6, 5), 3.0f);

//// And finally start the search for the shortest path form start to end
//Position[] path = grid.GetPath(new Position(0, 0), new Position(99, 99));
