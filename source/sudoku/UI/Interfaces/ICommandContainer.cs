using System;
using System.Collections.Generic;
using SudokuConsole.Commands;

namespace SudokuConsole.Interfaces
{
    internal interface ICommandContainer
    {
        IEnumerable<Command> Commands { get; }
    }
}
