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
        Stack<Task> undoTask;
       // Stack<Operation> redoStack;        
        Storage storageXML;

        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string RESPONSE_ADD_SUCCESS = "Added \"{0}\" successfully.";
        const string RESPONSE_ADD_FAIL = "Failed to add task!";
        const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string RESPONSE_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string REPONSE_INVALID_COMMAND = "Invalid command!";
        const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        #endregion

        public OperationHandler(Storage storageXML)
        {
            lastListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
            this.storageXML = storageXML;
        } 

        public string Execute(Operation operation, ref List<Task> taskList)
        {
            string response; 
            bool successFlag;

            TrackOperation(operation);

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
                 int? index = ((OperationDelete)operation).Index;
                 string deleteString = ((OperationDelete)operation).DeleteString;
                 if (index.HasValue == false && deleteString != null)
                 {                  
                     response = Search(taskList, deleteString);
                 }
                 else if (index < 0 || index > taskList.Count - 1)
                 {
                     return RESPONSE_INVALID_TASK_INDEX;
                 }
                 else if (deleteString == null)
                 {
                     Task taskToDelete = lastListedTasks[index.Value];
                     response = Delete(ref taskToDelete, ref taskList, out successFlag);
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
                /*
                 *  when modify, if user key in nothing or only index or only task details
                 *  after the commandtype, then all tasks will be shown.
                 *  only when user input full information will modify operated.
                 */
                int? index = ((OperationModify)operation).OldIndex;
                Task newTask = ((OperationModify)operation).NewTask;
                if (index.HasValue == false || newTask == null)
                {
                    response = DisplayAll(taskList);
                }
                else if (index < 0 || index > taskList.Count - 1)
                {
                    response = DisplayAll(taskList);
                }
                else
                {
                    Task taskToModify = lastListedTasks[index.Value];
                    response = Modify(ref taskToModify, newTask, ref taskList, out successFlag);
                }
            }
            else if (operation is OperationUndo)
            {
                undoStack.Pop();
                Operation undoOperation = undoStack.Pop();
                if (undoOperation is OperationAdd)
                {
                    Task task =((OperationAdd)undoOperation).NewTask;
                    response = Delete(ref task, ref taskList, out successFlag);
                }
                else if (undoOperation is OperationDelete &&(((OperationDelete)undoOperation).Index.HasValue == true))
                {
                   
                    Task task = undoTask.Pop();
                    response = Add(task, ref taskList, out successFlag);
                }
                else if (undoOperation is OperationModify && ((OperationModify)undoOperation).NewTask!=null)
                {
                    Task taskToModify = ((OperationModify)undoOperation).NewTask;
                    Task newTask = undoTask.Pop();
                    response = Modify(ref taskToModify, newTask, ref taskList, out successFlag);
                }
                else
                {
                    response = "cannot undo this operation";
                }
            }
            else if (operation is OperationSearch)
            {
                string searchString = ((OperationSearch)operation).SearchString;
                response = Search(taskList, searchString);
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
            undoTask.Push(taskToDelete);
            taskList.Remove(taskToDelete);
            if (storageXML.RemoveTaskFromFile(taskToDelete))
            {
                successFlag = true;
                return String.Format(RESPONSE_DELETE_SUCCESS, taskToDelete.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;            
        }

        private string Modify(ref Task taskToModify,Task newTask, ref List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            undoTask.Push(taskToModify);
            taskList.Remove(taskToModify);
            taskList.Add(newTask);
            if (storageXML.RemoveTaskFromFile(taskToModify)&&storageXML.AddTaskToFile(newTask))
            {
                successFlag = true;
                return String.Format(RESPONSE_MODIFY_SUCCESS, taskToModify.TaskName, newTask.TaskName);
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

        private string Search(List<Task> taskList, string searchString)
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
           // redoStack.Clear();
        }
    }
}
