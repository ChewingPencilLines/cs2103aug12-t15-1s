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
        static Dictionary<string, DayOfWeek> dayKeywords;
        static List<string> monthKeywords;        
        static List<string> timeSpecificKeywords;
        static List<string> timeGeneralKeywords;
        static List<string> timePostpositionKeywords;
        static List<string> prepositionKeywords;

        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static Regex time_24HourFormat =
            new Regex(@"(?i)\b(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?\b");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static Regex time_12HourFormat =
            new Regex(@"(?i)\b(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<context>am|pm)\b");

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
            // todo: change to dictionary? has a constant look up time. should be faster
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
            dayKeywords = new Dictionary<string, DayOfWeek>();
            dayKeywords.Add("mon", DayOfWeek.Monday);
            dayKeywords.Add("monday", DayOfWeek.Monday);
            dayKeywords.Add("tue", DayOfWeek.Tuesday);
            dayKeywords.Add("tues", DayOfWeek.Tuesday);
            dayKeywords.Add("tuesday", DayOfWeek.Tuesday);
            dayKeywords.Add("wed", DayOfWeek.Wednesday);
            dayKeywords.Add("wednesday", DayOfWeek.Wednesday);
            dayKeywords.Add("thur", DayOfWeek.Thursday);
            dayKeywords.Add("thurs", DayOfWeek.Thursday);
            dayKeywords.Add("thursday", DayOfWeek.Thursday);
            dayKeywords.Add("fri", DayOfWeek.Friday);
            dayKeywords.Add("friday", DayOfWeek.Friday);
            dayKeywords.Add("sat", DayOfWeek.Saturday);
            dayKeywords.Add("saturday", DayOfWeek.Saturday);
            dayKeywords.Add("sun", DayOfWeek.Sunday);
            dayKeywords.Add("sunday", DayOfWeek.Sunday);
            dayKeywords.Add("weekend", DayOfWeek.Sunday);
            timeSpecificKeywords = new List<string> { "noon", "midnight" };            
            timeGeneralKeywords = new List<string> { "morning", "afternoon", "evening", "night" };
            timePostpositionKeywords = new List<string> { "am", "pm", "hr", "hrs", "hour", "hours" };
            //todo: preposition keywords? i.e. next, following, this, from, to, "-", until, by.
            prepositionKeywords = new List<string> { "by", "at", "next", "following", "this", "from", "to", "-" };
        }

        internal static bool IsValidTime(string thetime)
        {
            return (time_24HourFormat.IsMatch(thetime)||time_12HourFormat.IsMatch(thetime));
        }

        internal static bool IsValidDay(string theday)
        {
            return true;
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
                    if (startIndex >= 0 && endIndex > startIndex)
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
        /// <param name="command">Command type to be returned by reference on first match</param>
        /// <param name="indexOfCommand">Index of command to be returned by reference on first match</param>
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

        internal static List<string> RemoveWordFromSentence_ByIndex(List<string> words, int index)
        {
            words.RemoveAt(index);
            return words;
        }

        // todo: after completion, take into account efficiency.
        // return object should be able to respresent signifance of each DateTime
        // use a TaskTime object?
        internal static List<DateTime> SearchForDateTime(List<string> input)
        {
            // add task friday 5 pm 28 sept 2012
            // => add task friday 5pm 28 sept 2012
            // => add task friday 5pm "28 sept 2012" (date is a single string in the list)
            // days has <index 0, DayOfWeek.Friday>.
            // dates has <index 2, 28/09/2012 as DateTime>.
            // time has <index 1, 17 hours in TimeSpan>.            
            List<Tuple<int, DayOfWeek>> days;
            List<Tuple<int, DateTime>> dates;
            List<Tuple<int, TimeSpan>> times;
            input = MergeTimeWords(input);
            input = MergeDateWords(input);
            days = SearchForDays(input);
            SearchForDates(input);
            SearchForTime(input);
            // Merge date and times (and days)? Use their indexes. (initial and end for each DateTime)
            // MergeDateTimes();
            SearchForContext(input);
            return null;
        }

        private static List<string> MergeDateWords(List<string> input)
        {
            // using regex:
            // find month words
            // check if word before or after match a date type (i.e. 26th, 26 etc)
            // check if word after is year (if word after is not date i.e. jan 2013, 26th jan 2013)
            // check if word 2 index after is year (if word after is date i.e. jan 26th 2013)
            // merge if it is (into 26th jan etc)
            throw new NotImplementedException();
        }

        internal static List<Tuple<int, DayOfWeek>> SearchForDays(List<string> input)
        {
            List<Tuple<int, DayOfWeek>> dayWords = new List<Tuple<int, DayOfWeek>>();
            DayOfWeek day;
            int index = 0;
            foreach (string word in input)
            {
                if (dayKeywords.ContainsKey(word))
                {
                    dayKeywords.TryGetValue(word, out day);
                    Tuple<int, DayOfWeek> indexDayPair = new Tuple<int, DayOfWeek>(index, day);
                    dayWords.Add(indexDayPair);
                }
                index++;
            }
            return dayWords;
        }

        private static void SearchForTime(List<string> input)
        {
            // use a combined regex to get hour, minute, second via tags and return a TimeSpan.
            throw new NotImplementedException();
        }

        private static void SearchForDates(List<string> input)
        {            
            // use a combined regex, to return DateTime using month and date and year tags
            throw new NotImplementedException();
        }

        // use lists of index to derive user intention, with consideration of prepositionKeywords.
        private static void SearchForContext(List<string> input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method checks all words within an input list of words for valid times and returns a list of words
        /// where all times are merged as a single word.
        /// For example, if there is a valid time such as i.e. 5 pm, it combines "5" and "pm" in the returned list of words as "5pm".
        /// </summary>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        internal static List<string> MergeTimeWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0;
            bool wordAdded = false;
            foreach (string word in input)
            {
                foreach (string keyword in timePostpositionKeywords)
                {                    
                    if (word.ToLower() == keyword)
                    {
                        wordAdded = MergeWord_IfValidTime(ref output, input, position);
                        if (wordAdded) break;
                    }                    
                }
                if(!wordAdded) output.Add(word);
                wordAdded = false;
                position++;
            }
            return output;
        }

        private static bool MergeWord_IfValidTime(ref List<string> output, List<string> input, int position)
        {
            string backHalf = input.ElementAt(position);
            string frontHalf;
            if(position == 0)
            {
                return false;
            }
            frontHalf = input.ElementAt(position-1);
            string mergedWord = String.Concat(frontHalf, backHalf);
            if (IsValidTime(mergedWord))
            {
                output.RemoveAt(output.Count-1);
                output.Add(mergedWord);
                return true;
            }
            else return false;
        }

        //todo: ref string output no longer neccesary!
        /// <summary>
        /// This method parses a string of words into a list of strings, each containing a word.
        /// By inputting a list of integer pairs to mark delimiting characters, multiple words can be taken as a single absolute substring (word).        
        /// An output string passed by ref is required, which will contain the input string without any words bounded by delimiters, if any.
        /// If there are no delimiters, the ref output is exactly the same as the input.
        /// </summary>
        /// <param name="input">The string of words to be split.</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings.</param>
        /// <param name="output">The original string without words bounded by delimiters.</param>
        /// <returns>The individual words as a list of strings.</returns>
        internal static List<string> SplitStringIntoWords(string input, ref string output, List<int[]> indexOfDelimiters = null)
        {
            List<string> words = new List<string>();

            int processedIndex = 0, removedCount = 0;
            output = input;

            if (indexOfDelimiters == null)
                return input.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList();
            
            foreach (int[] substringIndex in indexOfDelimiters)
            {
                int count = substringIndex[END_INDEX] - substringIndex[START_INDEX] + 1;
                int startIndex = substringIndex[START_INDEX];
                string subStr = input.Substring(processedIndex, startIndex - processedIndex);

                // Add words leading up to the delimiting character
                words.AddRange(subStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());

                // Get absolute substring without the delimiter characters and add to return list
                string absoluteSubstr = input.Substring(startIndex + 1, count - 2);
                words.Add("\" " + absoluteSubstr); // " marks an absolute string

                // Remove absolute string from output
                output = output.Remove(startIndex - removedCount, count);

                // Update processed index state and count of removed characters
                processedIndex = substringIndex[END_INDEX] + 1;
                removedCount += count;
            }

            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());

            return words;
        }
  
    }
}
