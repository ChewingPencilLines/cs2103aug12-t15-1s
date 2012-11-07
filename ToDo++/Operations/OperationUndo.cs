using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationUndo : Operation
    {
        public OperationUndo()
        { }
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            if (undoStack.Count == 0)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            Operation undoOp = Operation.undoStack.Pop();
            Response result = undoOp.Undo(taskList, storageIO);
            if (result.IsSuccessful())
                redoStack.Push(undoOp);
            return result;
        }
    }
}
    