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
        static char[] unaryDelimiters = { '\'', '\"' };
        static char[,] binaryDelimiters = { { '[', ']' }, { '(', ')' }, { '{', '}' } };
        static List<List<string>> commandKeywords = new List<List<string>>()
        {
            //@todo: can possibly add commandType to zero-index so that order is not important
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


        internal static List<int[]> FindIndexOfDelimiters(string input)
        {
            List<int[]> indexOfDelimiters = new List<int[]>();
            // Split unarys first
            int startIndex = 0, endIndex = -1;
            for (int i = 0; i < unaryDelimiters.Length; i++)
            {
                endIndex = -1;
                do
                {
                    startIndex = input.IndexOf(unaryDelimiters[i], endIndex + 1);
                    endIndex = input.IndexOf(unaryDelimiters[i], startIndex + 1);
                    if (startIndex >= 0 && endIndex > 0)
                    {
                        int[] index = new int[2] { startIndex, endIndex };
                        indexOfDelimiters.Add(index);
                    }
                    else break;
                } while (true);

            }
            return indexOfDelimiters;
        }

        /// <summary>
        /// Operation search an input list of strings against the list of command words.
        /// Upon a succesful first match, the operation updates the command and indexOfCommand input parameters by reference.
        /// Returns the number of matches at the end of search.
        /// </summary>
        /// <param name="inputWords">Input array of words</param>
        /// <param name="command">Command type to be updated by reference on first match</param>
        /// <param name="indexOfCommand">Index of command to be updated by reference on first match</param>
        /// <returns>Number of matches</returns>
        internal static int SearchForCommandKeyword(List<string> inputWords, ref CommandType command, ref int indexOfCommand)
        {
            int index = 0;
            int matchCount = 0;
            CommandType commandType = 0;
            foreach (string word in inputWords)
            {
                commandType = 0;
                foreach (List<String> specificCommandTypeKeywords in commandKeywords)
                {
                    foreach (string possibleCommandKeyword in specificCommandTypeKeywords)
                    {
                        if (word.ToLower() == possibleCommandKeyword)
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

        internal static void RemoveWordFromSentence_ByIndex(ref List<string> words, int index)
        {
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
