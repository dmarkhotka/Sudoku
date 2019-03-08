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

        private static string Execute(CommandParser consoleParser)
        {
            throw new NotImplementedException();
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

        static void Main1(string[] args)
        {
            int[,] input =
            {{ 5,1,7,6,0,0,0,3,4},
             { 2,8,9,0,0,4,0,0,0},
             { 3,4,6,2,0,5,0,9,0},
             { 6,0,2,0,0,0,0,1,0},
             { 0,3,8,0,0,6,0,4,7},
             { 0,0,0,0,0,0,0,0,0},
             { 0,9,0,0,0,0,0,7,8},
             { 7,0,3,4,0,0,5,6,0},
             { 0,0,0,0,0,0,0,0,0} };
            int[,] input2 =
            {{ 5,1,7,6,0,0,0,3,4},
             { 0,8,9,0,0,4,0,0,0},
             { 3,0,6,2,0,5,0,9,0},
             { 6,0,0,0,0,0,0,1,0},
             { 0,3,0,0,0,6,0,4,7},
             { 0,0,0,0,0,0,0,0,0},
             { 0,9,0,0,0,0,0,7,8},
             { 7,0,3,4,0,0,5,6,0},
             { 0,0,0,0,0,0,0,0,0} };
            int[,] hard =
            {{ 8,5,0,0,0,2,4,0,0},
             { 7,2,0,0,0,0,0,0,9},
             { 0,0,4,0,0,0,0,0,0},
             { 0,0,0,1,0,7,0,0,2},
             { 3,0,5,0,0,0,9,0,0},
             { 0,4,0,0,0,0,0,0,0},
             { 0,0,0,0,8,0,0,7,0},
             { 0,1,7,0,0,0,0,0,0},
             { 0,0,0,0,3,6,0,4,0} };
            int[,] diff =
            {{ 0,0,5,3,0,0,0,0,0},
             { 8,0,0,0,0,0,0,2,0},
             { 0,7,0,0,1,0,5,0,0},
             { 4,0,0,0,0,5,3,0,0},
             { 0,1,0,0,7,0,0,0,6},
             { 0,0,3,2,0,0,0,8,0},
             { 0,6,0,5,0,0,0,0,9},
             { 0,0,4,0,0,0,0,3,0},
             { 0,0,0,0,0,9,7,0,0} };
            int[,] veryHard =
            {{ 0,7,0,2,5,0,4,0,0},
             { 8,0,0,0,0,0,9,0,3},
             { 0,0,0,0,0,3,0,7,0},
             { 7,0,0,0,0,4,0,2,0},
             { 1,0,0,0,0,0,0,0,7},
             { 0,4,0,5,0,0,0,0,8},
             { 0,9,0,6,0,0,0,0,0},
             { 4,0,1,0,0,0,0,0,5},
             { 0,0,7,0,8,2,0,3,0} };//3
            int[,] empty =
            {{ 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0},
             { 0,0,0,0,0,0,0,0,0} };


            int[,] Easy =
            {{5,1,0,0,0,0,0,8,3},
            {8,0,0,4,1,6,0,0,5},
            {0,0,0,0,0,0,0,0,0},
            {0,9,8,5,0,4,6,1,0},
            {0,0,0,9,0,1,0,0,0},
            {0,6,4,2,0,3,5,7,0},
            {0,0,0,0,0,0,0,0,0},
            {6,0,0,1,5,7,0,0,4},
            {7,8,0,0,0,0,0,9,6}};

            int[,] Medium =
            {{7,0,0,0,9,0,0,0,3},
            {2,0,0,4,6,8,0,0,1},
            {0,0,8,0,0,0,6,0,0},
            {0,4,0,0,2,0,0,9,0},
            {0,0,0,3,0,4,0,0,0},
            {0,8,0,0,1,0,0,3,0},
            {0,0,9,0,0,0,7,0,0},
            {5,0,0,1,4,2,0,0,6},
            {8,0,0,0,5,0,0,0,2}};

            int[,] Hard =
            {{0,5,2,3,0,0,6,0,0},
            {6,0,0,0,4,0,0,0,3},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,6,3,0,0,1,0},
            {4,7,0,0,0,0,0,3,5},
            {0,2,0,0,5,8,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {1,0,0,0,9,0,0,0,6},
            {0,0,5,0,0,1,7,2,0}};

            int[,] Samurai =
           {{5,0,0,0,0,0,1,0,7},
            {0,0,4,3,0,0,5,0,0},
            {0,0,0,2,0,0,0,8,0},
            {0,9,0,4,0,2,0,0,0},
            {4,0,0,0,0,0,0,0,6},
            {0,0,0,1,0,3,0,5,0},
            {0,8,0,0,0,4,0,0,0},
            {0,0,2,0,0,6,7,0,0},
            {3,0,9,0,0,0,0,0,1}};
            int[,] dmFirst =
             {{0,0,0,0,4,0,6,0,9},
              {0,0,0,0,8,0,1,0,7},
              {0,0,0,1,0,7,0,0,0},
              {1,0,0,7,0,4,9,6,0},
              {0,0,8,2,0,1,0,5,0},
              {0,5,6,0,0,0,0,0,0},
              {0,0,1,5,7,0,8,9,4},
              {0,0,0,9,0,8,0,1,0},
              {0,0,7,0,0,2,5,0,0}};

            int[,] unsolver =
{{0,1,0,0,4,0,6,0,0,},
{0,0,0,0,0,8,1,0,0,},
{0,0,8,0,2,0,0,0,0,},
{0,0,0,0,0,0,9,0,7,},
{0,8,0,0,0,0,0,5,0,},
{0,0,6,7,0,3,0,0,0,},
{0,3,1,5,0,6,0,9,0,},
{0,0,4,9,0,0,8,0,0,},
{0,0,7,0,0,0,0,3,0}};

            //Solve(input);
            //Solve(input2);
            //Solve(hard);
            //Solve(diff);
            //Solve(veryHard);
            //Solve(unsolver);
            //Solve(empty);

            //Solve(Easy);
            //Solve(Medium);
            //Solve(Hard);
            //Solve(Samurai);

            //Solve(dmFirst);
            //Generate();

        }

        //public static void Generate()
        //{
        //    var sudoku = Sudoku.GetSudoku(ESudokuType.Classic16x16);
        //    for (int i = 0; i < 10; i++){
        //        var result = sudoku.Generate(ESudokuLevel.Unbelievable);
        //        Console.WriteLine($"{i}.\tBackTrackCount = {result.BackTrackCount}, SolutionCount = {result.SolutionCount}, LevelCount = {result.ComplexitiesScore}");
        //        //result.Data
        //        PrintMatrix(result.Data);
        //        result = sudoku.Solve(result.Data);
        //        PrintMatrix(result.Data);
        //        Console.WriteLine($"\tBackTrackCount = {result.BackTrackCount}, SolutionCount = {result.SolutionCount}, LevelCount = {result.ComplexitiesScore}");
        //        //Console.WriteLine();
        //        Console.WriteLine();
        //    } 
        //}

        //public static void Solve(int[,] data)
        //{
        //    var sd = new ClassicSudokuSolver();
        //    //sd.Print = 
        //    //(x,y) => { 
        //        //PrintMatrix(x);
        //        //if (y != null)
        //        //{ PrintPossibleValues(y); }
        //        //Console.ReadKey(); 
        //        //};

        //    PrintMatrix(data);
        //    var result = sd.Solve(data);
        //    Console.WriteLine($"BackTrackCount = {result.BackTrackCount}, SolutionCount = {result.SolutionCount}, LevelCount = {result.ComplexitiesScore}");
        //    PrintMatrix(result.Data);
        //}

        //private static void PrintMatrix(int[,] matrix)
        //{
        //    for (int i = 0; i < 9; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            Console.Write(matrix[i, j] == 0 ? " " : matrix[i, j].ToString());
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine();
        //}

        //private static void PrintPossibleValues(Dictionary<SudokuCell, List<int>> possibleValues)
        //{
        //    var x = Console.CursorLeft + 25;
        //    var y = Console.CursorTop - 9;
        //    Console.SetCursorPosition(x, y++);
        //    for (int i = 0; i < 9; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            var cell = new SudokuCell(i, j);
        //            if (possibleValues.ContainsKey(cell))
        //            {
        //                Console.Write($"{String.Join("", possibleValues[cell])}\t");
        //            }
        //            else
        //            {
        //                Console.Write("0\t");
        //            }
        //        }
        //        Console.SetCursorPosition(x, y++);
        //    }
        //}
    }
}
