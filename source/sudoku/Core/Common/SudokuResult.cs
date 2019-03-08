using System;
using Core.Enums;
using Core.Interfaces.Sudoku;

namespace Core.Common
{
    public class SudokuResult : ISudokuResult
    {
        public int BackTrackCount { get; set; }

        public int ComplexitiesScore { get; set; }

        public ESudokuLevel Level { get; set; }

        public int[,] Data { get; private set; }

        public bool HasSolution { get; private set; }

        public bool IsUnique { get; private set; } = true;

        public bool HasUniqueSolution => HasSolution && IsUnique;

        public void AddSolution(int[,] data)
        {
            if (data != null)
            {
                Data = data;

                IsUnique &= !HasSolution;
                HasSolution = true;
            }
        }
    }
}
