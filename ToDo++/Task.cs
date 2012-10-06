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

        abstract public void ExecuteTask();
    }

    public class FloatingTask : Task
    {
        public FloatingTask(string TaskName)
        {
            taskname = TaskName;
        }

        public override void ExecuteTask()
        {

        }
    }

    public class DeadlineTask : Task
    {
        private DateTime endtime;

        public DeadlineTask(string TaskName, DateTime EndTime)
        {
            taskname = TaskName;
            endtime = EndTime;
        }

        public override void ExecuteTask()
        {

        }
    }

    public class TimedTask : Task
    {
        private DateTime endtime;
        private DateTime starttime;

        public TimedTask(string TaskName,DateTime StartTime, DateTime EndTime)
        {
            taskname = TaskName;
            starttime = StartTime;
            endtime = EndTime;
        }

        public override void ExecuteTask()
        {

        }
    }
}
