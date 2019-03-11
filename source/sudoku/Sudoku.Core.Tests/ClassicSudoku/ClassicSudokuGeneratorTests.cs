
using NUnit.Framework;
using Sudoku.Core.ClassicSudoku;
using Sudoku.Core.Common;
using Sudoku.Core.Enums;

namespace Sudoku.Core.Tests.ClassicSudoku
{
    [TestFixture()]
    public class ClassicSudokuGeneratorTests
    {
        [Test()]
        public void GenerateTest()
        {
            var solver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
            var generator = new ClassicSudokuGenerator(solver);
            var level = new SudokuLevel(ESudokuLevel.Medium);
            var data = generator.Generate(level);
            var resultOfSolvingGeneratedSudoku = solver.Solve(data.Data);
            Assert.IsTrue(resultOfSolvingGeneratedSudoku.HasUniqueSolution);
        }
    }
}