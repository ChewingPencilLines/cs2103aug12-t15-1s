using System;
using System.Collections.Generic;

namespace ToDo
{
    public class TaskComparer : IComparer<Task>
    {
        public int Compare(Task taskx, Task tasky)
        {
            if (taskx == null)
            {
                if (tasky == null)
                { 
                    return 0;
                }
                else
                { 
                    return -1;
                }
            }
            else
            { 
                if (tasky == null) 
                {
                    return 1;
                }
                else
                { 
                    int retval = taskx.TaskName.CompareTo(tasky.TaskName);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    { 
                        return taskx.ID.CompareTo(tasky.ID);
                    }
                }
            }
        }
    }
}
