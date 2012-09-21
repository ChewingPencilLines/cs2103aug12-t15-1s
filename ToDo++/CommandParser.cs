
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
            CommandType command = CommandType.INVALID;
            // Search for delimiters
            int [,] indexOfDelimiters = StringParser.FindIndexOfDelimiters(input);
            // Split string input into words
            List<string> inputWords = StringParser.SplitStringIntoWords(input);
            // Search for command keyword            
            int indexOfCommandKeyword = -1;
            int matchCount = StringParser.SearchForCommandKeyword(inputWords, ref command, ref indexOfCommandKeyword);
            if (indexOfCommandKeyword < 0) return null; // failed, either no match or multiple matches
            StringParser.RemoveWordFromSentence_ByIndex(ref inputWords, indexOfCommandKeyword);
            // Search for dates/times
            DateTime [] taskTime = StringParser.SearchForDateTime(input);
            return new Operation();
        }
    }
}
