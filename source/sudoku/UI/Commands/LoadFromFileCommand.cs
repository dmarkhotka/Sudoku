using Sudoku.Console.Interfaces;
using Sudoku.Core.Interfaces;

namespace Sudoku.Console.Commands
{
    internal class LoadFromFileCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "l", "load" };
        internal override string[] CommandArgs => new[] { Resources.LoadFromFileCommand_CommandArgs_path};

        internal override string HelpDescription => Resources.LoadFromFileCommand_HelpDescription;

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
            return Resources.LoadFromFileCommand_Execute_IncorrectPath;
        }
    }
}
