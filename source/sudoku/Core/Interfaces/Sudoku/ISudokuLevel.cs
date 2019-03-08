using Core.Enums;

namespace Core.Interfaces.Sudoku
{
    public interface ISudokuLevel
    {
        ESudokuLevel Level { get; }

        bool IsMatching(ISudokuResult result);
        bool IsMaxLimitExceeded(ISudokuResult result);
    }
}