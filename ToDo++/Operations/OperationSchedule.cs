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
            // default response: failure to schedule task
            Response response = null;
            // if there is no time span indicated i.e. 3 days etc., get default 
            // getting task duration
            if (timeRangeIndex == 0 && timeRangeType == TimeRangeType.DEFAULT)
            {
                // todo: get default time range from settings
                // for now, just take it to be 1 hour long
                timeRangeIndex = 1;
                timeRangeType = TimeRangeType.HOUR;
            }
            // todo: check that range > span, else return failure response
            // (error response should be different from if no fitting slot can be found)
            bool isTimeSpanSpecified = isSpecific.StartTime && isSpecific.EndTime;
            if (endDateTime == null)
            {
                if (isSpecific.StartDate.Day)
                {
                    endDateTime = startDateTime.AddDays(1);
                }
                else
                {
                    endDateTime = startDateTime.AddMonths(1);
                }
            }
            if (!isTimeSpanSpecified)
            {
                startDateTime = startDateTime.AddHours(DateTime.Now.AddHours(1).Hour);
            }
            switch (timeRangeType)
            {
                case TimeRangeType.HOUR:
                    int numberOfConsecutiveHours = ((DateTime)endDateTime).Hour - startDateTime.Hour;
                    if (timeRangeIndex > numberOfConsecutiveHours)
                    {
                        // error
                    }
                    break;
                case TimeRangeType.DAY:
                    int numberOfConsecutiveDays = ((DateTime)endDateTime).Day - startDateTime.Day;
                    if (timeRangeIndex > numberOfConsecutiveDays)
                    {
                        //error
                    }
                    break;
                case TimeRangeType.MONTH:
                    if (startDateTime.AddMonths(timeRangeIndex) > ((DateTime)endDateTime))
                    {
                        // error
                    }
                    break;
                default:
                    // should never fall to this
                    break;
            }
            // todo: loop through all tasks to find earliest possible fitting time
            bool isSlotFound = false;
            int index = 1;
            DateTime tryStartTime = new DateTime();
            DateTime tryEndTime = new DateTime();
            DateTimeSpecificity searchSpecificity = new DateTimeSpecificity();
            while (!isSlotFound && tryEndTime <= ((DateTime)endDateTime))
            {
                tryStartTime = startDateTime.AddHours((index-1)*timeRangeIndex);
                switch (timeRangeType)
                {
                    case TimeRangeType.HOUR:
                        tryEndTime = startDateTime.AddHours(index*timeRangeIndex);
                        break;
                    case TimeRangeType.DAY:
                        tryEndTime = startDateTime.AddDays(index*timeRangeIndex);
                        break;
                    case TimeRangeType.MONTH:
                        tryEndTime = startDateTime.AddMonths(index*timeRangeIndex);
                        break;
                }
                List<Task> searchResults = SearchForTasks(taskList, null, searchSpecificity, false, tryStartTime, tryEndTime);
                // once fitting time is found, change its start and end datetime then
                // add the task; return success response
                if (searchResults.Count == 0)
                {
                    TaskEvent newTask = new TaskEvent(taskName, tryStartTime, tryEndTime, searchSpecificity);
                    response = AddTask(newTask, taskList);
                    if (response.IsSuccessful())
                    {
                        TrackOperation();
                    }
                    isSlotFound = true;
                }
                index++;
            }
            return response;
        }
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return DeleteTask(task, taskList);
        }
    }
}
