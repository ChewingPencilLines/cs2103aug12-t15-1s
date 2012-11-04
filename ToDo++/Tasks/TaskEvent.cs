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

        private DateTime endDateTime;
        public DateTime EndDateTime
        {
          //  set { endTime = value; }
            get { return endDateTime; }
        }

        private DateTime startDateTime;
        public DateTime StartDateTime
        {
          //  set { startTime = value; }
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

        public override bool IsWithinTime(DateTimeSpecificity isSpecific, DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            if (start != null)
            {
                if (end == null)
                {
                    if (startDateTime.Date > ((DateTime)start).Date
                        && (isSpecific.StartDate.Day
                            || (!isSpecific.StartDate.Month && endDateTime.Date.Month != ((DateTime)start).Month)
                            || (!isSpecific.StartDate.Year && endDateTime.Date.Year != ((DateTime)start).Year)))
                    {
                        isWithinTime = false;
                    }
                    if (endDateTime.Date < ((DateTime)start).Date)
                    {
                        isWithinTime = false;
                    }
                }
                if (startDateTime < start) isWithinTime = false;
            }
            if (end != null)
            {
                if (endDateTime > end) isWithinTime = false;
            }
            return isWithinTime;
        }

        public override string GetTimeString()
        {
            string timeString = "";
            
            if (isSpecific.StartDate.Day) timeString += startDateTime.ToString("d MMM");
            if (startDateTime.Year != DateTime.Now.Year) timeString += " " + startDateTime.Year;
            if (isSpecific.StartTime) timeString += ", " + startDateTime.ToShortTimeString();

            if (startDateTime != EndDateTime)
            {
                timeString += " -- ";
                if (StartDateTime.Date != EndDateTime.Date)
                {
                    if (isSpecific.EndDate.Day) timeString += endDateTime.ToString("d MMM");
                    if (endDateTime.Year != DateTime.Now.Year) timeString += " " + endDateTime.Year;
                    if (isSpecific.EndTime) timeString += ", ";
                }
                if (isSpecific.EndTime) timeString +=  endDateTime.ToShortTimeString();
            }
            return timeString;
        }

        public string test()
        {
            return String.Empty;
        }

        public override Task Postpone(DateTime? NewDate)
        {
            TaskEvent result;
            if (NewDate == null)
                result= new TaskEvent(this.taskName, this.startDateTime.AddDays(1),this.endDateTime.AddDays(1), this.isSpecific, this.doneState);           
            else
            {
                DateTime NewEnd = this.endDateTime + (NewDate.Value - this.startDateTime);
                result = new TaskEvent(this.taskName, NewDate.Value, NewEnd, this.isSpecific, this.doneState);  
            }
            return result;
        }
    
    }
}
