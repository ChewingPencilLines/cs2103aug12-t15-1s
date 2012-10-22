using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{   
    public class Task
    {
        /*
         * taskname should be public then xml can write this.
         * state is false means the task is undone.
         * @ivan to @alice: NO. make it a property (encapsulate field). Look @ how i do Tokens.
         */

        public enum TaskType { Floating, Deadline, Event}

        protected string taskName;
        protected bool state;
        protected TaskType type;

        internal Task() { }
        public virtual XElement ToXElement() { return null; }

        internal string TaskName
        {
            get { return taskName; }
        }

        internal bool State
        {
            get { return state; }
            set { state = value; }
        }

        internal TaskType Type
        { 
            get { return type; } 
        }
    } 


    // ******************************************************************
    // Definition of three different task types
    // ******************************************************************

    #region Definition of three different task types
    class TaskFloating : Task
    {
        internal TaskFloating()
            : this(null)
        { }

        internal TaskFloating(string taskName)
        {
            this.taskName = taskName;
            state = false;
            type = TaskType.Floating;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XElement("type",type.ToString()),
                            new XElement("Name", taskName),
                            new XElement("State", state.ToString())
                            );
            return task;
        }
   
    }

    class TaskDeadline : Task
    {
        DateTime endTime;

        internal DateTime EndTime
        {
            get { return endTime; }
        }

        internal TaskDeadline() : this(null, DateTime.Now)
        { }

        internal TaskDeadline(string taskName, DateTime endTime)
        {
            this.taskName = taskName;
            this.endTime = endTime;
            state = false;
            type = TaskType.Deadline;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XElement("type", type.ToString()),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endTime.ToString()),
                            new XElement("State", state.ToString())
                            );
            return task;
        }
    }

    class TaskEvent : Task
    {
        DateTime endTime;
        DateTime startTime;

        internal DateTime StartTime
        {
            get { return startTime; }
        }

        internal DateTime EndTime
        {
            get { return endTime; }
        }

        public TaskEvent() : this(null, DateTime.Now, DateTime.Now)
        { }

        public TaskEvent(string taskName, DateTime startTime, DateTime endTime)
        {
            this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            state = false;
            type = TaskType.Event;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XElement("type", type.ToString()),
                            new XElement("Name", taskName),
                            new XElement("StartTime", startTime.ToString()),
                            new XElement("EndTime", endTime.ToString()),
                            new XElement("State", state.ToString())
                            );
            return task;
        }
    }
     #endregion
}
