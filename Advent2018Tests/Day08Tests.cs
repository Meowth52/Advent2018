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
    public class Day08Tests
    {
        [TestMethod()]
        public void Day08Test()
        {
            Day _day08 = new Day08("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2");
            string PartOneExpected = "138";
            string PartTwoExpected = "66";
            Tuple<string, string> Actual = _day08.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}