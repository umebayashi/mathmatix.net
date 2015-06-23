using System;
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
            var seed = new NumberPlaceMatrix();

            var centerBlock = new int[] { 3, 2, 8, 4, 5, 1, 6, 9, 7 };
            int rowIndexStart = seed.BlockRowSize * 1;
            int rowIndexEnd = seed.BlockRowSize * 2 - 1;
            int columnIndexStart = seed.BlockColumnSize * 1;
            int columnIndexEnd = seed.BlockColumnSize * 2 - 1;

            int i = 0;
            for (int r = rowIndexStart; r <= rowIndexEnd; r++)
            {
                for (int c = columnIndexStart; c <= columnIndexEnd; c++)
                {
                    seed[r, c] = centerBlock[i++];
                }
            }

            var solver = new NumberPlaceSolver();
            var results = solver.SolveAsync(seed);
        }
    }
}
