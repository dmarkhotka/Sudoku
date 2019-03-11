using System;
using System.Linq;
using Sudoku.Console.Interfaces;
using Sudoku.Core.Enums;
using Sudoku.Core.Interfaces;

namespace Sudoku.Console.Commands
{
    internal class GenerateCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "g", "generate" };
        internal override string[] CommandArgs => new[] { Resources.GenerateCommand_CommandArgs_level };

        internal override string HelpDescription => Resources.GenerateCommand_HelpDescriptioni;

        internal GenerateCommand(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
        }

        internal override string Execute(string[] args)
        {
            var stringLevel = args.FirstOrDefault();
            if (Enum.TryParse(stringLevel ?? ESudokuLevel.Medium.ToString(), out ESudokuLevel level))
            {
                var result = _sudoku.Generate(level);
                _gameContext.CurrentGame = result.Data;
                return PrintHelper.PrintResult(result);
            }
            return Resources.GenerateCommand_Execute_IncorectLevelNumber;
        }
    }
}
