﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for task
    // ******************************************************************

    #region Abstract definition for task    
    
    public abstract class Task
    {
        /*
         * taskname should be public then xml can write this.
         * state is false means the task is undone.
         * @ivan to @alice: NO. make it a property (encapsulate field). Look @ how i do Tokens.
         */
        public string taskname;
        public bool state;
        public Task()
        {

        }
    } 
    #endregion

    // ******************************************************************
    // Definition of three different task types
    // ******************************************************************

    #region Definition of three different task types
    [XmlInclude(typeof(TaskFloating))]
    public class TaskFloating : Task
    {
        public TaskFloating()
            : this(null)
        { }

        public TaskFloating(string TaskName)
        {
            taskname = TaskName;
            state = false;
        }
    }

    [XmlInclude(typeof(TaskDeadline))]
    public class TaskDeadline : Task
    {
        public DateTime endtime;

        public TaskDeadline() : this(null, DateTime.Now)
        { }

        public TaskDeadline(string TaskName, DateTime EndTime)
        {
            taskname = TaskName;
            endtime = EndTime;
            state = false;
        }
    }

    [XmlInclude(typeof(TaskEvent))]
    public class TaskEvent : Task
    {
        public DateTime endtime;
        public DateTime starttime;

        public TaskEvent() : this(null, DateTime.Now, DateTime.Now)
        { }

        public TaskEvent(string TaskName, DateTime StartTime, DateTime EndTime)
        {
            taskname = TaskName;
            starttime = StartTime;
            endtime = EndTime;
            state = false;
        }
    }
     #endregion
}
