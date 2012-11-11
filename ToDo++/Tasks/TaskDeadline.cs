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
            Logger.Info("Created a deadline task", "TaskDeadline::TaskDeadline");
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

        public override bool IsWithinTime(DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            DateTime startCompare, endCompare;

            // Start search
            if (start != null)
            {
                startCompare = (DateTime)start;

                // If comparison is not specific to Day/Month, extend search range
                if (!isSpecific.StartDate.Day && !isSpecific.StartTime)
                {
                    if (!isSpecific.StartDate.Month)
                    {
                        startCompare = new DateTime(startCompare.Year, 1, 1);
                    }
                    else
                    {
                        startCompare = new DateTime(startCompare.Year, startCompare.Month, 1);
                    }
                }

                if (endDateTime < startCompare)
                {
                    isWithinTime = false;
                }
            }
            if (end != null)
            {
                endCompare = (DateTime)end;

                // Extend compare range if task dates are not specific                
                if ((!isSpecific.EndDate.Day && !isSpecific.EndTime))
                {
                    if (!isSpecific.EndDate.Month)
                    {
                        endCompare = new DateTime(endCompare.Year + 1, 1, 1);
                    }
                    else
                    {
                        endCompare = endCompare.AddMonths(1);
                        endCompare = new DateTime(endCompare.Year, endCompare.Month, 1);
                    }
                    endCompare = endCompare.AddMinutes(-1);
                }

                if (endDateTime > endCompare)
                {
                    isWithinTime = false;
                }
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

        public override bool Postpone(TimeSpan postponeDuration)
        {
            bool result = true;

            // Return failure if trying to postpone at a higher specificity level then task allows.
            if ((!isSpecific.EndTime && postponeDuration.Hours != 0) ||
                (!isSpecific.EndDate.Day && postponeDuration.Days != 0))
            {
                Logger.Warning("Attempted to postpone a deadline task with no start and end datetimes.", "Postpone::TaskDeadline");
                return false;
            }

            try
            {
                endDateTime = endDateTime.Add(postponeDuration);
                Logger.Info("Attmpted to postpone deadline task.", "Postpone::TaskDeadline");
            }
            catch
            {
                result = false;
                Logger.Warning("Failed to postpone deadline task.", "Postpone::TaskDeadline");
            }
            return result;
        }

        public override void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific)
        {
            startTime = null;
            endTime = this.endDateTime;
            specific = this.isSpecific;
            Logger.Info("Updated datetimes and their specificity.", "CopyDateTimes::TaskDeadline");
        } 
    }
}
