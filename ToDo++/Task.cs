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

    public class FloatingTask : Task
    {
        public FloatingTask() : this(null)
        { }

        public FloatingTask(string TaskName)
        {
            taskname = TaskName;
        }
    }

    public class DeadlineTask : Task
    {
        private DateTime endtime;

        public DeadlineTask() : this(null, DateTime.Now)
        { }

        public DeadlineTask(string TaskName, DateTime EndTime)
        {
            taskname = TaskName;
            endtime = EndTime;
        }

    }

    public class TimedTask : Task
    {
        private DateTime endtime;
        private DateTime starttime;

        public TimedTask() : this(null, DateTime.Now, DateTime.Now)
        { }

        public TimedTask(string TaskName, DateTime StartTime, DateTime EndTime)
        {
            taskname = TaskName;
            starttime = StartTime;
            endtime = EndTime;
        }

    }
}
