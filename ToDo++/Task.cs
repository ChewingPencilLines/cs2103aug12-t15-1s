using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for task
    // ******************************************************************

    #region Abstract definition for task
    public abstract class Task
    {
        // taskname should be public then xml can write this.
        public string taskname;
        public Task()
        {

        }
    } 
    #endregion

    // ******************************************************************
    // Definition of three different task types
    // ******************************************************************

    #region Definition of three different task types
    public class TaskFloating : Task
    {
        public TaskFloating()
            : this(null)
        { }

        public TaskFloating(string TaskName)
        {
            taskname = TaskName;
        }
    } 

    public class TaskDeadline : Task
    {
        public DateTime endtime;

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
        public DateTime endtime;
        public DateTime starttime;

        public TaskTimed() : this(null, DateTime.Now, DateTime.Now)
        { }

        public TaskTimed(string TaskName, DateTime StartTime, DateTime EndTime)
        {
            taskname = TaskName;
            starttime = StartTime;
            endtime = EndTime;
        }

    }
     #endregion
}
