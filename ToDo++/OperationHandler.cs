﻿using System;
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

        protected const string Add_Suceess_Message= "Added task successfully.";
        protected const string Delete_Success_Message ="Deleted task successfully.";
        protected const string Modify_Success_Message = "Modified task successfully.";
        protected const string Undo_Success_Message = "Undone task successfully.";
                  
        protected const string Wrong_Message = "Command failed";
        
        public OperationHandler()
        {
            taskList = new TaskList();
            pastTask = new Stack<Task>();
            undoStack = new Stack<Operation>();
       //     redoStack = new Stack<Operation>();
            xml = new Storage();
        }

        //Need to take in an instance of Operation to execute
        public virtual string ExecuteOperation(Operation operation)
        {
            return Wrong_Message;
        }

        public string Execute(Operation operation)
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
            else if (operation is OperationSearch)
            {
                undoStack.Pop();
                ExecuteSearch execute = new ExecuteSearch();
                return execute.ExecuteOperation(operation);
            }
            else
                return Wrong_Message;
        }
    }
}
