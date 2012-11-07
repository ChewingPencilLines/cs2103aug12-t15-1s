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

        #region Variables for task tracking/storage
        protected static List<Task> currentListedTasks;
        protected static Stack<Operation> undoStack;
        protected static Stack<Operation> redoStack;
        protected Queue<Task> executedTasks;
        protected List<Task> taskList;
        protected Storage storageIO;
        #endregion

        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
        }

        protected Operation()
        {
            executedTasks = new Queue<Task>();
        }

        public abstract Response Execute(List<Task> taskList, Storage storageIO);

        public static void UpdateCurrentListedTasks(List<Task> tasks)
        {
            currentListedTasks = tasks;
        }

        /// <summary>
        /// This method sets the operating task list and storage IO controller
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

        protected void TrackOperation()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }

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
        protected Response AddTask(Task taskToAdd)
        {
            try
            {
                taskList.Add(taskToAdd);
                executedTasks.Enqueue(taskToAdd);
                bool success = storageIO.AddTaskToFile(taskToAdd);
                if (success)
                {
                    currentListedTasks.Add(taskToAdd);
                    return GenerateStandardSuccessResponse(taskToAdd);
                }
                else
                    return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationAdd), currentListedTasks);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString()); // <- this is not logging.
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationAdd), currentListedTasks);
                //log this: return RESPONSE_ADD_FAILURE + "\r\nThe following exception occured: " + e.ToString();
            }
        }

        protected Response DeleteTask(Task taskToDelete)
        {
            taskList.Remove(taskToDelete);
            executedTasks.Enqueue(taskToDelete);
    
            if(currentListedTasks.Contains(taskToDelete))
                currentListedTasks.Remove(taskToDelete);

            if (storageIO.RemoveTaskFromFile(taskToDelete))
            {
                return GenerateStandardSuccessResponse(taskToDelete);
            }
            else
                return GenerateXMLFailureResponse();

        }

        protected Response ModifyTask(Task taskToModify, Task newTask)
        {
            Response response = null;
            response = DeleteTask(taskToModify);
            if (response.IsSuccessful()) response = AddTask(newTask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationModify), currentListedTasks);
            else
                return response;
        }

        protected List<Task> SearchForTasks(
            string searchString,
            DateTimeSpecificity isSpecific,
            bool exact = false,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SearchType searchType = SearchType.NONE
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

        protected Response MarkTaskAs(Task taskToMark, bool doneState)
        {            
            SetMembers(taskList, storageIO);
            taskToMark.DoneState = doneState;
            executedTasks.Enqueue(taskToMark);

            if (storageIO.MarkTaskAs(taskToMark, doneState))
            {
                return GenerateStandardSuccessResponse(taskToMark);
            }
            else
                return GenerateXMLFailureResponse();
        }
        
        #endregion

        // ****************************************************************************************
        // Task Selection + Operation Execution Methods
        // ****************************************************************************************

        #region Task Selection + Operation Execution Methods

        // ******************************************************************
        // Search & Execute
        // ******************************************************************

        #region Search & Execute
        protected Response ExecuteBySearch(
                        string taskName,
                        bool isExact,
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

            searchResults = SearchForTasks(taskName, isSpecific, isExact, startTime, endTime, searchType);

            // If no results and not trying to display all, try non-exact search.
            if (searchResults.Count == 0 && !isAll)
                response = TrySearchNonExact(taskName, isSpecific, startTime, endTime, searchType);

            // If only one result and is searching by name, delete immediately.
            else if (searchResults.Count == 1 && !(taskName == null || taskName == ""))
            {
                var parameters = AddTaskToParameters(args, searchResults[0]);
                response = (Response)action.DynamicInvoke(parameters);
            }
            
            else if (isAll)
            {
                response = ExecuteOnAll(taskName, startTime, endTime, searchType, searchResults, isAll, action, args);
            }

            // If not, display search results.
            else
            {
                response = DisplaySearchResults(searchResults, taskName, startTime, endTime, searchType, isAll);
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
            DateTimeSpecificity isSpecific,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType
            )
        {
            Response response = null;
            List<Task> searchResults = new List<Task>();

            searchResults = SearchForTasks(taskName, isSpecific);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] stringArgs;
                SetArgumentsForFeedbackString(out stringArgs, taskName, startTime, endTime, SearchType.NONE);
                response =
                    new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, stringArgs);
            }
            return response;
        }

        private Response ExecuteOnAllSearchResults(List<Task> searchResults, Delegate action, params object[] args)
        {
            Response response = null;
            foreach (Task task in searchResults)
            {
                var parameters = AddTaskToParameters(args, searchResults[0]);
                response = (Response)action.DynamicInvoke(parameters);

                if (!response.IsSuccessful()) return response;
            }
            response = new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
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

            return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, args);
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
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                else
                {
                    var parameters = AddTaskToParameters(args, task);
                    response = (Response)action.DynamicInvoke(parameters);
                    if (!response.IsSuccessful()) return response;
                }
            }
            return new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
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
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                else
                {
                    var parameters = AddTaskToParameters(args, task);
                    response = (Response)action.DynamicInvoke(parameters);
                    if (!response.IsSuccessful()) return response;
                }
            }
            return new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
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
                return new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else return null;
        }

        protected Response GenerateStandardSuccessResponse(Task task)
        {
            string[] args = new string[1];
            args[0] = task.TaskName;
            return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks, args);
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
    }
}
