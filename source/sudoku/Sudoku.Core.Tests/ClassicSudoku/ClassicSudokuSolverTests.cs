
using System.Linq;
using NUnit.Framework;
using Sudoku.Core.ClassicSudoku;

namespace Sudoku.Core.Tests.ClassicSudoku
{
    [TestFixture()]
    public class ClassicSudokuSolverTests
    {
        private readonly int[,] _sudokuMediumLevel =
        {
            {7, 0, 0, 0, 9, 0, 0, 0, 3},
            {2, 0, 0, 4, 6, 8, 0, 0, 1},
            {0, 0, 8, 0, 0, 0, 6, 0, 0},
            {0, 4, 0, 0, 2, 0, 0, 9, 0},
            {0, 0, 0, 3, 0, 4, 0, 0, 0},
            {0, 8, 0, 0, 1, 0, 0, 3, 0},
            {0, 0, 9, 0, 0, 0, 7, 0, 0},
            {5, 0, 0, 1, 4, 2, 0, 0, 6},
            {8, 0, 0, 0, 5, 0, 0, 0, 2}
        };

        private readonly int[,] _sudokuNotUniqueSolution =
        {
            {7, 0, 0, 0, 9, 0, 0, 0, 3},
            {2, 0, 0, 4, 6, 8, 0, 0, 1},
            {0, 0, 8, 0, 0, 0, 6, 0, 0},
            {0, 4, 0, 0, 2, 0, 0, 0, 0},
            {0, 0, 0, 3, 0, 4, 0, 0, 0},
            {0, 8, 0, 0, 1, 0, 0, 3, 0},
            {0, 0, 9, 0, 0, 0, 7, 0, 0},
            {5, 0, 0, 1, 4, 2, 0, 0, 6},
            {8, 0, 0, 0, 5, 0, 0, 0, 2}
        };

        private readonly int[,] _sudokuNoAnySolution =
        {
            {7, 0, 0, 0, 9, 0, 0, 0, 3},
            {2, 0, 0, 4, 6, 8, 0, 0, 1},
            {0, 0, 8, 0, 0, 0, 6, 0, 0},
            {0, 4, 0, 0, 2, 0, 0, 9, 0},
            {0, 0, 0, 3, 0, 4, 0, 0, 0},
            {0, 8, 0, 0, 1, 0, 2, 3, 0},
            {0, 0, 9, 0, 0, 0, 7, 0, 0},
            {5, 0, 0, 1, 4, 2, 0, 0, 6},
            {8, 0, 0, 0, 5, 0, 0, 0, 2}
        };

        private readonly int[,] _sudokuSolved =
        {
            {7, 5, 6, 2, 9, 1, 8, 4, 3},
            {2, 9, 3, 4, 6, 8, 5, 7, 1},
            {4, 1, 8, 5, 7, 3, 6, 2, 9},
            {3, 4, 5, 6, 2, 7, 1, 9, 8},
            {9, 7, 1, 3, 8, 4, 2, 6, 5},
            {6, 8, 2, 9, 1, 5, 4, 3, 7},
            {1, 2, 9, 8, 3, 6, 7, 5, 4},
            {5, 3, 7, 1, 4, 2, 9, 8, 6},
            {8, 6, 4, 7, 5, 9, 3, 1, 2}
        };

        [Test()]
        public void GenerateSolvedTest()
        {
            var sudokuSolver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
            var data = sudokuSolver.GenerateSolved().Data;

            for (int i = 0; i < 9; i++)
            {
                var possibleRowValues = Enumerable.Range(1, 9).ToList();
                var possibleColValues = Enumerable.Range(1, 9).ToList();
                for (int j = 0; i < 9; i++)
                {
                    if (!possibleRowValues.Remove(data[i, j]) || !possibleColValues.Remove(data[j, i]))
                    {
                        Assert.Fail("Column or Row contain not unique value.");
                    }
                }
            }

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    var possibleValues = Enumerable.Range(1, 9).ToList();
                    for (int i = x * 3; i < x * 3 + 3; i++)
                    {
                        for (int j = y * 3; j < y * 3 + 3; j++)
                        {
                            if (!possibleValues.Remove(data[i, j]))
                            {
                                Assert.Fail("Block contain not unique value.");
                            }
                        }
                    }
                }
            }
        }

        [Test()]
        public void SolveTest_Positive()
        {
            var sudokuSolver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
            var data = sudokuSolver.Solve(_sudokuMediumLevel);
            Assert.AreEqual(_sudokuSolved, data.Data);
        }

        [Test()]
        public void SolveTest_Negative_NoAnySolution()
        {
            var sudokuSolver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
            var data = sudokuSolver.Solve(_sudokuNoAnySolution);
            Assert.IsFalse(data.HasSolution);
        }
        [Test()]
        public void SolveTest_Negative_NotUniqueSolution()
        {
            var sudokuSolver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
            var data = sudokuSolver.Solve(_sudokuNotUniqueSolution);
            Assert.IsFalse(data.IsUnique);
        }
    }
}