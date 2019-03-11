using NUnit.Framework;
using Sudoku.Core.Common;

namespace Sudoku.Core.Tests.Common
{
    [TestFixture()]
    public class SudokuResultTests
    {
        [Test()]
        public void SudokuResult_NoSolution()
        {
            var result = new SudokuResult();
            Assert.IsFalse(result.HasSolution);
        }

        [Test()]
        public void SudokuResult_Unique()
        {
            var result = new SudokuResult();
            Assert.IsTrue(result.IsUnique);
        }
        [Test()]
        public void AddSolutionTest_HasSolution()
        {
            var result = new SudokuResult();
            result.AddSolution(new int[1, 1]);
            Assert.IsTrue(result.HasSolution);
        }

        [Test()]
        public void AddSolutionTest_NotUnique()
        {
            var result = new SudokuResult();
            result.AddSolution(new int[1, 1]);
            result.AddSolution(new int[1, 1]);
            Assert.IsFalse(result.IsUnique);
        }
    }
}