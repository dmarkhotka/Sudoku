namespace Sudoku.Core.Interfaces.Sudoku
{
    public interface ISudokuSolver
    {
        ISudokuResult Solve(int[,] data);
        ISudokuResult GenerateSolved();
    }
}
