using Core.Interfaces;
using SudokuConsole.Interfaces;

namespace SudokuConsole.Commands
{
    internal class LoadFromFileCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "l", "load" };
        internal override string[] CommandArgs => new[] { "<path>"};

        internal override string HelpDescription => "Load game from a file";

        internal LoadFromFileCommand(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
        }

        internal override string Execute(string[] args)
        {
            if (args.Length == 1)
            {
                _gameContext.CurrentGame = _sudoku.LoadFromFile(args[0]);
                return PrintHelper.PrintMatrix(_gameContext.CurrentGame);
            }
            return "Please, enter correct path to file.";
        }
    }
}
