using Moq;
using NUnit.Framework;
using Sudoku.Core.Common;
using Sudoku.Core.Enums;
using Sudoku.Core.Interfaces.Sudoku;

namespace Sudoku.Core.Tests.Common
{
    [TestFixture()]
    public class SudokuLevelTests
    {
        [Test()]
        public void IsMatchingTest_Positive()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            sudokuResultMock.SetupGet(r => r.HasUniqueSolution).Returns(true);
            sudokuResultMock.SetupGet(r => r.BackTrackCount).Returns(200);
            Assert.IsTrue(sudokuLevel.IsMatching(sudokuResultMock.Object));
        }

        [Test()]
        public void IsMatchingTest_Negative_NotUniqueSolution()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            sudokuResultMock.SetupGet(r => r.HasUniqueSolution).Returns(false);
            sudokuResultMock.SetupGet(r => r.BackTrackCount).Returns(200);
            Assert.IsFalse(sudokuLevel.IsMatching(sudokuResultMock.Object));
        }

        [Test()]
        public void IsMatchingTest_Negative_NotEnoughLevel()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            sudokuResultMock.SetupGet(r => r.HasUniqueSolution).Returns(true);
            Assert.IsFalse(sudokuLevel.IsMatching(sudokuResultMock.Object));
        }

        [Test()]
        public void IsMatchingTest_Negative_UpperLevel()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            sudokuResultMock.SetupGet(r => r.HasUniqueSolution).Returns(true);
            sudokuResultMock.SetupGet(r => r.BackTrackCount).Returns(1000);
            Assert.IsFalse(sudokuLevel.IsMatching(sudokuResultMock.Object));
        }

        [Test()]
        public void IsMaxLimitExceededTest_Positive()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            sudokuResultMock.SetupGet(r => r.BackTrackCount).Returns(1000);
            Assert.IsTrue(sudokuLevel.IsMaxLimitExceeded(sudokuResultMock.Object));
        }

        [Test()]
        public void IsMaxLimitExceededTest_Negativee()
        {
            var sudokuLevel = new SudokuLevel(ESudokuLevel.Medium);
            var sudokuResultMock = new Mock<ISudokuResult>();
            Assert.IsFalse(sudokuLevel.IsMaxLimitExceeded(sudokuResultMock.Object));
        }
    }
}