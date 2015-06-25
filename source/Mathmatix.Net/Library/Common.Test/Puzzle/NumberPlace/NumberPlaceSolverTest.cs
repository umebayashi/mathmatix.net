﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common.Puzzle.NumberPlace
{
    [TestClass]
    public class NumberPlaceSolverTest
    {
        [TestMethod]
        public void TestSolveAsync1()
        {
            var seed = new NumberPlaceMatrix(new int[] {
                0, 6, 0, 8, 0, 0, 0, 0, 0,
                0, 2, 0, 4, 0, 0, 0, 7, 5,
                0, 0, 0, 0, 0, 3, 0, 0, 0,
                0, 0, 6, 0, 7, 0, 0, 9, 3,
                0, 0, 0, 5, 0, 1, 0, 0, 0,
                8, 3, 0, 0, 6, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0, 0,
                5, 7, 0, 0, 0, 9, 0, 6, 0,
                0, 0, 0, 0, 0, 7, 0, 2, 0
            });

            var solver = new NumberPlaceSolver();
            var results = solver.Solve(seed);

            Assert.AreEqual(1, results.Count());

            var expected = new int[] { 
                7, 6, 1, 8, 9, 5, 2, 3, 4,
                3, 2, 8, 4, 1, 6, 9, 7, 5,
                9, 4, 5, 7, 2, 3, 6, 1, 8,
                1, 5, 6, 2, 7, 8, 4, 9, 3,
                2, 9, 4, 5, 3, 1, 7, 8, 6,
                8, 3, 7, 9, 6, 4, 1, 5, 2,
                6, 8, 9, 1, 5, 2, 3, 4, 7,
                5, 7, 2, 3, 4, 9, 8, 6, 1,
                4, 1, 3, 6, 8, 7, 5, 2, 9
            };

            Assert.IsTrue(results.First().GetCells().Select(x => x.Value).ToArray().SequenceEqual(expected));
        }
    }
}
