using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo.Tasks
{
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

        public TaskEvent(string taskName, DateTime startTime, DateTime endTime, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
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
                            new XElement("State", doneState.ToString())
                            );
            return task;
        }
    }
}
