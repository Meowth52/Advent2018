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
    public class Day12Tests
    {
        [TestMethod()]
        public void Day12Test()
        {
            Day _day12 = new Day12("initial state: #..#.#..##......###...###\r\n\r\n...## => #\r\n..#.. => #\r\n.#... => #\r\n.#.#. => #\r\n.#.## => #\r\n.##.. => #\r\n.#### => #\r\n#.#.# => #\r\n#.### => #\r\n##.#. => #\r\n##.## => #\r\n###.. => #\r\n###.# => #\r\n####. => #");
            string PartOneExpected = "325";
            Tuple<string, string> Actual = _day12.getResult();
            Assert.AreEqual(PartOneExpected, Actual.Item1);
            // Part2 not testable
        }
    }
}