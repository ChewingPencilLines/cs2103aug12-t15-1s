using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public class Operation
    {
        private OperationType operationType;
        private Task task;
    }

    public enum OperationType
    {
        ADD_FLOATING,
        ADD_EVENT,
        ADD_DEADLINE,
        DISPLAY_ALL,
        DISPLAY_TIME,
        DISPLAY_NAME,
        DISPLAY_TYPE,
        MODIFY_NAME,
        MODIFY_TIME,
        DELETE_TASK,
        DELETE_DATE,
        UNDO,
        REDO,
    }
}
