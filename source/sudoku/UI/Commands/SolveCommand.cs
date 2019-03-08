using Core.Interfaces;
using SudokuConsole.Interfaces;

namespace SudokuConsole.Commands
{
    internal class SolveCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "s", "solve" };

        internal override string HelpDescription => "\tSolve loaded or generated game";

        internal SolveCommand(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
        }

        internal override string Execute(string[] args)
        {
            if (_gameContext.CurrentGame == null)
            {
                return "Please, load or generate game.";
            }

            var result = _sudoku.Solve(_gameContext.CurrentGame);

            if (!result.HasSolution)
            {
                return "No any solution for this game.";
            }
            if (!result.IsUnique)
            {
                return "More than one solution for this game.";
            }

            return PrintHelper.PrintResult(result);
        }
    }
}
