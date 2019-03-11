namespace Sudoku.Core.Common
{
    internal static class BlockHelper
    {
        internal static SudokuCell GetCellByIndex(int index, int blockCount)
        {
            return new SudokuCell(index / blockCount, index % blockCount);
        }

        internal static int GetBlockStartPosition(int index, int blockRowCount)
        {
            return index - index % blockRowCount;
        }

        internal static int GetBlockEndPosition(int index, int blockRowCount)
        {
            return index - index % blockRowCount + blockRowCount;
        }
    }
}
