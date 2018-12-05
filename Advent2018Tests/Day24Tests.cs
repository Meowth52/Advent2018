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
    public class Day24Tests
    {
        [TestMethod()]
        public void Day24Test()
        {
            Day _day24 = new Day24("");
            string PartOneExpected = "Fail";
            string PartTwoExpected = "Fail";
            Tuple<string, string> Actual = _day24.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            Assert.AreEqual(PartTwoExpected, Actual.Item2);
        }
    }
}