using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class OperationSchedule : Operation
    {
        string taskName;
        DateTime startDateTime;
        DateTime? endDateTime;
        DateTimeSpecificity isSpecific;
        int timeRangeAmount;
        TimeRangeType timeRangeType;
        Task scheduledTask;
        DateTimeSpecificity searchSpecificity = new DateTimeSpecificity();
        
        public OperationSchedule(string taskName, DateTime startDateTime, DateTime? endDateTime, DateTimeSpecificity isSpecific, int timeRangeAmount, TimeRangeType timeRangeType)
        {
            this.taskName = taskName;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.isSpecific = isSpecific;
            this.timeRangeAmount = timeRangeAmount;
            this.timeRangeType = timeRangeType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            Response response;
            SetMembers(taskList, storageIO);
            RetrieveParameters();
            if (!CheckTaskDurationWithinRange())
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
            }
            else
            {
                response = TryScheduleTask();
            }
            return response;
        }

        private Response TryScheduleTask()
        {
            Response response = null;
            bool isSlotFound = false;
            DateTime tryStartTime = startDateTime;
            DateTime tryEndTime = new DateTime();
            DateTime copyTryStartTime = startDateTime;
            int index = 0;
            // loop through all tasks to find earliest possible fitting time
            while (!isSlotFound && tryEndTime <= ((DateTime)endDateTime))
            {
                int numberOfSetsToLoop = 1;
                switch (timeRangeType)
                {
                    case TimeRangeType.HOUR:
                        tryStartTime = startDateTime.AddHours(index);
                        tryEndTime = tryStartTime.AddHours(timeRangeAmount);
                        break;
                    case TimeRangeType.DAY:
                        if (!isSpecific.StartTime)
                        {
                            timeRangeType = TimeRangeType.HOUR;
                            timeRangeAmount *= CustomDictionary.HOURS_IN_DAY;
                        }
                        numberOfSetsToLoop = timeRangeAmount;
                        break;
                    case TimeRangeType.WEEK:
                        if (!isSpecific.StartTime)
                        {
                            timeRangeType = TimeRangeType.HOUR;
                            timeRangeAmount *= CustomDictionary.HOURS_IN_DAY * CustomDictionary.DAYS_IN_WEEK;
                        }
                        else
                        {
                            timeRangeType = TimeRangeType.DAY;
                            timeRangeAmount *= CustomDictionary.DAYS_IN_WEEK;
                        }
                        numberOfSetsToLoop = timeRangeAmount;
                        break;
                    case TimeRangeType.MONTH:
                        TimeSpan span = tryStartTime.AddMonths(timeRangeAmount) - tryStartTime;
                        if (!isSpecific.StartTime)
                        {
                            timeRangeType = TimeRangeType.HOUR;
                            timeRangeAmount = numberOfSetsToLoop = (int)span.TotalHours;
                        }
                        else
                        {
                            numberOfSetsToLoop = (int)span.TotalDays;
                        }
                        break;
                }
                copyTryStartTime = tryStartTime;
                List<Task> searchResults = new List<Task>();
                if (numberOfSetsToLoop == 0)
                {
                    response = new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
                }
                for (int i = 0; i < numberOfSetsToLoop; i++)
                {
                    if (i > 0)
                    {
                        if (timeRangeType == TimeRangeType.HOUR)
                        {
                            tryStartTime = tryStartTime.AddHours(1);
                        }
                        else
                        {
                            tryStartTime = tryStartTime.AddDays(1);
                        }
                    }
                    if (tryEndTime <= tryStartTime)
                    {
                        tryEndTime = tryStartTime.AddDays(1).Add(((DateTime)endDateTime).TimeOfDay);
                    }
                    searchResults = SearchForTasks(null, searchSpecificity, false, tryStartTime, tryEndTime);
                    if (searchResults.Count != 0)
                    {
                        break;
                    }
                }
                if (searchResults.Count == 0 && tryEndTime > endDateTime)
                {
                    scheduledTask = new TaskEvent(taskName, copyTryStartTime, tryEndTime.AddSeconds(-1), searchSpecificity);
                    response = AddTask(scheduledTask);
                    if (response.IsSuccessful())
                    {
                        TrackOperation();
                    }
                    isSlotFound = true;
                }
                tryStartTime = copyTryStartTime.AddDays(1);
                index++;
            }
            return response;
        }

        private void RetrieveParameters()
        {
            // if there is no time duration specified i.e. 3 days etc., get default 
            if (timeRangeAmount == 0 && timeRangeType == TimeRangeType.DEFAULT)
            {
                timeRangeAmount = CustomDictionary.defaultScheduleTimeLength;
                timeRangeType = CustomDictionary.defaultScheduleTimeLengthType;
            }
            // setting the start and end search datetimes
            if (isSpecific.StartTime && !isSpecific.EndTime)
            {
                if (isSpecific.StartDate.Day)
                {
                    if (endDateTime == null)
                        endDateTime = startDateTime.AddDays(1).Date;
                    else
                        endDateTime = ((DateTime)endDateTime).AddDays(1).Date;
                }
                else
                {
                    if (endDateTime == null)
                        endDateTime = startDateTime.AddMonths(1).Date;
                    else
                    {
                        if (isSpecific.EndDate.Day)
                            endDateTime = ((DateTime)endDateTime).AddDays(1).Date;
                        else
                            endDateTime = startDateTime.AddMonths(1).Date;
                    }
                }
            }
            else if (!isSpecific.StartTime && !isSpecific.EndTime)
            {
                if (endDateTime != null)
                {
                    if (isSpecific.EndDate.Day)
                        endDateTime = ((DateTime)endDateTime).AddDays(1).Date;
                    else
                        endDateTime = ((DateTime)endDateTime).AddMonths(1).Date;
                }
                else
                {
                    if (isSpecific.StartDate.Day)
                        endDateTime = startDateTime.AddDays(1).Date;
                    else if (isSpecific.StartDate.Month)
                        endDateTime = startDateTime.AddMonths(1).Date;
                    else
                        endDateTime = DateTime.MaxValue.Date;
                }
            }
            if (startDateTime < DateTime.Now)
            {
                startDateTime = startDateTime.AddHours(DateTime.Now.Hour+1);
            }
        }

        private bool CheckTaskDurationWithinRange()
        {
            // check that range > span, else return failure response
            // (error response should be different from if no fitting slot can be found)
            TimeSpan span = ((DateTime)endDateTime) - startDateTime;
            switch (timeRangeType)
            {
                case TimeRangeType.HOUR:
                    if (timeRangeAmount > span.TotalHours)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.DAY:
                    if (timeRangeAmount > span.TotalDays)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.MONTH:
                    if (startDateTime.AddMonths(timeRangeAmount) > ((DateTime)endDateTime))
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = DeleteTask(scheduledTask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = AddTask(scheduledTask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
    }
}