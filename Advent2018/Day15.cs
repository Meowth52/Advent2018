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
            Map = new string[Instructions.Length, Instructions[0].Length];
            int x = 0;
            int y = 0;
            foreach (string s in Instructions)
            {
                foreach (char c in s)
                {
                    Map[x, y] = c.ToString();
                    x += 1;
                }
                x = 0;
                y += 1;
            }
            grid = new Grid(Map.GetLength(0), Map.GetLength(1), 1.0f);
            Fighters = new Dictionary<string, Fighter>[2];
            Fighters[0] = new Dictionary<string, Fighter>();
            Fighters[1] = new Dictionary<string, Fighter>();
            int DictionaryIndex = 0;
            for (y = 0; y < Map.GetLength(1); y++)
            {
                for (x = 0; x < Map.GetLength(0); x++)
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
            string Test = "";
            int Rounds = 0;
            while (Fighters[0].Count > 0 && Fighters[1].Count > 0)
            {
                Rounds++;
                string TestOutput = "";
                Coordinate LastFucker = new Coordinate(0,0);
                for (int y = Map.GetLength(1)-1; y >= 0; y--)
                {
                    for (int x = Map.GetLength(0)-1; x >= 0; x--)
                    {
                        if (Map[x, y][0] == 'E' || Map[x, y][0] == 'G')
                        {
                            LastFucker = new Coordinate(x, y);
                            goto Found;
                        }
                    }
                }
                Found:
                List<string> MovedPieces = new List<string>();
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    for (int x = 0; x < Map.GetLength(0); x++)
                    {
                        if (!MovedPieces.Contains(Map[x, y]))
                        {
                            if (Map[x, y][0] == 'E')
                            {
                                MovedPieces.Add(Map[x, y]);
                                Act(0, x, y);
                            }
                            else if (Map[x, y][0] == 'G')
                            {
                                MovedPieces.Add(Map[x, y]);
                                Act(1, x, y);
                            }
                            if (!LastFucker.IsOn(new Coordinate(x, y)) && (Fighters[0].Count == 0 || Fighters[1].Count == 0))
                            {
                                Rounds--;
                                goto GetOut;
                            }
                            if (LastFucker.IsOn(new Coordinate(x, y)) && (Fighters[0].Count == 0 || Fighters[1].Count == 0))
                            {
                                goto GetOut;
                            }
                        }
                    }
                }                
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    for (int x = 0; x < Map.GetLength(0); x++)
                    {
                        TestOutput += Map[x, y][0].ToString();                        
                    }                    
                    TestOutput += "\n";
                }
                //Console.Write("Round " + Rounds.ToString() + "\n" + TestOutput + "\n");
                Test = TestOutput;
            }
            GetOut:
            int WinningTeam = 0;
            if (Fighters[0].Count == 0)
                WinningTeam = 1;
            int RemainingHp = 0;
            foreach (KeyValuePair<string, Fighter> winner in Fighters[WinningTeam])
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
                string Victim = Enemies.OrderBy(s => s.Value.Hp).ThenBy(s => s.Value.LocalPosition.y).ThenBy(s => s.Value.LocalPosition.x).First().Key;
                if (!Fighters[EnemyType][Victim].TakeHit(CurrentFighter.AttackPower)) //if the target doesnt survive
                {
                    Coordinate RemoveThis = new Coordinate(Fighters[EnemyType][Victim].LocalPosition.x, Fighters[EnemyType][Victim].LocalPosition.y);
                    Map[RemoveThis.x, RemoveThis.y] = ".";
                    grid.UnblockCell(new Position(RemoveThis.x, RemoveThis.y));
                    Fighters[EnemyType].Remove(Victim); //Arghh!!
                }
            }
            else
            {
                //walk
                if (CurrentFighter.HasFreeNeighbours(Map))  //can we walk
                {
                    //to what position do we want to walk
                    List<Coordinate> EnemyPositions = new List<Coordinate>();
                    foreach (KeyValuePair<string, Fighter> target in Fighters[(FighterType + 1) % 2])
                    {
                        EnemyPositions.AddRange(target.Value.GetFreeNeighbours(Map));
                    }
                    Coordinate ClosestTarget = CurrentFighter.GetClosestTarget(EnemyPositions, grid);
                    if (!(ClosestTarget.x == -1))
                    {
                        //what square is next
                        Coordinate NextSquare = CurrentFighter.GetNextSquare(ClosestTarget, grid, Map);
                        //walk
                        UpdatePosition(CurrentFighter.LocalPosition, NextSquare, FighterType);
                    }

                    Enemies.Clear();
                    Neighbours = CurrentFighter.GetAllNeighbours(Map);
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
                        string Victim = Enemies.OrderBy(s => s.Value.Hp).ThenBy(s => s.Value.LocalPosition.y).ThenBy(s => s.Value.LocalPosition.x).First().Key;
                        if (!Fighters[EnemyType][Victim].TakeHit(CurrentFighter.AttackPower)) //if the target doesnt survive
                        {
                            Coordinate RemoveThis = new Coordinate(Fighters[EnemyType][Victim].LocalPosition.x, Fighters[EnemyType][Victim].LocalPosition.y);
                            Map[RemoveThis.x, RemoveThis.y] = ".";
                            grid.UnblockCell(new Position(RemoveThis.x, RemoveThis.y));
                            Fighters[EnemyType].Remove(Victim); //Arghh!!
                        }
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
            grid.UnblockCell(new Position(from.x, from.y));
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
                Position[] _path = grid.GetPath(new Position(LocalPosition.x, LocalPosition.y), new Position(target.x, target.y), MovementPatterns.LateralOnly);
                if (_path.Length > 0)
                    Resultset.Add(_path);
                grid.BlockCell(new Position(LocalPosition.x, LocalPosition.y));
                ;
            }
            if (Resultset.Count == 0)
                return new Coordinate(-1, -1);
            Position CLosestTarget = Resultset.OrderBy(s => s.Length).ThenBy(s => s.Last().Y).ThenBy(s => s.Last().X).First().Last();
            return new Coordinate(CLosestTarget.X, CLosestTarget.Y);
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
                Position[] Path = grid.GetPath(new Position(alternative.x, alternative.y), new Position(target.x, target.y), MovementPatterns.LateralOnly);
                if (Path.Length < BestestDistance)
                {
                    BestestDistance = Path.Length;
                }
                Paths.Add(Path);
                grid.BlockCell(new Position(LocalPosition.x, LocalPosition.y));
            }
            foreach (Position[] p in Paths)
            {
                if (p.Length == BestestDistance && p.Length > 0)
                {
                    GoodAlternatives.Add(new Coordinate(p[0].X, p[0].Y));
                }
            }
            Coordinate TheOne = GoodAlternatives.OrderBy(s => s.y).ThenBy(s => s.x).First();
            return TheOne;
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
                if (neighbour.IsInPositiveBounds(map.GetLength(0), map.GetLength(1)))
                    returnList.Add(neighbour);
            }
            return returnList;
        }
        public List<Coordinate> GetFreeNeighbours(string[,] map)
        {
            List<Coordinate> returnList = new List<Coordinate>();
            List<Coordinate> Neighbours = this.GetAllNeighbours(map);
            foreach (Coordinate neighbour in Neighbours)
            {
                if (map[neighbour.x, neighbour.y] == ".")
                    returnList.Add(neighbour);
            }
            return returnList;
        }

    }
}
