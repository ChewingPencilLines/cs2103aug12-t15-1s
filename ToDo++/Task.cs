using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public enum TaskType
    {
        TimedTask,
        DeadlineTask,
        FloatingTask,
    }

    public abstract class Task
    {
        protected TaskType taskID;
        protected string taskName;
    }

    public class FloatingTask : Task
    {
        public FloatingTask()
        {

        }

        public FloatingTask(string setTaskName)
        {
            taskID = TaskType.FloatingTask;
            taskName = setTaskName;
        }
    }
}
