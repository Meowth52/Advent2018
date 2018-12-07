
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018
{
    public class Day07 : Day
    {
        List<string[]> Instructions;
        public Day07(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            string Sum = getPartOne();
            string Sum2 = getPartTwo();
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            string Sum = "";
            List<string> AssemblyInstructions = new List<string>();
            List<char> Steps = new List<char>();
            foreach (string[] s in Instructions)
            {
                AssemblyInstructions.Add("" + s[7][0] + s[1][0]);
                if (!Steps.Contains(s[1][0]))
                {
                    Steps.Add(s[1][0]);
                }
                if (!Steps.Contains(s[7][0]))
                {
                    Steps.Add(s[7][0]);
                }
            }
            AssemblyInstructions.Sort();
            Steps.Sort();

            List<string> RemoveIndex = new List<string>();
            while (Steps.Count > 0)
            {
                char RemoveChar = '_';
                foreach (char c in Steps)
                {
                    bool IsReady = true;
                    foreach (string s in AssemblyInstructions)
                    {
                        if (s[0] == c)
                        {
                            if (Sum.Contains(s[1]))
                            {
                                RemoveIndex.Add(s);
                            }
                            else
                            {
                                IsReady = false;
                            }
                        }
                        else
                        {
                            ;
                        }

                    }
                    if (IsReady)
                    {
                        Sum += c;
                        RemoveChar = c;
                        foreach (string s in RemoveIndex)
                        {
                            AssemblyInstructions.Remove(s);
                        }
                        RemoveIndex.Clear();
                        break;
                    }
                    else
                    {
                        RemoveIndex.Clear();
                    }
                }
                Steps.Remove(RemoveChar);
            }
            return Sum;
        }
        public override string getPartTwo()
        {
            int NumberOfWorkers = 2;
            int ArbetaryTime = 0;
            int Sum2 = 0;
            string Sleigh = "";
            Dictionary<char, int> Workers = new Dictionary<char, int>();
            List<string> AssemblyInstructions = new List<string>();
            List<char> Steps = new List<char>();
            foreach (string[] s in Instructions)
            {
                AssemblyInstructions.Add("" + s[7][0] + s[1][0]);
                if (!Steps.Contains(s[1][0]))
                {
                    Steps.Add(s[1][0]);
                }
                if (!Steps.Contains(s[7][0]))
                {
                    Steps.Add(s[7][0]);
                }
            }
            AssemblyInstructions.Sort();
            Steps.Sort();
            List<string> RemoveIndex = new List<string>();
            while (Steps.Count > 0 || Workers.Count > 0)
            {
                Sum2++;
                List<char> RemoveChar = new List<char>();
                foreach (char c in Steps)
                {
                    bool IsReady = true;
                    foreach (string s in AssemblyInstructions)
                    {
                        if (s[0] == c)
                        {
                            if (Sleigh.Contains(s[1]))
                            {
                                RemoveIndex.Add(s);
                            }
                            else
                            {
                                IsReady = false;
                            }
                        }
                        else
                        {
                            ;
                        }

                    }
                    if (IsReady)
                    {
                        if (Workers.Count < NumberOfWorkers)
                        {
                            foreach(string s in RemoveIndex)
                            {
                                if (!Workers.ContainsKey(s[0]))
                                {
                                    Workers.Add(c, ArbetaryTime + (int)c - 64);
                                    RemoveChar.Add(s[0]);
                                }
                            }
                        }
                        foreach (string s in RemoveIndex)
                        {
                            AssemblyInstructions.Remove(s);
                        }
                        RemoveIndex.Clear();
                        break;
                    }
                    else
                    {
                        RemoveIndex.Clear();
                    }
                }
                foreach(char c in RemoveChar)
                    Steps.Remove(c);
                List<char> WorkersDone = new List<char>();
                foreach (KeyValuePair<char, int> Worker in Workers)
                {
                    if (Worker.Value == 0)
                        WorkersDone.Add(Worker.Key);
                }
                for (int i = 0; i < Workers.Count; i++)
                {
                    Workers[Workers.ElementAt(i).Key]--;
                }
                foreach (char c in WorkersDone)
                {
                    Sleigh += c;
                    Workers.Remove(c);
                }
                WorkersDone.Clear();
            }
            return Sum2.ToString();
        }
    }
}