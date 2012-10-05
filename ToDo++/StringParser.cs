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
    enum ContextType { STARTTIME = 0, ENDTIME, DEADLINE, CURRENT, NEXT, FOLLOWING }
    public static class StringParser
    {
        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] delimitingCharacters = { { '\'', '\'' }, { '\"', '\"' }, { '[', ']' }, { '(', ')' }, { '{', '}' } };
        static List<List<string>> commandKeywords;
        static Dictionary<string, ContextType> contextKeywords;
        static Dictionary<string, DayOfWeek> dayKeywords;
        static List<string> timeSpecificKeywords;        
        static List<string> timeGeneralKeywords;
        static List<string> timeSuffixes;
        

        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static Regex time_24HourFormat =
            new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?$");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static Regex time_12HourFormat =
            new Regex(@"(?i)^(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<format>am|pm)$");

        static StringParser()
        {
            InitializeDefaultKeywords();
        }

        private static void InitializeDefaultKeywords()
        {
            InitializeCommandKeywords();
            InitializeDateTimeKeywords();
            InitializeContextKeywords();
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
            // NYI
            timeSpecificKeywords = new List<string> { "noon", "midnight" };        // special case    
            timeGeneralKeywords = new List<string> { "morning", "afternoon", "evening", "night" }; // todo?
            // ===
            timeSuffixes = new List<string> { "am", "pm", "hr", "hrs", "hour", "hours" };            
        }

        private static void InitializeContextKeywords()
        {
            contextKeywords = new Dictionary<string, ContextType>();
            contextKeywords.Add("by", ContextType.DEADLINE);
            contextKeywords.Add("on", ContextType.STARTTIME);
            contextKeywords.Add("from", ContextType.STARTTIME);
            contextKeywords.Add("to", ContextType.ENDTIME);
            contextKeywords.Add("-", ContextType.ENDTIME);
            contextKeywords.Add("this", ContextType.CURRENT);
            contextKeywords.Add("next", ContextType.NEXT);
            contextKeywords.Add("following", ContextType.FOLLOWING);
        }


        internal static bool IsValidTime(string thetime)
        {
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
    
        /// <summary>
        /// This method parses a string of words into a list of tokens, each containing a token representing the meaning of each word or substring.
        /// By inputting a list of integer pairs to mark delimiting characters, multiple words can be taken as a single absolute substring (word).  
        /// </summary>
        /// <param name="input">The string of words to be parsed.</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings.</param>
        /// <returns>The list of tokens.</returns>
        internal static List<Token> ParseStringIntoTokens(string input, List<int[]> indexOfDelimiters = null)
        {
            List<string> words = SplitStringIntoSubstrings(input, indexOfDelimiters);
            return GenerateTokens(words);
        }

        /// <summary>
        /// This method splits a string and returns a list of substrings, each containing either a word delimited by a space,
        /// or a substring delimited by positions in the parameter indexOfDelimiters
        /// </summary>
        /// <param name="input">The string of words to be split.</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings.</param>
        /// <returns></returns>
        private static List<string> SplitStringIntoSubstrings(string input, List<int[]> indexOfDelimiters)
        {
            List<string> words = new List<string>();

            int processedIndex = 0, removedCount = 0;

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

                // Update processed index state and count of removed characters
                processedIndex = substringIndex[END_INDEX] + 1;
                removedCount += count;
            }

            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());
            words = MergeDateAndTimeWords(words);
            return words;
        }

        private static List<string> MergeDateAndTimeWords(List<string> input)
        {
            // add task friday 5 pm 28 sept 2012
            // => add task friday 5pm 28 sept 2012
            // => add task friday 5pm "28 sept 2012" (date is a single string in the list)
            input = MergeTimeWords(input);
            input = MergeDateWords(input);
            return input;
        }

        private static List<string> MergeDateWords(List<string> input)
        {
            // using regex:
            // find month words
            // check if word before or after match a date type (i.e. 26th, 26 etc)
            // check if word after is year (if word after is not date i.e. jan 2013, 26th jan 2013)
            // check if word 2 index after is year (if word after is date i.e. jan 26th 2013)
            // merge if it is (into 26th jan etc)
            return input;
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
        private static List<string> MergeTimeWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0;
            bool wordAdded = false;
            foreach (string word in input)
            {
                foreach (string keyword in timeSuffixes)
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

        private static List<Token> GenerateTokens(List<string> input)
        {
            List<Token> tokens = new List<Token>();
            tokens.AddRange(GenerateCommandTokens(input));
            tokens.AddRange(GenerateDayTokens(input));
            tokens.AddRange(GenerateDateTokens(input));
            tokens.AddRange(GenerateTimeTokens(input));
            tokens.AddRange(GenerateContextTokens(input));
            // must be done last. all non-hits are taken to be literals
            tokens.AddRange(GenerateLiteralTokens(input, tokens));
            // SORT()!
            return tokens;
        }

        private static List<Token> GenerateContextTokens(List<string> input)
        {
            return new List<Token>();
            throw new NotImplementedException();
        }

        private static List<Token> GenerateLiteralTokens(List<string> input, List<Token> parsedTokens)
        {
            List<Token> tokens = new List<Token>();
            foreach (Token token in parsedTokens)
            {
                input[token.Position] = null;
            }
            int index = 0;
            string literal = "";
            foreach (string remainingWord in input)
            {
                if (remainingWord != null)
                    literal = literal + remainingWord + " ";
                else if (literal != "")
                {
                    literal.Trim();
                    TokenLiteral literalToken = new TokenLiteral(index-1, literal);
                    tokens.Add(literalToken);
                    literal = "";
                }
                index++;
            }
            return tokens;
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// This operation searches an input list of strings against the set list of command words and returns as list of tokens
        /// corresponding to the matched command keywords.
        /// </summary>
        /// <param name="inputWords">Input array of words</param>
        /// <returns>List of command tokens</returns>
        private static List<Token> GenerateCommandTokens(List<string> inputWords)
        {
            int index = 0;
            CommandType commandType = 0;
            List<Token> tokens = new List<Token>();
            foreach (string word in inputWords)
            {
                commandType = 0;
                foreach (List<String> specificCommandTypeKeywords in commandKeywords)
                {
                    foreach (string possibleCommandKeyword in specificCommandTypeKeywords)
                    {
                        if (word.ToLower() == possibleCommandKeyword)
                        {
                            System.Diagnostics.Debug.Assert(!(commandType > CommandType.INVALID), "Fatal error: Logic flow error!");
                            TokenCommand commandToken = new TokenCommand(index, commandType);
                            tokens.Add(commandToken);
                        }
                    }
                    commandType++;
                }
                index++;
            }
            return tokens;
        }

        private static List<Token> GenerateDayTokens(List<string> input)
        {
            List<Token> dayTokens = new List<Token>();
            DayOfWeek day;
            int index = 0;
            foreach (string word in input)
            {
                if (dayKeywords.ContainsKey(word))
                {
                    dayKeywords.TryGetValue(word, out day);
                    TokenDay dayToken = new TokenDay(index, day);
                    dayTokens.Add(dayToken);
                }
                index++;
            }
            return dayTokens;
        }

        // uses a combined regex to get hour, minute, second via tags and return a TimeSpan.
        private static List<Token> GenerateTimeTokens(List<string> input)
        {
            List<Token> timeTokens = new List<Token>();
            Match match;
            int index = 0, hours = 0, minutes = 0, seconds = 0;
            bool Format_12Hour = false;
            foreach (string word in input)
            {
                match = time_12HourFormat.Match(word);
                if (!match.Success) match = time_24HourFormat.Match(word);
                else Format_12Hour = true;
                if (match.Success)
                {
                    string strHours = match.Groups["hours"].Value;
                    string strMinutes = match.Groups["minutes"].Value;
                    if (strHours.Length != 0)
                    {
                        hours = Int32.Parse(strHours);
                        if (Format_12Hour && match.Groups["format"].Value.ToLower() == "pm")
                        {
                            hours += 12;
                        }
                    }
                    if (strMinutes.Length != 0)
                        minutes = Int32.Parse(strMinutes);
                    TimeSpan time = new TimeSpan(hours, minutes, seconds);
                    TokenTime timeToken = new TokenTime(index, time);
                    timeTokens.Add(timeToken);
                }
                index++;
            }
            return timeTokens;
        }

        private static List<Token> GenerateDateTokens(List<string> input)
        {
            return new List<Token>();
            throw new NotImplementedException();
        }
        
    }
}
