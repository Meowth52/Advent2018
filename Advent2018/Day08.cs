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
        Node FullStack;
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
            FullStack = new Node(SystemLicenceFile);
            string Sum = getPartOne();
            string Sum2 = getPartTwo();
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            int Sum = FullStack.getEntrySum();
            return Sum.ToString();
        }
        public override string getPartTwo()
        {
            int Sum2 = FullStack.getNodeValue();
            return Sum2.ToString();
        }
    }
    public class Node
    {
        public int Children;
        public int DataEntries;
        public List<int> Entries;
        public List<Node> ChildrenNodes;
        public List<int> SystemLicenceFile;
        public Node(List<int> _SystemLicenceFile)
        {
            SystemLicenceFile = _SystemLicenceFile;
            Children = SystemLicenceFile[0];
            DataEntries = SystemLicenceFile[1];
            Entries = new List<int>();
            ChildrenNodes = new List<Node>();
            SystemLicenceFile.RemoveRange(0, 2);
            for (int i = 0; i < Children; i++)
            {
                ChildrenNodes.Add(new Node(SystemLicenceFile));
                SystemLicenceFile = ChildrenNodes[i].getSystemLicenceFile();
            }
            for (int i = 0; i < DataEntries; i++)
            {
                Entries.Add(SystemLicenceFile[i]);
            }
            SystemLicenceFile.RemoveRange(0, DataEntries);
        }
        public List<int> getSystemLicenceFile()
        {
            return SystemLicenceFile;
        }
        public int getEntrySum()
        {
            int EntrySum = 0;
            foreach(int i in Entries)
            {
                EntrySum += i;
            }
            foreach(Node n in ChildrenNodes)
            {
                EntrySum += n.getEntrySum();
            }
            return EntrySum;
        }
        public int getNodeValue()
        {
            int NodeValue = 0;
            if (Children == 0)
            {
                foreach (int i in Entries)
                {
                    NodeValue += i;
                }
            }
            else
            {
                foreach (int i in Entries)
                {
                    if (i < ChildrenNodes.Count && i>0)
                        NodeValue += ChildrenNodes[i-1].getNodeValue();
                }
            }
            return NodeValue;
        }
    }
}
