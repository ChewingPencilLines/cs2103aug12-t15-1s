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

    #region Abstract definition for task    
    
    public abstract class Task
    {
        protected string taskName;
        public string TaskName
        {
            get { return taskName; }
            //set { taskName = value; }
        }

        protected bool state;
        public bool State
        {
            get { return state; }
            set { state = value; }
        }

        protected int id;
        public int ID
        {
            get { return id; }
            //set { id = value; }
        }

        public Task(string taskName, bool state, int forceID)
        {
            this.taskName = taskName;
            this.state = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else id = forceID;
        }

        public abstract XElement ToXElement();

        public override int GetHashCode()
        {
            int newHashCode = Math.Abs(base.GetHashCode() ^ (int)DateTime.Now.ToBinary());
            return newHashCode;
        }
    } 
    #endregion

    // ******************************************************************
    // Definition of derived Tasks
    // ******************************************************************

    #region Definition of derived tasks
    public class TaskFloating : Task
    {
        public TaskFloating(string taskName, bool state = false, int forceID = -1) : base (taskName, state, forceID)
        {            
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Floating"),
                            new XElement("Name", taskName),
                            new XElement("State", state.ToString())
                            );
            return task;
        }
    }

    public class TaskDeadline : Task
    {
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            //set { endTime = value; }
        }

        public TaskDeadline(string taskName, DateTime endTime, bool state = false, int forceID = -1)
            : base(taskName, state, forceID)
        {
            this.endTime = endTime;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Deadline"),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endTime.ToString()),
                            new XElement("State", state.ToString())
                            );
            return task;
        }
    }

    public class TaskEvent : Task
    {
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            //set { endTime = value; }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            //set { startTime = value; }
        }
        
        public TaskEvent(string taskName, DateTime startTime, DateTime endTime, bool state = false, int forceID = -1)
            : base(taskName, state, forceID)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Event"),
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
