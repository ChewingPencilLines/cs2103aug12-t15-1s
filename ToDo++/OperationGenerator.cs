using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    // ****************************************************************************
    // Factory class for creating Operations.
    // This class is first configured by Tokens, and once finalized,
    // can generate the appropriate operations using the GenerateOperation method.
    // ****************************************************************************
    class OperationGenerator
    {
        // ******************************************************************
        // Operation Attributes
        // ******************************************************************

        #region Operation Attributes
        public CommandType commandType = new CommandType();
        public DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        public TimeRangeType timeRangeType = new TimeRangeType();
        public TimeRangeKeywordsType timeRangeOne = new TimeRangeKeywordsType();
        public TimeRangeKeywordsType timeRangeTwo = new TimeRangeKeywordsType();
        public SortType sortType = new SortType();
        public SearchType searchType = new SearchType();
        public string taskName = null;
        public int[] taskRangeIndex = null;
        public int timeRangeIndex = 0;
        public bool rangeIsAll = false;
        #endregion

        // ******************************************************************
        // Properties For Operation Generation
        // ******************************************************************

        #region Properties For Operation Generation (Hidden)
        // The following properties are only used internally once set and hence cannot be "get".
        // Set as private to prevent confusion.
        private TimeSpan? startTimeOnly = null, endTimeOnly = null;
        private DateTime? startDateOnly = null, endDateOnly = null;
        private DayOfWeek? startDay = null, endDay = null;

        // Setter methods
        public TimeSpan? EndTimeOnly { set { endTimeOnly = value; } }
        public TimeSpan? StartTimeOnly { set { startTimeOnly = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public DayOfWeek? EndDay { set { endDay = value; } }
        public DayOfWeek? StartDay { set { startDay = value; } }

        // The following attributes are used during derivation of Operation type and should not be otherwised used.
        public ContextType currentSpecifier = new ContextType();
        public ContextType currentMode = new ContextType();
        private DateTime? startDateTime = null, endDateTime = null;
        private bool crossDayBoundary = false;
        #endregion

        public OperationGenerator()
        {
            // Initialize enumerations
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;
            sortType = SortType.DEFAULT;
            searchType = SearchType.NONE;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRangeOne = TimeRangeKeywordsType.NONE;
            timeRangeTwo = TimeRangeKeywordsType.NONE;
        }

        // ******************************************************************
        // Finalize generator for operation creation
        // ******************************************************************

        #region Finalize Generator
        public void FinalizeGenerator()
        {
            GetTimeRangeValues();
            if (commandType == CommandType.SCHEDULE)
            {
                FinalizeSchedulingTime();
            }
            else if (CommandIsSearchableType())
            {
                FinalizeSearchTime();
            }
            CombineDateTimes();
        }

        private bool CommandIsSearchableType()
        {
            return !((commandType == CommandType.ADD) ||
                     (commandType == CommandType.SCHEDULE) ||
                     (commandType == CommandType.MODIFY && taskRangeIndex != null));
        }

        // ******************************************************************
        // Finalize Search Times
        // ******************************************************************

        #region Finalize Search Times
        private void FinalizeSearchTime()
        { 
            // If searching only for a single time, assume it's the end time.
            if (IsOnlyStartTimeSet())
            {
                endTimeOnly = startTimeOnly;
                isSpecific.EndTime = isSpecific.StartTime;
                startTimeOnly = null;
            }

            // If searching for a single date, assume the whole range is that date.
            if (IsOnlyStartDateSet())
            {
                endDateOnly = startDateOnly;
                isSpecific.EndDate = isSpecific.StartDate;
            }

            if (endDateOnly != null)
                ExtendEndSearchDate();
        }

        private bool IsOnlyStartTimeSet()
        {
            return startTimeOnly != null && endTimeOnly == null && endDateOnly == null;
        }

        private bool IsOnlyStartDateSet()
        {
            return startDateOnly != null && endDateOnly == null && startTimeOnly == null && endTimeOnly == null;
        }

        private void ExtendEndSearchDate()
        {
            // Extend compare range if task end date is not specific         
            if (!isSpecific.EndDate.Day && endTimeOnly == null)
            {
                ExtendEndMonthOrYear();
                endDateOnly = endDateOnly.Value.AddMinutes(-1);
            }
            else if (endTimeOnly == null && isSpecific.StartDate.Day == true)
            {
                endDateOnly = new DateTime(endDateOnly.Value.Year, endDateOnly.Value.Month, endDateOnly.Value.Day, 23, 59, 0);
            }
        }

        private void ExtendEndMonthOrYear()
        {
            if (!isSpecific.EndDate.Month)
            {
                endDateOnly = new DateTime(endDateOnly.Value.Year + 1, 1, 1);
            }
            else
            {
                endDateOnly = endDateOnly.Value.AddMonths(1);
                endDateOnly = new DateTime(endDateOnly.Value.Year, endDateOnly.Value.Month, 1);
            }
        }
        #endregion

        // ******************************************************************
        // Finalize Scheduling Time
        // ******************************************************************

        #region Finalize Scheduling Time
        private void FinalizeSchedulingTime()
        {
            if (startDateOnly == null)
            {
                startDateOnly = DateTime.Today;
                isSpecific.StartDate.Day = false;
                isSpecific.StartDate.Month = false;
                isSpecific.StartDate.Year = false;
            }
        }
        #endregion

        // ******************************************************************
        // Set Time Ranges
        // ******************************************************************

        #region Set Time Ranges
        private void GetTimeRangeValues()
        {
            int startTimeHour = 0, endTimeHour = 0;
            if (TryGetTimeRangeValues(ref startTimeHour, ref endTimeHour))
            {
                // pick the correct start time and end time if other times were
                // specified beyond the time range keywords i.e. by time tokens
                if (IsSpecificTimeSupplied())
                {
                    RetrieveFinalStartAndEndTimes(startTimeHour, endTimeHour);
                }
                else
                {
                    if (currentMode != ContextType.DEADLINE)
                    {
                        startTimeOnly = new TimeSpan(startTimeHour, 0, 0);
                    }
                    else
                    {
                        startTimeOnly = null;
                    }
                    endTimeOnly = new TimeSpan(endTimeHour, 0, 0);
                }
            }
        }

        private bool TryGetTimeRangeValues(ref int startTimeHour, ref int endTimeHour)
        {
            if (timeRangeOne != TimeRangeKeywordsType.NONE)
            {
                // getting values from specified time range keywords
                CustomDictionary.timeRangeKeywordsStartTime.TryGetValue(timeRangeOne, out startTimeHour);
                if (timeRangeTwo == TimeRangeKeywordsType.NONE)
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeOne, out endTimeHour);
                    if (CustomDictionary.IsTimeRangeOverDayBoundary(timeRangeOne))
                    {
                        crossDayBoundary = true;
                    }
                }
                else
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeTwo, out endTimeHour);
                    if (CustomDictionary.IsTimeRangeOverDayBoundary(timeRangeTwo))
                    {
                        crossDayBoundary = true;
                    }
                }
                return true;
            }
            return false;
        }

        private bool IsSpecificTimeSupplied()
        {
            if (startTimeOnly == null && endTimeOnly == null)
            {
                return false;
            }
            return true;
        }

        private void RetrieveFinalStartAndEndTimes(int startTimeHour, int endTimeHour)
        {
            if (startTimeOnly != null && endTimeOnly == null)
            {
                if (startTimeOnly.Value.Hours < endTimeHour
                    && startTimeOnly.Value.Hours > startTimeHour)
                {
                    endTimeOnly = startTimeOnly;
                    startTimeOnly = new TimeSpan(startTimeHour, 0, 0);
                }
                else
                {
                    // warn user that specified time is not within specified time range
                }
            }
            else if (startTimeOnly != null && endTimeOnly != null)
            {
                if (!IsSpecifiedTimeWithinTimeRange(startTimeHour, endTimeHour))
                {
                    // warn user that specified time is not within specified time range
                }
            }
        }

        private bool IsSpecifiedTimeWithinTimeRange(int startTimeHour, int endTimeHour)
        {
            if (!(startTimeOnly.Value.Hours < endTimeHour
                && startTimeOnly.Value.Hours > startTimeHour
                && endTimeOnly.Value.Hours < endTimeHour
                && endTimeOnly.Value.Hours > startTimeHour))
            {
                return false;
            }
            return true;
        }
        #endregion

        // ******************************************************************
        // Combine Date And Times As Single Date Time
        // ******************************************************************

        #region CombineDateTimes
        private void CombineDateTimes()
        {
            // Combine Date/Times
            if (startTimeOnly == null)
            {
                isSpecific.StartTime = false;
            }
            if (endTimeOnly == null)
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

            startDateTime = CombineDateAndTime(startTimeOnly, startDateOnly, DateTime.Now);
            if (startDateTime == null)
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, DateTime.Now);
            else
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, startDateTime.Value);
        }

        private DateTime? CombineDateAndTime(TimeSpan? time, DateTime? date, DateTime limit)
        {
            DateTime? combinedDT = null;
            // Time defined but not date
            if (date == null && time != null)
            {
                TimeSpan limitTime = limit.TimeOfDay;
                TimeSpan taskTime = time.Value;
                combinedDT = new DateTime(limit.Year, limit.Month, limit.Day, taskTime.Hours, taskTime.Minutes, taskTime.Seconds);
                if (limitTime > time)
                {
                    combinedDT = combinedDT.Value.AddDays(1);
                }
            }
            // Date and Time both defined
            else if (date != null && time != null)
            {
                DateTime setDate = date.Value;
                TimeSpan setTime = time.Value;
                combinedDT = new DateTime(setDate.Year, setDate.Month, setDate.Day, setTime.Hours, setTime.Minutes, setTime.Seconds);
            }
            // Date defined but not time
            else if (time == null && date != null)
            {
                combinedDT = date;
            }
            if (crossDayBoundary || (combinedDT < limit && isSpecific.EndDate.Day == false))
            {
                combinedDT = combinedDT.Value.AddDays(1);
            }
            if (limit > combinedDT)
                if (this.commandType == CommandType.ADD)
                    AlertBox.Show("Note that date specified is past.");
            return combinedDT;
        }
        #endregion
        #endregion

        // ******************************************************************
        // Operation Generation
        // ******************************************************************

        #region Operation Generation
        public Operation CreateOperation()
        {
            Task task;
            Operation newOperation = null;
            switch (commandType)
            {
                case CommandType.ADD:
                    task = Task.GenerateNewTask(taskName, startDateTime, endDateTime, isSpecific);
                    newOperation = new OperationAdd(task, sortType);
                    break;
                case CommandType.DELETE:
                    newOperation = new OperationDelete(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.DISPLAY:
                    newOperation = new OperationDisplayDefault(sortType);
                    break;
                case CommandType.MODIFY:
                    newOperation = new OperationModify(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.SEARCH:
                    newOperation = new OperationSearch(taskName, startDateTime, endDateTime, isSpecific, searchType, sortType);
                    break;
                case CommandType.SORT:
                    newOperation = new OperationSort(sortType);
                    break;
                case CommandType.REDO:
                    newOperation = new OperationRedo(sortType);
                    break;
                case CommandType.UNDO:
                    newOperation = new OperationUndo(sortType);
                    break;
                case CommandType.DONE:
                    newOperation = new OperationMarkAsDone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.UNDONE:
                    newOperation = new OperationMarkAsUndone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.POSTPONE:
                    TimeSpan postponeDuration = new TimeSpan();
                    if (timeRangeType == TimeRangeType.DEFAULT)
                    {
                        timeRangeType = CustomDictionary.defaultPostponeDurationType;
                        timeRangeIndex = CustomDictionary.defaultPostponeDurationLength;
                    }
                    switch (timeRangeType)
                    {
                        case TimeRangeType.HOUR:
                            postponeDuration = new TimeSpan(timeRangeIndex, 0, 0);
                            break;
                        case TimeRangeType.DAY:
                            postponeDuration = new TimeSpan(timeRangeIndex, 0, 0, 0);
                            break;
                        case TimeRangeType.WEEK:
                            postponeDuration = new TimeSpan(timeRangeIndex * CustomDictionary.DAYS_IN_WEEK, 0, 0, 0);
                            break;
                        case TimeRangeType.MONTH:
                            postponeDuration = new TimeSpan(timeRangeIndex * CustomDictionary.DAYS_IN_MONTH, 0, 0, 0);
                            break;
                    }
                    newOperation = new OperationPostpone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, postponeDuration, sortType);
                    break;
                case CommandType.SCHEDULE:
                    newOperation = new OperationSchedule(taskName, (DateTime)startDateTime, endDateTime, isSpecific, timeRangeIndex, timeRangeType, sortType);
                    break;
                case CommandType.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            return newOperation;
        }
#endregion
        
        // ******************************************************************
        // Generator configuration methods
        // ******************************************************************

        #region Generator configuration methods
        internal void SetConditionalEndTime(TimeSpan Value, bool IsSpecific)
        {
            if (startTimeOnly == null && endTimeOnly != null)
            {
                this.startTimeOnly = this.endTimeOnly;
                this.isSpecific.StartTime = this.isSpecific.EndTime;
            }
            this.endTimeOnly = Value;
            this.isSpecific.EndTime = IsSpecific;
        }

        internal void SetConditionalEndDate(DateTime Value, Specificity IsSpecific)
        {
            if (startDateOnly == null && endDateOnly != null)
            {
                this.startDateOnly = this.endDateOnly;
                this.isSpecific.StartDate = this.isSpecific.EndDate;
            }
            this.endDateOnly = Value;
            this.isSpecific.EndDate = IsSpecific;
        }
        #endregion
    }
}