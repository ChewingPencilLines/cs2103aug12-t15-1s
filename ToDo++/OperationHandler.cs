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
        const string RESPONSE_ADD_SUCCESS = "Added \"{0}\" successfully.";
        const string RESPONSE_ADD_FAIL = "Failed to add task!";
        const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string RESPONSE_MODIFY_SUCCESS = "Modified task successfully.";
        const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string REPONSE_INVALID_COMMAND = "Invalid command!"; 
        #endregion

        public OperationHandler()
        {
            lastListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            storageXML = new Storage("tasklist.xml", "settings.xml");
        }

        public string Execute(Operation operation, ref List<Task> taskList)
        {
            string response;
            bool successFlag;
            if (operation == null)
            {
                return REPONSE_INVALID_COMMAND;
            }
            else if (operation is OperationAdd)
            {
                Task taskToAdd = ((OperationAdd)operation).GetTask();
                if (taskToAdd == null) return RESPONSE_ADD_FAIL;
                response = Add(taskToAdd, ref taskList, out successFlag);
            }
            else if (operation is OperationDelete)
            {
                int index = ((OperationDelete)operation).Index;
                Debug.Assert(index >= 0 && index < taskList.Count);
                Task taskToDelete = lastListedTasks[index];
                response = Delete(ref taskToDelete, ref taskList, out successFlag);
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
                
        private string Add(Task taskToAdd, ref List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            try
            {
                taskList.Add(taskToAdd);
                if (storageXML.AddTask(taskToAdd, 0))
                {
                    successFlag = true;
                    return String.Format(RESPONSE_ADD_SUCCESS, taskToAdd.taskname);
                }
                else
                    return RESPONSE_XML_READWRITE_FAIL;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return RESPONSE_ADD_FAIL + "\r\nThe following exception occured: " + e.ToString();
            }     
        }

        private string Delete(ref Task taskToDelete, ref List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            taskList.Remove(taskToDelete);
            if (storageXML.RemoveTask(taskToDelete))
            {
                successFlag = true;
                return String.Format(RESPONSE_DELETE_SUCCESS, taskToDelete.taskname);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;            
        }

        private string DisplayAll(List<Task> taskList)
        {
            string displayString = String.Empty;
            foreach (Task task in taskList)
            {
                displayString += (task.taskname);
                if (task is TaskDeadline)
                {
                    displayString += (" BY: " + ((TaskDeadline)task).endtime);
                }
                else if (task is TaskEvent)
                {
                    DateTime startTime = ((TaskEvent)task).starttime;
                    DateTime endTime = ((TaskEvent)task).endtime;
                    displayString += (" AT: " + startTime.ToString());
                    if(startTime != endTime && endTime != null)
                    displayString += (" TO: " + endTime.ToString());
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
