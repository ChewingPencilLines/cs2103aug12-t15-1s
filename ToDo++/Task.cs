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
        protected int id;

        public virtual XElement ToXElement() { return null; }

        internal Task(string taskName, bool state, int forceID)
        {
            this.taskName = taskName;
            this.state = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else forceID = id;
        }

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

        internal int ID
        {
            get { return id; }
            //set { id = value; }
        }
    } 


    // ******************************************************************
    // Definition of derived Tasks
    // ******************************************************************

    #region Definition of derived Tasks
    class TaskFloating : Task
    {
        internal TaskFloating()
            : this(null)
        { }

        internal TaskFloating(string taskName, bool state = false, int forceID = -1) : base (taskName, state, forceID)
        {
            type = TaskType.Floating;
        }
        
        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", type.ToString()),
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

        internal TaskDeadline(string taskName, DateTime endTime, bool state = false, int forceID = -1)
            : base(taskName, state, forceID)
        {       
            this.endTime = endTime;
            type = TaskType.Deadline;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", type.ToString()),
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

        internal TaskEvent(string taskName, DateTime startTime, DateTime endTime, bool state = false, int forceID = -1)
            : base(taskName, state, forceID)
         {
            this.startTime = startTime;
            this.endTime = endTime;
            type = TaskType.Event;
         }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", type.ToString()),
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
