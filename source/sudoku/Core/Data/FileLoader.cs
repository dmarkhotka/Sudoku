using System;
using System.IO;
using Sudoku.Core.Interfaces.Data;

namespace Sudoku.Core.Data
{
    public class FileLoader : IDataLoader
    {
        readonly string _path;

        public FileLoader(string path)
        {
            _path = path;
        }

        public int[,] Load()
        {
            var file = File.ReadAllLines(_path);
            int[,] result = new int[file.Length,file.Length];
            for(int i = 0; i< file.Length; i++)
            {
                if (file[i].Length != file.Length)
                {
                    throw new NotSupportedException(Resources.FileLoader_Load_UnsupportedFileFormat);
                }
                for (int j = 0; j < file[i].Length; j++)
                {
                    if (int.TryParse(file[i][j].ToString(), out int number))
                    {
                        result[i, j] = number;
                    }
                }
            }
            return result;
        }
    }
}
