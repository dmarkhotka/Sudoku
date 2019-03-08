using System;
using Core.ClassicSudoku;
using Core.Common;
using Core.Data;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Data;
using Core.Interfaces.Sudoku;

namespace Core
{
    public class Sudoku: ISudoku
    {
        ISudokuSolver _solver;
        ISudokuGenerator _generator;
        DataLoadFactory _loadFactory;

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
                case ESudokuType.Classic9x9:
                    solver = new ClassicSudokuSolver(EClassicSudokuType.Classic9x9);
                    generator =  new ClassicSudokuGenerator((IClassicSudokuSolver)solver);
                    break;
                case ESudokuType.Classic16x16:
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
