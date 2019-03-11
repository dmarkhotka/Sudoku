using System.Collections.Generic;
using Sudoku.Console.Commands;

namespace Sudoku.Console.Interfaces
{
    internal interface ICommandContainer
    {
        IEnumerable<Command> Commands { get; }
    }
}
