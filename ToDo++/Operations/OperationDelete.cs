﻿using System;
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
        private string taskName;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;
        #endregion Parameters
        
        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        public OperationDelete(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll)
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

            // todo: should differentiate between nothing to delete vs. invalid index range
            if (currentListedTasks.Count == 0)
                return new Response(Result.INVALID_COMMAND, Format.DEFAULT);
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

            searchResults = SearchForTasks(taskList, taskName, isSpecific, isSpecific.StartTime && isSpecific.EndTime, startTime, endTime);
            if (searchResults.Count == 0)
                response = TrySearchBySubstring(ref searchResults, taskList);

            else if (searchResults.Count == 1)
                response = DeleteTask(searchResults[0], taskList);

            else if (isAll)
                response = DeleteAllDisplayedTasks(searchResults, taskList);

            else
                response = DisplaySearchResults(searchResults);

            return response;
        }

        private Response TrySearchBySubstring(ref List<Task> searchResults, List<Task> taskList)
        {
            Response response = null;
            searchResults = SearchForTasks(taskList, taskName, isSpecific);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else
            {
                currentListedTasks = new List<Task>(searchResults);
                response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
            }

            return response;
        }

        private Response DeleteAllDisplayedTasks(List<Task> searchResults, List<Task> taskList)
        {
            Response response = null;
            foreach (Task task in searchResults)
            {
                if (currentListedTasks.Contains(task))
                    currentListedTasks.Remove(task);
                response = DeleteTask(task, taskList);
                if (!response.IsSuccessful()) return response;
            }
            response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
            return response;
        }

        private Response DisplaySearchResults(List<Task> searchResults)
        {
            currentListedTasks = new List<Task>(searchResults);
            return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks); return response;
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
            response = new Response(Result.INVALID_TASK, Format.DEFAULT);
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
            return response;
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
            return AddTask(task, taskList);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            return AddTask(task, taskList);
        }
        #endregion
    }
}
