using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Mathmatix.Common;
using Mathmatix.Common.Algebra;
using Mathmatix.Common.Puzzle.NumberPlace;

namespace Mathmatix.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var observable = PermutationGroup.CalculateEx(4);

            //int i = 0;
            //observable.Subscribe(
            //    p => Console.WriteLine("{0} : {1}", (++i).ToString("000000"), p),
            //    ex => Console.WriteLine(ex),
            //    () => Console.WriteLine("Complete."));
            int length = 9;
            var permutations = Enumerable.Range(1, length).ToArray().Permutations(length);
            int count = 0;
            foreach (var permutation in permutations)
            {
                WriteArray(permutation, ++count);
            }
        }

        private static void WriteArray<T>(T[] array, int count)
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
