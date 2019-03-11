namespace Sudoku.Core.Common
{
    internal struct SudokuCell
    {
        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal int X { get; }
        internal int Y { get; }
    }
}
