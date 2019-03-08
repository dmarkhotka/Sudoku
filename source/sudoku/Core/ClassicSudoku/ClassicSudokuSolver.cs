using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common;
using Core.Interfaces.Sudoku;

namespace Core.ClassicSudoku
{
    internal class ClassicSudokuSolver : IClassicSudokuSolver
    {
        private Dictionary<SudokuCell, List<int>> cellCandidatesDictionary;
        public ClassicSudokuSolver(EClassicSudokuType type)
        {
            BlockCount = (int)type;
        }

        public int BlockCount { get; private set; }

        private int BlockRowCount => (int)Math.Sqrt(BlockCount);

        public ISudokuResult GenereateSolved()
        {
            int[,] data = new int[BlockCount, BlockCount];
            var result = new SudokuResult();
            FindAllCandidates(data);
            Solve(data, true, result);
            return result;
        }

        public ISudokuResult Solve(int[,] data)
        {
            var result = new SudokuResult();
            FindAllCandidates(data);
            int[,] tempData = new int[BlockCount, BlockCount];
            Array.Copy(data, tempData, BlockCount * BlockCount);
            Solve(tempData, false, result);
            return result;
        }

        private bool Solve(int[,] data, bool useRandom, SudokuResult result)
        {
            var keyValuesPair = FindNextCell(result);
            if (keyValuesPair == null)
            {
                var solveResult = new int[BlockCount, BlockCount];
                Array.Copy(data, solveResult, BlockCount * BlockCount);
                result.AddSolution(solveResult);

                return true;
            }
            result.BackTrackCount++;
            List<int> candidateList = 
            useRandom ? keyValuesPair.Item2.OrderBy(a => Guid.NewGuid()).ToList() 
                        : keyValuesPair.Item2;
            foreach (int value in candidateList)
            {
                var cells = ApplyCandidate(keyValuesPair.Item1, value);
                if (ValidateIntegrity(cells))
                {
                    data[keyValuesPair.Item1.X, keyValuesPair.Item1.Y] = value;
                    var values = new List<int>(cellCandidatesDictionary[keyValuesPair.Item1]);
                    cellCandidatesDictionary[keyValuesPair.Item1].Clear();
                    if (Solve(data, useRandom, result) && !result.IsUnique)
                    {
                        return true;
                    }
                    data[keyValuesPair.Item1.X, keyValuesPair.Item1.Y] = 0;
                    cellCandidatesDictionary[keyValuesPair.Item1].AddRange(values);
                }
                UndoApplyCandidate(cells, value);
            }
            return false;
        }

