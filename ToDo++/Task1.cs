using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    abstract class Task1
    {
        protected string taskname;

        public Task1()
        {
            
        }

        abstract public void ExecuteTask();
    }

    public class FloatingTask1 : Task1
    {
        public FloatingTask1(string TaskName)
        {
            taskname = TaskName;
        }

        public override void ExecuteTask()
        {

        }
    }

    public class DeadlineTask1 : Task1
    {
        private DateTime endtime;

        public DeadlineTask1(string TaskName, DateTime EndTime)
        {
            taskname = TaskName;
            endtime = EndTime;
        }

        public override void ExecuteTask()
        {

        }
    }

    public class TimedTask1 : Task1
    {
        private DateTime endtime;
        private DateTime starttime;

        public TimedTask1(string TaskName,DateTime StartTime, DateTime EndTime)
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
