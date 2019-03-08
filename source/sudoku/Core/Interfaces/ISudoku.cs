using System;
using Core.Enums;
using Core.Interfaces.Sudoku;

namespace Core.Interfaces
{
    public interface ISudoku
    {
        int[,] LoadFromFile(string path);
        ISudokuResult Solve(int[,] data);
        ISudokuResult Generate(ESudokuLevel level);
    }
}
