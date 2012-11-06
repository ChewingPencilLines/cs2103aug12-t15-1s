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
        protected static List<Task> currentListedTasks;
        protected static Stack<Operation> undoStack;
        protected static Stack<Operation> redoStack;
        protected static Stack<Task> undoTask;
        protected static Stack<Task> redoTask;
        protected Storage storageIO;
        protected List<Task> taskList;
        //     protected bool successFlag;

        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
            redoTask = new Stack<Task>();
        }

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

        private void TrackTask(Task task, bool isAdd)
        {
            if (isAdd) taskList.Add(task);
            else taskList.Remove(task);

            undoTask.Push(task);
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
        protected Response AddTask(Task taskToAdd)
        {
            try
            {
                TrackTask(taskToAdd, true);
                bool success = storageIO.AddTaskToFile(taskToAdd);
                if (success)
                {
                    currentListedTasks.Add(taskToAdd);
                    return GenerateSuccessResponse(taskToAdd);
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
            TrackTask(taskToDelete, false);
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

        protected Response ModifyTask(Task taskToModify, Task newTask)
        {
            TrackTask(taskToModify, false);
            TrackTask(newTask, true);

            if (storageIO.ModifyTask(taskToModify, newTask))
            {
                currentListedTasks.Remove(taskToModify);
                currentListedTasks.Add(newTask);
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationModify), currentListedTasks);
            }
            else
                return GenerateXMLFailureResponse();
        }

        protected Response PostponeTask(Task taskToPostpone, DateTime? NewDate)
        {
            //TaskId doesn't change though the sequence may change
            Task taskPostponed = taskToPostpone.Postpone(NewDate);

            if (taskPostponed == null)
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationPostpone));
            else
            {
                TrackTask(taskToPostpone, false);
                TrackTask(taskPostponed, true);
                currentListedTasks.Remove(taskToPostpone);
                currentListedTasks.Add(taskPostponed);
            }
            if (storageIO.RemoveTaskFromFile(taskToPostpone) && storageIO.AddTaskToFile(taskPostponed))
            {
                return GenerateSuccessResponse(taskPostponed);
                //return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationPostpone),currentListedTasks);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL);
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

        protected void SetArgumentsForFeedbackString(out string[] criteria, string searchString, DateTime? startTime, DateTime? endTime, SearchType searchType, bool isAll)
        {
            criteria = new string[Response.SEARCH_PARAM_NUM];
            criteria[Response.SEARCH_PARAM_ALL] = "";
            criteria[Response.SEARCH_PARAM_DONE] = "";
            criteria[Response.SEARCH_PARAM_SEARCH_STRING] = "";

            if (searchType == SearchType.DONE)
                criteria[Response.SEARCH_PARAM_DONE] = "[DONE] ";
            else if (searchType == SearchType.UNDONE)
                criteria[Response.SEARCH_PARAM_DONE] = "undone ";
            if (isAll == true)
                criteria[Response.SEARCH_PARAM_ALL] = "all ";

            if (searchString != "" && searchString != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " matching \"" + searchString + "\"";
            if (startTime != null || endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " with time constraints";
        }
        #endregion


        // ****************************************************************************************
        // Task Selection And Execution Methods
        // ****************************************************************************************

        #region Task Selection And Execution Methods

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
                response = TrySearchNonExact(ref searchResults, taskName, isSpecific);

            // If only one result and is searching by name, delete immediately.
            else if (searchResults.Count == 1 && !(taskName == null || taskName == ""))
            {
                var parameters = AddTaskToParameters(args, searchResults[0]);
                response = (Response)action.DynamicInvoke(parameters);
            }

            // If all keyword is used, delete all in search results if not searching empty string.
            // If not, delete all currently displayed tasks.
            else if (isAll)
            {
                if (taskName == "" || taskName == null)
                    response = ExecuteOnAllDisplayedTasks(action, args);
                else
                    response = ExecuteOnAllSearchResults(searchResults, action, args);
            }

            // If not, display search results.
            else
                response = DisplaySearchResults(searchResults, taskName, startTime, endTime, searchType, isAll);

            return response;
        }

        private Response TrySearchNonExact(ref List<Task> searchResults, string taskName, DateTimeSpecificity isSpecific)
        {
            Response response = null;
            searchResults = SearchForTasks(taskName, isSpecific);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] stringArgs;
                SetArgumentsForFeedbackString(out stringArgs, taskName, null, null, SearchType.NONE, false);
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
                if (currentListedTasks.Contains(task))
                    currentListedTasks.Remove(task);

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
            SetArgumentsForFeedbackString(out args, taskName, startTime, endTime, searchType, isAll);

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

    }
}
