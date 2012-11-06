using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ToDo
{
    class OperationMarkAsUndone : Operation
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
        public OperationMarkAsUndone(string taskName, int[] indexRange, DateTime? startTime,
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

            Func<Task, Response> action = MarkAsUndone;
            object[] args = null;
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
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }

        private Response MarkAsUndone(Task taskToMarkAsUndone)
        {
            SetMembers(taskList, storageIO);
            undoTask.Push(taskToMarkAsUndone);
            taskToMarkAsUndone.DoneState = false;

            if (storageIO.MarkTaskAs(taskToMarkAsUndone, false))
            {
                return GenerateSuccessResponse(taskToMarkAsUndone);
            }
            else
                return GenerateXMLFailureResponse();
        }

        #endregion

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task task = undoTask.Pop();
            redoTask.Push(task);
            task.DoneState = true;
            if (storageIO.MarkTaskAs(task, true))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo),  currentListedTasks);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            task.DoneState = false;
            if (storageIO.MarkTaskAs(task, false))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
    } 
}

