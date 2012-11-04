using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class OperationSchedule : Operation
    {
        private Task newTask;
        int taskSpanIndex;
        TimeRangeType taskSpanType;
        public OperationSchedule(Task setTask, int timeRangeIndex, TimeRangeType timeRangeType)
        {
            newTask = setTask;
            taskSpanIndex = timeRangeIndex;
            taskSpanType = timeRangeType;
        }
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            Response response = null;
            if (newTask == null)
            {
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            // if there is no time span indicated i.e. 3 days etc., get default 
            // getting task duration
            if (taskSpanIndex == 0 && taskSpanType == TimeRangeType.DEFAULT)
            {
                // todo: get default time range from settings
                // for now, just take it to be 1 hour long
                taskSpanIndex = 1;
                taskSpanType = TimeRangeType.HOUR;
            }
            // todo: check that range > span, else return failure response
            // (error response should be different from if no fitting slot can be found)
            bool isTimeSpanSpecified = ((TaskEvent)newTask).isSpecific.StartTime && ((TaskEvent)newTask).isSpecific.EndTime;
            if (taskSpanType == TimeRangeType.HOUR)
            {
                int span = ((TaskEvent)newTask).EndDateTime.Hour - ((TaskEvent)newTask).StartDateTime.Hour;
                if (isTimeSpanSpecified && taskSpanIndex > span)
                {
                    // error
                }
            }
            else if (taskSpanType == TimeRangeType.DAY)
            {
                TimeSpan span = ((TaskEvent)newTask).EndDateTime - ((TaskEvent)newTask).StartDateTime;
                if (!(!isTimeSpanSpecified && taskSpanIndex <= span.Days))
                {
                    //error
                }
            }
            else if (taskSpanType == TimeRangeType.MONTH)
            {
                if (((TaskEvent)newTask).StartDateTime.AddMonths(taskSpanIndex) > ((TaskEvent)newTask).EndDateTime)
                {
                    // error
                }
            }
            // todo: loop through all tasks to find earliest possible fitting time
            // once fitting time is found, change its start and end datetime then
            // add the task; return success response
            response = AddTask(newTask, taskList);
            if (response.IsSuccessful()) TrackOperation();
            return response;// "stub"; (failure to schedule task)
        }
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return DeleteTask(task, taskList);
        }
    }
}
