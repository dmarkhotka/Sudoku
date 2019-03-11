using Sudoku.Console.Interfaces;

namespace Sudoku.Console
{
    public class GameContext: IGameContext
    {
        public int[,] CurrentGame { get; set; }
    }
}
