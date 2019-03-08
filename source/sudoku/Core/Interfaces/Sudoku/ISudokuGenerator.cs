
namespace Core.Interfaces.Sudoku
{
    public interface ISudokuGenerator
    {
        ISudokuResult Generate(ISudokuLevel level);
    }
}
