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
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType, TimeSpan postponeDuration)
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

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Func<Task, TimeSpan, Response> action = PostponeTask;
            object[] args = null;
            Response response = null;

            response = CheckIfIndexesAreValid(startIndex, endIndex);
            if (response != null) return response;

            if (!hasIndex)
                response = ExecuteBySearch(
                    taskName, isSpecific.StartTime && isSpecific.EndTime,
                    startTime, endTime, isSpecific, isAll, SearchType.NONE, action, args);

            else if (hasIndex)
                response = ExecuteByIndex(startIndex, endIndex, action, args);

            else
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();

            return response;
        }

        protected Response PostponeTask(Task taskToPostpone, TimeSpan postponeDuration)
        {
            if (taskToPostpone.Postpone(postponeDuration) == false)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
            if (storageIO.UpdateTask(taskToPostpone))
                return GenerateStandardSuccessResponse(taskToPostpone);
            else
                return GenerateXMLFailureResponse();
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            throw new NotImplementedException();
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            throw new NotImplementedException();
        }
    }
}
