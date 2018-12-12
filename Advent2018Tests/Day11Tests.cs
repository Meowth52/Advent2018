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
    public class Day11Tests
    {
        [TestMethod()]
        public void Day11TestPart1_1()
        {
            Day _day11 = new Day11("18");
            string PartOneExpected = "33,45";
            string PartTwoExpected = "90,269,16";
            Tuple<string, string> Actual = _day11.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day11TestPart1_2()
        {
            Day _day11 = new Day11("42");
            string PartOneExpected = "21,61";
            string PartTwoExpected = "232,251,12";
            Tuple<string, string> Actual = _day11.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}