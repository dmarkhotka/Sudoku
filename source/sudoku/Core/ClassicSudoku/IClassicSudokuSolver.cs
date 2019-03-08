using System;
using Core.Interfaces.Sudoku;

namespace Core.ClassicSudoku
{
    public interface IClassicSudokuSolver: ISudokuSolver
    {
        int BlockCount { get; }
    }
}
