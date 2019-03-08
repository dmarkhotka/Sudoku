namespace Core.Common
{
    internal struct SudokuCell
    {
        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal int X { get; private set; }
        internal int Y { get; private set; }
    }
}
