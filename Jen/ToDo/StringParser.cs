using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

/* Remember to revert to private the methods that need not be public.
 */ 

namespace ToDo
{
    enum NumericDateType { YMD, DMY, MDY };    

    public static class StringParser
    {   
        static List<string> monthKeywords;
        static List<string> suffixedDayOfMonthKeywords;

        static StringParser()
        {
            InitializeMonthKeywords();
            InitializeSuffixedDayOfMonthKeywords();
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
        
        private static void InitializeSuffixedDayOfMonthKeywords()
        {
            suffixedDayOfMonthKeywords = new List<string>() { "1st", "2nd", "3rd",
                                                 "4th", "5th", "6th",
                                                 "7th", "8th", "9th",
                                                 "10th", "11th", "12th",
                                                 "13th", "14th", "15th",
                                                 "16th", "17th", "18th",
                                                 "19th", "20th", "21st",
                                                 "22nd", "23rd", "24th",
                                                 "25th", "26th", "27th",
                                                 "28th", "29th", "30th",
                                                 "31st"
            };
        }

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


        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous non-existent dates such as 31st feb
        internal static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate);
        }

        internal static bool IsValidAlphabeticDate(string theDate)
        {
            return date_alphabeticFormat.IsMatch(theDate);
        }

        internal static void CheckAlphabeticDateDayMonthYearTags(string input, ref string theMatch, ref string day, ref string month, ref string year)
        {
            Match match = date_alphabeticFormat.Match(input.ToLower());
            theMatch = match.Value;
            day = match.Groups["day"].Value;
            month = match.Groups["month"].Value;
            year = match.Groups["year"].Value;
        }

        public static List<string> MergeDateWords(List<string> input)
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
                foreach (string keyword in monthKeywords)
                {
                    if (word.ToLower() == keyword)
                    {
                        isWordAdded = MergeWord_IfValidAlphabeticDate(ref output, input, position, ref skipWords);
                        if (isWordAdded) break;
                    }
                }
                if (!isWordAdded) output.Add(word);
                isWordAdded = false;
                position++;
            }
            // dates in numeric date formats and dates that are only specified by day with suffixes i.e. "15th"
            // need not be checked for and merged since they are already whole words on their own.
            return output;
        }

        /* Note that "12 may 2012 2012" will produce merged word "12 may 2012"
         * and "12 may 23 2012" will produce the merged word "12 may 23".
         */

        public static bool MergeWord_IfValidAlphabeticDate(ref List<string> output, List<string> input, int position, ref int numberOfWords)
        {
            string month = input.ElementAt(position);
            string mergedWord = month;
            bool isWordUsed = false;
            int i = 1;
            if((position > 0) && (IsValidAlphabeticDate(input[position - 1] + " " + mergedWord.ToLower())))
            {
                mergedWord = input[position - 1] + " " + mergedWord;
                isWordUsed = true;
            }
            while (position + i < input.Count)
            {
                if (IsValidAlphabeticDate(mergedWord.ToLower() + " " + input[position + i]))
                    mergedWord = mergedWord + " " + input[position + i];
                else break;
                i++;
            }
            if (mergedWord == month)
                return false;
            if (isWordUsed == true)
                output.RemoveAt(output.Count - 1);
            output.Add(mergedWord);
            numberOfWords = i - 1;
            return true;
        }

        public static List<Token> GenerateDateTokens(List<string> input)
        {
            int day, year, month, index = 0;
            string monthString;
            DateTime dateTime;
            bool isSpecific = true;
            List<Token> dateTokens = new List<Token>();
            foreach (string word in input)
            {
                if (IsValidNumericDate(word.ToLower()) || IsValidAlphabeticDate(word.ToLower()))
                {
                    Match match = date_numericFormat.Match(word);
                    int.TryParse(match.Groups["day"].Value, out day);
                    monthString = match.Groups["month"].Value;
                    int.TryParse(match.Groups["year"].Value, out year);
                    if (day == 0)
                    {
                        isSpecific = false;
                        day = 1;
                    }
                    
                    if (!Char.IsDigit(monthString[0]))
                        monthString = convertToNumericMonth(monthString);
                    int.TryParse(monthString, out month);
                    if (year == 0)
                    {
                        dateTime = new DateTime(DateTime.Today.Year, month, day);
                        if (DateTime.Compare(dateTime, DateTime.Today) < 0)
                            dateTime = new DateTime(DateTime.Today.AddYears(1).Year, month, day);
                    }
                    else
                    {
                        dateTime = new DateTime(year, month, day);
                    }
                    TokenDate dateToken = new TokenDate(index, dateTime, isSpecific);
                    dateTokens.Add(dateToken);
                }
                index++;
                isSpecific = true;
            }
            return dateTokens;
        }

        public static string convertToNumericMonth(string month)
        {
            switch (month.ToLower())
            {
                case "jan":
                    return "1";
                case "feb":
                    return "2";
                case "mar":
                    return "3";
                case "apr":
                    return "4";
                case "may":
                    return "5";
                case "jun":
                    return "6";
                case "jul":
                    return "7";
                case "aug":
                    return "8";
                case "sep":
                    return "9";
                case "oct":
                    return "10";
                case "nov":
                    return "11";
                case "dec":
                    return "12";
                default:
                    throw new Exception("Unrecognized month");
            }
        }
        }

    public class Token
    {
        enum TokenType { COMMAND, DATE, TIME, DAY, CONTEXT, LITERAL };
        private int position;
        internal int Position
        {
            get { return position; }
        }

        internal Token(int position)
        {
            this.position = position;
        }
    }

    public class TokenDate : Token
    {
        DateTime dateTime;
        bool specificity;
        internal DateTime Value
        {
            get { return dateTime; }
        }
        internal bool IsSpecific
        {
            get { return specificity; }
        }
        internal TokenDate(int position, DateTime date, Boolean particularity)
            : base(position)    
        {
            dateTime = date;
            specificity = particularity;
        }
    }
}