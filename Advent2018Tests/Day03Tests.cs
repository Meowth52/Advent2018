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
    public class Day03Tests
    {
        [TestMethod()]
        public void Day03Test()
        {
            Day _day03 = new Day03("#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2");
            string PartOneExpected = "4";
            string PartTwoExpected = "3";
            Tuple<string, string> Actual = _day03.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}