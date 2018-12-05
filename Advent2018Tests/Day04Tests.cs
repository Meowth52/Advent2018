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
    public class Day04Tests
    {
        [TestMethod()]
        public void Day04Test()
        {
            Day _day04 = new Day04("[1518-11-01 00:00] Guard #10 begins shift\r\n[1518-11-01 00:05] falls asleep\r\n[1518-11-01 00:25] wakes up\r\n[1518-11-01 00:30] falls asleep\r\n[1518-11-01 00:55] wakes up\r\n[1518-11-01 23:58] Guard #99 begins shift\r\n[1518-11-02 00:40] falls asleep\r\n[1518-11-02 00:50] wakes up\r\n[1518-11-03 00:05] Guard #10 begins shift\r\n[1518-11-03 00:24] falls asleep\r\n[1518-11-03 00:29] wakes up\r\n[1518-11-04 00:02] Guard #99 begins shift\r\n[1518-11-04 00:36] falls asleep\r\n[1518-11-04 00:46] wakes up\r\n[1518-11-05 00:03] Guard #99 begins shift\r\n[1518-11-05 00:45] falls asleep\r\n[1518-11-05 00:55] wakes up");
            string PartOneExpected = "240";
            string PartTwoExpected = "4455";
            Tuple<string, string> Actual = _day04.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}