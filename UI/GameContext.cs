using SudokuConsole.Interfaces;

namespace SudokuConsole
{
    public class GameContext: IGameContext
    {
        public int[,] CurrentGame { get; set; }
    }
}
