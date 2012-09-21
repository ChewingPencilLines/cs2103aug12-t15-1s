using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    enum CommandType { ADD, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO };
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
        /// Operation search an array of strings against the list of command words.
        /// If exactly one command word is found, it returns the positive index of the position in the input array
        /// where it was found. If more than one matching words are found, it returns the negative of the total number
        /// of matching words. If no matches were found, it returns a null-value.
        /// </summary>
        /// <param name="words">Input array of words</param>
        /// <returns>Null if no matching results, index of word if one match, negative of total matches if more than one match</returns>
        internal static int? SearchForCommandKeyword(string [] words)
        {
            int matchCount = 0;
            int matchingIndex = -1;
            for(int i = 0; i < words.Length; i++)
            {
                foreach (List<String> specificCommandKeywords in commandKeywords)
                {
                    foreach (string matchingCommand in specificCommandKeywords)
                    {
                        if (words[i] == matchingCommand)
                        {
                            if (matchCount == 0) matchingIndex = i;                            
                            matchCount++;
                        }
                    }

                }
            }
            if (matchCount == 0)
                return null;
            else if (matchCount == 1)
                return matchingIndex;
            else if (matchCount > 1)
                return matchCount * -1;
            else throw new Exception("Fatal logic error!");
        }

        internal static DateTime[] SearchForDateTime(string input)
        {
            throw new NotImplementedException();
        }

        internal static CommandType SplitCommandFromString(ref string[] words, int? index)
        {
            throw new NotImplementedException();
        }

        internal static string[] SplitStringIntoWords(string input)
        {
            throw new NotImplementedException();
        }
    }
}
