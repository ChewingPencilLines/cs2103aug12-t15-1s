using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationAttributes
    {
        // ******************************************************************
        // Operation Attributes
        // ******************************************************************

        #region Attributes
        public CommandType commandType = new CommandType();
        public DateTime? startDateTime = null, endDateTime = null;
        public DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        public TimeRangeType timeRangeType = TimeRangeType.DEFAULT;
        public TimeRangeKeywordsType timeRangeOne = TimeRangeKeywordsType.NONE;
        public SortType? sortType = new SortType();
        public TimeRangeKeywordsType timeRangeTwo = TimeRangeKeywordsType.NONE;
        public string taskName = null;
        public int[] rangeIndexes = null;
        public int timeRangeIndex = 0;
        public bool rangeIsAll = false;
        #endregion

        // ******************************************************************
        // Properties For Operation Generation
        // ******************************************************************

        #region Properties For Operation Generation (Hidden)
        // The following properties are only used internally once set and hence cannot be "get".
        // Set as private to prevent confusion.
        private TimeSpan? startTime = null, endTime = null;
        private DateTime? startDateOnly = null, endDateOnly = null;
        private DayOfWeek? startDay = null, endDay = null;

        // Setter methods
        public TimeSpan? EndTime { set { endTime = value; } }
        public TimeSpan? StartTime { set { startTime = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public DayOfWeek? EndDay { set { endDay = value; } }
        public DayOfWeek? StartDay { set { startDay = value; } }

        // The following attributes are used during derivation of Operation type and should not be otherwised used.
        public ContextType currentSpecifier = new ContextType();
        public ContextType currentMode = new ContextType();
        #endregion

        public OperationAttributes()
        {
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;
            sortType = SortType.DEFAULT;
        }

        public void SetSearchTime()
        {
            if (commandType == CommandType.SEARCH
                || commandType == CommandType.DELETE
                || commandType == CommandType.DONE
                || commandType == CommandType.MODIFY)
            {
                // If searching only for a single time, assume it's the end time.
                if (startTime != null && endTime == null && endDateOnly == null)
                {
                    endTime = startTime;
                    isSpecific.EndTime = isSpecific.StartTime;
                    startTime = null;
                }

                // If searching for a single date, assume the range is that date.
                if (startDateOnly != null && endDateOnly == null)
                {
                    endDateOnly = startDateOnly;
                    isSpecific.EndDate = isSpecific.StartDate;
                }
            }
        }

        public void SetScheduleTime()
        {
            if (timeRangeOne != TimeRangeKeywordsType.NONE)
            {
                int startTimeHour, endTimeHour;
                // getting values from specified time range keywords
                CustomDictionary.timeRangeKeywordsStartTime.TryGetValue(timeRangeOne, out startTimeHour);
                if (timeRangeTwo != TimeRangeKeywordsType.NONE)
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeTwo, out endTimeHour);
                }
                else
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeOne, out endTimeHour);
                }
                // pick the correct start time and end time if other times were
                // specified beyond the time range keywords i.e. by time tokens
                if (startTime == null && endTime == null)
                {
                    startTime = new TimeSpan(startTimeHour, 0, 0);
                    endTime = new TimeSpan(endTimeHour, 0, 0);
                }
                else if (startTime != null && endTime == null)
                {
                    if (((TimeSpan)startTime).Hours < endTimeHour
                        && ((TimeSpan)startTime).Hours > startTimeHour)
                    {
                        endTime = startTime;
                        startTime = new TimeSpan(startTimeHour, 0, 0);
                    }
                    else
                    {
                        // warn user that specified time is not within specified time range
                    }
                }
                else if (startTime != null && endTime != null)
                {
                    if (!(((TimeSpan)startTime).Hours < endTimeHour
                        && ((TimeSpan)startTime).Hours > startTimeHour
                        && ((TimeSpan)endTime).Hours < endTimeHour
                        && ((TimeSpan)endTime).Hours > startTimeHour))
                    {
                        // warn user that specified time is not within specified time range
                    }
                }
            }
            if (startDateOnly == null)
            {
                startDateOnly = DateTime.Today;
            }
            // start time and end time will stil be null if there is no time token &
            // no time range token i.e. both are NONE
        }

        public void CombineDateTimes()
        {
            // Combine Date/Times
            if (startTime == null)
            {
                isSpecific.StartTime = false;
            }
            if (endTime == null)
            {
                isSpecific.EndTime = false;
            }
            // If only one date is specified, we assume both dates is that date.
            if (isSpecific.StartTime && isSpecific.EndTime)
            {
                // assign start date to end date
                if (startDateOnly == null && endDateOnly != null)
                {
                    startDateOnly = endDateOnly;
                    isSpecific.StartDate = isSpecific.EndDate;
                }
                // assign end date to start date
                else if (startDateOnly != null && endDateOnly == null)
                {
                    endDateOnly = startDateOnly;
                    isSpecific.EndDate = isSpecific.StartDate;
                }
            }

            startDateTime = CombineDateAndTime(startTime, startDateOnly, DateTime.Now);
            if (startDateTime == null)
                endDateTime = CombineDateAndTime(endTime, endDateOnly, DateTime.Now);
            else
                endDateTime = CombineDateAndTime(endTime, endDateOnly, (DateTime)startDateTime);
        }

        private DateTime? CombineDateAndTime(TimeSpan? time, DateTime? date, DateTime limit)
        {
            DateTime? combinedDT = null;
            // Time defined but not date
            if (date == null && time != null)
            {
                TimeSpan limitTime = limit.TimeOfDay;
                TimeSpan taskTime = (TimeSpan)time;
                combinedDT = new DateTime(limit.Year, limit.Month, limit.Day, taskTime.Hours, taskTime.Minutes, taskTime.Seconds);
                if (limitTime > time)
                {
                    combinedDT = ((DateTime)combinedDT).AddDays(1);
                }
            }
            // Date and Time both defined
            else if (date != null && time != null)
            {
                DateTime setDate = (DateTime)date;
                TimeSpan setTime = (TimeSpan)time;
                combinedDT = new DateTime(setDate.Year, setDate.Month, setDate.Day, setTime.Hours, setTime.Minutes, setTime.Seconds);
            }
            // Date defined but not time
            else if (time == null && date != null)
            {
                combinedDT = date;
            }
            /*
            if (limit > combinedDT)
                if (combinedDT != new DateTime(0001, 1, 1)
                    && this.commandType == CommandType.ADD
                    && this.commandType == CommandType.SCHEDULE)
                    AlertBox.Show("Note that date specified is past.");
            */
            return combinedDT;
        }
    }
}