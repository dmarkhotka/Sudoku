using Sudoku.Core.Interfaces.Sudoku;

namespace Sudoku.Core.ClassicSudoku
{
    public interface IClassicSudokuSolver: ISudokuSolver
    {
        int BlockCount { get; }
    }
}
