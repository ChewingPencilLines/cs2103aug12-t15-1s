
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
            if (indexOfCommand == null || indexOfCommand < 0) return null; // failed, either no match or multiple matches
            // Split command from string
            CommandType command = StringParser.SplitCommandFromSentence(ref inputWords, (int)indexOfCommand);
            // Search for dates/times
            DateTime [] taskTime = StringParser.SearchForDateTime(input);
            return new Operation();
        }
    }
}
