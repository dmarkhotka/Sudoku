using System;
using Core;
using Core.Enums;
using SudokuConsole.Commands;

namespace SudokuConsole
{
    class Program
    {
        private const string PromtMessage = "sudoku";

        static void Main()
        {
            var context = new GameContext();
            var sudoku = Sudoku.GetSudoku(ESudokuType.Classic9x9);
            CommandManager commandManager = new CommandManager(context, sudoku);
            commandManager.AddCommand(new LoadFromFileCommand(context, sudoku));
            commandManager.AddCommand(new SolveCommand(context, sudoku));
            commandManager.AddCommand(new GenerateCommand(context, sudoku));
            commandManager.AddCommand(new HelpCommand(commandManager));

            Run(commandManager);
        }

        private static void Run(CommandManager commandManager)
        {
            while (true)
            {
                var input = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                try
                {
                    var consoleParser = new CommandParser(input);

                    var output = commandManager.Execute(consoleParser);

                    WriteToConsole(output);
                }
                catch (Exception ex)
                {
                    WriteToConsole($"Something went wrong: {ex.Message}");
                }
            }
        }

        public static string ReadFromConsole()
        {
            Console.Write($"{PromtMessage}> ");
            return Console.ReadLine();
        }

        public static void WriteToConsole(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
        }
    }
}
