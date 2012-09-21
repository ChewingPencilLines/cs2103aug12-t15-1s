using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    enum CommandType { ADD, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO, INVALID };
    public static class StringParser
    {        
        static string[] unaryDelimiters = { "\"", "'" };
        static string[,] binaryDelimiters = { { "<", ">" }, { "(", ")" }, { "<", ">" } };
        static List<List<string>> commandKeywords = new List<List<string>>()
        {
            new List<String> { "add" },
            new List<String> { "display" },
            new List<String> { "sort" },
            new List<String> { "search" },
            new List<String> { "modify" },
            new List<String> { "undo" },
            new List<String> { "redo" },
        };
        static string monthKeywords;
        static string dayKeywords;
        static string timeKeywords;

        internal static int? [,] FindIndexOfDelimiters(string input)
        {
            return new int? [,] { { null, null } };
        }

        /// <summary>
        /// Operation search an input list of strings against the list of command words.
        /// Upon a succesful first match, the operation updates the command and indexOfCommand input parameters by reference.
        /// Returns the number of matches at the end of search.
        /// </summary>
        /// <param name="words">Input array of words</param>
        /// <param name="command">Command type to be updated by reference on first match</param>
        /// <param name="indexOfCommand">Index of command to be updated by reference on first match</param>
        /// <returns>Number of matches</returns>
        internal static int SearchForCommandKeyword(List<string> words, ref CommandType command, ref int indexOfCommand)
        {
            int index = 0;         
            int matchCount = 0;
            CommandType commandType = 0;
            foreach(string word in words)
            {
                commandType = 0;
                foreach (List<String> specificCommandKeywords in commandKeywords)
                {
                    foreach (string matchingCommand in specificCommandKeywords)
                    {
                        if (word == matchingCommand)
                        {
                            if (matchCount == 0)
                            {
                                indexOfCommand = index;
                                if (command > CommandType.INVALID)
                                    throw new Exception("Fatal error: Logic flow error!");
                                else command = commandType;
                            }
                            matchCount++;
                        }
                    }
                    commandType++;
                }
                index++;
            }
            return matchCount;
        }

        internal static void SplitCommandFromSentence(ref List<string> words, int index)
        {
            string command = words.ElementAt(index);
            words.RemoveAt(index);
        }

        internal static DateTime[] SearchForDateTime(string input)
        {
            throw new NotImplementedException();
        }

        internal static List<string> SplitStringIntoWords(string input)
        {
            throw new NotImplementedException();
        }
    }
}
