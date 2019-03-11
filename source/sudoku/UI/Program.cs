using System;
using Sudoku.Console.Commands;
using Sudoku.Console.Enums;
using Sudoku.Core.Enums;

namespace Sudoku.Console
{
    class Program
    {
        static void Main()
        {
            var context = new GameContext();
            var sudoku = Core.Sudoku.GetSudoku(ESudokuType.Classic9X9);
            CommandManager commandManager = new CommandManager();
            commandManager.AddCommand(ECommandType.LoadFromFile, new LoadFromFileCommand(context, sudoku));
            commandManager.AddCommand(ECommandType.Solve, new SolveCommand(context, sudoku));
            commandManager.AddCommand(ECommandType.Generate, new GenerateCommand(context, sudoku));
            commandManager.AddCommand(ECommandType.Help, new HelpCommand(commandManager));

            Run(commandManager);
        }

        private static void Run(CommandManager commandManager)
        {
            Command helpCommand = commandManager.GetCommand(ECommandType.Help);
            WriteToConsole(helpCommand.Execute(null));
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
                    WriteToConsole(string.Format(Resources.Program_Run_SomethingWentWrong, ex.Message));
                }
            }
        }

        public static string ReadFromConsole()
        {
            System.Console.Write(Resources.Program_ReadFromConsole_PromptMessage);
            return System.Console.ReadLine();
        }

        public static void WriteToConsole(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                System.Console.WriteLine(message);
            }
        }
    }
}
