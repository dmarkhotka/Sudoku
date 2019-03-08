using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common;
using Core.Interfaces.Sudoku;

namespace Core.ClassicSudoku
{
    internal class ClassicSudokuGenerator : ISudokuGenerator
    {
        private IClassicSudokuSolver _solver;
        Queue<SudokuCell> cellsForHiding;

        public ClassicSudokuGenerator(IClassicSudokuSolver solver)
        {
            _solver = solver;
        }

        public ISudokuResult Generate(ISudokuLevel level)
        {
            var result = _solver.GenereateSolved();

            cellsForHiding = new Queue<SudokuCell>(
                Enumerable.Range(0, _solver.BlockCount * _solver.BlockCount)
                    .OrderBy(x => Guid.NewGuid())
                    .Select(x => Helper.GetCellByIndex(x, _solver.BlockCount)));

            result = RemovePossibleValue(result.Data, level);
            result.Level = level.Level;
            return result;
        }

        private ISudokuResult RemovePossibleValue(int[,] data, ISudokuLevel level)
        {
            var notSuitableCells = new List<SudokuCell>();
            while (cellsForHiding.Count > 0)
            {
                var cell = cellsForHiding.Dequeue();
                if (notSuitableCells.Contains(cell))
                {
                    continue;
                }
                int value = data[cell.X, cell.Y];

                data[cell.X, cell.Y] = 0;
                var result = _solver.Solve(data);
                if (result.HasUniqueSolution)
                {
                    if (level.IsMatching(result))
                    {
                        var generateResult = new SudokuResult { BackTrackCount = result.BackTrackCount };
                        generateResult.AddSolution(data);
                        return generateResult;
                    }
                    if (!level.IsMaxLimitExceeded(result))
                    {
                        result = RemovePossibleValue(data, level);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                data[cell.X, cell.Y] = value;
                notSuitableCells.Add(cell);
            }
            foreach (var cell in notSuitableCells)
            {
                cellsForHiding.Enqueue(cell);
            }
            return null;
        }
    }
}
