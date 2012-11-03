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
            if (undoStack.Count == 0 || undoTask.Count == 0)
                // return RESPONSE_UNDO_FAILURE;
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), lastListedTasks);
            Operation undoOp = Operation.undoStack.Pop();
            return undoOp.Undo(taskList, storageIO);
        }
  /*
        public override string Undo(List<Task> taskList, Storage storageIO)
        {
            return null;
            //throw new NotImplementedException();
            // @ivan: all undoable Operations should have a undo method so we can just call operation.undo
            //Operation opToUndo = undoStack.Pop();
            //opToUndo.Undo();
  
            string response;
            bool successFlag;
            if (undoOperation is OperationAdd)
            {
                Task task = ((OperationAdd)undoOperation).NewTask;
                response = Delete(task, taskList, out successFlag);
            }
            else if (undoOperation is OperationDelete && (((OperationDelete)undoOperation).Index.HasValue == true))
            {

                Task task = undoTask.Pop();
                response = Add(task, taskList, out successFlag);
            }
            else if (undoOperation is OperationModify && ((OperationModify)undoOperation).NewTask != null)
            {
                Task taskToModify = ((OperationModify)undoOperation).NewTask;
                Task newTask = undoTask.Pop();
                response = Modify(ref taskToModify, newTask, ref taskList, out successFlag);
            }
            else
            {
                response = RESPONSE_UNDO_FAILURE;
            }
            return response;
           
        }
    */
    }
}
    