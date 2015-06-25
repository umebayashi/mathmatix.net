using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathmatix.Common.Puzzle.NumberPlace
{
    public class NumberPlaceSolver
    {
        public IEnumerable<NumberPlaceMatrix> Solve(NumberPlaceMatrix seed)
        {
            var candidates = seed.GetCells().ToDictionary(
                x => x,
                x => (x.Value == 0 ? Enumerable.Repeat(true, seed.Size) : new bool[seed.Size]).ToList());
            var sizeRange = Enumerable.Range(0, seed.Size);
            var blockRowRange = Enumerable.Range(0, seed.BlockRowSize);
            var blockColumnRange = Enumerable.Range(0, seed.BlockColumnCount);
            var blockRange = blockRowRange.SelectMany(r => blockColumnRange.Select(c => new { BlockRowIndex = r, BlockColumnIndex = c }));

            while (true)
            {
                var unsolvedCount = seed.GetCells().Count(x => x.Value == 0);
                if (unsolvedCount == 0)
                {
                    break;
                }

                for (int r = 0; r < seed.Size; r++)
                {
                    var values = seed.GetRow(r).Where(x => x.Value > 0).Select(x => x.Value).ToArray();
                    for (int c = 0; c < seed.Size; c++)
                    {
                        foreach (var value in values)
                        {
                            candidates.Single(x => x.Key.RowIndex == r && x.Key.ColumnIndex == c).Value[value - 1] = false;
                        }
                    }
                }

                for (int c = 0; c < seed.Size; c++)
                {
                    var values = seed.GetColumn(c).Where(x => x.Value > 0).Select(x => x.Value).ToArray();
                    for (int r = 0; r < seed.Size; r++)
                    {
                        foreach (var value in values)
                        {
                            candidates.Single(x => x.Key.ColumnIndex == c && x.Key.RowIndex == r).Value[value - 1] = false;
                        }
                    }
                }

                foreach (var b in blockRange)
                {
                    var values = seed.GetBlock(b.BlockRowIndex, b.BlockColumnIndex).Where(x => x.Value > 0).Select(x => x.Value).ToArray();
                    for (int r = b.BlockRowIndex * seed.BlockRowSize; r < (b.BlockRowIndex + 1) * seed.BlockRowSize; r++)
                    {
                        for (int c = b.BlockColumnIndex * seed.BlockColumnSize; c < (b.BlockColumnIndex + 1) * seed.BlockRowSize; c++)
                        {
                            foreach (var value in values)
                            {
                                candidates.Single(x => x.Key.RowIndex == r && x.Key.ColumnIndex == c).Value[value - 1] = false;
                            }
                        }
                    }
                }

                var solvedCells = candidates.Where(x => x.Key.Value == 0 && x.Value.Count(b => b) == 1).ToArray();
                if (solvedCells.Any())
                {
                    foreach (var solvedCell in solvedCells)
                    {
                        seed[solvedCell.Key.RowIndex, solvedCell.Key.ColumnIndex] = solvedCell.Value.FindIndex(b => b) + 1;
                    }
                }
                else
                {
                    var minCount = candidates.Where(x => x.Key.Value == 0).Min(x => x.Value.Count(b => b));
                    var unsolvedCell = candidates.Where(x => x.Key.Value == 0 && x.Value.Count(b => b) == minCount).First();
                }
            }

            yield return seed;
        }
    }
}
