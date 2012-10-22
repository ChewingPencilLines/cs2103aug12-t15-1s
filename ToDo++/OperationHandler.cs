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
        const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        #endregion

        public OperationHandler(Storage storageXML)
        {
            lastListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            this.storageXML = storageXML;
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
                Task taskToAdd = ((OperationAdd)operation).NewTask;
                if (taskToAdd == null) return RESPONSE_ADD_FAIL;
                response = Add(taskToAdd, ref taskList, out successFlag);
            }
            else if (operation is OperationDelete)
            {   
                 int index = ((OperationDelete)operation).Index;
                 string deleteString = ((OperationDelete)operation).DeleteString;
                 if (index == -1 && deleteString != null)
                 {                  
                     response = Search(ref lastListedTasks, taskList, deleteString);
                 }
                 else if (index < 0 || index > taskList.Count - 1)
                 {
                     return RESPONSE_INVALID_TASK_INDEX;
                 }
                 else if(deleteString == null)
                 {
                     Task taskToDelete = lastListedTasks[index];
                     response = Delete(ref taskToDelete, ref taskList, out successFlag);
                     lastListedTasks = null;
                 }                 
                 else
                 {
                     return REPONSE_INVALID_COMMAND;
                 }
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
                string searchString = ((OperationSearch)operation).GetSearchString();
                response = Search(ref lastListedTasks, taskList, searchString);
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
                if (storageXML.AddTaskToFile(taskToAdd))
                {
                    successFlag = true;
                    return String.Format(RESPONSE_ADD_SUCCESS, taskToAdd.TaskName);
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
            if (storageXML.RemoveTaskFromFile(taskToDelete))
            {
                successFlag = true;
                return String.Format(RESPONSE_DELETE_SUCCESS, taskToDelete.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;            
        }

        private string DisplayAll(List<Task> taskList)
        {
            string displayString = String.Empty;
            int index = 1;
            foreach (Task task in taskList)
            {                
                displayString += ((index) + ". " + task.TaskName);
                if (task is TaskDeadline)
                {
                    displayString += (" BY: " + ((TaskDeadline)task).EndTime);
                }
                else if (task is TaskEvent)
                {
                    DateTime startTime = ((TaskEvent)task).StartTime;
                    DateTime endTime = ((TaskEvent)task).EndTime;
                    displayString += (" AT: " + startTime.ToString());
                    if(startTime != endTime && endTime != null)
                    displayString += (" TO: " + endTime.ToString());
                }
                displayString += "\r\n";
                index++;                
            }
            lastListedTasks = new List<Task>(taskList);
            return displayString;
        }

        private string Search(ref List<Task> lastListedTasks, List<Task> taskList, string searchString)
        {
            string displayString = String.Empty;
            int index = 1;
            foreach (Task task in taskList)
            {
                if (task.TaskName.IndexOf(searchString) >= 0)
                {
                    lastListedTasks.Add(task);
                    displayString += ((index) + ". " + task.TaskName);
                    if (task is TaskDeadline)
                    {
                        displayString += (" BY: " + ((TaskDeadline)task).EndTime);
                    }
                    else if (task is TaskEvent)
                    {
                        DateTime startTime = ((TaskEvent)task).StartTime;
                        DateTime endTime = ((TaskEvent)task).EndTime;
                        displayString += (" AT: " + startTime.ToString());
                        if (startTime != endTime && endTime != null)
                            displayString += (" TO: " + endTime.ToString());
                    }
                    displayString += "\r\n";
                    index++;
                }
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
