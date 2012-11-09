using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    class OperationMarkAsDone : Operation
    {
        // ******************************************************************
        // Parameters
        // ******************************************************************

        #region Parameters
        private string taskName;
        private int startIndex;
        private int endIndex;
        private bool hasIndex;
        private bool isAll;
        private SearchType searchType;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;
        #endregion Parameters

        // ******************************************************************
        // Constructors
        // ******************************************************************

        /// <summary>
        /// This is the base constructor for the MarkAsDone operation.
        /// There are three ways to execute this operation in the following priorities.
        /// By using a single date to mark all tasks on that date.
        /// by searching the task name against a string,
        /// and by using the current display index of the task.
        /// </summary>
        /// <param name="doneString">The name of the task to mark as done. Can be a substring of it.</param>
        /// <param name="indexRange">The display index of the task to be marked.</param>
        /// <param name="doneDate">The date in which to mark all tasks as done.</param>
        /// <param name="isAll">If this boolean is true, the current displayed tasks will all be marked as done.</param>
        /// <returns></returns>
        public OperationMarkAsDone(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType, SortType sortType)
            : base(sortType)
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
            this.searchType = searchType;
        }

        public override bool AllowSkipOver(Response response)
        {
            if (response.IsInvalidTask())
                return true;
            else return false;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Func<Task, bool, Response> action = MarkTaskAs;
            object[] args = { (bool)true };

            Response response = null;

            response = CheckIfIndexesAreValid(startIndex, endIndex);
            if (response != null) return response;

            if (!hasIndex)
                response = ExecuteBySearch(
                    taskName, isSpecific.StartTime && isSpecific.EndTime,
                    startTime, endTime, isSpecific, isAll, searchType, action, args);

            else if (hasIndex)
                response = ExecuteByIndex(startIndex, endIndex, action, args);

            else
                response = new Response(Result.FAILURE, sortType, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = MarkTaskAs(taskToUndo, false);
                if (!response.IsSuccessful())
                    return response;
            }
            
            if (response == null ) 
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = MarkTaskAs(taskToUndo, true);
                if (!response.IsSuccessful())
                    return response;
            }

            if (response == null)
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }
    } 
}
