using Sudoku.Console.Interfaces;
using Sudoku.Core.Interfaces;

namespace Sudoku.Console.Commands
{
    internal class SolveCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "s", "solve" };

        internal override string HelpDescription => Resources.SolveCommand_HelpDescription;

        internal SolveCommand(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
        }

        internal override string Execute(string[] args)
        {
            if (_gameContext.CurrentGame == null)
            {
                return Resources.SolveCommand_Execute_NotLoadedGame;
            }

            var result = _sudoku.Solve(_gameContext.CurrentGame);

            if (!result.HasSolution)
            {
                return Resources.SolveCommand_Execute_NoSolution;
            }
            if (!result.IsUnique)
            {
                return Resources.SolveCommand_Execute_MoreThanOneSolution;
            }

            return PrintHelper.PrintResult(result);
        }
    }
}
