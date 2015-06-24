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
            var sizeRange = Enumerable.Range(0, seed.Size);
            var blockRowRange = Enumerable.Range(0, seed.BlockRowSize);
            var blockColumnRange = Enumerable.Range(0, seed.BlockColumnCount);
            var blockRange = blockRowRange.SelectMany(r => blockColumnRange.Select(c => new { BlockRowIndex = r, BlockColumnIndex = c }));

            var rows = sizeRange.Select(x => new 
            { 
                RowIndex = x, 
                HasValueCount = seed.GetRow(x).Count(y => y.Value > 0) 
            }).ToArray();
            var columns = sizeRange.Select(x => new 
            { 
                ColumnIndex = x, 
                HasValueCount = seed.GetColumn(x).Count(y => y.Value > 0) 
            }).ToArray();
            var blocks = blockRange.Select(x => new
            {
                BlockRowIndex = x.BlockRowIndex,
                BlockColumnIndex = x.BlockColumnIndex,
                HasValueCount = seed.GetBlock(x.BlockRowIndex, x.BlockColumnIndex).Count(y => y.Value > 0)
            }).ToArray();
            
            yield return seed;
        }
    }
}
