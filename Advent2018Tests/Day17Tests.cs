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
    public class Day17Tests
    {
        [TestMethod()]
        public void Day17Test()
        {
            Day _day17 = new Day17("x=495, y=2..7\r\ny=7, x=495..501\r\nx=501, y=3..7\r\nx=498, y=2..4\r\nx=506, y=1..2\r\nx=498, y=10..13\r\nx=504, y=10..13\r\ny=13, x=498..504");
            string PartOneExpected = "57";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day17.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}