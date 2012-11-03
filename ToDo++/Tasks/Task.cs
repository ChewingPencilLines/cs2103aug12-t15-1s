using System;
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

        public Task(string taskName, Boolean state, int forceID)
        {
            this.taskName = taskName;
            this.doneState = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else id = forceID;
        }

        public abstract XElement ToXElement();

        public abstract bool IsWithinTime(DateTime? start, DateTime? end);

        public virtual DayOfWeek GetDay()
        {
            throw new TaskHasNoDayException();
        }

        public override int GetHashCode()
        {
            int newHashCode = Math.Abs(base.GetHashCode() ^ (int)DateTime.Now.ToBinary());
            return newHashCode;
        }

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
                    return 0;
                else return -1;
            }
            else if (b is TaskFloating)
                return 1;

            DateTime aDT, bDT;
            if (a is TaskEvent)
                aDT = ((TaskEvent)a).StartTime;
            else
                aDT = ((TaskDeadline)a).EndTime;

            if (b is TaskEvent)
                bDT = ((TaskEvent)b).StartTime;
            else
                bDT = ((TaskDeadline)b).EndTime;

            return DateTime.Compare(aDT, bDT);
        }

        public virtual string GetTimeString()
        {
            return String.Empty;
        }
    }
}
