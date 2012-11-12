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
        // Operation Properties
        // ******************************************************************

        #region Operation Properties
        public CommandType commandType;
        public DateTimeSpecificity isSpecific;
        public TimeRangeType timeRangeType;
        public TimeRangeKeywordsType timeRangeOne;
        public TimeRangeKeywordsType timeRangeTwo;
        public SortType sortType;
        public SearchType searchType;
        public string taskName;
        public int[] taskRangeIndex;
        public int timeRangeIndex;
        public bool rangeIsAll;
        #endregion

        // ******************************************************************
        // Configuration Attributes For Operation Generation
        // ******************************************************************

        #region Configuration Attributes For Operation Generation
        // The following properties are only used internally once set and hence cannot be "get".
        // Set as private to prevent confusion.
        private TimeSpan? startTimeOnly, endTimeOnly;
        private DateTime? startDateOnly, endDateOnly;
        private bool startDayOfWeekSet, endDayOfWeekSet;

        // Setter methods
        public TimeSpan? EndTimeOnly { set { endTimeOnly = value; } }
        public TimeSpan? StartTimeOnly { set { startTimeOnly = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public bool EndDayOfWeekSet { set { endDayOfWeekSet = value; } }
        public bool StartDayOfWeekSet { set { startDayOfWeekSet = value; } }

        // The following attributes are used during derivation of Operation type and should not be otherwised used.
        public ContextType currentSpecifier;
        public ContextType currentMode;
        private DateTime? startDateTime, endDateTime;
        private bool crossDayBoundary;
        #endregion

        // ******************************************************************
        // Constructors and initializers
        // ******************************************************************

        #region Constructors and initializers
        /// <summary>
        /// Constructor for the generator which initializes it's 
        /// settings to the default values.
        /// </summary>
        public OperationGenerator()
        {
            InitializeNewConfiguration();
        }

        /// <summary>
        /// Initializes the generator's configuration to it's default values.
        /// </summary>
        /// <returns></returns>
        public void InitializeNewConfiguration()
        {
            commandType = new CommandType();
            isSpecific = new DateTimeSpecificity();
            timeRangeType = new TimeRangeType();
            timeRangeOne = new TimeRangeKeywordsType();
            timeRangeTwo = new TimeRangeKeywordsType();
            sortType = new SortType();
            searchType = new SearchType();
            taskName = null;
            taskRangeIndex = null;
            timeRangeIndex = 0;
            rangeIsAll = false;
            startDateTime = null; endDateTime = null;
            startTimeOnly = null; endTimeOnly = null;
            startDateOnly = null; endDateOnly = null;
            startDayOfWeekSet = false; endDayOfWeekSet = false;
            currentSpecifier = new ContextType();
            currentMode = new ContextType();
            crossDayBoundary = false;

            ResetEnumerations();
        }

        /// <summary>
        /// Resets enums to their default values.
        /// </summary>
        private void ResetEnumerations()
        {
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;
            sortType = SortType.DEFAULT;
            searchType = SearchType.NONE;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRangeOne = TimeRangeKeywordsType.NONE;
            timeRangeTwo = TimeRangeKeywordsType.NONE;
        }
        #endregion

        // ******************************************************************
        // Finalize generator for operation creation
        // ******************************************************************

        #region Finalize Generator
        /// <summary>
        /// Finalizes the generator so that it can begin generating operations
        /// with the correct time ranges.
        /// </summary>
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

        /// <summary>
        /// Returns true if the operation to be generated can carry out a search
        /// on the task list, and false if not.
        /// </summary>
        /// <returns>Boolean indicating if the command is of a searchable type.</returns>
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
        /// <summary>
        /// Finalizes the date/times of the operation to be generated
        /// by setting it as an appropriate search range.
        /// </summary>
        /// <returns></returns>
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

            // If end time is not specific, extend search range to cover appropriate period.
            if (endDateOnly != null && endTimeOnly == null)
                ExtendEndSearchDate();
        }

        /// <summary>
        /// Returns a boolean indicating if only the start time for the generated
        /// Operation is set and the end date/times are not.
        /// </summary>
        /// <returns>True if the start time is set, false if not.</returns>
        private bool IsOnlyStartTimeSet()
        {
            return startTimeOnly != null && endTimeOnly == null && endDateOnly == null;
        }

        /// <summary>
        /// Returns a boolean indicating if only the start date for the generated
        /// Operation is set, and the start time, end date/times are not.
        /// </summary>
        /// <returns>True if the only the start date is set, false if not.</returns>
        private bool IsOnlyStartDateSet()
        {
            return startDateOnly != null && endDateOnly == null && startTimeOnly == null && endTimeOnly == null;
        }

        /// <summary>
        /// Extends the end date to the end of the day, month or year,
        /// depending on the already set Specificity of the generator.
        /// </summary>
        private void ExtendEndSearchDate()
        {
            if (!isSpecific.EndDate.Day)
            {
                ExtendEndMonthOrYear();
                endDateOnly = endDateOnly.Value.AddMinutes(-1);
            }
            else if (isSpecific.StartDate.Day == true)
            {
                ExtendEndDay();
            }
        }

        /// <summary>
        /// Extends the end date to the end of the month or year,
        /// depending on the already set Specificity of the generator.
        /// </summary>
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

        /// <summary>
        /// Extends the end date to the end of the day.
        /// </summary>
        private void ExtendEndDay()
        {
            endDateOnly = new DateTime(endDateOnly.Value.Year, endDateOnly.Value.Month, endDateOnly.Value.Day, 23, 59, 0);
        }
        #endregion

        // ******************************************************************
        // Finalize Scheduling Time
        // ******************************************************************

        #region Finalize Scheduling Time
        /// <summary>
        /// Finalizes the scheduling time range.
        /// </summary>
        private void FinalizeSchedulingTime()
        {
            FinalizeScheduleStartDate();
        }

        /// <summary>
        /// Sets the start date to today if no starting date was given.
        /// </summary>
        private void FinalizeScheduleStartDate()
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
        /// <summary>
        /// This method sets the final startTimeOnly and endTimeOnly values based on the input
        /// time (3am, 4pm...) and time range keyword(s) (morning, afternoon...).
        /// </summary>
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

        /// <summary>
        /// This method retrieves the start and end hours of the input time range keyword(s) (morning, afternoon...).
        /// It also updates the crossDayBoundary boolean to true if the time ranges crosses the day boundary
        /// i.e. 11pm to 1am.
        /// </summary>
        /// <param name="startTimeHour">The time range's start hour</param>
        /// <param name="endTimeHour">The time range's end hour</param>
        /// <returns>Returns true if there were input time range keyword(s); false if otherwise</returns>
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

        /// <summary>
        /// This method checks if a specific time was supplied.
        /// </summary>
        /// <returns>True if both the start time and end time were not specified; false if otherwise</returns>
        private bool IsSpecificTimeSupplied()
        {
            if (startTimeOnly == null && endTimeOnly == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method picks the final hours values to be used for the time range start and end hours values
        /// from the input time (3am, 4pm...) and time range keyword(s) (morning, afternoon...).
        /// </summary>
        /// <param name="startTimeHour">The time range's start hour</param>
        /// <param name="endTimeHour">The time range's end hour</param>
        private void RetrieveFinalStartAndEndTimes(int startTimeHour, int endTimeHour)
        {
            TimeSpan startTimeRange = new TimeSpan(startTimeHour, 0, 0);
            TimeSpan endTimeRange = new TimeSpan(endTimeHour, 0, 0);
            if (startTimeOnly != null && endTimeOnly == null)
            {
                if (!IsStartTimeWithinTimeRange(startTimeRange, endTimeRange))
                {
                    AlertBox.Show("Specified end time not within specified time range.");
                }
                endTimeOnly = startTimeOnly;
                startTimeOnly = startTimeRange;
            }
            else if (startTimeOnly != null && endTimeOnly != null)
            {
                if (!IsStartAndEndTimeWithinTimeRange(startTimeRange, endTimeRange))
                {
                    AlertBox.Show("Specified start and end times are not within specified time range.");
                }
            }
        }

        /// <summary>
        /// This method checks if the startTimeOnly is within the specified start and end hours
        /// </summary>
        /// <param name="startTimeHour">The specified start hour</param>
        /// <param name="endTimeHour">The specified end hour</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool IsStartTimeWithinTimeRange(TimeSpan startTimeRange, TimeSpan endTimeRange)
        {
            if (!(startTimeRange <= startTimeOnly
                && startTimeOnly <= endTimeRange))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method checks if the startTimeOnly and endTimeOnly are within the specified start and end hours
        /// </summary>
        /// <param name="startTimeHour">The specified start hour</param>
        /// <param name="endTimeHour">The specified end hour</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool IsStartAndEndTimeWithinTimeRange(TimeSpan startTimeRange, TimeSpan endTimeRange)
        {
            if (!(startTimeRange <= startTimeOnly
                && startTimeOnly <= endTimeRange
                && startTimeRange <= endTimeOnly
                && endTimeOnly <= endTimeRange))
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
            {
                if (this.commandType == CommandType.ADD)
                {
                    if (endDayOfWeekSet)
                    {
                        while (limit > combinedDT)
                        {
                            combinedDT = combinedDT.Value.AddDays(7);
                        }
                    }
                    if (!isSpecific.EndDate.Month)
                    {
                        while (limit > combinedDT)
                        {
                            combinedDT = combinedDT.Value.AddMonths(1);
                        }
                    }
                    else if (!isSpecific.EndDate.Year)
                    {
                        while (limit > combinedDT)
                        {
                            combinedDT = combinedDT.Value.AddYears(1);
                        }
                    }
                }
            }
            return combinedDT;
        }
        #endregion
        #endregion

        // ******************************************************************
        // Operation Generation
        // ******************************************************************

        #region Operation Generation
        /// <summary>
        /// This operation generates an operation based on how this generator has been configured.
        /// </summary>
        /// <returns>The generated operation.</returns>
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
        /// <summary>
        /// Sets the configured end time to the specified time and specificity.
        /// Moves the end time to the start time if necessary.
        /// </summary>
        /// <param name="Value">The end time to be set.</param>
        /// <param name="IsSpecific">The specificity of the end time.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the configured end date to the specified date and specificity.
        /// Moves the end date to the start date if necessary.
        /// </summary>
        /// <param name="Value">The end daate to be set.</param>
        /// <param name="IsSpecific">The specificity of the end date.</param>
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