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
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1_1()
        {
            Day _day01 = new Day01("+1\r\n-2\r\n+3\r\n+1");
            string PartOneExpected = "3";
            string Actual = _day01.getPartOne();
            Assert.AreEqual(PartOneExpected,Actual);
        }
        [TestMethod()]
        public void Part1_2()
        {
            Day _day01 = new Day01("+1\r\n+1\r\n+1");
            string PartOneExpected = "3";
            string Actual = _day01.getPartOne();
            Assert.AreEqual(PartOneExpected, Actual);
        }
        [TestMethod()]
        public void Part1_3()
        {
            Day _day01 = new Day01("+1\r\n+1\r\n-2");
            string PartOneExpected = "0";
            string Actual = _day01.getPartOne();
            Assert.AreEqual(PartOneExpected, Actual);
        }
        [TestMethod()]
        public void Part1_4()
        {
            Day _day01 = new Day01("-1\r\n-2\r\n-3");
            string PartOneExpected = "-6";
            string Actual = _day01.getPartOne();
            Assert.AreEqual(PartOneExpected, Actual);
        }
        [TestMethod()]
        public void Part2_1()
        {
            Day _day01 = new Day01("+1\r\n-2\r\n+3\r\n+1");
            string PartTwoExpected = "2";
            Tuple<string, string> Actual = _day01.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Part2_2()
        {
            Day _day01 = new Day01("+1\r\n-1");
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day01.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Part2_3()
        {
            Day _day01 = new Day01("+3\r\n+3\r\n+4\r\n-2\r\n-4");
            string PartTwoExpected = "10";
            Tuple<string, string> Actual = _day01.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Part2_4()
        {
            Day _day01 = new Day01("-6\r\n+3\r\n+8\r\n+5\r\n-6");
            string PartTwoExpected = "5";
            Tuple<string, string> Actual = _day01.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        [TestMethod()]
        public void Part2_5()
        {
            Day _day01 = new Day01("+7\r\n+7\r\n-2\r\n-7\r\n-4");
            string PartTwoExpected = "14";
            Tuple<string, string> Actual = _day01.getResult();
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}