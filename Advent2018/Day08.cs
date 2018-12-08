using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day08 : Day
    {
        List<string[]> Instructions;
        List<int> SystemLicenceFile;
        Dictionary<int,Scmenum> FullStack;
        public Day08(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            SystemLicenceFile = new List<int>();
            foreach (string s in Instructions[0])
            {
                int TryParseInt = 0;
                Int32.TryParse(s, out TryParseInt);
                SystemLicenceFile.Add(TryParseInt);
            }
            FullStack = new Dictionary<int, Scmenum>();
            string Sum = getPartOne();
            string Sum2 = getPartTwo();
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            int Sum = 0;
            int Index = 0;
            List<Scmenum> Stack = new List<Scmenum>();
            int SchmenumIndex = 0;
            while (Index < SystemLicenceFile.Count - 1)
            {
                if (Stack.Count == 0 || Stack.Last().Children > 0)
                {
                    if (Stack.Count > 0)
                        Stack.Last().Children--;
                    Stack.Add(new Scmenum(SystemLicenceFile[Index], SystemLicenceFile[Index + 1],SchmenumIndex));
                    FullStack.Add(SchmenumIndex,new Scmenum(SystemLicenceFile[Index], SystemLicenceFile[Index + 1],SchmenumIndex));
                    Index += 2;
                    SchmenumIndex++;                    
                }
                else
                {
                    for (int i = 0; i < Stack.Last().DataEntries; i++)
                    {
                        Sum += SystemLicenceFile[Index];
                        FullStack[Stack[Stack.Count-1].Index].Entries.Add(SystemLicenceFile[Index]);
                        Index++;
                    }
                    Stack.RemoveAt(Stack.Count - 1);
                }
            }
            return Sum.ToString();
        }
        public override string getPartTwo()
        {
            int Sum2 = 0;
            List<int> Index = new List<int>();
            List<int> NextIndex = new List<int>();
            Index.Add(0);
            while (Index.Count > 0)
            {
                foreach (int index in Index)
                {
                    if (index < FullStack.Count)
                    {
                        if (FullStack[index].Children == 0)
                        {
                            foreach (int entry in FullStack[index].Entries)
                            {
                                Sum2 += entry;
                            }
                        }
                        else
                        {
                            foreach (int entry in FullStack[index].Entries)
                            {
                                if(entry!=0)
                                    NextIndex.Add(index+entry);
                            }
                        }
                    }
                }
                Index = new List<int>(NextIndex);
                NextIndex.Clear();
            }
            return Sum2.ToString();
        }
    }
    public class Scmenum
    {
        public int Index;
        public int Children;
        public int DataEntries;
        public List<int> Entries;
        public Scmenum(int _children, int _dataEntries, int _index)
        {
            Children = _children;
            DataEntries = _dataEntries;
            Index = _index;
            Entries = new List<int>();
        }

    }
}
