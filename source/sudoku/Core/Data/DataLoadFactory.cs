using System;
using Core.Interfaces.Data;

namespace Core.Data
{
    internal class DataLoadFactory
    {
        internal IDataLoader GetFileLoader(string path)
        {
            return new FileLoader(path);
        }
    }
}