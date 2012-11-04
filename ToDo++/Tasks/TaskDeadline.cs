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

        private DateTime endDateTime;
        public DateTime EndDateTime
        {
            get { return endDateTime; }
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
            this.endDateTime = endTime;
            isSpecific = endDateSpecificity;
        }

        public override DayOfWeek GetDay()
        {
            return endDateTime.DayOfWeek;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Deadline"),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endDateTime.ToString()),
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
                    if ((endDateTime.Date != ((DateTime)start) && isSpecific.StartDate.Day)
                        || (!isSpecific.StartDate.Month && endDateTime.Date.Month != ((DateTime)start).Month)
                        || (!isSpecific.StartDate.Year && endDateTime.Date.Year != ((DateTime)start).Year))
                    {
                        isWithinTime = false;
                    }
                }
                if (endDateTime < start) isWithinTime = false;
            }   
            if (end != null)
            {
                if (endDateTime > end) isWithinTime = false;
            }
            return isWithinTime;
        }

        public override string GetTimeString()
        {
            string timeString = "By ";
            if (isSpecific.EndDate.Day) timeString += endDateTime.ToString("d MMM");
            if (endDateTime.Year != DateTime.Now.Year) timeString += " " + endDateTime.Year;
            if (isSpecific.EndTime) timeString += ", " + endDateTime.ToShortTimeString();
            return timeString;
        }

        public override Task Postpone(DateTime? NewDate)
        {
            TaskDeadline result;
            if (NewDate == null)
                result = new TaskDeadline(this.taskName, this.endDateTime.AddDays(1), this.isSpecific, this.doneState);
            else
                result = new TaskDeadline(this.taskName, NewDate.Value, this.isSpecific, this.doneState);
            return result;
        }
    }
}
