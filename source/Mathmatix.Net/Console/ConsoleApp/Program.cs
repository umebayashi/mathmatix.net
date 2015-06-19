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
            var observable = PermutationGroup.CalculateEx(4);

            int i = 0;
            observable.Subscribe(
                p => Console.WriteLine("{0} : {1}", (++i).ToString("000000"), p),
                ex => Console.WriteLine(ex),
                () => Console.WriteLine("Complete."));
        }
    }
}
