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
        const string RESPONSE_ADD_FAILURE = "Failed to add task!";
        const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string RESPONSE_DELETE_FAILURE = "No matching task found!";
        const string RESPONSE_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string RESPONSE_DISPLAY_NOTASK = "There is no task for display.";
        const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        const string RESPONSE_UNDO_FAILURE = "Cannot undo last executed task!";
        const string RESPONSE_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string REPONSE_INVALID_COMMAND = "Invalid command input!";
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
            string response = String.Empty;
            bool successFlag;

            TrackOperation(operation);

            if (operation == null)
            {
                return REPONSE_INVALID_COMMAND;
            }

            if (operation is OperationAdd)
            {
                Task taskToAdd = ((OperationAdd)operation).NewTask;
                if (taskToAdd == null) return RESPONSE_ADD_FAILURE;
                response = Add(taskToAdd, ref taskList, out successFlag);
            }

            if (operation is OperationDelete)
            {
                int? index = ((OperationDelete)operation).Index;
                string deleteString = ((OperationDelete)operation).DeleteString;
                if (index.HasValue == false && deleteString != null)
                 {
                     int numberOfMatches;
                     response = Search(out numberOfMatches, taskList, deleteString);
                     if (numberOfMatches == 0) { response = RESPONSE_DELETE_FAILURE; }
                     if (numberOfMatches == 1) { response = Delete(lastListedTasks[0], ref taskList, out successFlag); }
                }
                else if (index < 0 || index > taskList.Count - 1)
                {
                    return RESPONSE_INVALID_TASK_INDEX;
                }
                else if (deleteString == null)
                {
                    Task taskToDelete = lastListedTasks[index.Value];
                     response = Delete(taskToDelete, ref taskList, out successFlag);
                }
                else
                {
                    return REPONSE_INVALID_COMMAND;
                }
            }

            if (operation is OperationModify)
            {
                /*
                 *  when modify, if user key in nothing or only index or only task details
                 *  after the commandtype, then all tasks will be shown.
                 *  only when user input full information will modify operated.
                 */
                int? index = ((OperationModify)operation).OldIndex;
                Task newTask = ((OperationModify)operation).NewTask;
                if (index.HasValue == false && newTask == null)
                {
                    response = DisplayAll(taskList);
                }
                else if (index.HasValue == false && newTask != null)
                {
                    int numberOfMatches;
                    response = Search(out numberOfMatches, taskList, newTask.TaskName);
                }
                else if (index.HasValue == true && (index < 0 || index > taskList.Count - 1))
                {
                    if (newTask != null)
                    {
                        int numberOfMatches;
                        response = Search(out numberOfMatches, taskList, newTask.TaskName);
                    }
                    else
                    {
                        response = DisplayAll(taskList);
                    }
                }
                else
                {
                    Task taskToModify = lastListedTasks[index.Value];
                    response = Modify(ref taskToModify, newTask, ref taskList, out successFlag);
                }
            }

            if (operation is OperationUndo)
            {
                undoStack.Pop();
                Operation undoOperation = undoStack.Pop();
                response = Undo(undoOperation, ref taskList);
            }

            if (operation is OperationDisplay)
            {
                response = DisplayAll(taskList);
            }

            if (operation is OperationSearch)
            {
                int numberOfMatches;
                OperationSearch searchOp = ((OperationSearch)operation);
                string searchString = searchOp.SearchString;
                DateTime? startTime = searchOp.StartTime;
                DateTime? endTime = searchOp.EndTime;
                response = Search(out numberOfMatches, taskList, searchString, startTime, endTime);
            }

            if (operation is OperationSort)
            {
                //sort only change what user view, but not change in storage
                //TaskComparer tc = new TaskComparer();
                //lastListedTasks.Sort(tc);
                lastListedTasks = (from task in lastListedTasks
                                  orderby task.TaskName
                                  select task).ToList();
                response = DisplayAll(lastListedTasks);
            }

            if (operation is OperationMarkAsDone)
            {
                int? index = ((OperationMarkAsDone)operation).Index;
                string doneString = ((OperationMarkAsDone)operation).DoneString;
                if (index.HasValue == false && doneString != null)
                {
                    int numberOfMatches = 0;
                    response = Search(out numberOfMatches, taskList, doneString);
                    if (numberOfMatches == 1)
                    {
                        response = MarkAsDone(lastListedTasks[0], out successFlag);
                    }
                }
                else if (index < 0 || index > taskList.Count - 1)
                {
                    return RESPONSE_INVALID_TASK_INDEX;
                }
                else if (doneString == null)
                {
                    Task taskToMarkAsDone = lastListedTasks[index.Value];
                    response = MarkAsDone(taskToMarkAsDone, out successFlag);
                }
                else
                {
                    return REPONSE_INVALID_COMMAND;
                }
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
                return RESPONSE_ADD_FAILURE + "\r\nThe following exception occured: " + e.ToString();
            }
        }

        private string Delete(Task taskToDelete, ref List<Task> taskList, out bool successFlag)
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

        private string MarkAsDone(Task taskToMarkAsDone, out bool successFlag)
        {
            successFlag = false;
            undoTask.Push(taskToMarkAsDone);
            taskToMarkAsDone.State = true;

            if (storageXML.MarkTaskAsDone(taskToMarkAsDone))
            {
            successFlag = true;
            return String.Format(RESPONSE_MARKASDONE_SUCCESS, taskToMarkAsDone.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }

        private string Modify(ref Task taskToModify, Task newTask, ref List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            undoTask.Push(taskToModify);
            taskList.Remove(taskToModify);
            taskList.Add(newTask);
            if (storageXML.RemoveTaskFromFile(taskToModify) && storageXML.AddTaskToFile(newTask))
            {
                successFlag = true;
                return String.Format(RESPONSE_MODIFY_SUCCESS, taskToModify.TaskName, newTask.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }

        private string Undo(Operation undoOperation, ref List<Task> taskList)
        {
            string response;
            bool successFlag;
            if (undoOperation is OperationAdd)
            {
                Task task = ((OperationAdd)undoOperation).NewTask;
                response = Delete(task, ref taskList, out successFlag);
            }
            else if (undoOperation is OperationDelete && (((OperationDelete)undoOperation).Index.HasValue == true))
            {

                Task task = undoTask.Pop();
                response = Add(task, ref taskList, out successFlag);
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

        private string DisplayAll(List<Task> taskList)
        {
            return GenerateDisplayString(taskList);
        }

        private string Search(out int numberOfMatches, List<Task> taskList, string searchString, DateTime? startTime = null, DateTime? endTime = null)
        {            
            List<Task> filteredTasks = taskList;
            if (searchString != null)
                filteredTasks = (from task in filteredTasks
                                 where task.TaskName.IndexOf(searchString) >= 0 
                                 select task).ToList();

            // Search all tasks that end before EndTime or have deadlines before EndTime
            if (startTime == null && endTime != null)
            {
                List<Task> tempList = new List<Task>();
                tempList = filteredTasks.GetRange(0, filteredTasks.Count);
                filteredTasks = (from task in tempList
                                 where (task is TaskDeadline)
                                 where (((TaskDeadline)task).EndTime <= endTime)
                                 select task                                 
                                 ).ToList();
                filteredTasks.AddRange((from task in tempList
                                        where (task is TaskEvent)
                                        where (((TaskEvent)task).EndTime <= endTime)
                                        select task).ToList());
            }

            // Search all tasks that occur between StartTime and EndTime
            else if (startTime != null && endTime != null)
            {
                List<Task> tempList = new List<Task>();
                tempList = filteredTasks.GetRange(0, filteredTasks.Count);
                filteredTasks = (from task in tempList
                                 where (task is TaskDeadline)
                                 where (((TaskDeadline)task).EndTime >= startTime)
                                 where (((TaskDeadline)task).EndTime <= endTime)
                                 select task).ToList();
                filteredTasks.AddRange((from task in tempList
                                        where (task is TaskEvent)
                                        where (((TaskEvent)task).StartTime >= startTime)
                                        where (((TaskEvent)task).EndTime <= endTime)
                                        select task).ToList());
            }

            // Search all tasks that fall on the day of StartTime
            else if (startTime != null && endTime == null)
            {
                List<Task> tempList = new List<Task>();
                tempList = filteredTasks.GetRange(0, filteredTasks.Count);
                filteredTasks = (from task in tempList
                                 where (task is TaskDeadline)
                                 where (((TaskDeadline)task).EndTime.Date == ((DateTime)startTime).Date)
                                 select task).ToList();
                filteredTasks.AddRange((from task in tempList
                                        where (task is TaskEvent)
                                        where (((TaskEvent)task).StartTime.Date <= ((DateTime)startTime).Date)
                                        where (((TaskEvent)task).EndTime.Date >= ((DateTime)startTime).Date)
                                        select task).ToList());
            }
            numberOfMatches = filteredTasks.Count;
            return GenerateDisplayString(filteredTasks);
        }

        private string GenerateDisplayString(List<Task> tasksToDisplay)
        {
            string displayString = "";
            if (tasksToDisplay.Count == 0)
                return RESPONSE_DISPLAY_NOTASK;
            int index = 1;
            foreach (Task task in tasksToDisplay)
            {
                displayString += "\r\n";
                displayString += index;
                displayString += GetTaskInformation(task);
                index++;  
            }
            lastListedTasks = new List<Task>(tasksToDisplay);
            return displayString;
        }

        private string GetTaskInformation(Task task)
        {
            string taskString = String.Empty;
            if (task is TaskFloating)
            {
                taskString += ShowFloating((TaskFloating)task);
            }
            else if (task is TaskDeadline)
            {
                taskString += ShowDeadline((TaskDeadline)task);
            }
            else if (task is TaskEvent)
            {
                taskString += ShowEvent((TaskEvent)task);
            }
            if (task.State)
                taskString += " [DONE]";
            return taskString;
        }

        private string ShowFloating(TaskFloating task)
        {
            string feedback;
            feedback = ". " + task.TaskName;
            return feedback;
        }

        private string ShowDeadline(TaskDeadline task)
        {
            string feedback;
            feedback = ". " + task.TaskName;

            feedback += (" BY: " + ((TaskDeadline)task).EndTime);
            return feedback;
        }

        private string ShowEvent(TaskEvent task)
        {
            string feedback;
            feedback = ". " + task.TaskName;

            DateTime startTime = ((TaskEvent)task).StartTime;
            DateTime endTime = ((TaskEvent)task).EndTime;
            feedback += (" AT: " + startTime.ToString());
            if (startTime != endTime && endTime != null)
                feedback += (" TO: " + endTime.ToString());
            return feedback;
        }

        private void TrackOperation(Operation operation)
        {
            undoStack.Push(operation);
            // redoStack.Clear();
        }
    }
}
