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
    public class Day02Tests
    {
        [TestMethod()]
        public void Part1()
        {
            Day _day02 = new Day02("abcdef\r\nbababc\r\nabbcde\r\nabcccd\r\naabcdd\r\nabcdee\r\nababab");
            string PartOneExpected = "12";
            Tuple<string, string> Actual = _day02.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Part2()
        {
            Day _day02 = new Day02("abcde\r\nfghij\r\nklmno\r\npqrst\r\nfguij\r\naxcye\r\nwvxyz");
            string PartTwoExpected = "fgij";
            Tuple<string, string> Actual = _day02.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}