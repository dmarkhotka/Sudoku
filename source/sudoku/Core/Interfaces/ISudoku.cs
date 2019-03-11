using Sudoku.Core.Enums;
using Sudoku.Core.Interfaces.Sudoku;

namespace Sudoku.Core.Interfaces
{
    public interface ISudoku
    {
        int[,] LoadFromFile(string path);
        ISudokuResult Solve(int[,] data);
        ISudokuResult Generate(ESudokuLevel level);
    }
}
