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

            Operation redoOp = GetLastRevertedOperation();
            if (redoOp == null)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            
            Response result = redoOp.Redo(taskList, storageIO);
            if (result.IsSuccessful())
            {
                undoStack.Push(redoOp);
                result = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
            }
            else
                result = new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);

            return result;
        }

        private Operation GetLastRevertedOperation()
        {
            if (redoStack.Count == 0)
                return null;
            try
            {
                Operation lastRevertedOperation = Operation.redoStack.Pop();
                return lastRevertedOperation;
            }
            catch
            {
                return null;
            }
        }
    }
}
