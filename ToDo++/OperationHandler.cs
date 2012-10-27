using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    public class OperationHandler
    {
        static List<Task> lastListedTasks;
        public List<Task> LastListedTasks
        {
            get { return OperationHandler.lastListedTasks; }
            //set { OperationHandler.lastListedTasks = value; }
        }

        static Stack<Operation> undoStack;
        static Stack<Operation> redoStack;
        static Stack<Task> undoTask;  
    
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
        const string RESPONSE_DISPLAY_NOTASK = "There are no tasks for display.";
        const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        const string RESPONSE_UNDO_FAILURE = "Cannot undo last executed task!";
        const string RESPONSE_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string REPONSE_INVALID_COMMAND = "Invalid command input!";
        const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        #endregion

        public OperationHandler(Storage storageXML)
        {
            undoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
            this.storageXML = storageXML;
        }

        static OperationHandler()
        {
            lastListedTasks = new List<Task>();
        }
        
        public string Add(Task taskToAdd, List<Task> taskList, out bool successFlag)
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

        public string Delete(Task taskToDelete, List<Task> taskList, out bool successFlag)
        {
            successFlag = false;

            // Remove tasks and push to undo stack.
            undoTask.Push(taskToDelete);
            taskList.Remove(taskToDelete);

            // Adjust list to null references to deleted tasks without changing order.
            int nullIndex = lastListedTasks.IndexOf(taskToDelete);
            lastListedTasks[nullIndex] = null;

            if (storageXML.RemoveTaskFromFile(taskToDelete))
            {
                successFlag = true;
                return String.Format(RESPONSE_DELETE_SUCCESS, taskToDelete.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }

        public string MarkAsDone(Task taskToMarkAsDone, out bool successFlag)
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

        public string Modify(ref Task taskToModify, Task newTask, ref List<Task> taskList, out bool successFlag)
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

        public string UndoLastOperation()
        {
            // @ivan: all undoable Operations should have a undo method so we can just call operation.undo
            //Operation opToUndo = undoStack.Pop();
            //opToUndo.Undo();
            throw new NotImplementedException();
            
            /*
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
             * */            
        }

        public List<Task> Search(List<Task> taskList, string searchString, bool exact = false, DateTime? startTime = null, DateTime? endTime = null)
        {            
            List<Task> filteredTasks = taskList;
            if (searchString != null)
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString,task.TaskName,true) == 0 && exact == true))
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
            return filteredTasks;
        }

        public string Display(List<Task> taskList)
        {
            return GenerateDisplayString(taskList);
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

        public void TrackOperation(Operation operation)
        {
            undoStack.Push(operation);
            // redoStack.Clear();
        }
    }
}
