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
    public class Day14Tests
    {
        [TestMethod()]
        public void Day14Test1()
        {
            Day _day14 = new Day14("9");
            string PartOneExpected = "5158916779";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day14Test2()
        {
            Day _day14 = new Day14("5");
            string PartOneExpected = "0124515891";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day14Test3()
        {
            Day _day14 = new Day14("18");
            string PartOneExpected = "9251071085";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day14Test4()
        {
            Day _day14 = new Day14("2018");
            string PartOneExpected = "5941429882";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
        }
        [TestMethod()]
        public void Day14Test1_2()
        {
            Day _day14 = new Day14("51589");
            string PartTwoExpected = "9";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day14Test2_2()
        {
            Day _day14 = new Day14("01245");
            string PartTwoExpected = "5";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day14Test3_2()
        {
            Day _day14 = new Day14("92510");
            string PartTwoExpected = "18";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Day14Test4_2()
        {
            Day _day14 = new Day14("59414");
            string PartTwoExpected = "2018";
            Tuple<string, string> Actual = _day14.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}