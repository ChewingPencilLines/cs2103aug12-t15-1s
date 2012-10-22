using System;
using System.Collections.Generic;

namespace ToDo
{
    public class TaskComparer : IComparer<Task>
    {
        public int Compare(Task x, Task y)
        {
            if (x == null)
            {
                if (y == null)
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
                if (y == null) 
                {
                    return 1;
                }
                else
                { 
                    int retval = x.TaskName.CompareTo(y.TaskName);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    { 
                        return x.ID.CompareTo(y.ID);
                    }
                }
            }
        }
    }
}
