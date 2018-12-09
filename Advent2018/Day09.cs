using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day09 : Day
    {
        List<string[]> Instruction;
        int NrOfPlayers = 0;
        int LastMarble = 0;
        public Day09(string _input) : base(_input)
        {
            Instruction = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            Int32.TryParse(Instruction[0][0], out NrOfPlayers);
            Int32.TryParse(Instruction[0][6], out LastMarble);
            long Sum = getPartOneInt();
            LastMarble *= 100;
            long Sum2 = getPartOneInt();
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public long getPartOneInt()
        {
            long Sum = 0;
            LinkedList<int> TheCircle = new LinkedList<int>();
            TheCircle.AddFirst(0);
            TheCircle.AddLast(1);
            LinkedListNode<int> Current = TheCircle.Last;
            int CurrentPlayer = 2;
            List<long> PlayerScores = new List<long>();
            for (int i = 0; i <= NrOfPlayers; i++)
            {
                PlayerScores.Add(0);
            }
            for (int i = 2; i <= LastMarble; i++)
            {
                ;
                if (i % 23 == 0)
                {
                    PlayerScores[CurrentPlayer] += i;
                    pushTheIndex(ref Current, -7, false);
                    PlayerScores[CurrentPlayer] += Current.Value;
                    LinkedListNode<int> RemoveThis = Current;
                    pushTheIndex(ref Current, 1, true);
                    TheCircle.Remove(RemoveThis);
                }
                else
                {
                    pushTheIndex(ref Current, 1, true);
                    TheCircle.AddAfter(Current, i);
                    Current = Current.Next;
                }
                CurrentPlayer++;
                if (CurrentPlayer > NrOfPlayers)
                {
                    CurrentPlayer = 1;
                }
            }
            foreach (long i in PlayerScores)
            {
                if (i > Sum)
                    Sum = i;
            }
            return Sum;
        }
        public override string getPartOne()
        {
            throw new NotImplementedException();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
        public void pushTheIndex(ref LinkedListNode<int> Current, int NrOfSteps, bool Positive)
        {
            if (Positive)
                for (int i = 0; i < NrOfSteps; i++)
                    Current = Current.Next ?? Current.List.First;
            else
                for (int i = 0; i > NrOfSteps; i--)
                    Current = Current.Previous ?? Current.List.Last;
        }
    }
}