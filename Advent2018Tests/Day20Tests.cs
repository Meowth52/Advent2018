using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advent2018;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018.Tests
{
    [TestClass()]
    public class Day20Tests
    {
        [TestMethod()]
        public void Day20Test1()
        {
            Day _day20 = new Day20("^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$");
            string PartOneExpected = "18";
            Tuple<string, string> Actual = _day20.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day20Test2()
        {
            Day _day20 = new Day20("^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$");
            string PartOneExpected = "23";
            Tuple<string, string> Actual = _day20.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day20Test3()
        {
            Day _day20 = new Day20("^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$");
            string PartOneExpected = "31";
            Tuple<string, string> Actual = _day20.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
    }
}