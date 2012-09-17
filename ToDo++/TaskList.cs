using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class TaskList:List<Task>
    {
        internal Task Task
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    class TaskListDelegate
    {
        public TaskList TaskList
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
