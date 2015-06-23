using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathmatix.Common.Puzzle.NumberPlace
{
    public class NumberPlaceSolver
    {
        public IEnumerable<NumberPlaceMatrix> SolveAsync(NumberPlaceMatrix seed)
        {
            var candidateTable = new Dictionary<NumberPlaceMatrixCell, List<int>>();

            for (int r = 0; r < seed.Size; r++)
            {
                var row = seed.GetRow(r);
                foreach (var cell in row)
                {
                    candidateTable.Add(cell, new List<int>());
                }
                var rowValues = row.Where(x => x.Value > 0).Select(x => x.Value).ToArray();
                var candidateValues = Enumerable.Range(1, seed.Size).Except(rowValues).ToArray();

                foreach (var emptyCell in row.Where(x => x.Value == 0))
                {
                    candidateTable[emptyCell].AddRange(candidateValues);
                }
            }

            for (int c = 0; c < seed.Size; c++)
            {
                var column = seed.GetColumn(c);
                var columnValues = column.Where(x => x.Value > 0).Select(x => x.Value).ToArray();

                foreach (var emptyCell in column.Where(x => x.Value == 0))
                {
                    var list = candidateTable[emptyCell];
                    var candidateValues = list.Except(columnValues).ToArray();
                    list.Clear();
                    list.AddRange(candidateValues);
                }
            }

            return null;
        }
    }
}
