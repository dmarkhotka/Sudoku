using Sudoku.Core.Interfaces.Data;

namespace Sudoku.Core.Data
{
    internal class DataLoadFactory
    {
        internal IDataLoader GetFileLoader(string path)
        {
            return new FileLoader(path);
        }
    }
}