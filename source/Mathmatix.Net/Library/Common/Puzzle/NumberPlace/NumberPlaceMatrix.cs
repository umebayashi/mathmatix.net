using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathmatix.Common.Puzzle.NumberPlace
{
    /// <summary>
    /// 数独のマトリクスを保持するオブジェクト
    /// </summary>
    public class NumberPlaceMatrix
    {
        #region ネストクラス

        #endregion

        #region フィールド

        private List<NumberPlaceMatrixCell> _Cells = new List<NumberPlaceMatrixCell>();

        #endregion

        #region 定数

        public const int DEFAULT_SIZE = 9;

        public const int DEFAULT_BLOCK_ROW_SIZE = 3;

        public const int DEFAULT_BLOCK_COLUMN_SIZE = 3;

        #endregion

        /// <summary>
        /// 数独のマトリクスを作成する
        /// </summary>
        /// <param name="size">縦・横の各マスの数</param>
        /// <param name="blockRowSize">1つのブロックに含まれる行数</param>
        /// <param name="blockColumnSize">1つのブロックに含まれる列数</param>
        public NumberPlaceMatrix(int size, int blockRowSize, int blockColumnSize)
        {
            this.Size = size;
            this.BlockRowSize = blockRowSize;
            this.BlockColumnSize = blockColumnSize;

            CheckCondition(this.Size % this.BlockRowSize == 0);
            CheckCondition(this.Size % this.BlockColumnSize == 0);

            this.BlockRowCount = this.Size / this.BlockRowSize;
            this.BlockColumnCount = this.Size / this.BlockColumnSize;
        }

        /// <summary>
        /// 数独のマトリクスを作成する
        /// </summary>
        /// <param name="size">縦・横の各マスの数</param>
        /// <param name="blockRowSize">1つのブロックに含まれる行数</param>
        /// <param name="blockColumnSize">1つのブロックに含まれる列数</param>
        public NumberPlaceMatrix(int size, int blockRowSize, int blockColumnSize, int[] values) : this(size, blockRowSize, blockColumnSize)
        {
            this.Initialize(values);
        }

        /// <summary>
        /// 既定のサイズ（9x9）の数独マトリクスを作成する
        /// </summary>
        public NumberPlaceMatrix(int[] values)
            : this(DEFAULT_SIZE, DEFAULT_BLOCK_ROW_SIZE, DEFAULT_BLOCK_COLUMN_SIZE) 
        {
            this.Initialize(values);
        }

        /// <summary>
        /// 既定のサイズ（9x9）の数独マトリクスを作成する
        /// </summary>
        public NumberPlaceMatrix()
            : this(DEFAULT_SIZE, DEFAULT_BLOCK_ROW_SIZE, DEFAULT_BLOCK_COLUMN_SIZE) 
        {
            var values = Enumerable.Repeat(0, DEFAULT_SIZE * DEFAULT_SIZE).ToArray();
            this.Initialize(values);
        }

        /// <summary>
        /// 縦・横の各マスの数
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 1つのブロックに含まれる行数
        /// </summary>
        public int BlockRowSize { get; private set; }

        /// <summary>
        /// 1つのブロックに含まれる列数
        /// </summary>
        public int BlockColumnSize { get; private set; }

        /// <summary>
        /// 行方向のブロックの数
        /// </summary>
        public int BlockRowCount { get; private set; }

        /// <summary>
        /// 列方向のブロックの数
        /// </summary>
        public int BlockColumnCount { get; private set; }

        /// <summary>
        /// 各マスの値の取得・設定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public int this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckCondition(0 <= rowIndex && rowIndex < this.Size);
                CheckCondition(0 <= columnIndex && columnIndex < this.Size);

                return _Cells.Find(c => c.RowIndex == rowIndex && c.ColumnIndex == columnIndex).Value;
            }
            set
            {
                CheckCondition(0 <= rowIndex && rowIndex < this.Size);
                CheckCondition(0 <= columnIndex && columnIndex < this.Size);
                CheckCondition(0 <= value && value <= this.Size);

                var cell = _Cells.Find(c => c.RowIndex == rowIndex && c.ColumnIndex == columnIndex);
                if (cell == null)
                {
                    cell = new NumberPlaceMatrixCell { RowIndex = rowIndex, ColumnIndex = columnIndex, Value = value };
                    _Cells.Add(cell);
                }
                else
                {
                    cell.Value = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        private void Initialize(int[] values)
        {
            CheckCondition(values.Length == this.Size * this.Size,
                string.Format("配列の要素数が{0}x{0}と一致しません", this.Size));
            CheckCondition(0 <= values.Min(),
                string.Format("要素の最小値は0ですが{0}を検出しました", values.Min()));
            CheckCondition(values.Max() <= this.Size,
                string.Format("要素の最大値は{0}ですが{1}を検出しました", this.Size, values.Max()));

            _Cells.Clear();

            for (int r = 0; r < this.Size; r++)
            {
                for (int c = 0; c < this.Size; c++)
                {
                    var cell = new NumberPlaceMatrixCell { RowIndex = r, ColumnIndex = c, Value = values[r * this.Size + c] };
                    _Cells.Add(cell);
                }
            }
        }

        /// <summary>
        /// 有効な数独マトリクスであるかどうかを判定する
        /// </summary>
        public bool IsValid()
        {
            // 各行に1～Sizeの数値が重複なく含まれているかどうか
            for (int r = 0; r < this.Size; r++)
            {
                if (!this.IsRowValid(r))
                {
                    return false;
                }
            }
            // 各列に1～Sizeの数値が重複なく含まれているかどうか
            for (int c = 0; c < this.Size; c++)
            {
                if (!this.IsColumnValid(c))
                {
                    return false;
                }
            }
            // 各ブロックに1～Sizeの数値が重複なく含まれているかどうか
            for (int br = 0; br < this.BlockRowCount; br++)
            {
                for (int bc = 0; bc < this.BlockColumnCount; bc++)
                {
                    if (!this.IsBlockValid(br, bc))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 指定した行が数独の条件を満たすかどうかを判定する
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool IsRowValid(int rowIndex)
        {
            CheckCondition(0 <= rowIndex && rowIndex < this.Size, "インデックスが範囲外です");

            // 行を抽出
            var row = _Cells.Where(x => x.RowIndex == rowIndex).OrderBy(x => x.ColumnIndex).Select(x => x.Value);

            // 1～Sizeの数値が重複なく含まれているかどうか
            var isValid = row.OrderBy(x => x).SequenceEqual(Enumerable.Range(1, this.Size));

            return isValid;
        }

        /// <summary>
        /// 指定した列が数独の条件を満たすかどうかを判定する
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public bool IsColumnValid(int columnIndex)
        {
            CheckCondition(0 <= columnIndex && columnIndex < this.Size, "インデックスが範囲外です");

            // 列を抽出
            var column = _Cells.Where(x => x.ColumnIndex == columnIndex).OrderBy(x => x.RowIndex).Select(x => x.Value);

            // 1～Sizeの数値が重複なく含まれているかどうか
            var isValid = column.OrderBy(x => x).SequenceEqual(Enumerable.Range(1, this.Size));

            return isValid;
        }

        /// <summary>
        /// 指定したブロックが数独の条件を満たすかどうかを判定する
        /// </summary>
        /// <param name="blockRowIndex"></param>
        /// <param name="blockColumnIndex"></param>
        /// <returns></returns>
        public bool IsBlockValid(int blockRowIndex, int blockColumnIndex)
        {
            CheckCondition(0 <= blockRowIndex && blockRowIndex < this.BlockRowCount, "インデックスが範囲外です");
            CheckCondition(0 <= blockColumnIndex && blockColumnIndex < this.BlockColumnCount, "インデックスが範囲外です");

            // ブロックを抽出
            var rowIndexStart = blockRowIndex * this.BlockRowSize;
            var rowIndexEnd = rowIndexStart + this.BlockRowSize - 1;
            var columnIndexStart = blockColumnIndex * this.BlockColumnSize;
            var columnIndexEnd = columnIndexStart + this.BlockColumnSize - 1;

            var block = _Cells
                .Where(x =>
                    rowIndexStart <= x.RowIndex && x.RowIndex <= rowIndexEnd &&
                    columnIndexStart <= x.ColumnIndex && x.ColumnIndex <= columnIndexEnd)
                .OrderBy(x => x.RowIndex)
                .ThenBy(x => x.ColumnIndex)
                .Select(x => x.Value);

            // 1～Sizeの数値が重複なく含まれているかどうか
            var isValid = block.OrderBy(x => x).SequenceEqual(Enumerable.Range(1, this.Size));

            return isValid;
        }

        private static void CheckCondition(bool condition, string falseMessage = null)
        {
            if (string.IsNullOrWhiteSpace(falseMessage))
            {
                Contract.Assert(condition);
            }
            else
            {
                Contract.Assert(condition, falseMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public NumberPlaceMatrixCell[] GetRow(int rowIndex)
        {
            var row = _Cells.Where(x => x.RowIndex == rowIndex).ToArray();

            return row;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public NumberPlaceMatrixCell[] GetColumn(int columnIndex)
        {
            var column = _Cells.Where(x => x.ColumnIndex == columnIndex).ToArray();

            return column;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NumberPlaceMatrixCell
    {
        public NumberPlaceMatrixCell()
        {
        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            return string.Format("[R:{0} C:{1} V:{2}]", this.RowIndex, this.ColumnIndex, this.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is NumberPlaceMatrixCell)
            {
                var target = (NumberPlaceMatrixCell)obj;
                return (this.RowIndex == target.RowIndex && this.ColumnIndex == target.ColumnIndex); ;
            }
            return base.Equals(obj);
        }
    }
}