        private bool ValidateIntegrity(List<SudokuCell> cells)
        {
            var isValid = true;
            foreach (var cell in cells)
            {
                if (cellCandidatesDictionary[cell].Count < 1)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        private bool Validate(int[,] data, SudokuCell cell, int value)
        {

            for (int i = 0; i < BlockCount; i++)
            {
                // Check row
                if (i != cell.Y && data[cell.X, i] == value)
                {
                    return false;
                }
                // Check column
                if (i != cell.X && data[i, cell.Y] == value)
                {
                    return false;
                }
            }
            int xStartPosition = Helper.GetBlockStartPosition(cell.X, BlockRowCount);
            int xEndPosition = Helper.GetBlockEndPosition(cell.X, BlockRowCount);
            int yStartPosition = Helper.GetBlockStartPosition(cell.Y, BlockRowCount);
            int yEndPosition = Helper.GetBlockEndPosition(cell.Y, BlockRowCount);
            // Check sector
            for (int i = xStartPosition; i < xEndPosition; i++)
            {
                for (int j = yStartPosition; j < yEndPosition; j++)
                {
                    if (i != cell.X && j != cell.Y && data[i, j] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private List<SudokuCell> ApplyCandidate(SudokuCell cell, int value)
        {
            List<SudokuCell> result = new List<SudokuCell>();

            for (int i = 0; i < BlockCount; i++)
            {
                // Check row
                var reducedCell = new SudokuCell(cell.X, i);
                if (i != cell.Y && cellCandidatesDictionary.ContainsKey(reducedCell) 
                                && cellCandidatesDictionary[reducedCell].Remove(value))
                {
                    result.Add(reducedCell);
                }
                // Check column
                reducedCell = new SudokuCell(i, cell.Y);
                if (i != cell.X && cellCandidatesDictionary.ContainsKey(reducedCell) 
                                && cellCandidatesDictionary[reducedCell].Remove(value))
                {
                    result.Add(reducedCell);
                }
            }
            int xStartPosition = Helper.GetBlockStartPosition(cell.X, BlockRowCount);
            int xEndPosition = Helper.GetBlockEndPosition(cell.X, BlockRowCount);
            int yStartPosition = Helper.GetBlockStartPosition(cell.Y, BlockRowCount);
            int yEndPosition = Helper.GetBlockEndPosition(cell.Y, BlockRowCount);
            // Check sector
            for (int i = xStartPosition; i < xEndPosition; i++)
            {
                for (int j = yStartPosition; j < yEndPosition; j++)
                {
                    var reducedCell = new SudokuCell(i, j);
                    if (!(i == cell.X || j == cell.Y) && cellCandidatesDictionary.ContainsKey(reducedCell) 
                                                      && cellCandidatesDictionary[reducedCell].Remove(value))
                    {
                        result.Add(reducedCell);
                    }
                }
            }
            return result;
        }

        private void UndoApplyCandidate(List<SudokuCell> cells, int value)
        {
            foreach (SudokuCell cell in cells)
            {
                cellCandidatesDictionary[cell].Add(value);
            }
        }

        private Tuple<SudokuCell, List<int>> FindNextCell(SudokuResult result)
        {
            IEnumerable<List<int>> cellsWithNonEmptyCandidates = cellCandidatesDictionary.Values.Where(x => x.Count > 0);
            if (cellsWithNonEmptyCandidates.Any())
            {
                var levelDictionary = new Dictionary<int, int>();
                foreach (var values in cellCandidatesDictionary.Values)
                {
                    if (!levelDictionary.ContainsKey(values.Count))
                    {
                        levelDictionary.Add(values.Count, 0);
                    }
                    levelDictionary[values.Count]++;
                }
                result.ComplexitiesScore += levelDictionary.Select(x => x.Key * x.Value).Sum();
                int minWeight = cellsWithNonEmptyCandidates.Min(x => x.Count);
                return cellCandidatesDictionary.Where(x => x.Value.Count == minWeight)
                    .Select(x => new Tuple<SudokuCell, List<int>>(x.Key, x.Value.ToList()))
                    .FirstOrDefault();
            }
            return null;
        }

        private void FindAllCandidates(int[,] data)
        {
            cellCandidatesDictionary = new Dictionary<SudokuCell, List<int>>();
            for (int i = 0; i < BlockCount; i++)
            {
                for (int j = 0; j < BlockCount; j++)
                {
                    if (data[i, j] == 0)
                    {
                        for (int k = 1; k <= BlockCount; k++)
                        {
                            if (Validate(data, new SudokuCell(i, j), k))
                            {
                                SudokuCell newCell = new SudokuCell(i, j);
                                if (!cellCandidatesDictionary.ContainsKey(newCell))
                                {
                                    cellCandidatesDictionary.Add(newCell, new List<int>());
                                }
                                if (!cellCandidatesDictionary[newCell].Contains(k))
                                {
                                    cellCandidatesDictionary[newCell].Add(k);
                                }

                            }
                        }
                    }
                }
            }
            ExcludeUnnecessary();
        }

        //Todo: Should be implemented for reduce all possible values for each row, column and block
        private void ExcludeUnnecessary()
        {
        }
    }
}
