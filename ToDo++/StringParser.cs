using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    // enum is used as a list index. do not modify numbering!
    enum CommandType { ADD = 0, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO, INVALID };
    public static class StringParser
    {
        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] delimitingCharacters = { { '\'', '\'' }, { '\"', '\"' }, { '[', ']' }, { '(', ')' }, { '{', '}' } };
        static List<List<string>> commandKeywords;
        static Dictionary<string, List<string>> dayKeywords;
        static List<string> monthKeywords;        
        static List<string> timeSpecificKeywords;
        static List<string> timeGeneralKeywords;

        static StringParser()
        {
            InitializeDefaultKeywords();
        }

        private static void InitializeDefaultKeywords()
        {
            InitializeCommandKeywords();
            InitializeDateTimeKeywords();
        }

        private static void InitializeCommandKeywords()
        {
            commandKeywords = new List<List<string>>();
            commandKeywords.Insert((int)CommandType.ADD, new List<String> { "add" });
            commandKeywords.Insert((int)CommandType.DISPLAY, new List<String> { "display" });
            commandKeywords.Insert((int)CommandType.SORT, new List<String> { "sort" });
            commandKeywords.Insert((int)CommandType.SEARCH, new List<String> { "search" });
            commandKeywords.Insert((int)CommandType.MODIFY, new List<String> { "modify" });
            commandKeywords.Insert((int)CommandType.UNDO, new List<String> { "undo" });
            commandKeywords.Insert((int)CommandType.REDO, new List<String> { "redo" });
        }

        private static void InitializeDateTimeKeywords()
        {
            dayKeywords = new Dictionary<string, List<string>>();
            dayKeywords.Add("Monday", new List<string> { "mon", "monday" });
            dayKeywords.Add("Tuesday", new List<string> { "tue", "tues", "tuesday" });
            dayKeywords.Add("Wednesday", new List<string> { "wed", "wednesday" });
            dayKeywords.Add("Thursday", new List<string> { "thur", "thurs", "thursday" });
            dayKeywords.Add("Friday", new List<string> { "fri", "friday" });
            dayKeywords.Add("Saturday", new List<string> { "sat", "saturday"});
            dayKeywords.Add("Sunday", new List<string> { "sun", "sunday", "weekend" });
            timeSpecificKeywords = new List<string> { "noon", "midnight" };
            timeGeneralKeywords = new List<string> { "morning", "afternoon", "evening", "night" };
            //todo: preposition keywords? i.e. next, following, this, from, to, "-", until, by.
        }

        internal static bool IsValidTime(string thetime)
        {
            // checks the input for 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
            Regex time_24HourFormat =
                new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?$");
            // military and standard with the use of AM and PM (optional and insensitive)
            Regex time_12HourFormat =
                new Regex(@"(?i)\b(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<context>am|pm)\b");

            return (time_24HourFormat.IsMatch(thetime)||time_12HourFormat.IsMatch(thetime));
        }

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
            for (int i = 0; i < delimitingCharacters.GetLength(0); i++)
            {
                startIndex = 0;
                endIndex = -1;
                do
                {
                    startIndex = input.IndexOf(delimitingCharacters[i, START_INDEX], endIndex + 1);
                    endIndex = input.IndexOf(delimitingCharacters[i, END_INDEX], startIndex + 1);
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

        internal static DateTime[] SearchForDateTime(List<string> input)
        {
            throw new NotImplementedException();
        }

        internal static List<string> SplitStringIntoWords(string input, List<int[]> indexOfDelimiters)
        {
            List<string> words = new List<string>();

            // Generate the absolute substrings based on delimiters first, removing them from the input string.
            int processedIndex = 0;
            foreach (int[] substringIndex in indexOfDelimiters)
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
