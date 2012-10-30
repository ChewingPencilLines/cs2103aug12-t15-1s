using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    class TaskEvent : Task
    {
        public DateTimeSpecificity isSpecific;

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

        public TaskEvent(
            string taskName,
            DateTime startTime,
            DateTime endTime,
            DateTimeSpecificity isSpecific,
            Boolean isDone = false,
            int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Event"),
                            new XElement("Name", taskName),
                            new XElement("StartTime", startTime.ToString()),
                            new XElement("EndTime", endTime.ToString()),
                            isSpecific.ToXElement<DateTimeSpecificity>(),
                            new XElement("State", doneState.ToString())
                            );
            return task;
        }

        public override bool IsWithinTime(DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            if (start != null)
            {
                if (end == null)
                {
                    if (startTime.Date > ((DateTime)start).Date) isWithinTime = false;
                    if (endTime.Date < ((DateTime)start).Date) isWithinTime = false;
                }
                if (startTime < start) isWithinTime = false;
            }
            if (end != null)
            {
                if (endTime > end) isWithinTime = false;
            }
            return isWithinTime;
        }
    }
}
