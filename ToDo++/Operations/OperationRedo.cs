using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationRedo : Operation
    {
        public OperationRedo()
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            if (redoStack.Count == 0)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            Operation redoOp = Operation.redoStack.Pop();
            Response result = redoOp.Redo(taskList, storageIO);
            if (result.IsSuccessful())
                undoStack.Push(redoOp);
            return result;
        }
    }
}
