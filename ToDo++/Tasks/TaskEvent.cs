using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo.Tasks
{
    public class TaskDeadline : Task
    {
        private struct IsSpecific
        {
            public DateSpecificity startDate;
            public DateSpecificity endDate;
            DateSpecificity StartDate
            {
                get { return startDate; }
                set { startDate = value; }
            }
            DateSpecificity EndDate
            {
                get { return endDate; }
                set { endDate = value; }
            }
        }

        private IsSpecific isSpecific = new IsSpecific();
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            //set { endTime = value; }
        }
        public TaskDeadline(string taskName, DateTime endTime, DateSpecificity startDateSpecificity, DateSpecificity endDateSpecificity, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            this.endTime = endTime;
            isSpecific.startDate = startDateSpecificity;
            isSpecific.endDate = endDateSpecificity;
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
