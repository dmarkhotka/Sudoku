
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using SudokuConsole.Commands;
using SudokuConsole.Interfaces;

namespace SudokuConsole
{
    internal class CommandManager: ICommandContainer
    {
        private readonly ISudoku _sudoku;
        private readonly IGameContext _gameContext;
        private readonly List<Command> _commands;

        public CommandManager(IGameContext gameContext, ISudoku sudoku)
        {
            _sudoku = sudoku;
            _gameContext = gameContext;
            _commands = new List<Command>();
        }

        public IEnumerable<Command> Commands => _commands;


        internal string Execute(CommandParser consoleParser)
        {
            foreach (var command in Commands)
            {
                foreach (var name in command.CommandNames)
                {
                    if (string.Equals(name, consoleParser.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return command.Execute(consoleParser.Arguments.ToArray<string>());
                    }
                }
            }

            return $"Command '{consoleParser.Name}' not implemented.";
        }

        internal void AddCommand(Command command)
        {
            _commands.Add(command);
        }
    }
}
