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
        static List<string> monthKeywords;
        static List<string> timeSpecificKeywords;
        static List<string> timeGeneralKeywords;
        static List<string> timeSuffixes;


        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static Regex time_24HourFormat =
            new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?$");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static Regex time_12HourFormat =
            new Regex(@"(?i)^(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<format>am|pm)$");

        /* All date regexes only check for dates within the 21st century.
         * They do not return the leading 2 numbers for year, leading zeroes for month and day
         * and the possible suffixes for days in match values. These are optional.
         * The relevent groups are tagged as <day>, <month> and <year>.
         * The day input is actually optional in the DMY formats
         * so as to detect partial date inputs such as "06/13" or "June 2012".
         * Note that only the default numeric date format will be checked for!
         */

        // The following numeric date regexes do check for consistent separation by hyphens, forward slashes or periods.

        // This is the default format that will be checked for
        // for numeric date words unless otherwise indicated in the settings.
        // matches dd-mm-yyyy, d-m-yyyy, dd-mm-yy or d-m-yy
        static Regex date_numericDMYFormat =
            new Regex(@"\b(?<day>(0?[1-9]|[12][0-9]|3[01]))?([-/.])(?<month>(0[1-9]|1[012]))\2(?<year>(?:(20)?)\d\d)\b");

        // matches mm-dd-yyyy, m-d-yyyy, mm-dd-yy, m-d-yy
        static Regex date_numericMDYFormat =
            new Regex(@"\b(?<month>(0?[1-9]|1[012]))([-/.])(?<day>(0[1-9]|[12][0-9]|3[01]))\2(?<year>(?:(20)?)\d\d)\b");

        // matches yyyy-mm-dd, yyyy-m-d, yy-mm-dd, yy-m-d
        static Regex date_numericYMDFormat =
            new Regex(@"\b(?<year>(?:(20)?)\d\d)([-/.])(?<month>(0?[1-9]|1[012]))\2(?<day>(0[1-9]|[12][0-9]|3[01]))\b");

        // This is the default format that will be checked for first
        // for alphabetic date words unless otherwise indicated in the settings.
        // matches dd mmm yyyy or dd(st/nd/rd/th) mmm yyyy
        static Regex date_alphabeticDMYFormat =
            new Regex(@"\b(?<day>(([123]?[1](?:st)?)|([12]?[2](?:nd)?)|([12]?[3](?:rd)?)|([12]?[4-9](?:th)?)|([123][0](?:th)?)))?\s(?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))\s(?<year>(?:(20)?)\d\d)\b");

        // matchess mmm dd yyyy
        static Regex date_alphabeticMDYFormat =
            new Regex(@"\b(?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))\s(?<day>(([123]?[1])|([12]?[2])|([12]?[3])|([12]?[4-9])|([123][0])))\s(?<year>(?:(20)?)\d\d)\b");

        // matchess yyyy mm dd
        static Regex date_alphabeticYMDFormat =
            new Regex(@"\b(?<year>(?:(20)?)\d\d)\s(?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))\s(?<day>(([123]?[1])|([12]?[2])|([12]?[3])|([12]?[4-9])|([123][0])))");

        
        static Regex date_numericFormat =
            new Regex(@"\b
                        # Day and Month
                        (?:
                            # DD/MM
                            (?:
                            ((?<day>(0?[1-9]|[12][0-9]|3[01]))(?<separator>[-/.]))?                            
                            (?<month>(0[1-9]|1[012]))
                           )
                        |
                            # MM/DD
                            (?:
                            (?<month>(0[1-9]|1[012]))
                            (\separator)
                            (?<day>(0?[1-9]|[12][0-9]|3[01]))
                            )
                        )
                        # Year => YY or YYYY
                        # if day not captured, force year
                        (?(day)
                            (?:
                            (\separator)
                            (?<year>(\d\d)?\d\d)
                            )?
                            |
                            (?:
                            (\separator)
                            (?<year>\d\d\d\d)
                            )
                        )
                        \b"
                    , RegexOptions.IgnorePatternWhitespace);

        internal static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate);
        }

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

        private static void InitializeMonthKeywords()
        {
            monthKeywords = new List<string>() { "jan", "january",
                                                  "feb", "february",
                                                  "mar", "march",
                                                  "apr", "april",
                                                  "may",
                                                  "jun", "june",
                                                  "jul", "july",
                                                  "aug", "august",
                                                  "sep", "sept", "september",
                                                  "oct", "october",
                                                  "nov", "november",
                                                  "dec", "december" };
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


        internal static bool IsValidTime(string theTime)
        {
            return (time_24HourFormat.IsMatch(theTime) || time_12HourFormat.IsMatch(theTime));
        }


        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous dates such as 31st feb
        internal static bool IsValidDMYAlphabeticDate(string theDate)
        {
            return date_alphabeticDMYFormat.IsMatch(theDate);
        }

        internal static bool IsValidMDYAlphabeticDate(string theDate)
        {
            return date_alphabeticMDYFormat.IsMatch(theDate);
        }

        internal static bool IsValidYMDAlphabeticDate(string theDate)
        {
            return date_alphabeticYMDFormat.IsMatch(theDate);
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
            // input = MergeDateWords(input);
            return input;
        }

        private static List<string> MergeDateWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0;
            bool wordAdded = false;
            // check for all full or partial month-year dates in alphabetic date formats
            foreach (string word in input)
            {
                foreach (string keyword in monthKeywords)
                {
                    if (word.ToLower() == keyword)
                    {
                        wordAdded = MergeWord_IfValidAlphabeticDate(ref output, input, position);
                        if (wordAdded) break;
                    }
                }
                if (!wordAdded) output.Add(word);
                wordAdded = false;
                position++;
            }

            // dates in numeric date formats and dates that are only specified by day with suffixes i.e. "15th"
            // need not be checked for and merged since they are already whole words on their own.
            return output;
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
                if (!wordAdded) output.Add(word);
                wordAdded = false;
                position++;
            }
            return output;
        }

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


        private static bool MergeWord_IfValidAlphabeticDate(ref List<string> output, List<string> input, int position)
        {
            if (position == 0)
            {
                return false;
            }
            string month = input.ElementAt(position);

            // checks default dmy format first
            string day = input.ElementAt(position - 1);
            string year = input.ElementAt(position + 1);
            string mergedWord = String.Concat(day, month, year);
            if (IsValidDMYAlphabeticDate(mergedWord))
            {
                // if the merged word is still valid as a dmy date format even without the day component
                if (IsValidDMYAlphabeticDate(String.Concat(month, year)))
                {
                    mergedWord = String.Concat(month, year);
                }
                output.RemoveAt(output.Count - 1);
                output.Add(mergedWord);
                return true;
            }

            // checks mdy format next
            day = input.ElementAt(position + 1);
            year = input.ElementAt(position + 2);
            mergedWord = String.Concat(day, month, year);
            if (IsValidMDYAlphabeticDate(mergedWord))
            {
                output.RemoveAt(output.Count - 1);
                output.Add(mergedWord);
                return true;
            }

            // checks ymd format next
            day = input.ElementAt(position + 1);
            year = input.ElementAt(position - 2);
            mergedWord = String.Concat(day, month, year);
            if (IsValidMDYAlphabeticDate(mergedWord))
            {
                output.RemoveAt(output.Count - 1);
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
            // must be done after generating day/date/time tokens.
            tokens.AddRange(GenerateContextTokens(input, tokens));
            // must be done last. all non-hits are taken to be literals
            tokens.AddRange(GenerateLiteralTokens(input, tokens));
            tokens.Sort(CompareByPosition);
            return tokens;
        }

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
                            System.Diagnostics.Debug.Assert(!(commandType > CommandType.INVALID), "Fatal error: Logic flow error in GenerateCommandTokens!");
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
            }
            return tokens;
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
                    literal = literal.Trim();
                    TokenLiteral literalToken = new TokenLiteral(index - 1, literal);
                    tokens.Add(literalToken);
                    literal = "";
                }
                index++;
            }
            return tokens;
        }

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
