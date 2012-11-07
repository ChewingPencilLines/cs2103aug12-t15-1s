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
        int timeRangeIndex;
        TimeRangeType timeRangeType;
        Task scheduledTask;
        
        public OperationSchedule(string taskName, DateTime startDateTime, DateTime? endDateTime, DateTimeSpecificity isSpecific, int timeRangeIndex, TimeRangeType timeRangeType)
        {
            this.taskName = taskName;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.isSpecific = isSpecific;
            this.timeRangeIndex = timeRangeIndex;
            this.timeRangeType = timeRangeType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            // default response: failure to schedule task i.e. cannot find fitting slot
            Response response = new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
            retrieveParameters();
            if (!CheckTaskDurationWithinRange())
            {
                return response = new Response(Result.INVALID_TASK, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
            }
            bool isSlotFound = false;;
            DateTime tryStartTime = startDateTime;
            DateTime tryEndTime = new DateTime();
            DateTime copyTryStartTime = startDateTime;
            DateTimeSpecificity searchSpecificity = new DateTimeSpecificity();
            int index = 0;
            // loop through all tasks to find earliest possible fitting time
            while (!isSlotFound && tryEndTime <= ((DateTime)endDateTime))
            {
                int numberOfSetsToLoop = 1;
                switch (timeRangeType)
                {
                    case TimeRangeType.HOUR:
                        tryStartTime = startDateTime.AddHours(index);
                        tryEndTime = tryStartTime.AddHours(timeRangeIndex);
                        break;
                    case TimeRangeType.DAY:
                        if (!isSpecific.StartTime)
                        {
                            timeRangeType = TimeRangeType.HOUR;
                            timeRangeIndex *= 24;
                        }
                        numberOfSetsToLoop = timeRangeIndex;
                        break;
                    case TimeRangeType.MONTH:
                        TimeSpan span = tryStartTime.AddMonths(timeRangeIndex) - tryStartTime;
                        if (!isSpecific.StartTime)
                        {
                            timeRangeType = TimeRangeType.HOUR;
                            timeRangeIndex = numberOfSetsToLoop = (int)span.TotalHours;
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
                    return response;
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
                // once fitting time is found, change its start and end datetime then
                // add the task; return success response
                if (searchResults.Count == 0)
                {
                    if (tryEndTime > endDateTime)
                    {
                        break;
                    }
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

        private void retrieveParameters()
        {
            // if there is no time duration specified i.e. 3 days etc., get default 
            if (timeRangeIndex == 0 && timeRangeType == TimeRangeType.DEFAULT)
            {
                timeRangeIndex = CustomDictionary.defaultTimeRangeIndex;
                timeRangeType = CustomDictionary.defaultTimeRangeType;
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
                    if (timeRangeIndex > span.TotalHours)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.DAY:
                    if (timeRangeIndex > span.TotalDays)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.MONTH:
                    if (startDateTime.AddMonths(timeRangeIndex) > ((DateTime)endDateTime))
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