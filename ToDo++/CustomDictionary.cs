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
    static class CustomDictionary
    {
        static public Dictionary<string, CommandType> commandKeywords;
        static public Dictionary<string, Month> monthKeywords;
        static public Dictionary<string, ContextType> contextKeywords;
        static public Dictionary<string, DayOfWeek> dayKeywords;
        static public List<string> timeSpecificKeywords;
        static public List<string> timeGeneralKeywords;
        static public List<string> timeSuffixes;

        // ******************************************************************
        // Regular Expressions
        // ******************************************************************

        #region Regexes For Time & Date Parsing
        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static public Regex time_24HourFormat =
            new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9])\s?(h(ou)?rs?)?$");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static public Regex time_12HourFormat =
            new Regex(@"(?i)^(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])?\s?(?<format>am|pm)$");
        // checks day-month-year and month-day-year format; the formal takes precedence if the input matches both
        static public Regex date_numericFormat =
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
        static public Regex date_alphabeticFormat =
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

        static public Regex date_daysWithSuffixes =
             new Regex(@"^(?<day>(([23]?1(?:st))|(2?2(?:nd))|(2?3(?:rd))|([12]?[4-9](?:th))|([123][0](?:th))|(1[123](?:th))))$");

        static public Regex isNumericalRange =
            new Regex(@"^(?<start>\d?\d?\d)(\-(?<end>\d?\d?\d))?");
        #endregion

        static CustomDictionary()
        {
            InitializeCommandKeywords();
            InitializeMonthKeywords();
            InitializeContextKeywords();
            InitializeDateTimeKeywords();
        }

        // ******************************************************************
        // Initialization Methods
        // ******************************************************************

        #region Initialization Methods
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
            dayKeywords.Add("tmr", DateTime.Today.AddDays(1).DayOfWeek);
            dayKeywords.Add("tomorrow", DateTime.Today.AddDays(1).DayOfWeek);
            // NYI
            timeSuffixes = new List<string> { "am", "pm", "hr", "hrs", "hour", "hours" };
            timeSpecificKeywords = new List<string> { "noon", "midnight" };        // special case    
            timeGeneralKeywords = new List<string> { "morning", "afternoon", "evening", "night" }; // todo?
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
        // Getter Methods
        // ******************************************************************

        #region Getter Methods For Dictionaries & List
        public static Dictionary<string, CommandType> GetCommandKeywords()
        {
            return commandKeywords;
        }

        public static Dictionary<string, Month> GetMonthKeywords()
        {
            return monthKeywords;
        }

        public static Dictionary<string, ContextType> GetContextKeywords()
        {
            return contextKeywords;
        }

        public static Dictionary<string, DayOfWeek> GetDayKeywords()
        {
            return dayKeywords;
        }

        public static List<string> GetTimeSpecificKeywords()
        {
            return timeSpecificKeywords;
        }

        public static List<string> GetTimeGeneralKeywords()
        {
            return timeGeneralKeywords;
        }

        public static List<string> GetTimeSuffixes()
        {
            return timeSuffixes;
        }

        public static Regex GetRegexTime24HourFormat()
        {
            return time_24HourFormat;
        }

        public static Regex GetRegexTime12HourFormat()
        {
            return isNumericalRange;
        }

        public static Regex GetRegexDateNumericFormat()
        {
            return date_numericFormat;
        }

        public static Regex GetRegexDateAlphabeticFormat()
        {
            return date_alphabeticFormat;
        }

        public static Regex GetRegexDateDaysWithSuffixes()
        {
            return date_daysWithSuffixes;
        }

        public static Regex GetRegexIsNumericalRange()
        {
            return isNumericalRange;
        }
        #endregion

        // ******************************************************************
        // Auxilliary Methods
        // ******************************************************************

        #region Regex Comparison Methods
        public static bool IsValidDate(string theDate)
        {
            return IsValidNumericDate(theDate) || IsValidAlphabeticDate(theDate);
        }

        public static bool IsValidTime(string theTime)
        {
            return time_24HourFormat.IsMatch(theTime) || time_12HourFormat.IsMatch(theTime);
        }

        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous non-existent dates such as 31st feb
        public static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate) || date_daysWithSuffixes.IsMatch(theDate.ToLower();
        }

        public static bool IsValidAlphabeticDate(string theDate)
        {
            return date_alphabeticFormat.IsMatch(theDate);
        }
        #endregion
    }
}
