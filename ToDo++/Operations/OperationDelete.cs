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

        #region Constructors
        public OperationDelete(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType)
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
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            Func<Task, Response> action = DeleteTask;
            object[] args = null;

            // No tasks to delete
            if (taskList.Count == 0)
                return new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);

            if (!hasIndex)
                response = ExecuteBySearch(
                    taskName, isSpecific.StartTime && isSpecific.EndTime,
                    startTime, endTime, isSpecific, isAll, searchType, action, args);

            else if (hasIndex)
            {
                response = ExecuteByIndex(startIndex, endIndex, action, args);
            }

            else
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }
        #endregion

        // ******************************************************************
        // Overrides for Undoing and Redoing this operation
        // ******************************************************************

        #region UndoRedo
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task task = undoTask.Pop();
            redoTask.Push(task);
            Response response = AddTask(task);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task task = redoTask.Pop();
            undoTask.Push(task);
            Response response = DeleteTask(task);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
        #endregion
    }
}
