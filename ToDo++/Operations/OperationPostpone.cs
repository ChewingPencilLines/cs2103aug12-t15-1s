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
        private DateTime? oldTime = null, postponeTime = null;
        private DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        #endregion Parameters

        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? postponeTime, DateTimeSpecificity isSpecific, bool isAll)
        {
            this.oldTime = startTime;
            this.postponeTime = postponeTime;
            this.isAll = isAll;
            this.isSpecific = isSpecific;
            if (indexRange == null) hasIndex = false;
            else
            {
                hasIndex = true;
                this.startIndex = indexRange[TokenIndexRange.START_INDEX] - 1;
                this.endIndex = indexRange[TokenIndexRange.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;

            //alter for only one date in command as the postpone date
            if ((indexRange != null || taskName != null || isAll == true) && postponeTime == null && startTime != null)
            {
                this.oldTime = postponeTime;
                this.postponeTime = startTime;
                this.isSpecific.StartTime = isSpecific.EndTime;
                this.isSpecific.EndTime = isSpecific.StartTime;
            }
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response;

            if (currentListedTasks.Count == 0)
                return new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);

            if (!hasIndex)
                response = PostponeBySearch(taskList);
            else if (hasIndex)
                response = PostponeByIndex(taskList);
            else
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());

            if (response.IsSuccessful())
                TrackOperation();
            return response;
        }

        private Response PostponeBySearch(List<Task> taskList)
        {
            Response response;
            List<Task> searchResults;

            searchResults = GenerateSearchResult(taskList);
           
            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else if (searchResults.Count == 1)
                response = PostponeTask(searchResults[0], postponeTime);
            else
            {
                if (isAll)
                    response = PostponeAllTasks(taskList, searchResults);
                else
                {
                    currentListedTasks = new List<Task>(searchResults);
                    response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                }
            }
            return response;
        }

        private Response PostponeByIndex(List<Task> taskList)
        {
            Response response;
            if (endIndex == startIndex)
                response = PostponeSingleTask(taskList);
            else if (endIndex < 0 || endIndex > currentListedTasks.Count - 1)
                response = new Response(Result.INVALID_TASK, Format.DEFAULT);
            else
                response = PostponeMultipleTasks(taskList);

            return response;
        }
       
        private Response PostponeAllTasks(List<Task> taskList, List<Task> searchResults)
        {
            Response response = null;
            foreach (Task task in searchResults)
            {
                response = PostponeTask(task, postponeTime);
                if (!response.IsSuccessful()) return response;
            }
            return response;
        }

        private Response PostponeSingleTask(List<Task> taskList)
        {
            Task taskToPostpone = currentListedTasks[startIndex];
            if (taskToPostpone == null)
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
            else
                return PostponeTask(taskToPostpone, postponeTime);
        }

        private Response PostponeMultipleTasks(List<Task> taskList)
        {
            Response response;

            response = null;
            for (int i = startIndex; i <= endIndex; i++)
            {
                Task taskToPostpone = currentListedTasks[i];

                if (taskToPostpone == null)
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                else
                {
                    // this is a hack. delete task range properly!
                    response = PostponeTask(taskToPostpone, postponeTime);
                    if (!response.IsSuccessful()) return response;
                }
            }
            return response;
        }

        private List<Task> GenerateSearchResult(List<Task> taskList)
        {
            if (oldTime == null)
                return SearchTimedTasks(taskList);
            else
                return SearchForTasks(taskName, isSpecific, isSpecific.StartTime, oldTime);
        }

        private List<Task> SearchTimedTasks(List<Task> taskList)
        {
            List<Task> searchResults = SearchForTasks(taskName, isSpecific);
            //filter floating tasks
            return (from task in searchResults
                    where (task is TaskEvent || task is TaskDeadline)
                    select task).ToList();
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task taskToUndo = undoTask.Pop();
            Task Undonetask = undoTask.Pop();
            redoTask.Push(taskToUndo);
            redoTask.Push(Undonetask);
            Response response = ModifyTask(taskToUndo,Undonetask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Task taskToRedo = undoTask.Pop();
            Task Redonetask = undoTask.Pop();
            redoTask.Push(taskToRedo);
            redoTask.Push(Redonetask);
            Response response = ModifyTask(taskToRedo, Redonetask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
            else
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
    }
}
