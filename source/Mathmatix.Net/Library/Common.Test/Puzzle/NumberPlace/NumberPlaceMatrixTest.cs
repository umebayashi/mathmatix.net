using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common.Puzzle.NumberPlace
{
    [TestClass]
    public class NumberPlaceMatrixTest
    {
        [TestMethod]
        public void TestIsValidTrue()
        {
            var matrix = new NumberPlaceMatrix(new int[] { 
                7, 6, 1, 8, 9, 5, 2, 3, 4,
                3, 2, 8, 4, 1, 6, 9, 7, 5,
                9, 4, 5, 7, 2, 3, 6, 1, 8,
                1, 5, 6, 2, 7, 8, 4, 9, 3,
                2, 9, 4, 5, 3, 1, 7, 8, 6,
                8, 3, 7, 9, 6, 4, 1, 5, 2,
                6, 8, 9, 1, 5, 2, 3, 4, 7,
                5, 7, 2, 3, 4, 9, 8, 6, 1,
                4, 1, 3, 6, 8, 7, 5, 2, 9
            });

            Assert.IsTrue(matrix.IsValid());
        }

        [TestMethod]
        public void TestIsValidFalse1()
        {
            var matrix = new NumberPlaceMatrix();

            Assert.IsFalse(matrix.IsValid());
        }

        [TestMethod]
        public void TestIsValidFalse2()
        {
            var matrix = new NumberPlaceMatrix(new int[] { 
                1, 2, 3, 4, 5, 6, 7, 8, 9,
                2, 3, 4, 5, 6, 7, 8, 9, 1,
                3, 4, 5, 6, 7, 8, 9, 1, 2,
                4, 5, 6, 7, 8, 9, 1, 2, 3,
                5, 6, 7, 8, 9, 1, 2, 3, 4,
                6, 7, 8, 9, 1, 2, 3, 4, 5,
                7, 8, 9, 1, 2, 3, 4, 5, 6,
                8, 9, 1, 2, 3, 4, 5, 6, 7,
                9, 1, 2, 3, 4, 5, 6, 7, 8
            });

            Assert.IsFalse(matrix.IsValid());
        }

        [TestMethod]
        public void TestIsValidFalse3()
        {
            var matrix = new NumberPlaceMatrix(new int[] { 
                1, 2, 3, 1, 2, 3, 1, 2, 3, 
                4, 5, 6, 4, 5, 6, 4, 5, 6, 
                7, 8, 9, 7, 8, 9, 7, 8, 9, 
                1, 2, 3, 1, 2, 3, 1, 2, 3, 
                4, 5, 6, 4, 5, 6, 4, 5, 6, 
                7, 8, 9, 7, 8, 9, 7, 8, 9, 
                1, 2, 3, 1, 2, 3, 1, 2, 3, 
                4, 5, 6, 4, 5, 6, 4, 5, 6, 
                7, 8, 9, 7, 8, 9, 7, 8, 9, 
            });

            Assert.IsFalse(matrix.IsValid());
        }
    }
}
