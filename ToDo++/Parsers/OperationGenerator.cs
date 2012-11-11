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
 
        // These properties control the type of operation to be generated.
        private CommandType commandType;
        public CommandType CommandType
        {
            get { return commandType; }
            set { commandType = value; }
        }        

        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        private int[] taskRangeIndex;
        public int[] TaskRangeIndex
        {
            get { return taskRangeIndex; }
            set { taskRangeIndex = value; }
        }

        private DateTimeSpecificity isSpecific;
        public DateTimeSpecificity IsSpecific
        {
            get { return isSpecific; }
            set { isSpecific = value; }
        }

        private TimeRangeType timeRangeType;
        public TimeRangeType TimeRangeType
        {
            get { return timeRangeType; }
            set { timeRangeType = value; }
        }

        private TimeRangeKeywordsType timeRangeOne;
        public TimeRangeKeywordsType TimeRangeFirst
        {
            get { return timeRangeOne; }
            set { timeRangeOne = value; }
        }

        private TimeRangeKeywordsType timeRangeTwo;
        public TimeRangeKeywordsType TimeRangeSecond
        {
            get { return timeRangeTwo; }
            set { timeRangeTwo = value; }
        }

        private int timeRangeIndex;
        public int TimeRangeIndex
        {
            get { return timeRangeIndex; }
            set { timeRangeIndex = value; }
        }

        private SortType sortType;
        public SortType SortType
        {
            set { sortType = value; }
        }

        private SearchType searchType;
        public SearchType SearchType
        {
            set { searchType = value; }
        }

        private bool rangeIsAll;
        public bool RangeIsAll
        {
            set { rangeIsAll = value; }
        }
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
        private ContextType currentSpecifier;
        public ContextType CurrentSpecifier
        {
            get { return currentSpecifier; }
            set { currentSpecifier = value; }
        }
        private ContextType currentMode;
        public ContextType CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
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
        /// <returns>Nothing.</returns>
        private void FinalizeSchedulingTime()
        {
            FinalizeScheduleStartDate();
        }

        /// <summary>
        /// Sets the start date to today if no starting date was given.
        /// </summary>
        /// <returns>Nothing.</returns>
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
        /// <summary>
        /// Combines the start and end DateOnly and TimeOnly fields of the configuration properties
        /// into a two properly formatted start and end DateTimes for operation generation.
        /// </summary>
        /// <returns>Nothing</returns>
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

            startDateTime = CombineDateAndTime(startTimeOnly, startDateOnly, isSpecific.StartDate, DateTime.Now, true);
            if (startDateTime == null)
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, isSpecific.EndDate, DateTime.Now, true);
            else
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, isSpecific.EndDate, startDateTime.Value, false);

            if (startDateTime > endDateTime)
                AlertBox.Show("Warning: End date is before start date");
        }

        /// <summary>
        /// Combines the time and date fields into a single DateTime that is after the specified limit.
        /// </summary>
        /// <returns>Nothing</returns>
        private DateTime? CombineDateAndTime(TimeSpan? time, DateTime? date, Specificity dateSpecificity, DateTime limit, bool allowCurrent)
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
            if (crossDayBoundary || (combinedDT < limit && dateSpecificity.Day == false))
            {
                combinedDT = combinedDT.Value.AddDays(1);
            }
            if (limit > combinedDT)
                PushDateToBeyondLimit(ref combinedDT, ref dateSpecificity, limit, allowCurrent);
            return combinedDT;
        }

        private void PushDateToBeyondLimit(ref DateTime? combinedDT, ref Specificity dateSpecificity, DateTime limit, bool allowCurrent)
        {
            if (this.commandType == CommandType.ADD)
            {
                if (IsDayOfWeek(allowCurrent))
                {
                    while (limit > combinedDT)
                    {
                        combinedDT = combinedDT.Value.AddDays(7);
                    }
                }

                if (combinedDT.Value.Month == DateTime.Today.Month &&
                    combinedDT.Value.Year == DateTime.Today.Year &&
                    allowCurrent)
                    return;

                if (!dateSpecificity.Month)
                {
                    while (limit > combinedDT)
                        combinedDT = combinedDT.Value.AddMonths(1);

                    dateSpecificity.Month = true;
                }
                else if (!dateSpecificity.Year)
                {
                    if (combinedDT.Value.Date == DateTime.Today.Date && allowCurrent)
                        return;

                    while (limit > combinedDT)
                        combinedDT = combinedDT.Value.AddYears(1);

                    dateSpecificity.Year = true;
                }
            }
        }

        private bool IsDayOfWeek(bool allowCurrent)
        {
            return startDayOfWeekSet && allowCurrent ||
                                endDayOfWeekSet && !allowCurrent;
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
        /// <returns></returns>
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