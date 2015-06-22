using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common
{
    [TestClass]
    public class CollectionExtensionTest
    {
        [TestMethod]
        public void TestRandomize()
        {
            var source = Enumerable.Range(1, 9);

            for (int i = 0; i < 100; i++)
            {
                var random = source.Randomize().ToArray();
                WriteArray(random, i + 1);
            }
        }

        private void WriteArray<T>(T[] array, int count)
        {
            var result = new StringBuilder();
            result.AppendFormat("({0})[ ", count.ToString("000"));
            for (int i = 0; i < array.Length; i++)
            {
                result.Append(array[i]);
                if (i < array.Length - 1)
                {
                    result.Append(", ");
                }
            }
            result.Append(" ]");

            Console.WriteLine(result.ToString());
        }
    }
}
