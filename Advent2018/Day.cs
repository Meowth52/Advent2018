using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
    public abstract class Day
    {
        private MainView _mainView;
        public Day(string _input)
        {

        }
        public void SetMainView(MainView mainView)
        {
            this._mainView = mainView;
        }
        public abstract Tuple<string, string> getResult();
        public string parseJustOneLine(string input)
        {
            return input.Replace("\r\n", "");
        }
        public string[] parseStringArray(string input)
        {
            string Input = input.Replace("\r\n", "_");
            return Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public List<string[]> parseListOfStringArrays(string input)
        {
            List<string[]> ReturnList = new List<string[]>();
            string Input = input.Replace("\r\n", "_");
            string[] RawInstructions = Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in RawInstructions)
            {
                    ReturnList.Add(s.Split(' '));
            }
            return ReturnList;
        }
    }
}
