using System;
using System.IO;
using Core.Interfaces.Data;

namespace Core.Data
{
    internal class FileLoader : IDataLoader
    {
        string _path;

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
                    throw new NotSupportedException($"Unsuported file format: Column count not equals row count.");
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
