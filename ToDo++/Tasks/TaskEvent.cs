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

                if (startTime < startCompare)
                    isWithinTime = false;
            }
            if (end != null)
            {
                endCompare = (DateTime)end;

                // Extend compare range if task dates are not specific
                if (!isSpecific.EndDate.Day)
                {                 
                    if (!isSpecific.EndDate.Month)
                        endCompare = new DateTime(endCompare.Year+1, 1, 1);
                    else
                        endCompare = new DateTime(endCompare.Year, endCompare.Month+1, 1);
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
            string timeString = "";
            
            if (isSpecific.StartDate.Day) timeString += startTime.ToString("d ");
            timeString += startTime.ToString("MMM");
            if (startTime.Year != DateTime.Now.Year) timeString += " " + startTime.Year;
            if (isSpecific.StartTime) timeString += ", " + startTime.ToShortTimeString();

            if (startTime != EndTime)
            {
                timeString += " -- ";
                if (StartTime.Date != EndTime.Date)
                {
                    if (isSpecific.EndDate.Day) timeString += endTime.ToString("d ");
                    timeString += endTime.ToString("MMM");
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
