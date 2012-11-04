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

        public override bool IsWithinTime(DateTimeSpecificity compareIsSpecific, DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            DateTime startCompare, endCompare;

            // Start search
            if (start != null)
            {
                startCompare = (DateTime)start;

                // If comparision is not specific to Day/Month, extend search range
                if (!isSpecific.StartDate.Day)
                {
                    if (!isSpecific.StartDate.Month)
                        startCompare = new DateTime(startCompare.Year, 1, 1);
                    else
                        startCompare = new DateTime(startCompare.Year, startCompare.Month, 1);
                }
                if (!compareIsSpecific.StartDate.Day)
                {
                    if (!compareIsSpecific.StartDate.Month)
                        startCompare = new DateTime(startCompare.Year, 1, 1);
                    else
                        startCompare = new DateTime(startCompare.Year, startCompare.Month, 1);
                }

                if (endTime > startCompare && end != null)
                    isWithinTime = false;
            }
            if (end != null)
            {
                endCompare = (DateTime)end;

                // Extend compare range if task dates are not specific
                if (!isSpecific.EndDate.Day)
                {
                    if (!isSpecific.EndDate.Month)
                        endCompare = new DateTime(endCompare.Year + 1, 1, 1);
                    else
                        endCompare = new DateTime(endCompare.Year, endCompare.Month + 1, 1);
                    endCompare = endCompare.AddMinutes(-1);
                }
                if (!compareIsSpecific.EndDate.Day)
                {
                    if (!compareIsSpecific.EndDate.Month)
                        endCompare = new DateTime(endCompare.Year + 1, 1, 1);
                    else
                        endCompare = new DateTime(endCompare.Year, endCompare.Month + 1, 1);
                    endCompare = endCompare.AddMinutes(-1);
                }

                if (endTime > endCompare)
                    isWithinTime = false;
            }
            return isWithinTime;
        }


        public override string GetTimeString()
        {
            string timeString = "By ";
            if (isSpecific.EndDate.Day) timeString += endTime.ToString("d ");
            timeString += endTime.ToString("MMM");
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
