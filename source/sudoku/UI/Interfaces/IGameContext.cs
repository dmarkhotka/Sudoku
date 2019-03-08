using System;
namespace SudokuConsole.Interfaces
{
    public interface IGameContext
    {
        int[,] CurrentGame { get; set; }
    }
}
