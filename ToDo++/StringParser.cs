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
    public enum CommandType { ADD = 0, DELETE, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO, DONE, POSTPONE, INVALID };
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
        static Dictionary<string, ContextType> contextKeywords;
        static Dictionary<string, DayOfWeek> dayKeywords;
        static Dictionary<string, Month> monthKeywords;
        static List<string> timeSpecificKeywords;
        static List<string> timeGeneralKeywords;
        static List<string> timeSuffixes;

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
            InitializeDateTimeKeywords();
            InitializeMonthKeywords();
            InitializeContextKeywords();
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
            monthKeywords.Add("november", Month.NOV);
            monthKeywords.Add("dec", Month.DEC);
            monthKeywords.Add("december", Month.DEC);
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
            return GenerateTokens(words);
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
                words.Add("\" " + absoluteSubstr); // " marks an absolute string

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
                    if ((commandType == CommandType.DELETE || commandType == CommandType.MODIFY) && words.Count > 1)
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

        // Move to a new TokenGenerator class?
        #region Token Generation Methods
        /// <summary>
        /// This method searches an input list of strings and generates the relevant
        /// command, day, date, time, context and literal tokens of all the relevant matching strings.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of tokens</returns>
        private static List<Token> GenerateTokens(List<string> input)
        {
            List<Token> tokens = new List<Token>();
            tokens.AddRange(GenerateCommandTokens(input));
            tokens.AddRange(GenerateDayTokens(input));
            tokens.AddRange(GenerateDateTokens(input));
            tokens.AddRange(GenerateTimeTokens(input));
            // must be done after generating day/date/time tokens.
            tokens.AddRange(GenerateContextTokens(input, tokens));
            // must be done last. all non-hits are taken to be literals
            tokens.AddRange(GenerateLiteralTokens(input, tokens));
            tokens.Sort(CompareByPosition);
            return tokens;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of command keywords and returns
        /// a list of tokens corresponding to the matched command keywords.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of command tokens</returns>
        private static List<Token> GenerateCommandTokens(List<string> inputWords)
        {
            int index = 0;
            CommandType commandType;
            List<Token> tokens = new List<Token>();
            foreach (string word in inputWords)
            {
                if (commandKeywords.TryGetValue(word.ToLower(), out commandType))
                {
                    TokenCommand commandToken = new TokenCommand(index, commandType);
                    tokens.Add(commandToken);
                }
                else
                {
                    int taskIndex;
                    string[] multiWordCommand = word.Split();
                    if (multiWordCommand.Length == 2 &&
                        commandKeywords.TryGetValue(multiWordCommand[0].ToLower(), out commandType) &&
                        Int32.TryParse(multiWordCommand[1], out taskIndex))
                    {
                        TokenCommand commandToken = new TokenCommand(index, commandType, taskIndex);
                        tokens.Add(commandToken);
                    }
                }
                index++;
            }
            return tokens;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of day keywords and returns
        /// a list of tokens corresponding to the matched day keywords.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of day tokens</returns>
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

        /// <summary>
        /// This method searches an input list of strings for all valid dates and generates a list of date tokens
        /// corresponding to all the found matched date strings using regexes.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of date tokens</returns>

        // note: currently, the method just ignores invalid dates such as 30th feb
        // might wish to change the catch case to flag the invalid date input
        private static List<TokenDate> GenerateDateTokens(List<string> input)
        {
            string dayString = String.Empty;
            string monthString = String.Empty;
            string yearString = String.Empty;
            int day = 0;
            int month = 0;
            int year = 0;
            int index = 0;
            bool isSpecific = true;
            List<TokenDate> dateTokens = new List<TokenDate>(); ;
            foreach (string word in input)
            {
                Match match;
                DateTime dateTime = new DateTime();
                bool isMonthGiven = true;
                if (IsValidDate(word.ToLower()))
                {
                    match = GetDateMatch(word.ToLower());
                    GetMatchTagValues(match, ref dayString, ref monthString, ref yearString);
                    ConvertMatchTagValuesToInts(dayString, monthString, yearString, ref day, ref month, ref year);
                    // no day input
                    if (day == 0)
                    {
                        isSpecific = false;
                        day = 1;
                    }
                    // no month input
                    if (month == 0)
                    {
                        month = DateTime.Today.Month;
                        isMonthGiven = false;
                    }
                    // no year input
                    if (year == 0)
                    {
                        TryParsingDate(ref dateTime, DateTime.Today.Year, month, day, true);
                        if (DateTime.Compare(dateTime, DateTime.Today) < 0)
                        {
                            if (isMonthGiven == false)
                            {
                                isMonthGiven = true;
                                if (!TryParsingDate(ref dateTime, DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month, day, false)) continue;
                            }
                            else
                            {
                                if (!TryParsingDate(ref dateTime, DateTime.Today.AddYears(1).Year, month, day, false)) continue;
                            }
                        }
                    }
                    else
                    {
                        if (!TryParsingDate(ref dateTime, year, month, day, false)) continue;
                    }
                    TokenDate dateToken = new TokenDate(index, dateTime, isSpecific);
                    dateTokens.Add(dateToken);
                }
                index++;
                isSpecific = true;
            }
            return dateTokens;
        }

        private static bool TryParsingDate(ref DateTime date, int year, int month, int day, bool ignoreFailure)
        {
            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (ignoreFailure)
                    date = new DateTime(1, 1, 1);
                else
                {
                    //flag
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method searches a string for a date match (alphabetic, numeric or just day with suffixes)
        /// and returns the match.
        /// </summary>
        /// <param name="theWord">The string to be searched/matched</param>
        /// <returns>The match found</returns>
        private static Match GetDateMatch(string theWord)
        {
            Match theMatch = date_numericFormat.Match(theWord);
            if (!theMatch.Success)
            {
                theMatch = date_alphabeticFormat.Match(theWord);
            }
            if (!theMatch.Success)
            {
                theMatch = date_daysWithSuffixes.Match(theWord);
            }
            return theMatch;
        }

        /// <summary>
        /// This method retrieves the values of the day, month and year groups from an input match.
        /// </summary>
        /// <param name="match">The input match</param>
        /// <param name="day">The string value of the retrieved day group</param>
        /// <param name="month">The string value of the retrieved month group</param>
        /// <param name="year">The string value of the retrieved year group</param>
        private static void GetMatchTagValues(Match match, ref string day, ref string month, ref string year)
        {
            day = match.Groups["day"].Value;
            month = match.Groups["month"].Value;
            year = match.Groups["year"].Value;
        }

        /// <summary>
        /// This methods convert the day, month and year strings into their equivalent integers.
        /// If the day and year strings are empty, they will be converted to zeroes.
        /// </summary>
        /// <param name="dayString">The input day string (may contain suffixes)</param>
        /// <param name="monthString">The input month string (may be numeric or alphabetical)</param>
        /// <param name="yearString">The input year string</param>
        /// <param name="dayInt">The output day integer</param>
        /// <param name="monthInt">The output month integer</param>
        /// <param name="yearInt">The output year integer</param>      
        private static void ConvertMatchTagValuesToInts(string dayString, string monthString, string yearString, ref int dayInt, ref int monthInt, ref int yearInt)
        {
            dayString = RemoveSuffixesIfRequired(dayString);
            int.TryParse(dayString, out dayInt);
            monthInt = ConvertToNumericMonth(monthString);
            int.TryParse(yearString, out yearInt);
        }

        /// <summary>
        /// This method takes in an input month string and returns its corresponding index as an integer.
        /// If alphabetic, the string is looked up and compared to a dictionary.
        /// For example, "january" or "jan" returns 1.
        /// A 0 is returned if the string is empty.
        /// </summary>
        /// <param name="month">The input month string (can be numeric of alphabetic)</param>
        /// <returns>An integer month index</returns>
        private static int ConvertToNumericMonth(string month)
        {
            Month monthType;
            int monthInt = 0;
            bool success;
            if (month == String.Empty)
                return 0;
            if (Char.IsDigit(month[0]))
            {
                success = int.TryParse(month, out monthInt);
            }
            else if (monthKeywords.TryGetValue(month, out monthType))
            {
                monthInt = (int)monthType;
            }
            else Debug.Assert(false, "Conversion to numeric month failed! There should always be a valid month matched.");
            return monthInt;
        }

        /// <summary>
        /// This method removes the suffix from a specified day string if it exists and returns the
        /// shortened string.
        /// For example, both "15th" and "15" returns "15".
        /// </summary>
        /// <param name="day">The input day string (may contain suffixes)</param>
        /// <returns>The day string with no suffixes</returns>
        private static string RemoveSuffixesIfRequired(string day)
        {
            // No day input
            if (day == String.Empty)
            {
                return day;
            }
            if (!Char.IsDigit(day.Last()))
            {
                day = day.Remove(day.Length - 2, 2);
            }
            return day;
        }

        /// <summary>
        /// This method searches an input list of strings for all valid times and generates a list of time tokens
        /// corresponding to all the found matched time strings using regexes.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of time tokens</returns>
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
                        if (Format_12Hour) ConvertTo24HoursFormat(match.Groups["format"].Value, hours);
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

        private static int ConvertTo24HoursFormat(string format, int hours)
        {
            if (format.ToLower() == "pm" && hours != 12)
                hours += 12;
            if (format.ToLower() == "am" && hours == 12)
                hours = 24;
            return hours;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of context keywords and returns
        /// a list of tokens corresponding to the matched context keywords.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of context tokens</returns>
        private static List<TokenContext> GenerateContextTokens(List<string> input, List<Token> parsedTokens)
        {
            int index = 0;
            ContextType context;
            List<TokenContext> tokens = new List<TokenContext>();
            foreach (string word in input)
            {
                if (contextKeywords.TryGetValue(word, out context))
                {
                    object nextToken = GetTokenAtPosition(parsedTokens, index + 1);
                    if (nextToken is TokenDate || nextToken is TokenDay || nextToken is TokenTime)
                    {
                        TokenContext newToken = new TokenContext(index, context);
                        tokens.Add(newToken);
                    }
                }
                index++;
            }
            return tokens;
        }

        /// <summary>
        /// This method compares an input list of strings against a list of parsed Tokens, and returns a list of Tokens
        /// representing all strings which have not been been parsed as Tokens. The purpose of this method is to assign
        /// all unparsed strings as LiteralTokens.
        /// </summary>
        /// <param name="input">The list of input words</param>
        /// <param name="parsedTokens">The list of parsedTokens</param>
        /// <returns>List of context tokens</returns>
        private static List<Token> GenerateLiteralTokens(List<string> input, List<Token> parsedTokens)
        {
            List<Token> literalTokens = new List<Token>();
            foreach (Token token in parsedTokens)
            {
                input[token.Position] = null;
            }
            int index = 0;
            string literal = String.Empty;
            foreach (string remainingWord in input)
            {
                if (remainingWord != null)
                    literal = literal + remainingWord + " ";
                else if (remainingWord == null && literal != String.Empty)
                    AddLiteralToken(ref literal, index, ref literalTokens);
                index++;
            }
            if (literal != String.Empty)
            {
                AddLiteralToken(ref literal, index, ref literalTokens);
            }
            return literalTokens;
        }

        private static void AddLiteralToken(ref string literal, int index, ref List<Token> literalTokens)
        {
            literal = literal.Trim();
            TokenLiteral literalToken = new TokenLiteral(index - 1, literal);
            literalTokens.Add(literalToken);
            literal = String.Empty;
        }
        #endregion

        // ******************************************************************
        // Auxilliary Methods
        // ******************************************************************

        #region Comparison Methods

        /// <summary>
        /// This methods compares the 2 input tokens by their stored integer positions and
        /// returns a -1 if the first input token's position is smaller than the second.
        /// A 1 is returned if the reverse is true.
        /// No 2 tokens should have the same positions. However, should such an error arise, a 0 is returned.
        /// </summary>
        /// <param name="x">The first token</param>
        /// <param name="x">The second token to be compared with</param>
        /// <returns>-1, 1, or 0, indicating the results of the comparison</returns>
        private static int CompareByPosition(Token x, Token y)
        {
            int xPosition = x.Position;
            int yPosition = y.Position;
            if (xPosition < yPosition) return -1;
            else if (xPosition > yPosition) return 1;
            else
            {
                Debug.Assert(false, "Two tokens with same position!");
                return 0;
            }
        }

        private static bool IsValidTime(string theTime)
        {
            return (time_24HourFormat.IsMatch(theTime) || time_12HourFormat.IsMatch(theTime));
        }

        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous non-existent dates such as 31st feb
        private static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate) || date_daysWithSuffixes.IsMatch(theDate.ToLower());
        }

        private static bool IsValidAlphabeticDate(string theDate)
        {
            return date_alphabeticFormat.IsMatch(theDate);
        }

        private static bool IsValidDate(string theDate)
        {
            return IsValidNumericDate(theDate) || IsValidAlphabeticDate(theDate);
        }
        #endregion

        /// <summary>
        /// This methods takes in a list of tokens and an index and returns the indicated token
        /// at that indicated position.
        /// </summary>
        /// <param name="tokens">The list of input tokens</param>
        /// <param name="p">The position of the required token</param>
        /// <returns>The retrieved token</returns>
        private static object GetTokenAtPosition(List<Token> tokens, int p)
        {
            foreach (Token token in tokens)
            {
                if (token.Position == p) return token;
            }
            return null;
        }

    }
}
