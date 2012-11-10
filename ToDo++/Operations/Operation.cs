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

        #region Variables for Task and Operation tracking/storage
        protected static List<Task> currentListedTasks;     // The currently displayed list of tasks.        
        protected static Stack<Operation> undoStack;        // Contains the Operation history of all Operations.
        protected static Stack<Operation> redoStack;        // Contains the Operation Undo history of all Operations.        
        protected Queue<Task> executedTasks;                // Contains a history of tasks which this operation has manipulated.        
        protected List<Task> taskList;                      // The entire list of tasks which this Operation will execute on.        
        protected Storage storageIO;                        // The Storage controller which will be used for reading / writing from tasks to file.
        protected SortType sortType;
        #endregion

        /// <summary>
        /// Initializes the static variables used by all Operations.
        /// </summary>
        /// <returns>Nothing.</returns>
        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
        }

        /// <summary>
        /// Initializes the neccesary variables for all Operation.
        /// </summary>
        /// <returns>Nothing.</returns>
        protected Operation(SortType sortType)
        {
            this.sortType = sortType;
            executedTasks = new Queue<Task>();
        }
        
        /// <summary>
        /// Sets the currently displayed list of tasks shared by all Operations
        /// to the input list of tasks.
        /// </summary>
        /// <param name="tasks">The list of tasks to be displayed.</param>
        /// <returns></returns>
        public static void UpdateCurrentListedTasks(List<Task> tasks)
        {
            currentListedTasks = tasks;
        }

        /// <summary>
        /// Sets the operating task list and storage IO controller
        /// this Operation will use to the instances referred to by the input parameters.
        /// </summary>
        /// <param name="taskList">The task list this Operation will execute on.</param>
        /// <param name="storageIO">The storage IO controller this Operation will use to store data.</param>
        /// <returns></returns>
        protected void SetMembers(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            this.taskList = taskList;
        }

        /// <summary>
        /// Adds this Operation to the history of successful operations.
        /// </summary>
        /// <returns>Nothing.</returns>
        protected void TrackOperation()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }
        
        /// <summary>
        /// Base method to execute this Operation. Must be overriden by all derived Operations.
        /// </summary>
        /// <param name="taskList">The list of task to execute the Operation on.</param>
        /// <param name="storageIO">The Storage controller to use for reading/writing to file.</param>
        /// <returns></returns>
        public abstract Response Execute(List<Task> taskList, Storage storageIO);

        /// <summary>
        /// Base Undo method. All undoable operations must override this method.
        /// This base method will throw an assertion if called without being overriden
        /// and debug mode is on.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to be used to write task changes.</param>
        /// <returns>Null</returns>
        public virtual Response Undo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be undoable!");
            return null;
        }

        /// <summary>
        /// Base Redo method. All undoable operations must override this method.
        /// This base method will throw an assertion if it is called without being overriden
        /// and debug mode is on.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to be used to write task changes.</param>
        /// <returns>Null</returns>
        public virtual Response Redo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be redoable!");
            return null;
        }
        
        /// <summary>
        /// Indicates whether the Operation should allow a multiple-task
        /// execution to continue if one of the tasks execute unsuccessfully.
        /// This method can be overriden to specify when this condition should be allowed.
        /// If it is not overriden, it will return false by default.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>False</returns>
        public virtual bool AllowSkipOver(Response response)
        {
            return false;
        }

        // ******************************************************************
        // Task Manipulation Methods
        // ******************************************************************

        #region Task Manipulation Methods
        /// <summary>
        /// Adds a task to the system. The newly added task is written to file
        /// using the Storage controller specified with SetMembers.
        /// </summary>
        /// <param name="taskToAdd">The task to add.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        protected Response AddTask(Task taskToAdd)
        {
            try
            {
                if (TaskIsInvalid(taskToAdd))
                {
                    Logger.Warning("Attempted to add an invalid task.", "AddTask::Operation");
                    return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);
                }
                                
                taskList.Add(taskToAdd);
                AddToOperationHistory(taskToAdd);                

                bool success = storageIO.AddTaskToFile(taskToAdd);
                if (success)
                {
                    currentListedTasks.Add(taskToAdd);
                    Logger.Info("Added a new task successfully.", "AddTask::Operation");
                    return GenerateStandardSuccessResponse(taskToAdd);
                }
                else
                    return GenerateXMLFailureResponse();
            }
            catch (Exception e)
            {
                Logger.Error(e, "AddTask::Operation");
                return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);                
            }
        }
        
        /// <summary>
        /// Removes a task from the system. The deleted task is removed from file
        /// using the Storage controller specified with SetMembers.
        /// </summary>
        /// <param name="taskToDelete"></param>
        /// <returns></returns>
        protected Response DeleteTask(Task taskToDelete)
        {
            try
            {
                taskList.Remove(taskToDelete);
                AddToOperationHistory(taskToDelete);

                if (currentListedTasks.Contains(taskToDelete))
                    currentListedTasks.Remove(taskToDelete);

                if (storageIO.RemoveTaskFromFile(taskToDelete))
                {
                    Logger.Info("Deleted a task successfully.", "DeleteTask::Operation");
                    return GenerateStandardSuccessResponse(taskToDelete);
                }
                else
                    return GenerateXMLFailureResponse();
            }
            catch (Exception e)
            {
                Logger.Error(e, "DeleteTask::Operation");
                return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);
            }
        }

        /// <summary>
        /// Searches the Tasks specified in SetMembers using the specified
        /// parameters as filters and returns the Tasks matching those filters.
        /// </summary>
        /// <param name="searchString">The string to match the Task's name against.</param>
        /// <param name="requireExactMatch">The flag indicating whether the string comparison should be exact or not.</param>
        /// <param name="startTime">The start time by which Tasks falling before are not matched.</param>
        /// <param name="endTime">The end time by which Tasks falling after are not matched.</param>
        /// <param name="searchType">The type of search to perform.</param>
        /// <returns></returns>
        protected List<Task> SearchForTasks(
            string searchString,
            bool requireExactMatch = false,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SearchType searchType = SearchType.NONE
            )
        {
            List<Task> filteredTasks = taskList;
            try
            {
                filteredTasks = FilterByTaskName(filteredTasks, searchString, requireExactMatch);
                filteredTasks = FilterByTaskTime(filteredTasks, startTime, endTime);
                filteredTasks = FilterBySearchType(filteredTasks, searchType);                 
            }
            catch (Exception e)
            {
                Logger.Error(e, "SearchForTasks::Operation");
                return new List<Task>(); // return empty list.
            }
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by time, returning a list of the filtered results.
        /// Tasks within the specified start and end times are returned.
        /// As long as part of the task is within the specified tasks,
        /// they are considered as validly matched.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="startTime">The start time by which the filtered tasks should be within.</param>
        /// <param name="endTime">The end time by which the filtered tasks should be within.</param>
        /// <param name="isSpecific">The Specificity of the time constraints</param>
        /// <returns></returns>
        private List<Task> FilterByTaskTime(List<Task> tasks, DateTime? startTime, DateTime? endTime)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            if (!(startTime == null && endTime == null))
                filteredTasks = (from task in filteredTasks
                                 where task.IsWithinTime(startTime, endTime)
                                 select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by task name, returning the list of tasks with names matching the search string.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="searchString">The string by which to filter the task name against.</param>
        /// <param name="exact">The flag indicating whether the string comparison should exact or not.</param>
        /// <returns>The filtered list of tasks.</returns>
        private List<Task> FilterByTaskName(List<Task> tasks, string searchString, bool exact)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            if (!IsValidString(searchString))
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString, task.TaskName, true) == 0 && exact == true))
                                 select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by the specified SearchType, returning a list of the filtered results.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="searchType">The type of search to execute.</param>
        /// <returns></returns>
        private List<Task> FilterBySearchType(List<Task> tasks, SearchType searchType)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            bool doneMatch;
            if (searchType == SearchType.DONE)
                doneMatch = true;
            else if (searchType == SearchType.UNDONE)
                doneMatch = false;
            else return filteredTasks; // don't sort anymore.

            filteredTasks = (from task in filteredTasks
                             where task.DoneState == doneMatch
                             select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// Marks a task's [done] state depending on the given flag. Marks as done if true, undone if false.
        /// </summary>
        /// <param name="taskToMark">Task to mark.</param>
        /// <param name="doneState">Flag indiciating whether to mark as done or undone.</param>
        /// <returns></returns>
        protected Response MarkTaskAs(Task taskToMark, bool doneState)
        {            
            SetMembers(taskList, storageIO);

            if (taskToMark.DoneState == doneState)
                return new Response(Result.INVALID_TASK, sortType, this.GetType());
            else
                taskToMark.DoneState = doneState;

            executedTasks.Enqueue(taskToMark);

            if (storageIO.MarkTaskAs(taskToMark, doneState))
                return GenerateStandardSuccessResponse(taskToMark);            
            else
                return GenerateXMLFailureResponse();
        }

        /// <summary>
        /// This method checks for the validity of the specified task.
        /// </summary>
        /// <param name="taskToCheck">The Task to check validity of.</param>
        /// <returns>True if invalid. False if valid.</returns>
        private bool TaskIsInvalid(Task taskToCheck)
        {
            if (taskToCheck == null)
                return true;
            if (IsValidString(taskToCheck.TaskName))
                return true;
            return false;
        }

        /// <summary>
        /// This method adds the specified task to the history executed tasks for this operation.
        /// </summary>
        /// <param name="task">The task to add to history.</param>
        /// <returns></returns>
        private void AddToOperationHistory(Task task)
        {
            executedTasks.Enqueue(task);
        }        
        #endregion

        // ****************************************************************************************
        // Common Task Selection And Execution Methods
        // ****************************************************************************************

        #region Common Task Selection And Execution Methods

        // ******************************************************************
        // Search & Execute
        // ******************************************************************

        #region Search & Execute
        /// <summary>
        /// Searches for one or more tasks using the provided search paramters
        /// and then executes the specified action on them.
        /// </summary>
        /// <param name="searchString">The string to search the tasks' names against.</param>
        /// <param name="startTime">The start time by which the task must fall within.</param>
        /// <param name="endTime">The end time by which the task must fall within.</param>
        /// <param name="isSpecific">The specificty</param>
        /// <param name="isAll"></param>
        /// <param name="searchType"></param>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected Response ExecuteBySearch(
                        string searchString,
                        DateTime? startTime,
                        DateTime? endTime,
                        DateTimeSpecificity isSpecific,
                        bool isAll,
                        SearchType searchType,
                        Delegate action,
                        params object[] args
                        )
        {
            List<Task> searchResults = new List<Task>();
            Response response = null;

            searchResults = SearchForTasks(searchString, true, startTime, endTime, searchType);

            // If no results and not trying to display all, try non-exact search.
            if (searchResults.Count == 0 && !isAll)
                response = TrySearchNonExact(searchString, startTime, endTime, searchType);

            // If only one result and is searching by name, delete immediately.
            else if (searchResults.Count == 1 && !IsValidString(searchString))
            {
                var parameters = AddTaskToParameters(args, searchResults[0]);
                response = (Response)action.DynamicInvoke(parameters);
            }
            
            else if (isAll)
            {
                searchResults = SearchForTasks(searchString, false, startTime, endTime, searchType);
                response = ExecuteOnAll(searchString, startTime, endTime, searchType, searchResults, isAll, action, args);
            }

            // If not, display search results.
            else
            {
                response = DisplaySearchResults(searchResults, searchString, startTime, endTime, searchType, isAll);
            }
            return response;
        }

        // If all keyword is used, delete all in search results,
        // unless there were no constraining parameters.
        // If there were, delete all currently displayed tasks if no search string was used.
        // If not, display search results
        private Response ExecuteOnAll(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType,
            List<Task> searchResults,
            bool isAll,
            Delegate action,
            params object[] args
            )
        {
            Response response = null;
            if (startTime != null || endTime != null || searchType != SearchType.NONE)
                response = ExecuteOnAllSearchResults(searchResults, action, args);
            else if (taskName == "" || taskName == null)
                response = ExecuteOnAllDisplayedTasks(action, args);
            else
                response = DisplaySearchResults(searchResults, taskName, startTime, endTime, searchType, isAll);
            return response;
        }

        private Response TrySearchNonExact(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType
            )
        {
            Response response = null;
            List<Task> searchResults = new List<Task>();

            searchResults = SearchForTasks(taskName);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, sortType, this.GetType());
            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] stringArgs;
                SetArgumentsForFeedbackString(out stringArgs, taskName, startTime, endTime, SearchType.NONE);
                response =
                    new Response(Result.SUCCESS, sortType, typeof(OperationSearch), currentListedTasks, stringArgs);
            }
            return response;
        }

        private Response ExecuteOnAllSearchResults(List<Task> searchResults, Delegate action, params object[] args)
        {
            Response response = null;
            foreach (Task task in searchResults)
            {
                var parameters = AddTaskToParameters(args, task);
                response = (Response)action.DynamicInvoke(parameters);

                if (!response.IsSuccessful() && !AllowSkipOver(response)) return response;
            }
            response = new Response(Result.SUCCESS_MULTIPLE, sortType, this.GetType(), currentListedTasks);
            return response;
        }

        private Response DisplaySearchResults(
            List<Task> searchResults,
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType,
            bool isAll
            )
        {
            currentListedTasks = new List<Task>(searchResults);

            string[] args;
            SetArgumentsForFeedbackString(out args, taskName, startTime, endTime, searchType);

            return new Response(Result.SUCCESS, sortType, typeof(OperationSearch), currentListedTasks, args);
        }


        #endregion


        // ******************************************************************
        // Execute By Index
        // ******************************************************************

        #region Execute By Index
        protected Response ExecuteByIndex(int startIndex, int endIndex, Delegate action, params object[] args)
        {
            Response response = null;
            if (endIndex == startIndex)
            {
                response = ExecuteOnSingleTask(startIndex, action, args);
            }
            else
            {
                response = ExecuteOnMultipleTasks(startIndex, endIndex, action, args);
            }
            return response;
        }

        private Response ExecuteOnSingleTask(int startIndex, Delegate action, params object[] args)
        {
            Response response = null;
            Task task = currentListedTasks[startIndex];
            Debug.Assert(task != null, "Tried to manipulate a null task!");

            var parameters = AddTaskToParameters(args, task);
            response = (Response)action.DynamicInvoke(parameters);

            return response;
        }

        private Response ExecuteOnMultipleTasks(int startIndex, int endIndex, Delegate action, params object[] args)
        {
            Response response = null;
            response = new Response(Result.INVALID_TASK);
            for (int i = endIndex; i >= startIndex; i--)
            {
                Task task = currentListedTasks[i];
                if (task == null)
                    response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);
                else
                {
                    var parameters = AddTaskToParameters(args, task);
                    response = (Response)action.DynamicInvoke(parameters);
                    if (!response.IsSuccessful() && !AllowSkipOver(response)) return response;
                }
            }
            return new Response(Result.SUCCESS_MULTIPLE, sortType, this.GetType(), currentListedTasks);
        }
        #endregion DeleteByIndex

        // ******************************************************************
        // Execute On All
        // ******************************************************************

        #region Execute On All
        protected Response ExecuteOnAllDisplayedTasks(Delegate action, params object[] args)
        {
            Response response = null;
            for (int i = currentListedTasks.Count - 1; i >= 0; i--)
            {
                Task task = currentListedTasks[i];
                if (task == null)
                    response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);
                else
                {
                    var parameters = AddTaskToParameters(args, task);
                    response = (Response)action.DynamicInvoke(parameters);
                    if (!response.IsSuccessful() && !AllowSkipOver(response)) return response;
                }
            }
            return new Response(Result.SUCCESS_MULTIPLE, sortType, this.GetType(), currentListedTasks);
        }

        private static object[] AddTaskToParameters(object[] args, Task task)
        {
            if (args == null) return new object[1] { task };

            var parameterList = args.ToList();
            parameterList.Insert(0, task);
            var parameters = parameterList.ToArray();
            return parameters;
        }
        #endregion

        #endregion

        // ******************************************************************
        // Response Generation Methods
        // ******************************************************************

        #region Response Generation Methods

        /// <summary>
        /// This method checks if the two indices referring to a set of tasks in the currently displayed
        /// list of tasks are valid and returns null if they are valid.
        /// </summary>
        /// <param name="startIndex">The start index of the user requested tasks</param>
        /// <param name="endIndex">The end index of the user requested tasks</param>
        /// <returns>Returns a null value if indexes are valid. Returns a Response indicating the error if the indexes are invalid.</returns>
        protected Response CheckIfIndexesAreValid(int startIndex, int endIndex)
        {
            // No tasks to delete
            if (taskList.Count == 0)
                return new Response(Result.INVALID_TASK, sortType, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, sortType);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, sortType);
            else return null;
        }

        protected Response GenerateStandardSuccessResponse(Task task)
        {
            string[] args = new string[1];
            args[0] = task.TaskName;
            return new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks, args);
        }

        protected Response GenerateXMLFailureResponse()
        {
            return new Response(Result.XML_READWRITE_FAIL);
        }

        protected void SetArgumentsForFeedbackString(out string[] criteria, string searchString, DateTime? startTime, DateTime? endTime, SearchType searchType)
        {
            criteria = new string[Response.SEARCH_PARAM_NUM];
            criteria[Response.SEARCH_PARAM_DONE] = "";
            criteria[Response.SEARCH_PARAM_SEARCH_STRING] = "";

            if (searchType == SearchType.DONE)
                criteria[Response.SEARCH_PARAM_DONE] = "[DONE] ";
            else if (searchType == SearchType.UNDONE)
                criteria[Response.SEARCH_PARAM_DONE] = "undone ";

            if (searchString != "" && searchString != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " matching \"" + searchString + "\"";
            if (startTime != null || endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " within";
            if (startTime != null)
            {
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " " + ((DateTime)startTime).ToString("g");
                if (endTime != null)
                    criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " to";
            }
            if (endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " " + ((DateTime)endTime).ToString("g");
        }
        #endregion

        private static bool IsValidString(string searchString)
        {
            return (searchString == null || searchString == String.Empty);
        }
    }
}
