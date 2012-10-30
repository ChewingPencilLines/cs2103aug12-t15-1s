using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationAttributes
    {
        // These properties are only used internally once set and hence cannot be get.
        // Set as private to prevent confusion.
        private TimeSpan? startTime = null, endTime = null;
        private DateTime? startDateOnly = null, endDateOnly = null;
        private DayOfWeek? startDay = null, endDay = null;

        public TimeSpan? EndTime  { set { endTime = value; } }
        public TimeSpan? StartTime { set { startTime = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public DayOfWeek? EndDay { set { endDay = value; } }
        public DayOfWeek? StartDay { set { startDay = value; } }

        public ContextType currentSpecifier = new ContextType();
        public ContextType currentMode = new ContextType();
        public CommandType commandType = new CommandType();

        public DateTime? startDateTime = null, endDateTime = null;
        public DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        public string taskName = null;
        public int[] taskIndex = null;

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
            startDateTime = CombineDateAndTime(startTime, startDateOnly, DateTime.Now);
            if (startDateTime == null)
                endDateTime = CombineDateAndTime(endTime, endDateOnly, DateTime.Now);
            else
                endDateTime = CombineDateAndTime(endTime, endDateOnly, (DateTime)startDateTime);
        }

        private static DateTime? CombineDateAndTime(TimeSpan? time, DateTime? date, DateTime limit)
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
            if (limit > combinedDT) //throw new Exception("End DateTime set to later then limit or DateTime that is already over was set!");
                //@ivan -> jenna wtf is this??
                if (combinedDT != new DateTime(0001, 1, 1))
                    AlertBox.Show("Note that date specified is in the past.");
            return combinedDT;
        }
    }
}
