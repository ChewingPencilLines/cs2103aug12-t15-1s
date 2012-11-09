using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationUndo : Operation
    {
        public OperationUndo(SortType sortType)
            : base(sortType)
        { }
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Operation undoOp = GetLastOperation();
            if (undoOp == null)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            Response result = undoOp.Undo(taskList, storageIO);
            if (result == null)
                return result;

            if (result.IsSuccessful())
            {
                redoStack.Push(undoOp);
                result = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            }
            else
                result = new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);

            return result;
        }

        private Operation GetLastOperation()
        {            
            if (undoStack.Count == 0)
                return null;
            try
            {
                Operation lastOperation = Operation.undoStack.Pop();
                return lastOperation;
            }
            catch
            {
                return null;
            }            
        }
    }
}
    