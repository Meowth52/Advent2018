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
    public class Day06Tests
    {
        [TestMethod()]
        public void Day06Test()
        {
            Day _day06 = new Day06("1, 1\r\n1, 6\r\n8, 3\r\n3, 4\r\n5, 5\r\n8, 9");
            string PartOneExpected = "17";
            string PartTwoExpected = "0"; //No easy test availible
            Tuple<string, string> Actual = _day06.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}