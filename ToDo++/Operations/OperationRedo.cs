using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationRedo : Operation
    {
        public OperationRedo()
        { }

        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            if (redoStack.Count == 0 || redoTask.Count == 0)
                return RESPONSE_REDO_FAILURE;
            Operation redoOp = Operation.redoStack.Pop();
            return redoOp.Redo(taskList, storageIO);
        }
    }
}
