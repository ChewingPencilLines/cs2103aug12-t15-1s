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
            // check that range > span, else return failure response
            // (error response should be different from if no fitting slot can be found)
            TimeSpan span = ((DateTime)endDateTime) - startDateTime;
            switch (timeRangeType)
            {
                case TimeRangeType.HOUR:
                    if (timeRangeIndex > span.TotalHours)
                    {
                        // error: not enough time to schedule task; time span < task duration
                        return response;
                    }
                    break;
                case TimeRangeType.DAY:
                    if (timeRangeIndex > span.TotalDays)
                    {
                        // error: not enough time to schedule task; time span < task duration
                        return response;
                    }
                    break;
                case TimeRangeType.MONTH:
                    if (startDateTime.AddMonths(timeRangeIndex) > ((DateTime)endDateTime))
                    {
                        // error: not enough time to schedule task; time span < task duration
                        return response;
                    }
                    break;
                default:
                    // should never fall to this
                    break;
            }
            // todo: loop through all tasks to find earliest possible fitting time
            bool isSlotFound = false;;
            DateTime tryStartTime = startDateTime;
            DateTime tryEndTime = new DateTime();
            DateTime copyTryStartTime = startDateTime;
            DateTimeSpecificity searchSpecificity = new DateTimeSpecificity();
            int index = 0;
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
                        numberOfSetsToLoop = timeRangeIndex;
                        break;
                    case TimeRangeType.MONTH:
                        span = tryStartTime.AddMonths(timeRangeIndex) - tryStartTime;
                        numberOfSetsToLoop = (int)span.TotalDays;
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
                    if (i > 0) tryStartTime = tryStartTime.AddDays(1);
                    if (tryEndTime <= tryStartTime)
                        tryEndTime = tryStartTime.AddDays(1).Add(((DateTime)endDateTime).TimeOfDay);
                    searchResults = SearchForTasks(null, searchSpecificity, false, tryStartTime, tryEndTime);
                    if (searchResults.Count != 0) break;
                }
                // once fitting time is found, change its start and end datetime then
                // add the task; return success response
                if (searchResults.Count == 0)
                {
                    if (tryEndTime > endDateTime)
                        break;
                    TaskEvent newTask = new TaskEvent(taskName, copyTryStartTime, tryEndTime.AddSeconds(-1), searchSpecificity);
                    response = AddTask(newTask);
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
            // if there is no time span indicated i.e. 3 days etc., get default 
            // getting task duration
            if (timeRangeIndex == 0 && timeRangeType == TimeRangeType.DEFAULT)
            {
                // todo: get default time range from settings
                // for now, just take it to be 1 hour long
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
                    // shit: not able to differentiate between "schedule task today 3 hours" and
                    // "schedule task 3 days" -- endDateTime = DateTime.Max;
                    if (isSpecific.StartDate.Day)
                        endDateTime = startDateTime.AddDays(1).Date;
                    else
                        endDateTime = startDateTime.AddMonths(1).Date;
                }
            }
            if (startDateTime < DateTime.Now)
            {
                startDateTime = startDateTime.AddHours(DateTime.Now.Hour);
            }
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task task = undoTask.Pop();
            return DeleteTask(task);
        }
    }
}