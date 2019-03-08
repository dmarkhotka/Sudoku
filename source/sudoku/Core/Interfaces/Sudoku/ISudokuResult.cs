using Core.Enums;

namespace Core.Interfaces.Sudoku
{
    public interface ISudokuResult
    {
        bool HasSolution { get; }
        bool IsUnique { get; }
        bool HasUniqueSolution { get; }
        int BackTrackCount { get; }
        int ComplexitiesScore { get; }
        ESudokuLevel Level { get; set; }

        int[,] Data { get; }
    }
}
