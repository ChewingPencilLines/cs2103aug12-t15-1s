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
          //  set { endTime = value; }
            get { return endTime; }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
          //  set { startTime = value; }
            get { return startTime; }         
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
                            new XElement("Done", doneState.ToString())
                            );
            return task;
        }

        public override DayOfWeek GetDay()
        {
            return startTime.DayOfWeek;
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

        public override string GetTimeString()
        {
            string timeString = "";
            
            if (isSpecific.StartDate.Day) timeString += startTime.ToString("d MMM");
            if (startTime.Year != DateTime.Now.Year) timeString += " " + startTime.Year;
            if (isSpecific.StartTime) timeString += ", " + startTime.ToShortTimeString();

            if (startTime != EndTime)
            {
                timeString += " -- ";
                if (StartTime.Date != EndTime.Date)
                {
                    if (isSpecific.EndDate.Day) timeString += endTime.ToString("d MMM");
                    if (endTime.Year != DateTime.Now.Year) timeString += " " + endTime.Year;
                    if (isSpecific.EndTime) timeString += ", ";
                }
                if (isSpecific.EndTime) timeString +=  endTime.ToShortTimeString();
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
                result= new TaskEvent(this.taskName, this.startTime.AddDays(1),this.endTime.AddDays(1), this.isSpecific, this.doneState);           
            else
            {
                DateTime NewEnd = this.endTime + (NewDate.Value - this.startTime);
                result = new TaskEvent(this.taskName, NewDate.Value, NewEnd, this.isSpecific, this.doneState);  
            }
            return result;
        }
    
    }
}
