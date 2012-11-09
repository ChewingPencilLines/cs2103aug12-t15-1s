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
        int taskDurationAmount;
        TimeRangeType taskDurationType;
        Task scheduledTask;
        DateTimeSpecificity searchSpecificity = new DateTimeSpecificity();
        
        public OperationSchedule(string taskName, DateTime startDateTime, DateTime? endDateTime, DateTimeSpecificity isSpecific, int timeRangeAmount, TimeRangeType timeRangeType)
        {
            this.taskName = taskName;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.isSpecific = isSpecific;
            this.taskDurationAmount = timeRangeAmount;
            this.taskDurationType = timeRangeType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            Response response;
            SetMembers(taskList, storageIO);
            RetrieveParameters();
            if (!IsTaskDurationWithinRange() || taskDurationAmount == 0)
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
            }
            else
            {
                response = TryScheduleTask();
            }
            return response;
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

        // ******************************************************************
        // Private Methods
        // ******************************************************************

        #region Private Helper Methods
        private void RetrieveParameters()
        {
            SetTaskDuration();
            SetTimeRange();
        }

        /// <summary>
        /// This method sets the task duration to the default task duration if it was not specified by the user.
        /// </summary>
        private void SetTaskDuration()
        {
            // if there is no task duration specified i.e. 3 days etc., get default 
            if (taskDurationType == TimeRangeType.DEFAULT)
            {
                taskDurationAmount = CustomDictionary.defaultScheduleTimeLength;
                taskDurationType = CustomDictionary.defaultScheduleTimeLengthType;
            }
        }

        /// <summary>
        /// This method sets the schedule datetime range within which the task ought tbe scheduled.
        /// </summary>
        private void SetTimeRange()
        {
            // setting the start and end search datetimes
            if (isSpecific.StartTime && !isSpecific.EndTime)
            {
                if (isSpecific.StartDate.Day && endDateTime == null)
                {
                    endDateTime = startDateTime.AddDays(1).Date;
                }
                else if (!(isSpecific.StartDate.Day || isSpecific.EndDate.Day))
                {
                    endDateTime = startDateTime.AddMonths(1).Date;
                }
                else if (endDateTime != null
                    && (!isSpecific.StartDate.Day || isSpecific.EndDate.Day))
                {
                    endDateTime = ((DateTime)endDateTime).AddDays(1).Date;
                }
            }
            else if (!isSpecific.StartTime && !isSpecific.EndTime)
            {
                if (endDateTime == null)
                {
                    endDateTime = DateTime.MaxValue.Date;
                    if (isSpecific.StartDate.Day)
                    {
                        endDateTime = startDateTime.AddDays(1).Date;
                    }
                    if (isSpecific.StartDate.Month)
                    {
                        endDateTime = startDateTime.AddMonths(1).Date;
                    }
                }
                else
                {
                    if (isSpecific.EndDate.Day)
                    {
                        endDateTime = ((DateTime)endDateTime).AddDays(1).Date;
                    }
                    else
                    {
                        endDateTime = ((DateTime)endDateTime).AddMonths(1).Date;
                    }
                }
            }
            if (startDateTime < DateTime.Now)
            {
                startDateTime = startDateTime.AddHours(DateTime.Now.Hour + 1);
            }
        }

        /// <summary>
        /// This method checks if the task can possibly be scheduled within the schedule datetime range
        /// i.e. it checks that the task duration can be contained within the schedule time range.
        /// </summary>
        /// <returns>True if possible, false if otherwise</returns>
        private bool IsTaskDurationWithinRange()
        {
            // check that range > span, else return failure response
            // (error response should be different from if no fitting slot can be found)
            TimeSpan span = ((DateTime)endDateTime) - startDateTime;
            switch (taskDurationType)
            {
                case TimeRangeType.HOUR:
                    if (taskDurationAmount > span.TotalHours)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.DAY:
                    if (taskDurationAmount > span.TotalDays)
                    {
                        return false;
                    }
                    break;
                case TimeRangeType.MONTH:
                    if (startDateTime.AddMonths(taskDurationAmount) > ((DateTime)endDateTime))
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// This method tries to find a free time slot within the spceified schedule datetime range
        /// to schedule the task. 
        /// </summary>
        /// <returns>The appropriate response object, depending on whether a free slot could be found</returns>
        private Response TryScheduleTask()
        {
            Response response = new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationSchedule), currentListedTasks);
            DateTime tryStartTime = startDateTime;
            DateTime tryEndTime = new DateTime();
            int numberOfIterations = 0;
            bool isSlotFound = false;
            
            // loop through all tasks to find earliest possible fitting time
            while (!isSlotFound && tryEndTime <= ((DateTime)endDateTime))
            {
                int numberOfSetsToLoop = 1;
                switch (taskDurationType)
                {
                    case TimeRangeType.HOUR:
                        tryStartTime = startDateTime.AddHours(numberOfIterations);
                        tryEndTime = tryStartTime.AddHours(taskDurationAmount);
                        break;
                    case TimeRangeType.DAY:
                    case TimeRangeType.WEEK:
                    case TimeRangeType.MONTH:
                        numberOfSetsToLoop = GetNumberOfLoops(taskDurationType, tryStartTime);
                        break;
                }
                isSlotFound = IsTimeSlotFreeOfTasks(numberOfSetsToLoop, tryStartTime, ref tryEndTime);
                if (isSlotFound)
                {
                    response = ScheduleTaskAtSlot(taskName, tryStartTime, tryEndTime);
                }
                tryStartTime = tryStartTime.AddDays(1);
                numberOfIterations++;
            }
            return response;
        }

        /// <summary>
        /// This method breaks the task duration down to blocks of hours or days if the schedule start time is
        /// not specific.
        /// </summary>
        /// <param name="type">The task duration type: hours, days, weeks or months</param>
        /// <param name="startTime">The time slot's start time</param>
        /// <returns>Returns the number of divided blocks</returns>
        private int GetNumberOfLoops(TimeRangeType type, DateTime startTime)
        {
            int numberOfSetsToLoop = 0;
            switch (taskDurationType)
            {
                case TimeRangeType.DAY:
                    if (!isSpecific.StartTime)
                    {
                        taskDurationType = TimeRangeType.HOUR;
                        taskDurationAmount *= CustomDictionary.HOURS_IN_DAY;
                    }
                    numberOfSetsToLoop = taskDurationAmount;
                    break;
                case TimeRangeType.WEEK:
                    if (!isSpecific.StartTime)
                    {
                        taskDurationType = TimeRangeType.HOUR;
                        taskDurationAmount *= CustomDictionary.HOURS_IN_DAY * CustomDictionary.DAYS_IN_WEEK;
                    }
                    else
                    {
                        taskDurationType = TimeRangeType.DAY;
                        taskDurationAmount *= CustomDictionary.DAYS_IN_WEEK;
                    }
                    numberOfSetsToLoop = taskDurationAmount;
                    break;
                case TimeRangeType.MONTH:
                    TimeSpan span = startTime.AddMonths(taskDurationAmount) - startTime;
                    if (!isSpecific.StartTime)
                    {
                        taskDurationType = TimeRangeType.HOUR;
                        taskDurationAmount = numberOfSetsToLoop = (int)span.TotalHours;
                    }
                    else
                    {
                        numberOfSetsToLoop = (int)span.TotalDays;
                    }
                    break;
            }
            return numberOfSetsToLoop;
        }

        /// <summary>
        /// This method checks if the time slot is free of task and hence, available for task scheduling.
        /// </summary>
        /// <param name="numberOfLoops">The number of iterations (time slot may be composed of several hours/days)</param>
        /// <param name="tryStartTime">The time slot's start time</param>
        /// <param name="tryEndTime">The time slot's end time</param>
        /// <returns>True if the time slot is available for task schedule; false if otherwise</returns>
        private bool IsTimeSlotFreeOfTasks(int numberOfLoops, DateTime tryStartTime, ref DateTime tryEndTime)
        {
            List<Task> searchResults = new List<Task>();
            for (int i = 0; i < numberOfLoops; i++)
            {
                if (tryEndTime <= tryStartTime)
                {
                    tryEndTime = tryStartTime.AddDays(1).Add(((DateTime)endDateTime).TimeOfDay);
                }
                searchResults = SearchForTasks(null, searchSpecificity, false, tryStartTime, tryEndTime);
                if (taskDurationType == TimeRangeType.HOUR)
                {
                    tryStartTime = tryStartTime.AddHours(1);
                }
                else
                {
                    tryStartTime = tryStartTime.AddDays(1);
                }
                if (searchResults.Count != 0)
                {
                    break;
                }
            }
            if (searchResults.Count == 0 && tryEndTime <= endDateTime)
            {   
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method creates an event task and schedules it at the time slot.
        /// </summary>
        /// <param name="taskName">Name of the task</param>
        /// <param name="startTime">Start time of the time slot</param>
        /// <param name="endTime">End time of the time slot</param>
        /// <returns>The appropriate response object, depending on whether he task could be scheduled at the time slot</returns>
        private Response ScheduleTaskAtSlot(string taskName, DateTime startTime, DateTime endTime)
        {
            Response response;
            scheduledTask = new TaskEvent(taskName, startTime, endTime.AddSeconds(-1), searchSpecificity);
            response = AddTask(scheduledTask);
            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
        }
        #endregion
    }
}