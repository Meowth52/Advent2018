﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advent2018;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018.Tests
{
    [TestClass()]
    public class Day20Tests
    {
        [TestMethod()]
        public void Day20Test()
        {
            Day _day20 = new Day20("");
            string PartOneExpected = "Fail";
            string PartTwoExpected = "Fail";
            Tuple<string, string> Actual = _day20.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}