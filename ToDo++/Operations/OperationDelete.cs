using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ToDo
{
    public class OperationDelete : Operation
    {
        // ******************************************************************
        // Parameters
        // ******************************************************************

        #region Parameters
        private int startIndex;
        private int endIndex;
        private bool hasIndex;
        private bool isAll;
        private bool searchForIsDone;
        private string taskName;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;
        #endregion Parameters
        
        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        public OperationDelete(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, bool searchForIsDone)
        {
            if (indexRange == null) hasIndex = false;            
            else
            {
                hasIndex = true;
                this.startIndex = indexRange[TokenIndexRange.START_INDEX] - 1;
                this.endIndex = indexRange[TokenIndexRange.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
            this.isAll = isAll;
            this.searchForIsDone = searchForIsDone;
        }
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response = null;

            if (currentListedTasks.Count == 0)
                return new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);

            if (!hasIndex)
                response = DeleteBySearch(taskList);

            else if (hasIndex)
                response = DeleteByIndex(taskList);

            else if (isAll)
                response = DeleteAllDisplayedTasks(taskList);

            else
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }

        // ******************************************************************
        // Delete With Search
        // ******************************************************************

        #region DeleteWithSearch
        private Response DeleteBySearch(List<Task> taskList)
        {
            List<Task> searchResults = new List<Task>();
            Response response = null;

            searchResults = SearchForTasks(
                taskList, taskName, isSpecific, 
                isSpecific.StartTime && isSpecific.EndTime,
                startTime, endTime, searchForIsDone
                );

            // If no results and not trying to display all, try non-exact search.
            if (searchResults.Count == 0 && !isAll)
                response = TrySearchNonExact(ref searchResults, taskList);
            
            // If only one result and is searching by name, delete immediately.
            else if (searchResults.Count == 1 && !(taskName == null || taskName == ""))
                response = DeleteTask(searchResults[0], taskList);
            
            // If all keyword is used, delete all in search results if not searching empty string.
            // If not, delete all currently displayed tasks.
            else if (isAll)
            {
                if (taskName == "" || taskName == null)
                    response = DeleteAllDisplayedTasks(taskList);
                else
                    response = DeleteAllSearchResults(searchResults, taskList);
            }
            
            // If not, display search results.
            else
                response = DisplaySearchResults(searchResults);

            return response;
        }

        private Response TrySearchNonExact(ref List<Task> searchResults, List<Task> taskList)
        {
            Response response = null;
            searchResults = SearchForTasks(taskList, taskName, isSpecific);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] args;
                SetArgumentsForFeedbackString(out args, taskName, startTime, endTime, searchForIsDone, isAll);
                response =
                    new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, args);
            }
            return response;
        }

        private Response DeleteAllSearchResults(List<Task> searchResults, List<Task> taskList)
        {
            Response response = null;            
            foreach (Task task in searchResults)
            {
                if (currentListedTasks.Contains(task))
                    currentListedTasks.Remove(task);
                response = DeleteTask(task, taskList);
                if (!response.IsSuccessful()) return response;
            }
            response = new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
            return response;
        }

        private Response DisplaySearchResults(List<Task> searchResults)
        {
            currentListedTasks = new List<Task>(searchResults);

            string[] args;
            SetArgumentsForFeedbackString(out args, taskName, startTime, endTime, searchForIsDone, isAll);

            return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, args);
        }


        #endregion

        // ******************************************************************
        // Delete By Index
        // ******************************************************************

        #region DeleteByIndex
        private Response DeleteByIndex(List<Task> taskList)
        {
            Response response = null;
            if (endIndex == startIndex)
            {
                response = DeleteSingleTask(taskList);
            }
            else
            {
                return DeleteMultipleTasks(taskList);
            }
            return response;
        }

        private Response DeleteSingleTask(List<Task> taskList)
        {
            Response response = null;
            Task taskToDelete = currentListedTasks[startIndex];
            Debug.Assert(taskToDelete != null, "Tried to delete a null task!");
            response = DeleteTask(taskToDelete, taskList);
            return response;
        }

        private Response DeleteMultipleTasks(List<Task> taskList)
        {
            Response response = null;
            response = new Response(Result.INVALID_TASK);
            for (int i = endIndex; i >= startIndex; i--)
            {
                Task taskToDelete = currentListedTasks[i];
                if (taskToDelete == null)
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                else
                {
                    response = DeleteTask(taskToDelete, taskList);
                    if (!response.IsSuccessful()) return response;
                }
            }
            return new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
        }
        #endregion DeleteByIndex

        // ******************************************************************
        // Delete All
        // ******************************************************************

        #region DeleteAll
        private Response DeleteAllDisplayedTasks(List<Task> taskList)
        {
            Response response = null;
            for (int i = currentListedTasks.Count - 1; i >= 0; i--)
            {
                Task task = currentListedTasks[i];
                if (task == null)
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                else
                {
                    response = DeleteTask(task, taskList);
                    if (!response.IsSuccessful()) return response;
                }
            }
            return response;
        }
        #endregion

        #endregion

        // ******************************************************************
        // Overrides for Undoing and Redoing this operation
        // ******************************************************************

        #region UndoRedo
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            redoTask.Push(task);
            Response response = AddTask(task, taskList);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            Response response = DeleteTask(task, taskList);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
        #endregion
    }
}
