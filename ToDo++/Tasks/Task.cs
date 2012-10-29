using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for task
    // ******************************************************************

    public struct DateSpecificity
    {
        private bool day, month, year;
        bool Day { get { return day; } set { day = value; }  }
        bool Month { get { return month; } set { month = value; } }
        bool Year { get { return year; } set { year = value; } }
    }

    public abstract class Task
    {
        protected string taskName;
        public string TaskName
        {
            get { return taskName; }
            //set { taskName = value; }
        }

        protected Boolean doneState;
        public Boolean State
        {
            get { return doneState; }
            set { doneState = value; }
        }

        protected int id;
        public int ID
        {
            get { return id; }
            //set { id = value; }
        }

        public Task(string taskName, Boolean state, int forceID)
        {
            this.taskName = taskName;
            this.doneState = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else id = forceID;
        }

        public abstract XElement ToXElement();

        public override int GetHashCode()
        {
            int newHashCode = Math.Abs(base.GetHashCode() ^ (int)DateTime.Now.ToBinary());
            return newHashCode;
        }
    }
}
