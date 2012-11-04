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
         //   set { endTime = value; }
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

        public override DayOfWeek GetDay()
        {
            return endTime.DayOfWeek;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Deadline"),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endTime.ToString()),
                            new XElement("Done", doneState.ToString())
                            );
            return task;
        }

        public override bool IsWithinTime(DateTimeSpecificity isSpecific, DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            if (start != null)
            {
                if (end == null)
                {
                    if ((endTime.Date != ((DateTime)start) && isSpecific.StartDate.Day)
                        || (!isSpecific.StartDate.Month && endTime.Date.Month != ((DateTime)start).Month)
                        || (!isSpecific.StartDate.Year && endTime.Date.Year != ((DateTime)start).Year))
                    {
                        isWithinTime = false;
                    }
                }
                if (endTime < start) isWithinTime = false;
            }   
            if (end != null)
            {
                if (endTime > end) isWithinTime = false;
            }
            return isWithinTime;
        }

        public override string GetTimeString()
        {
            string timeString = "By ";
            if (isSpecific.EndDate.Day) timeString += endTime.ToString("d MMM");
            if (endTime.Year != DateTime.Now.Year) timeString += " " + endTime.Year;
            if (isSpecific.EndTime) timeString += ", " + endTime.ToShortTimeString();
            return timeString;
        }

        public override Task Postpone(DateTime? NewDate)
        {
            TaskDeadline result;
            if (NewDate == null)
                result = new TaskDeadline(this.taskName, this.endTime.AddDays(1), this.isSpecific, this.doneState);
            else
                result = new TaskDeadline(this.taskName, NewDate.Value, this.isSpecific, this.doneState);
            return result;
        }
    }
}
