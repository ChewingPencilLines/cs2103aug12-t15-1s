﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for task
    // ******************************************************************

    public abstract class Task
    {
        protected string taskName;
        public string TaskName
        {
            get { return taskName; }
            //set { taskName = value; }
        }

        protected Boolean doneState;
        public Boolean DoneState
        {
            get { return doneState; }
            set { doneState = value; }
        }

        protected int id;
        public int ID
        {
            get { return id; }
            //set { id = value; }
        }

        public static Task GenerateNewTask(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            DateTimeSpecificity isSpecific
            )
        {
            if (startTime == null && endTime == null)
                return new TaskFloating(taskName);
            else if (startTime == null && endTime != null)
                return new TaskDeadline(taskName, (DateTime)endTime, isSpecific);
            else if (startTime != null && endTime == null)
            {
                // If endTime is not specified set endTime based on startTime.
                endTime = startTime;
                if (!isSpecific.StartTime)
                {
                    endTime = ((DateTime)endTime).AddDays(1);
                    endTime = ((DateTime)endTime).AddMinutes(-1);
                }
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)startTime, isSpecific);
            }
            else
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)endTime, isSpecific);
        }

        public Task(string taskName, Boolean state, int forceID)
        {
            this.taskName = taskName;
            this.doneState = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else id = forceID;
        }

        public abstract XElement ToXElement();

        public abstract bool IsWithinTime(DateTimeSpecificity isSpecific, DateTime? start, DateTime? end);

        public abstract void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific);

        public virtual DayOfWeek GetDay()
        {
            throw new TaskHasNoDayException();
        }

        public virtual string GetTimeString()
        {
            return String.Empty;
        }

        public virtual bool Postpone(TimeSpan postponeDuration)
        {
            return false;
        }        
        
        // Need to handle exceptions (null?)
        public static int CompareByDateTime(Task a, Task b)
        {
            // A [DONE] task always sorts after an undone task.
            if (a.DoneState == true && b.DoneState == false)
                return 1;
            else if (b.DoneState == true && a.DoneState == false)
                return -1;

            // If they have the same state, continue sort by DateTime.
            if (a is TaskFloating)
            {
                if (b is TaskFloating)
                    return a.TaskName.CompareTo(b.TaskName);
                else return 1;
            }
            else if (b is TaskFloating)
                return -1;

            DateTime aDT, bDT;
            if (a is TaskEvent)
                aDT = ((TaskEvent)a).StartDateTime;
            else
                aDT = ((TaskDeadline)a).EndDateTime;

            if (b is TaskEvent)
                bDT = ((TaskEvent)b).StartDateTime;
            else
                bDT = ((TaskDeadline)b).EndDateTime;

            return DateTime.Compare(aDT, bDT);
        }

        public static int CompareByName(Task x, Task y)
        {
            int compare = x.TaskName.CompareTo(y.TaskName);
            if (compare == 0)
                return CompareByDateTime(x, y);
            else
                return compare;
        }
        
        public override int GetHashCode()
        {
            int newHashCode = Math.Abs(base.GetHashCode() ^ (int)DateTime.Now.ToBinary());
            return newHashCode;
        }
    }
}
