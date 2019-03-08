using System;
using System.Linq;
using Core.Enums;
using Core.Interfaces;
using SudokuConsole.Interfaces;

namespace SudokuConsole.Commands
{
    internal class GenerateCommand : Command
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;

        internal override string[] CommandNames => new[] { "g", "generate" };
        internal override string[] CommandArgs => new[] { "[level]" };

        internal override string HelpDescription => "Generate a game by level. 1-Easy, 2-Medium, 3-Hard, 4-Samurai";

        internal GenerateCommand(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
        }

        internal override string Execute(string[] args)
        {
            var stringLevel = args.FirstOrDefault();
            if (Enum.TryParse<ESudokuLevel>(stringLevel ?? ESudokuLevel.Medium.ToString(), out ESudokuLevel level))
            {
                var result = _sudoku.Generate(level);
                _gameContext.CurrentGame = result.Data;
                return PrintHelper.PrintResult(result);
            }
            return "Please, enter correct level number.";
        }
    }
}
