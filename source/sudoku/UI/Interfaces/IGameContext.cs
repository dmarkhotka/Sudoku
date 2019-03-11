namespace Sudoku.Console.Interfaces
{
    public interface IGameContext
    {
        int[,] CurrentGame { get; set; }
    }
}
