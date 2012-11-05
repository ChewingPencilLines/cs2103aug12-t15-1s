using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for Operation
    // ******************************************************************
    public abstract class Operation
    {
        // Containers for keeping track of executed operations
        protected static List<Task>  currentListedTasks;
        protected static Stack<Operation> undoStack;
        protected static Stack<Operation> redoStack;
        protected static Stack<Task> undoTask;
        protected static Stack<Task> redoTask;
        protected Storage storageIO;
   //     protected bool successFlag;

        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
            redoTask = new Stack<Task>();
        }

        protected void TrackOperation()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }

        public static void UpdateCurrentListedTasks(List<Task> tasks)
        {
            currentListedTasks = tasks;
        }

        public abstract Response Execute(List<Task> taskList, Storage storageIO);
        
        /// <summary>
        /// Base Undo Operation Method. All undoable operations should be override this method.
        /// This base method will throw an assertion if called.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to write changes to file. </param>
        /// <returns></returns>
        public virtual Response Undo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be undoable!");
            return null;
        }

        public virtual Response Redo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be redoable!");
            return null;
        }

        // ******************************************************************
        // Task Manipulation Methods
        // ******************************************************************

        #region Task Manipulation Methods
        protected Response AddTask(Task taskToAdd, List<Task> taskList)
        {
            try
            {
                taskList.Add(taskToAdd);
                undoTask.Push(taskToAdd);
                if (storageIO.AddTaskToFile(taskToAdd))
                {
                    currentListedTasks.Add(taskToAdd);

                    return GenerateSuccessResponse(taskToAdd);
                }
                else
                    return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationAdd),  currentListedTasks);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString()); // <- this is not logging.
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationAdd),  currentListedTasks); 
                //log this: return RESPONSE_ADD_FAILURE + "\r\nThe following exception occured: " + e.ToString();
            }
        }

        protected Response DeleteTask(Task taskToDelete, List<Task> taskList)
        {
            // Remove tasks and push to undo stack.
            undoTask.Push(taskToDelete);
            taskList.Remove(taskToDelete);
            
            currentListedTasks.Remove(taskToDelete);

            if (storageIO.RemoveTaskFromFile(taskToDelete))
            {
                return GenerateSuccessResponse(taskToDelete);
            }
            else
                return GenerateXMLFailureResponse();
                
        }

        protected Response MarkAsDone(Task taskToMarkAsDone)
        {
            undoTask.Push(taskToMarkAsDone);
            taskToMarkAsDone.DoneState = true;

            if (storageIO.MarkTaskAs(taskToMarkAsDone, true))
            {
                return GenerateSuccessResponse(taskToMarkAsDone);
            }
            else
                return GenerateXMLFailureResponse();
        }

        protected Response ModifyTask(Task taskToModify, Task newTask, List<Task> taskList)
        {
            undoTask.Push(taskToModify);
            taskList.Remove(taskToModify);
            taskList.Add(newTask);
            undoTask.Push(newTask);
            if (storageIO.ModifyTask(taskToModify, newTask))
            {
                 currentListedTasks.Remove(taskToModify);
                 currentListedTasks.Add(newTask);
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationModify),  currentListedTasks);

            }
            else 
                return GenerateXMLFailureResponse();
        }

        protected Response PostponeTask(Task taskToPostpone, List<Task> taskList, DateTime? NewDate)
        {
            Task taskPostponed = taskToPostpone.Postpone(NewDate);
            if (taskPostponed == null)
            {
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationPostpone));   
            }
            else
            {
                undoTask.Push(taskToPostpone);
                taskList.Remove(taskToPostpone);
                taskList.Add(taskPostponed);
                undoTask.Push(taskPostponed);
            }
            if (storageIO.RemoveTaskFromFile(taskToPostpone) && storageIO.AddTaskToFile(taskPostponed))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationPostpone));
            }
            else
                return new Response(Result.XML_READWRITE_FAIL);
        }
        
        protected List<Task> SearchForTasks(
            List<Task> taskList,
            string searchString,
            DateTimeSpecificity isSpecific,
            bool exact = false,
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool searchForIsDone = false
            )
        {
            List<Task> filteredTasks = taskList;
            if (searchString != null)
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString, task.TaskName, true) == 0 && exact == true))
                                 select task).ToList();
            if (!(startTime == null && endTime == null)) 
                filteredTasks = (from task in filteredTasks
                                 where task.IsWithinTime(isSpecific, startTime, endTime)
                                 select task).ToList();
            if (searchForIsDone)
                filteredTasks = (from task in filteredTasks
                                 where task.DoneState == true
                                 select task).ToList();
            return filteredTasks;
        }

        private Response GenerateSuccessResponse(Task task)
        {
            string[] args = new string[1];
            args[0] = task.TaskName;
            return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks, args);
        }

        private Response GenerateXMLFailureResponse()
        {
            return new Response(Result.XML_READWRITE_FAIL);
        }

        protected void SetArgumentsForFeedbackString(out string[] criteria, string searchString, DateTime? startTime, DateTime? endTime, bool searchForIsDone, bool isAll)
        {
            criteria = new string[Response.SEARCH_PARAM_NUM];
            criteria[Response.SEARCH_PARAM_ALL] = "";
            criteria[Response.SEARCH_PARAM_DONE] = "";
            criteria[Response.SEARCH_PARAM_SEARCH_STRING] = "";

            if (searchForIsDone == true)
                criteria[Response.SEARCH_PARAM_DONE] = "[DONE] ";
            if (isAll == true)
                criteria[Response.SEARCH_PARAM_ALL] = "all ";

            if (searchString != "" && searchString != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " matching \"" + searchString + "\"";
            if (startTime != null || endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " with time constraints";
        }

        #endregion
    }
}
