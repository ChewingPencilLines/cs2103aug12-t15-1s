using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class DateSpecificity
    {
        private bool day;
        private bool month;
        private bool year;
        public DateSpecificity()
        {
            day = month = year = true;
        }
        public DateSpecificity(bool day, bool month, bool year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
        public bool Day { get { return day; } set { day = value; } }
        public bool Month { get { return month; } set { month = value; } }
        public bool Year { get { return year; } set { year = value; } }
    }

    public class DateTimeSpecificity
    {
        private DateSpecificity startDate;
        private DateSpecificity endDate;
        private bool startTime;
        private bool endTime;
        public DateTimeSpecificity()
        {
            startTime = endTime = true;
            startDate = new DateSpecificity();
            endDate = new DateSpecificity();
        }
        public DateTimeSpecificity(bool startTime, bool endTime, DateSpecificity startDate, DateSpecificity endDate)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public DateSpecificity StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateSpecificity EndDate
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
    };
}
