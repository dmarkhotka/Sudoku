using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Console.Commands;
using Sudoku.Console.Enums;
using Sudoku.Console.Interfaces;

namespace Sudoku.Console
{
    internal class CommandManager: ICommandContainer
    {
        private readonly Dictionary<ECommandType, Command> _commands;

        public CommandManager()
        {
            _commands = new Dictionary<ECommandType, Command>();
        }

        public IEnumerable<Command> Commands => _commands.Values;


        internal string Execute(CommandParser consoleParser)
        {
            foreach (var command in Commands)
            {
                foreach (var name in command.CommandNames)
                {
                    if (string.Equals(name, consoleParser.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return command.Execute(consoleParser.Arguments.ToArray());
                    }
                }
            }

            return string.Format(Resources.CommandManager_Execute_CommandNotImplemented, consoleParser.Name);
        }

        internal void AddCommand(ECommandType type, Command command)
        {
            _commands.Add(type, command);
        }

        public Command GetCommand(ECommandType type)
        {
            return _commands.ContainsKey(type) ? _commands[type] : null;
        }
    }
}
