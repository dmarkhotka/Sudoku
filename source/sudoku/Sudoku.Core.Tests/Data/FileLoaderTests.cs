using System;
using System.IO;
using NUnit.Framework;
using Sudoku.Core.Data;

namespace Sudoku.Core.Tests.Data
{
    [TestFixture()]
    public class FileLoaderTests
    {
        [TestCase(@"TestData\EasyLevel.txt")]
        [TestCase(@"TestData\MediumLevel.txt")]
        [TestCase(@"TestData\HardLevel")]
        [TestCase(@"TestData\SamuraiLevel.txt")]
        public void LoadTestPositive(string filename)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var fileLoader = new FileLoader(Path.Combine(path, filename));
            Assert.NotNull(fileLoader.Load());
        }

        [TestCase(@"TestData\WrongFile.txt")]
        public void LoadTestNegative(string filename)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var fileLoader = new FileLoader(Path.Combine(path, filename));
            Assert.Throws<NotSupportedException>(() =>
            {
                fileLoader.Load();
            });
        }
    }
}