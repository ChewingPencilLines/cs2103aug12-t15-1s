using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    public class TaskDeadline : Task
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

        public override bool IsWithinTime(DateTimeSpecificity compareIsSpecific, DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            DateTime startCompare, endCompare;

            // Start search
            if (start != null)
            {
                startCompare = (DateTime)start;

                // If comparision is not specific to Day/Month, extend search range
                if ((!isSpecific.StartDate.Day && !isSpecific.StartTime ||
                    (!compareIsSpecific.StartDate.Day && compareIsSpecific.StartTime)))
                {
                    if (!isSpecific.StartDate.Month)
                        startCompare = new DateTime(startCompare.Year, 1, 1);
                    else
                        startCompare = new DateTime(startCompare.Year, startCompare.Month, 1);
                }

                if (endDateTime < startCompare)
                    isWithinTime = false;
            }
            if (end != null)
            {
                endCompare = (DateTime)end;

                // Extend compare range if task dates are not specific                
                if ((!isSpecific.EndDate.Day && !isSpecific.EndTime ||
                    (!compareIsSpecific.EndDate.Day && compareIsSpecific.EndTime)))
                {
                    if (!isSpecific.EndDate.Month)
                        endCompare = new DateTime(endCompare.Year + 1, 1, 1);
                    else
                    {
                        endCompare = endCompare.AddMonths(1);
                        endCompare = new DateTime(endCompare.Year, endCompare.Month, 1);
                    }
                    endCompare = endCompare.AddMinutes(-1);
                }
                else if (!isSpecific.EndTime || !compareIsSpecific.EndTime)
                {
                    endCompare = new DateTime(endCompare.Year, endCompare.Month, endCompare.Day, 23, 59, 0);
                }

                if (endDateTime > endCompare)
                    isWithinTime = false;
            }
            return isWithinTime;
        }


        public override string GetTimeString()
        {
            string timeString = "BY ";
            if (isSpecific.EndDate.Day) timeString += endDateTime.ToString("d ");
            timeString += endDateTime.ToString("MMM");
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
