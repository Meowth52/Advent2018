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
    public class Day07Tests
    {
        [TestMethod()]
        public void Day07Test()
        {
            Day _day07 = new Day07("Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.");
            string PartOneExpected = "CABDFE";
            string PartTwoExpected = "15";
            Tuple<string, string> Actual = _day07.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}