using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationAttributes
    {
        // The following properties are only used internally once set and hence cannot be "get".
        // Set as private to prevent confusion.
        private TimeSpan? startTime = null, endTime = null;
        private DateTime? startDateOnly = null, endDateOnly = null;
        private DayOfWeek? startDay = null, endDay = null;

        // Setter methods
        public TimeSpan? EndTime  { set { endTime = value; } }
        public TimeSpan? StartTime { set { startTime = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public DayOfWeek? EndDay { set { endDay = value; } }
        public DayOfWeek? StartDay { set { startDay = value; } }

        // The following attributes are used during derivation of Operation type and should not be otherwised used.
        public ContextType currentSpecifier = new ContextType();
        public ContextType currentMode = new ContextType();

        // ******************************************************************
        // Operation Attributes
        // ******************************************************************

        #region Attributes
        public CommandType commandType = new CommandType();
        public DateTime? startDateTime = null, endDateTime = null;
        public DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        public TimeRangeType? timeRangeType = new TimeRangeType();
        public TimeRangeKeywordsType? timeRange = new TimeRangeKeywordsType();
        public string taskName = null;
        public int[] rangeIndexes = null;
        public int timeRangeIndex;
        public bool rangeIsAll = false;
        #endregion

        public OperationAttributes()
        {
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;
        }

        public void SetSearchTime()
        {
            // If searching only for a single time, assume it's the end time.
            if (commandType == CommandType.SEARCH && startTime != null && endTime == null && endDateOnly == null)
            {
                endTime = startTime;
                startTime = null;
            }
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
            if (limit > combinedDT)
                //@ivan -> jenna wtf is this??
                //@jenna -> idk???!??! no idea why you test for 0001, 1, 1... is i do one meh?
                if (combinedDT != new DateTime(0001, 1, 1)
                    && this.commandType != CommandType.SEARCH
                    && this.commandType != CommandType.DISPLAY)
                    AlertBox.Show("Note that date specified is past.");
            return combinedDT;
        }
    }
}
