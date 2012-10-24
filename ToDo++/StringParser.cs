using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    // ******************************************************************
    // Enumerations
    // ******************************************************************
    public enum CommandType { ADD = 0, DELETE, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO, DONE, POSTPONE, EXIT, INVALID };
    public enum ContextType { STARTTIME = 0, ENDTIME, DEADLINE, CURRENT, NEXT, FOLLOWING };
    public enum Month { JAN = 1, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC };

    public class StringParser
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] delimitingCharacters = { { '\'', '\'' }, { '\"', '\"' }, { '[', ']' }, { '(', ')' }, { '{', '}' } };
        static Dictionary<string, CommandType> commandKeywords;
        static Dictionary<string, Month> monthKeywords;
        static List<string> timeSuffixes;

        // ******************************************************************
        // Getter Methods
        // ******************************************************************

        #region Getter Methods For Dictionaries & List
        public Dictionary<string, CommandType> getCommandKeywords()
        {
            return commandKeywords;
        }
        public Dictionary<string, Month> getMonthKeywords()
        {
            return monthKeywords;
        }
        public List<string> getTimeSuffixes()
        {
            return timeSuffixes;
        }
        #endregion

        // ******************************************************************
        // Regular Expressions
        // ******************************************************************

        #region Regex For Time & Date Parsing
        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static Regex time_24HourFormat =
            new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?$");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static Regex time_12HourFormat =
            new Regex(@"(?i)^(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<format>am|pm)$");
        // checks day-month-year and month-day-year format; the formal takes precedence if the input matches both
        static Regex date_numericFormat =
            new Regex(@"^
                        (?:
                        (
                        # DD/MM
                        (?:
                        ((?<day>(0?[1-9]|[12][0-9]|3[01]))
                        (?<separator>[-/.]))?
                        (?<month>(0?[1-9]|1[012]))
                        )
                        |
                        # MM/DD
                        (?:
                        (?<month>(0?[1-9]|1[012]))
                        (?<separator>[-/.])
                        (?<day>(0?[1-9]|[12][0-9]|3[01]))
                        )
                        )
                        # (YY)YY
                        (?:(?(day)((\<separator>(?<year>(\d\d)?\d\d))?)
                        |([-/.](?<year>\d\d\d\d))
                        )
                        ))
                        $"
            , RegexOptions.IgnorePatternWhitespace);

        // checks day-month-year and month-day-year format; the formal takes precedence if the input matches both
        // note that inputs such as "15th" will not result in a match; need to recheck later
        static Regex date_alphabeticFormat =
            new Regex(@"^
                        (
                        # DD/MM
                        (?:
                        ((?<day>(([23]?1(?:st)?)|(2?2(?:nd)?)|(2?3(?:rd)?)|([12]?[4-9](?:th)?)|([123]0(?:th)?)|(1[123](?:th)?)))\s)?
                        (?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))
                        )
                        |
                        # MM/DD
                        (?:
                        (?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))
                        \s
                        (?<day>(([23]?1(?:st)?)|(2?2(?:nd)?)|(2?3(?:rd)?)|([12]?[4-9](?:th)?)|([123][0](?:th)?)|(1[123](?:th)?)))
                        ))
                        # (YY)YY
                        (?:(?(day)(\s(?<year>(\d\d)?\d\d))?|(\s(?<year>\d\d\d\d))))$"
            , RegexOptions.IgnorePatternWhitespace);

        static Regex date_daysWithSuffixes =
             new Regex(@"^(?<day>(([23]?1(?:st))|(2?2(?:nd))|(2?3(?:rd))|([12]?[4-9](?:th))|([123][0](?:th))|(1[123](?:th))))$");
        #endregion

        // ******************************************************************
        // Parser & Keyword Initialization
        // ******************************************************************

        static StringParser()
        {
            InitializeDefaultKeywords();
        }

        #region Initialization Methods

        private static void InitializeDefaultKeywords()
        {
            InitializeCommandKeywords();
            InitializeMonthKeywords();
            InitializeTimeSuffixes();
        }

        private static void InitializeTimeSuffixes()
        {
            timeSuffixes = new List<string> { "am", "pm", "hr", "hrs", "hour", "hours" };
        }

        private static void InitializeCommandKeywords()
        {
            // todo: change to dictionary? has a constant look up time. should be faster
            commandKeywords = new Dictionary<string, CommandType>();
            commandKeywords.Add("add", CommandType.ADD);
            commandKeywords.Add("delete", CommandType.DELETE);
            commandKeywords.Add("display", CommandType.DISPLAY);
            commandKeywords.Add("sort", CommandType.SORT);
            commandKeywords.Add("search", CommandType.SEARCH);
            commandKeywords.Add("modify", CommandType.MODIFY);
            commandKeywords.Add("undo", CommandType.UNDO);
            commandKeywords.Add("redo", CommandType.REDO);
            commandKeywords.Add("done", CommandType.DONE);
            commandKeywords.Add("postpone", CommandType.POSTPONE);
            commandKeywords.Add("exit", CommandType.EXIT);
        }

        private static void InitializeMonthKeywords()
        {
            monthKeywords = new Dictionary<string, Month>();
            monthKeywords.Add("jan", Month.JAN);
            monthKeywords.Add("january", Month.JAN);
            monthKeywords.Add("feb", Month.FEB);
            monthKeywords.Add("february", Month.FEB);
            monthKeywords.Add("mar", Month.MAR);
            monthKeywords.Add("march", Month.MAR);
            monthKeywords.Add("may", Month.MAY);
            monthKeywords.Add("jun", Month.JUN);
            monthKeywords.Add("june", Month.JUN);
            monthKeywords.Add("jul", Month.JUL);
            monthKeywords.Add("july", Month.JUL);
            monthKeywords.Add("aug", Month.AUG);
            monthKeywords.Add("august", Month.AUG);
            monthKeywords.Add("sep", Month.SEP);
            monthKeywords.Add("sept", Month.SEP);
            monthKeywords.Add("september", Month.SEP);
            monthKeywords.Add("oct", Month.OCT);
            monthKeywords.Add("october", Month.OCT);
            monthKeywords.Add("nov", Month.NOV);
            monthKeywords.Add("november", Month.NOV);
            monthKeywords.Add("dec", Month.DEC);
            monthKeywords.Add("december", Month.DEC);
        }
        #endregion

        // ******************************************************************
        // Public Methods
        // ******************************************************************

        #region Public Methods
        internal static void AddUserCommand(string userCommand, CommandType commandType)
        {
            try
            {
                commandKeywords.Add(userCommand, commandType);

            }
            catch (Exception)
            {
                //Handle repeat keywords
                //If the key is already added, dont add ignore! need to do a better implementation
            }
        }

        internal static void ResetCommandKeywords()
        {
            InitializeCommandKeywords();
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
        /// <param name="input">The string of words to be parsed</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings</param>
        /// <returns>The list of tokens</returns>
        internal static List<Token> ParseStringIntoTokens(string input, List<int[]> indexOfDelimiters = null)
        {
            List<string> words = SplitStringIntoSubstrings(input, indexOfDelimiters);
            TokenGenerator tokenGenerator = new TokenGenerator();
            return tokenGenerator.GenerateTokens(words);
        }
        #endregion

        // ******************************************************************
        // String Parsing Algorithms
        // ******************************************************************

        #region String Splitting and Merging Methods
        /// <summary>
        /// This method splits a string and returns a list of substrings, each containing either a word delimited by a space,
        /// or a substring delimited by positions in the parameter indexOfDelimiters.
        /// </summary>
        /// <param name="input">The string of words to be split</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings</param>
        /// <returns>List of substrings</returns>
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
                words.Add(absoluteSubstr);

                // Update processed index state and count of removed characters
                processedIndex = substringIndex[END_INDEX] + 1;
                removedCount += count;
            }

            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());
            words = MergeCommandAndIndexKeywords(words);
            words = MergeDateAndTimeWords(words);
            return words;
        }

        /// <summary>
        /// This method checks to see if the command is followed by an index.
        /// If it is, it merges the command with the index.
        /// </summary>
        /// <param name="input">The list of separated words</param>
        /// <returns>List of string with command(s) merged with index(es)</returns>
        private static List<string> MergeCommandAndIndexKeywords(List<string> words)
        {
            List<string> output = new List<string>();
            bool merged = false;
            CommandType commandType;
            for (int i = 0; i < words.Count; i++) // don't check last word
            {
                if (commandKeywords.TryGetValue(words[i].ToLower(), out commandType) && i != words.Count)
                {
                    if ((commandType == CommandType.DELETE || commandType == CommandType.MODIFY || commandType == CommandType.DONE) && words.Count > 1)
                    {
                        int convert;
                        if (Int32.TryParse(words[i + 1], out convert))
                        {
                            output.Add(words[i] + " " + words[i + 1]);
                            merged = true;
                        }
                    }
                }
                if (merged)
                {
                    i++;
                    merged = false;
                }
                else output.Add(words[i]);
            }
            return output;
        }

        /// <summary>
        /// This method detects and merges all the date and time words into a single string
        /// while keeping the other words separate and unmerged.
        /// For example, the list input "add", "task", "friday", "5", "pm", "28", "sept", "2012"
        /// will return "add", "task", "friday", "5pm", "28 sept 2012"
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time/date phrases</returns>
        private static List<string> MergeDateAndTimeWords(List<string> input)
        {
            input = MergeTimeWords(input);
            input = MergeDateWords(input);
            return input;
        }

        /// <summary>
        /// This method checks all words within an input list of words for valid times and returns a list of words
        /// where all times are merged as a single word.
        /// For example, if there is a valid time such as i.e. 5 pm, it combines "5" and "pm" in the returned list of words as "5pm".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time phrases</returns>
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
                if (!wordAdded) output.Add(word);
                wordAdded = false;
                position++;
            }
            return output;
        }

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of a time phrase
        /// and merges it with the other words constituting the time phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged time phrases up to the indicated word/time phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word in the input list to be checked</param>
        /// <returns>True if the indicated word is part of a time phrase and false if otherwise</returns>
        private static bool MergeWord_IfValidTime(ref List<string> output, List<string> input, int position)
        {
            string backHalf = input.ElementAt(position);
            string frontHalf;
            if (position == 0)
            {
                return false;
            }
            frontHalf = input.ElementAt(position - 1);
            string mergedWord = String.Concat(frontHalf, backHalf);
            if (IsValidTime(mergedWord))
            {
                output.RemoveAt(output.Count - 1);
                output.Add(mergedWord);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// This method checks all words within an input list of words for valid date and returns a list of words
        /// where all dates are merged as a single word.
        /// For example, if there is a valid time such as i.e. 23 sept 2012, it combines "23", "sept" and "2012"
        /// in the returned list of words as "23 sept 2012".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged date phrases</returns>
        private static List<string> MergeDateWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0, skipWords = 0;
            bool isWordAdded = false;
            // check for all full or partial dates in alphabetic date formats
            foreach (string word in input)
            {
                if (skipWords > 0)
                {
                    skipWords--;
                    position++;
                    continue;
                }
                if (monthKeywords.ContainsKey(word.ToLower()))
                {
                    isWordAdded = MergeWord_IfValidAlphabeticDate(ref output, input, position, ref skipWords);
                }
                if (!isWordAdded)
                {
                    output.Add(word);
                }
                position++;
                isWordAdded = false;
            }
            // dates in numeric date formats and dates that are only specified by day with suffixes i.e. "15th"
            // need not be checked for and merged since they are already whole words on their own.
            return output;
        }

        /* Note that "12 may 2012 2012" will produce merged word "12 may 2012"
        * and "12 may 23 2012" will produce the merged word "12 may 23".
        */

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of an alphabetic date phrase
        /// and merges it with the other words constituting the date phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged alphabetic date phrases up to the indicated date phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word  in the input list to be checked</param>
        /// <param name="numberOfWords">The number of words behind the indicated word that were merged to form the date</param>
        /// <returns>True if the indicated word is part of a date phrase and false if otherwise</returns>
        private static bool MergeWord_IfValidAlphabeticDate(ref List<string> output, List<string> input, int position, ref int numberOfWords)
        {
            string month = input.ElementAt(position);
            string mergedWord = month;
            bool isWordUsed = false;
            int i = 1;
            // Backward check
            if ((position > 0) &&
                (IsValidAlphabeticDate(input[position - 1] + " " + mergedWord.ToLower())))
            {
                mergedWord = input[position - 1] + " " + mergedWord;
                isWordUsed = true;
            }
            // Forward check
            while (position + i < input.Count)
            {
                if (IsValidAlphabeticDate(mergedWord.ToLower() + " " + input[position + i]))
                {
                    mergedWord = mergedWord + " " + input[position + i];
                }
                else break;
                i++;
            }
            if (mergedWord == month)
            {
                return false;
            }
            if (isWordUsed == true)
            {
                output.RemoveAt(output.Count - 1);
            }
            output.Add(mergedWord);
            numberOfWords = i - 1;
            return true;
        }
        #endregion

        // ******************************************************************
        // Auxilliary Methods
        // ******************************************************************

        #region Comparison Methods For Regexes
        private static bool IsValidTime(string theTime)
        {
            return (time_24HourFormat.IsMatch(theTime) || time_12HourFormat.IsMatch(theTime));
        }

        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous non-existent dates such as 31st feb
        public static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate) || date_daysWithSuffixes.IsMatch(theDate.ToLower());
        }

        public static bool IsValidAlphabeticDate(string theDate)
        {
            return date_alphabeticFormat.IsMatch(theDate);
        }
        #endregion
    }
}
