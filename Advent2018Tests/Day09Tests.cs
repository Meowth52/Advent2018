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
    public class Day09Tests
    {
        [TestMethod()]
        public void Day09Test01()
        {
            Day _day09 = new Day09("9 players; last marble is worth 25 points");
            string PartOneExpected = "32";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day09Test02()
        {
            Day _day09 = new Day09("10 players; last marble is worth 1618 points");
            string PartOneExpected = "8317";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day09Test03()
        {
            Day _day09 = new Day09("13 players; last marble is worth 7999 points");
            string PartOneExpected = "146373";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day09Test04()
        {
            Day _day09 = new Day09("17 players; last marble is worth 1104 points");
            string PartOneExpected = "2764";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day09Test05()
        {
            Day _day09 = new Day09("21 players; last marble is worth 6111 points");
            string PartOneExpected = "54718";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day09Test06()
        {
            Day _day09 = new Day09("30 players; last marble is worth 5807 points");
            string PartOneExpected = "37305";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day09.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}