using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    public class TaskEvent : Task
    {
        public DateTimeSpecificity isSpecific;

        private DateTime endDateTime;
        public DateTime EndDateTime
        {
            get { return endDateTime; }
        }

        private DateTime startDateTime;
        public DateTime StartDateTime
        {
            get { return startDateTime; }         
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
            this.startDateTime = startTime;
            this.endDateTime = endTime;
            this.isSpecific = isSpecific;
            Logger.Info("Created an event task", "TaskEvent::TaskEvent");
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Event"),
                            new XElement("Name", taskName),
                            new XElement("StartTime", startDateTime.ToString()),
                            new XElement("EndTime", endDateTime.ToString()),
                            isSpecific.ToXElement<DateTimeSpecificity>(),
                            new XElement("Done", doneState.ToString())
                            );
            return task;
        }

        public override DayOfWeek GetDay()
        {
            return startDateTime.DayOfWeek;
        }

        public override bool IsWithinTime(DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            DateTime startCompare, endCompare;

            // Start search
            if (start != null)
            {
                startCompare = (DateTime)start;

                // If task is not specific to Day/Month, extend search range
                if ((!isSpecific.StartDate.Day && !isSpecific.StartTime))
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
                if (!isSpecific.EndDate.Day && !isSpecific.EndTime)
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
                else if (!isSpecific.EndTime)
                {
                    endCompare = new DateTime(endCompare.Year, endCompare.Month, endCompare.Day, 23, 59, 0);
                }

                if (startDateTime > endCompare)
                {
                    isWithinTime = false;
                }
            }
            return isWithinTime;
        }

        public override string GetTimeString()
        {
            string timeString = "";

            if (isSpecific.StartDate.Day)
            {
                timeString += startDateTime.ToString("d ");
            }
            timeString += startDateTime.ToString("MMM");
            if (startDateTime.Year != DateTime.Now.Year)
            {
                timeString += " " + startDateTime.Year;
            }
            if (isSpecific.StartTime)
            {
                timeString += ", " + startDateTime.ToShortTimeString();
            }

            if (startDateTime != EndDateTime)
            {
                timeString += " -- ";
                if (StartDateTime.Date != EndDateTime.Date)
                {
                    if (isSpecific.EndDate.Day)
                    {
                        timeString += endDateTime.ToString("d ");
                    }
                    timeString += endDateTime.ToString("MMM");
                    if (endDateTime.Year != DateTime.Now.Year)
                    {
                        timeString += " " + endDateTime.Year;
                    }
                    if (isSpecific.EndTime)
                    {
                        timeString += ", ";
                    }
                }
                if (isSpecific.EndTime)
                {
                    timeString += endDateTime.ToShortTimeString();
                }
            }
            return timeString;
        }
        
        public override bool Postpone(TimeSpan postponeDuration)
        {
            bool result = true;
            try
            {
                if (TaskDateTimeIsNotSpecificEnough(ref postponeDuration))
                {
                    return false;
                }
                startDateTime = startDateTime.Add(postponeDuration);
                if (endDateTime != null)
                {
                    endDateTime = endDateTime.Add(postponeDuration);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private bool TaskDateTimeIsNotSpecificEnough(ref TimeSpan postponeDuration)
        {
            if (endDateTime != null)
            {
                if (endDateTime != startDateTime)
                {
                    if ((!isSpecific.EndTime && postponeDuration.Hours != 0) ||
                        (!isSpecific.EndDate.Day && postponeDuration.Days != 0))
                    {
                        return true;
                    }
                }
            }
            if ((!isSpecific.StartTime && postponeDuration.Hours != 0) ||
                (!isSpecific.StartDate.Day && postponeDuration.Days != 0))
            {
                return true;
            }

            return false;
        }

        public override void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific)
        {
            startTime = this.startDateTime;
            endTime = this.endDateTime;
            specific = this.isSpecific;
            Logger.Info("Updated datetimes and their specificity.", "CopyDateTimes::TaskEvent");
        }    
    }
}
