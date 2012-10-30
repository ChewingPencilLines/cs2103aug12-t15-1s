using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    class TaskDeadline : Task
    {
        public DateTimeSpecificity isSpecific;

        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            //set { endTime = value; }
        }
        public TaskDeadline(
            string taskName,
            DateTime endTime,
            DateTimeSpecificity endDateSpecificity,
            Boolean isDone = false,
            int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            this.endTime = endTime;
            isSpecific = endDateSpecificity;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Deadline"),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endTime.ToString()),
                            new XElement("State", doneState.ToString())
                            );
            return task;
        }
    }
}
