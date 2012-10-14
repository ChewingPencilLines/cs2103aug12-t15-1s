using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace ToDo
{
    public class OperationHandler
    {
        List<Task> lastListedTasks;
        Stack<Operation> undoStack;
        Stack<Operation> redoStack;        
        Storage storageXML;

        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string RESPONSE_SUCCESS_ADD = "Added {0} successfully.";
        const string RESPONSE_FAIL_ADD = "Failed to add task!";
        const string RESPONSE_SUCCESS_DELETE = "Deleted task successfully.";
        const string RESPONSE_SUCCESS_MODIFY = "Modified task successfully.";
        const string RESPONSE_SUCCESS_UNDO = "Removed task successfully.";
        const string REPONSE_INVALID_COMMAND = "Invalid command!"; 
        #endregion

        public OperationHandler()
        {
            lastListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            storageXML = new Storage();
        }

        public string Execute(Operation operation, ref List<Task> taskList)
        {
            string response;
            if (operation == null)
            {
                return REPONSE_INVALID_COMMAND;
            }
            else if (operation is OperationAdd)
            {
                response = Add(operation, ref taskList);
            }
            else if (operation is OperationDelete)
            {
                int index = ((OperationDelete)operation).Index;
                Debug.Assert(index >= 0 && index < taskList.Count);
                Task taskToDelete = lastListedTasks[index];
                response = Delete(ref taskToDelete, ref taskList);
            }
            else if (operation is OperationDisplay)
            {
                response = DisplayAll(taskList);
            }
            else if (operation is OperationModify)
            {
                throw new NotImplementedException();
            }
            else if (operation is OperationUndo)
            {
                throw new NotImplementedException();
            }
            else if (operation is OperationSearch)
            {
                throw new NotImplementedException();
            }
            else
            {
                return REPONSE_INVALID_COMMAND;
            }
            return response;
        }
                
        private string Add(Operation operation, ref List<Task> taskList)
        {
            try
            {
                Task taskToAdd = ((OperationAdd)operation).GetTask();
                taskList.Add(taskToAdd);
                if (storageXML.AddTask(taskToAdd))
                {
                    TrackOperation(operation);
                    return String.Format(RESPONSE_SUCCESS_ADD, taskToAdd.taskname);
                }
                else return RESPONSE_FAIL_ADD;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return RESPONSE_FAIL_ADD + "\r\nThe following exception occured: " + e.ToString();
            }     
        }

        private string Delete(ref Task taskToDelete, ref List<Task> taskList)
        {
            throw new NotImplementedException();
        }

        private string DisplayAll(List<Task> taskList)
        {
            string displayString = String.Empty;
            foreach (Task task in taskList)
            {
                displayString += (task.taskname);
                if (task is TaskDeadline)
                {
                    displayString += (" by: " + ((TaskDeadline)task).endtime);
                }
                else if (task is TaskEvent)
                {
                    DateTime startTime = ((TaskEvent)task).starttime;
                    DateTime endTime = ((TaskEvent)task).endtime;
                    displayString += (" at: " + startTime.ToString());
                    if(startTime != endTime && endTime != null)
                    displayString += (" to: " + endTime.ToString());
                }
                displayString += "\r\n";
            }
            return displayString;
        }

        private void TrackOperation(Operation operation)
        {
            undoStack.Push(operation);
            redoStack.Clear();
        }
    }
}
