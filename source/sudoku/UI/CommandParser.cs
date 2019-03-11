using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sudoku.Console
{
    public class CommandParser
    {
        private readonly List<string> _arguments;

        public string Name { get; }
        public IEnumerable<string> Arguments => _arguments;

        public CommandParser(string str)
        {
            _arguments = new List<string>();

            var strings = Regex.Split(str, @"(?<=^[^\""]*(?:\""[^\""]*\""[^\""]*)*) (?=(?:[^\""]*\""[^\""]*\"")*[^\""]*$)");
            for (int i = 0; i < strings.Length; i++)
            {
                if (i == 0)
                {
                    Name = strings[i];
                }
                else
                {
                    var argument = strings[i];
                    var regex = new Regex("\"(.*?)\"", RegexOptions.Singleline);
                    var match = regex.Match(argument);

                    if (match.Captures.Count > 0)
                    {
                        var captureQuotedText = new Regex("[^\"]*[^\"]");
                        var quoted = captureQuotedText.Match(match.Captures[0].Value);
                        argument = quoted.Captures[0].Value;
                    }
                    _arguments.Add(argument);
                }
            }
        }
    }
}
