﻿using System;
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
        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] binaryDelimiters = { { '\'', '\'' }, { '\"', '\"' } , { '[', ']' }, { '(', ')' }, { '{', '}' } };
        static List<List<string>> commandKeywords = new List<List<string>>()
        {
            //@todo: can possibly add commandType to zero-index so that order is not important
            //i.e.: Zeroth index represents the type of command and the following indexes are the possible
            // command strings that represent keywords to execute the command.
            // OR fill commandKeywords during initialization based on enum as index.
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


        /// <summary>
        /// This method searches the input string against the set delimiters'
        /// and return the positions of the delimiters as a list of integer pairs.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List containing all matching sets of delimiters as integer pairs</returns>
        internal static List<int[]> FindPositionOfDelimiters(string input)
        {
            List<int[]> indexOfDelimiters = new List<int[]>();
            int startIndex = 0, endIndex = -1;
            for (int i = 0; i < binaryDelimiters.GetLength(0); i++)
            {
                startIndex = 0;
                endIndex = -1;
                do
                {
                    startIndex = input.IndexOf(binaryDelimiters[i,START_INDEX], endIndex + 1);
                    endIndex = input.IndexOf(binaryDelimiters[i,END_INDEX], startIndex + 1);
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

        //@todo: change to return commandType makes more sense. have matchCount as a ref param.
        /// <summary>
        /// This operation searches an input list of strings against the set list of command words.
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

        internal static List<string> SplitStringIntoWords(string input, List<int[]> indexOfDelimiters)
        {
            List<string> words = new List<string>();
             
            // Generate the absolute substrings based on delimiters first, removing them from the input string.
            int processedIndex = 0;
            foreach(int[] substringIndex in indexOfDelimiters)
            {                
                int count = substringIndex[END_INDEX] - substringIndex[START_INDEX] + 1;
                int startIndex = substringIndex[START_INDEX];
                string subStr = input.Substring(processedIndex, startIndex - processedIndex);

                // Add words leading up to the delimiting character
                words.AddRange(subStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());
                // Get absolute substring without the delimiter characters and add to return list
                string absoluteSubstr = input.Substring(startIndex + 1, count - 2);
                words.Add(absoluteSubstr);

                // Update processed index state
                processedIndex = substringIndex[END_INDEX] + 1;
            }

            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());            
            return words;
        }
    }
}