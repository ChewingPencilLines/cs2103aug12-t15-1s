
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class CommandParser
    {
        public Operation ParseOperation(string input)
        {
            // Search for delimiters
            int? [,] indexOfDelimiters = StringParser.FindIndexOfDelimiters(input);
            // Split string input into words
            string [] inputWords = StringParser.SplitStringIntoWords(input);
            // Search for command keyword
            int? indexOfCommand = StringParser.SearchForCommandKeyword(inputWords);
            // Split command from string
            CommandType command = StringParser.SplitCommandFromString(ref inputWords, indexOfCommand);
            // Search for dates/times
            DateTime [] taskTime = StringParser.SearchForDateTime(input);
            return new Operation();
        }
    }
}
