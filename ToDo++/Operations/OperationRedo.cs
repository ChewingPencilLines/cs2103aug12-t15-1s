using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationRedo : Operation
    {
        public OperationRedo(SortType sortType)
            : base(sortType)
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Operation redoOp = GetLastRevertedOperation();
            if (redoOp == null)
                return new Response(Result.FAILURE, sortType, this.GetType());
            
            Response result = redoOp.Redo(taskList, storageIO);
            if (result == null)
                return result;

            if (result.IsSuccessful())
            {
                undoStack.Push(redoOp);
                result = new Response(Result.SUCCESS, sortType, typeof(OperationRedo), currentListedTasks);
            }
            else
                result = new Response(Result.FAILURE, sortType, typeof(OperationRedo), currentListedTasks);

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
