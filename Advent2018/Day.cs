using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
    abstract class Day
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
    }
}
