using Sudoku.Core.Enums;

namespace Sudoku.Core.Interfaces.Sudoku
{
    public interface ISudokuLevel
    {
        ESudokuLevel Level { get; }

        bool IsMatching(ISudokuResult result);
        bool IsMaxLimitExceeded(ISudokuResult result);
    }
}