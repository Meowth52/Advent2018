using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{

    public class Day24 : Day
    {
        string[] ImmuneInstructions;
        string[] InfectionInstructions;
        List<Group> ImmuneSystem = new List<Group>();
        List<Group> Infection = new List<Group>();
        public Day24(string _input) : base(_input)
        {
            _input = _input.Replace("Immune System:", "");
            _input = _input.Replace("Infection:", "_");
            string[] PartedInput = _input.Split('_');
            ImmuneInstructions = this.parseStringArray(PartedInput[0]);
            InfectionInstructions = this.parseStringArray(PartedInput[1]);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            int Indexer = 0;
            foreach (string s in ImmuneInstructions)
            {
                ImmuneSystem.Add(new Group(s, Indexer));
                Indexer++;
            }
            Indexer = 0;
            foreach (string s in InfectionInstructions)
            {
                Infection.Add(new Group(s,Indexer));
                Indexer++;
            }
            bool KeepGoing = true;
            Sum = getIteration(ref KeepGoing);
            while (KeepGoing)
            {
                Sum2 = getIteration(ref KeepGoing);
            }
            return Tuple.Create(Sum.ToString(), Sum2.ToString());
        }
        public int getIteration(ref bool KeepGoing)
        {
            int Sum = 0;
            bool ItsOn = true;
            int TotalMembers = 0;
            int NrOfSame = 0;
            while (ItsOn)
            {
                ImmuneSystem = ImmuneSystem.OrderByDescending(s => s.EffectivePower).ThenByDescending(s => s.Initiative).ToList();
                Infection = Infection.OrderByDescending(s => s.EffectivePower).ThenByDescending(s => s.Initiative).ToList();
                List<int> PickedList = new List<int>();
                foreach (Group g in ImmuneSystem)
                {
                    int Index = g.PickTarget(Infection, PickedList);
                    if (Index >= 0)
                        PickedList.Add(Index);
                }
                PickedList.Clear();
                foreach (Group g in Infection)
                {
                    int Index = g.PickTarget(ImmuneSystem, PickedList);
                    if (Index >= 0)
                        PickedList.Add(Index);
                }
                for (int i = 20; i >= 0; i--)
                {
                    foreach (Group g in ImmuneSystem)
                    {
                        if (g.Initiative == i)
                        {
                            foreach (Group v in Infection)
                                if (v.Index == g.PickedTarget)
                                    v.TakeDamage(g.AttackType, g.EffectivePower);
                        }
                    }
                    foreach (Group g in Infection)
                    {
                        if (g.Initiative == i)
                        {
                            foreach (Group v in ImmuneSystem)
                                if (v.Index == g.PickedTarget)
                                    v.TakeDamage(g.AttackType, g.EffectivePower);
                        }
                    }
                }
                bool ImmuneIsOn = false;
                bool InfectionIsOn = false;
                int ThisTotalMembers = 0;
                foreach (Group g in ImmuneSystem)
                {
                    ThisTotalMembers += g.Members;
                    if (g.Members > 0)
                        ImmuneIsOn = true;
                }
                foreach (Group g in Infection)
                {
                    ThisTotalMembers += g.Members;
                    if (g.Members > 0)
                        InfectionIsOn = true;
                }
                ItsOn = ImmuneIsOn && InfectionIsOn;
                if (ThisTotalMembers == TotalMembers)
                    NrOfSame++;
                else
                    NrOfSame = 0;
                if (NrOfSame > 10)
                    ItsOn = false;                
                TotalMembers = ThisTotalMembers;
            }
            foreach (Group g in ImmuneSystem)
            {
                Sum += g.Members;
                g.Attack++;
                g.NextIteration();
            }
            if (Sum > 0)
                KeepGoing = false;
            foreach (Group g in Infection)
            {
                Sum += g.Members;
                g.NextIteration();
            }
            if (NrOfSame > 10)
                KeepGoing = true;
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
    }
    public class Group
    {
        public int Index;
        public int Members;
        public int OriginalMembers;
        public int HP;
        public int Attack;
        public int Initiative;
        public int EffectivePower;
        public string AttackType;
        public int PickedTarget;
        public List<string> Immunities;
        public List<string> Weaknesses;
        public Group(string s, int _index)
        {
            Index = _index;
            MatchCollection Matches = Regex.Matches(s, @"-?\d+");
            List<int> IntList = new List<int>();
            foreach (Match m in Matches)
            {
                int ParseInt = 0;
                Int32.TryParse(m.Value, out ParseInt);
                IntList.Add(ParseInt);
            }
            Members = IntList[0];
            OriginalMembers = Members;
            HP = IntList[1];
            Attack = IntList[2];
            Initiative = IntList[3];
            EffectivePower = Members * Attack;
            PickedTarget = -1;
            s = s.Replace("(", "");
            s = s.Replace(")", "");
            s = s.Replace(";", "");
            s = s.Replace(",", "");
            string[] SplitString = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            AttackType = SplitString[SplitString.Length - 5];
            Immunities = new List<string>();
            Weaknesses = new List<string>();
            string KeyWord = "";
            foreach (string ss in SplitString)
            {
                switch (ss)
                {
                    case "immune":
                        KeyWord = "immune";
                        break;
                    case "weak":
                        KeyWord = "weak";
                        break;
                    case "with":
                        KeyWord = "";
                        break;
                    case "to":
                        ;
                        break;
                    default:
                        if (KeyWord == "immune")
                        {
                            Immunities.Add(ss);
                        }
                        else if (KeyWord == "weak")
                        {
                            Weaknesses.Add(ss);
                        }
                        break;
                }
                if (ss == "weak")
                    KeyWord = "weak";
            }
        }
        public int PickTarget(List<Group> Victims, List<int> DontPick)
        {
            if (Members == 0)
                return -1;
            int BiggestBullseye = 0;
            int ReturnValue = -1;
            foreach(Group v in Victims)
            {
                if (v.Members>0 && !DontPick.Contains(v.Index))
                {
                    int Current = v.PotentialDamage(this.AttackType, this.EffectivePower);
                    if (Current > BiggestBullseye)
                    {
                        BiggestBullseye = Current;
                        ReturnValue = v.Index;
                    }
                }
            }
            PickedTarget = ReturnValue;
            return ReturnValue;
        }
        public int PotentialDamage(string AttackType, int AttackAmount)
        {
            if (Immunities.Contains(AttackType))
            {
                return 0;
            }
            else
            {
                int AttackMultiplier = 1;
                if (Weaknesses.Contains(AttackType))
                {
                    AttackMultiplier *= 2;
                }
                return AttackAmount * AttackMultiplier;
            }
        }
        public void TakeDamage(string AttackType, int AttackAmount)
        {
            int Damage = this.PotentialDamage(AttackType, AttackAmount);
            Members -= Damage / HP;
            if (Members < 0)
                Members = 0;
            EffectivePower = Members * Attack;
        }
        public void NextIteration()
        {
            Members = OriginalMembers;
            this.EffectivePower = Members * Attack;
        }
    }
}
