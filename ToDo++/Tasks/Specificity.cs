using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Specificity
    {
        private bool day;
        private bool month;
        private bool year;
        public Specificity()
        {
            day = month = year = true;
        }
        public bool Day { get { return day; } set { day = value; } }
        public bool Month { get { return month; } set { month = value; } }
        public bool Year { get { return year; } set { year = value; } }
    }

    public class DateTimeSpecificity
    {
        private Specificity startDate;
        private Specificity endDate;
        private bool startTime;
        private bool endTime;
        public DateTimeSpecificity()
        {
            startTime = endTime = true;
            startDate = new Specificity();
            endDate = new Specificity();
        }
        public DateTimeSpecificity(bool startTime, bool endTime, Specificity startDate, Specificity endDate)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public Specificity StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public Specificity EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public bool StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public bool EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
    }
}
