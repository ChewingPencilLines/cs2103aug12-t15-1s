using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationPostpone : Operation
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
        private DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        private SearchType searchType;
        private TimeSpan postponeDuration;
        #endregion Parameters

        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType, TimeSpan postponeDuration, SortType sortType)
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
            this.postponeDuration = postponeDuration;
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

            Func<Task, TimeSpan, Response> action = PostponeTask;
            object[] args = { postponeDuration };
            Response response = null;

            response = CheckIfIndexesAreValid(startIndex, endIndex);
            if (response != null) return response;

            if (!hasIndex)
                response = ExecuteBySearch(
                    taskName, startTime, endTime, isAll, searchType, action, args);

            else if (hasIndex)
                response = ExecuteByIndex(startIndex, endIndex, action, args);

            else
                response = new Response(Result.FAILURE, sortType, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }

        protected Response PostponeTask(Task taskToPostpone, TimeSpan postponeDuration)
        {
            if (taskToPostpone.Postpone(postponeDuration) == false)
                return new Response(Result.INVALID_TASK, sortType, this.GetType(), currentListedTasks);
            else
                executedTasks.Enqueue(taskToPostpone);
            if (storageIO.UpdateTask(taskToPostpone))
                return GenerateStandardSuccessResponse(taskToPostpone);
            else
                return GenerateXMLFailureResponse();
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = PostponeTask(taskToUndo, postponeDuration.Negate());
                if (!response.IsSuccessful())
                    return response;
            }

            if (response == null)
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = new Response(Result.FAILURE, sortType, this.GetType());

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToRedo = executedTasks.Dequeue();
                response = PostponeTask(taskToRedo, postponeDuration);
                if (!response.IsSuccessful())
                    return response;
            }

            if (response == null)
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }
    }
}
