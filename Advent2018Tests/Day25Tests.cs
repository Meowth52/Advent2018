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
    public class Day25Tests
    {
        [TestMethod()]
        public void Day25Test1()
        {
            Day _day25 = new Day25(" 0,0,0,0\r\n 3,0,0,0\r\n 0,3,0,0\r\n 0,0,3,0\r\n 0,0,0,3\r\n 0,0,0,6\r\n 9,0,0,0\r\n12,0,0,0");
            string PartOneExpected = "Fail";
            string PartTwoExpected = "2";
            Tuple<string, string> Actual = _day25.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        public void Day25Test2()
        {
            Day _day25 = new Day25("-1,2,2,0\r\n0,0,2,-2\r\n0,0,0,-2\r\n-1,2,0,0\r\n-2,-2,-2,2\r\n3,0,2,-1\r\n-1,3,2,2\r\n-1,0,-1,0\r\n0,2,1,-2\r\n3,0,0,0");
            string PartOneExpected = "4";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day25.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        public void Day25Test3()
        {
            Day _day25 = new Day25("1,-1,0,1\r\n2,0,-1,0\r\n3,2,-1,0\r\n0,0,3,1\r\n0,0,-1,-1\r\n2,3,-2,0\r\n-2,2,0,0\r\n2,-2,0,-1\r\n1,-1,0,-1\r\n3,2,0,2");
            string PartOneExpected = "3";
            string PartTwoExpected = "0";
            Tuple<string, string> Actual = _day25.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
        public void Day25Test4()
        {
            Day _day25 = new Day25("1,-1,-1,-2\r\n-2,-2,0,1\r\n0,2,1,3\r\n-2,3,-2,1\r\n0,2,3,-2\r\n-1,-1,1,-2\r\n0,-2,-1,0\r\n-2,2,3,-1\r\n1,2,2,0\r\n-1,-2,0,-2");
            string PartOneExpected = "Fail";
            string PartTwoExpected = "8";
            Tuple<string, string> Actual = _day25.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}