using System;
using Sudoku.Core.ClassicSudoku;
using Sudoku.Core.Common;
using Sudoku.Core.Data;
using Sudoku.Core.Enums;
using Sudoku.Core.Interfaces;
using Sudoku.Core.Interfaces.Data;
using Sudoku.Core.Interfaces.Sudoku;

namespace Sudoku.Core
{
    public class Sudoku: ISudoku
    {
        readonly ISudokuSolver _solver;
        readonly ISudokuGenerator _generator;
        readonly DataLoadFactory _loadFactory;

        private Sudoku(ISudokuSolver solver, ISudokuGenerator generator)
        {
            _solver = solver;
            _generator = generator;
            _loadFactory = new DataLoadFactory();
        }

        public static ISudoku GetSudoku(ESudokuType type)
        {
            ISudokuSolver solver;
            ISudokuGenerator generator;
            switch (type)
            {
                case ESudokuType.Classic9X9:
                    solver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
                    generator =  new ClassicSudokuGenerator((IClassicSudokuSolver)solver);
                    break;
                case ESudokuType.Classic16X16:
                    solver = new ClassicSudokuSolver(EClassicSudokuType.Classic16x16);
                    generator = new ClassicSudokuGenerator((IClassicSudokuSolver)solver);
                    break;
                default:
                    throw new NotSupportedException(Enum.GetName(typeof(ESudokuType), type));
            }
            return new Sudoku(solver, generator);
        }

        public int[,] LoadFromFile(string path)
        {
            IDataLoader loader = _loadFactory.GetFileLoader(path);
            return loader.Load();
        }

        public ISudokuResult Solve(int[,] data)
        {
            var result = _solver.Solve(data);
            result.Level = SudokuLevel.GetLevel(result);
            return result;
        }

        public ISudokuResult Generate(ESudokuLevel level)
        {
            return _generator.Generate(new SudokuLevel(level));
        }
    }
}
