namespace Sudoku.Console.Commands
{
    internal abstract class Command
    {
        internal abstract string[] CommandNames { get; }
        internal abstract string HelpDescription { get; }
        internal abstract string Execute(string[] args);

        internal virtual string[] CommandArgs => new string[0];

        internal virtual string GetDescription()
        {
            return $" '{string.Join<string>("|", CommandNames)} {string.Join<string>(" ", CommandArgs)}'\t{HelpDescription}";
        }
    }
}
