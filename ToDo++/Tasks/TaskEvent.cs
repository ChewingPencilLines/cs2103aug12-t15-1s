using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    class TaskEvent : Task
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

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            //set { startTime = value; }
        }

        public TaskEvent(string taskName, DateTime startTime, DateTime endTime, DateSpecificity startDateSpecificity, DateSpecificity endDateSpecificity, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            isSpecific.startDate = startDateSpecificity;
            isSpecific.endDate = endDateSpecificity;
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
