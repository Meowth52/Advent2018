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
    public class Day23Tests
    {
        [TestMethod()]
        public void Day23Test()
        {
            Day _day23 = new Day23("pos=<0,0,0>, r=4\r\npos=<1,0,0>, r=1\r\npos=<4,0,0>, r=3\r\npos=<0,2,0>, r=1\r\npos=<0,5,0>, r=3\r\npos=<0,0,3>, r=1\r\npos=<1,1,1>, r=1\r\npos=<1,1,2>, r=1\r\npos=<1,3,1>, r=1");
            string PartOneExpected = "7";
            Tuple<string, string> Actual = _day23.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day23Test1()
        {
            Day _day23 = new Day23("pos =< 10, 12, 12 >, r = 2\r\npos =< 12, 14, 12 >, r = 2\r\npos =< 16, 12, 12 >, r = 4\r\npos =< 14, 14, 14 >, r = 6\r\npos =< 50, 50, 50 >, r = 200\r\npos =< 10, 10, 10 >, r = 5");
            string PartTwoExpected = "36";
            Tuple<string, string> Actual = _day23.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}