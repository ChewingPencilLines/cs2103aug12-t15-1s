using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace ToDo
{
    public class OperationHandler
    {
        protected TaskList taskList;
        protected Stack<Task> pastTask;
        protected Stack<Operation> undoStack;
       // protected Stack<Operation> redoStack;
        protected Storage xml;
        
        public OperationHandler()
        {
            taskList = new TaskList();
            pastTask = new Stack<Task>();
            undoStack = new Stack<Operation>();
       //     redoStack = new Stack<Operation>();
            xml = new Storage();
        }

        //Need to take in an instance of Operation to execute
        public virtual Result ExecuteOperation(Operation operation)
        {
            return Result.ERROR;
        }

        public Result Execute(Operation operation)
        {
            undoStack.Push(operation);

            if (operation is OperationAdd)
            {
                ExecuteAdd execute = new ExecuteAdd();
                return execute.ExecuteOperation(operation);
            }
            else if (operation is OperationDelete)
            {
                ExecuteDelete execute = new ExecuteDelete();
                return execute.ExecuteOperation(operation);
            }
            else if (operation is OperationModify)
            {
                ExecuteModify execute = new ExecuteModify();
                return execute.ExecuteOperation(operation);
            }
            else if (operation is OperationUndo)
            {
                undoStack.Pop();
                ExecuteUndo execute = new ExecuteUndo();
                return execute.ExecuteOperation(operation);
            }
            else
                return Result.ERROR;
        }
    }
}
