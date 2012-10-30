using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for 

    // ******************************************************************
    public abstract class Operation
    {
        #region Feedback Strings
        protected const string RESPONSE_ADD_SUCCESS = "Added \"{0}\" successfully.";
        protected const string RESPONSE_ADD_FAILURE = "Failed to add task!";
        protected const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        protected const string RESPONSE_DELETE_FAILURE = "No matching task found!";
        protected const string RESPONSE_DELETE_ALREADY = "Task has already been deleted!";
        protected const string RESPONSE_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        protected const string RESPONSE_DISPLAY_NOTASK = "There are no tasks for display.";
        protected const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        protected const string RESPONSE_UNDO_FAILURE = "Cannot undo last executed task!";
        protected const string RESPONSE_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        protected const string RESPONSE_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone.";
        protected const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        protected const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        public const string REPONSE_INVALID_COMMAND = "Invalid command input!";
        #endregion
        
        // Containers for keeping track of executed operations
        protected static List<Task> lastListedTasks;
        protected static Stack<Operation> undoStack;
        static Stack<Operation> redoStack;
        protected static Stack<Task> undoTask;
        protected Storage storageXML;
        protected bool successFlag;

        static Operation()
        {
            lastListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
        }

        public abstract string Execute(List<Task> taskList, Storage storageXML);

        public virtual string Undo(List<Task> taskList, Storage storageXML)
        {
            Debug.Assert(false, "This operation should not be undoable!");
            return null;
        }

        protected string AddTask(Task taskToAdd, List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            try
            {
                taskList.Add(taskToAdd);
                undoTask.Push(taskToAdd);
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

        protected string DeleteTask(Task taskToDelete, List<Task> taskList, out bool successFlag)
        {
            successFlag = false;

            // Remove tasks and push to undo stack.
            undoTask.Push(taskToDelete);
            taskList.Remove(taskToDelete);

            // Adjust list to null references to deleted tasks without changing order.
            int nullIndex = lastListedTasks.IndexOf(taskToDelete);
            if (nullIndex >= 0 && nullIndex < lastListedTasks.Count)
            {
                lastListedTasks[nullIndex] = null;
            }

            if (storageXML.RemoveTaskFromFile(taskToDelete))
            {
                successFlag = true;
                return String.Format(RESPONSE_DELETE_SUCCESS, taskToDelete.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }

        protected string MarkAsDone(Task taskToMarkAsDone, out bool successFlag)
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

        protected string ModifyTask(ref Task taskToModify, Task newTask, ref List<Task> taskList, out bool successFlag)
        {
            successFlag = false;
            undoTask.Push(taskToModify);
            taskList.Remove(taskToModify);
            taskList.Add(newTask);
            undoTask.Push(newTask);
            if (storageXML.RemoveTaskFromFile(taskToModify) && storageXML.AddTaskToFile(newTask))
            {
                successFlag = true;
                return String.Format(RESPONSE_MODIFY_SUCCESS, taskToModify.TaskName, newTask.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }

        // todo: move search queries into the tasks themselves as methods.
        protected List<Task> SearchForTasks(List<Task> taskList, string searchString, bool exact = false, DateTime? startTime = null, DateTime? endTime = null)
        {
            List<Task> filteredTasks = taskList;
            if (searchString != null)
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString, task.TaskName, true) == 0 && exact == true))
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

        protected void TrackOperation()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }
        
        #region Display Formatters
        protected string GenerateDisplayString(List<Task> tasksToDisplay)
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
        #endregion
    }
}
