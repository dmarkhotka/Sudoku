using System.Text;
using SudokuConsole.Interfaces;

namespace SudokuConsole.Commands
{
    internal class HelpCommand : Command
    {
        private readonly ICommandContainer _commandContainer;

        internal override string[] CommandNames => new[] { "h", "help" };

        internal override string HelpDescription => "\tShow this message";

        internal HelpCommand(ICommandContainer commandContainer)
        {
            _commandContainer = commandContainer;
        }

        internal override string Execute(string[] args)
        {
            StringBuilder helpString = new StringBuilder();
            helpString.AppendLine("Usage:");
            foreach (var command in _commandContainer.Commands)
            {
                helpString.AppendLine(command.GetDescription());
            }
            return helpString.ToString();
        }
    }
}
