using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public abstract class Task
    {
        protected string taskname;

        public Task()
        {
            
        }
    }

    public class TaskFloating : Task
    {
        public TaskFloating() : this(null)
        { }

        public TaskFloating(string TaskName)
        {
            taskname = TaskName;
        }
    }

    public class TaskDeadline : Task
    {
        private DateTime endtime;

        public TaskDeadline() : this(null, DateTime.Now)
        { }

        public TaskDeadline(string TaskName, DateTime EndTime)
        {
            taskname = TaskName;
            endtime = EndTime;
        }

    }

    public class TaskTimed : Task
    {
        private DateTime endtime;
        private DateTime starttime;

        public TaskTimed() : this(null, DateTime.Now, DateTime.Now)
        { }

        public TaskTimed(string TaskName, DateTime StartTime, DateTime EndTime)
        {
            taskname = TaskName;
            starttime = StartTime;
            endtime = EndTime;
        }

    }
}
