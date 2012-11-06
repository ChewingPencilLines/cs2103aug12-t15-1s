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
            this.storageIO = storageIO;
            Response response;
            List<Task> searchResults;
            if (startIndex == null)
            {
                if (oldTime == null)
                {
                    searchResults = SearchForTasks(taskList, taskName, isSpecific);
                    //filter floating tasks
                    searchResults = (from task in searchResults
                                     where (task is TaskEvent || task is TaskDeadline)
                                     select task).ToList();
                }
                else
                {
                    searchResults = SearchForTasks(taskList, taskName, isSpecific, isSpecific.StartTime, oldTime);
                }

                if (searchResults.Count == 0)
                {
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                  //  response = RESPONSE_POSTPONE_FAILURE;
                }
                else if (searchResults.Count == 1)
                {
                    response = PostponeTask(searchResults[0], taskList, postponeTime);
                }
                else
                {
                    if (isAll)
                    {
                        response = null;
                        foreach (Task task in searchResults)
                        {
                            response = PostponeTask(task, taskList, postponeTime);
                            if (!response.IsSuccessful()) return response;
                        }
                    }
                    else
                    {
                        currentListedTasks = new List<Task>(searchResults);
                        response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                    }
                }
            }
            else if (startIndex < 0 || startIndex > currentListedTasks.Count - 1)
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT);
            }
            else
            {
                if (endIndex == startIndex)
                {
                    Task taskToPostpone = currentListedTasks[startIndex];
                    if (taskToPostpone == null)
                        response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                    else
                        response = PostponeTask(taskToPostpone, taskList, postponeTime);
                }
                else if (endIndex < 0 || endIndex > currentListedTasks.Count - 1)
                {
                    response = new Response(Result.INVALID_TASK, Format.DEFAULT);
                }
                else
                {
                    response = null;
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        Task taskToPostpone = currentListedTasks[i];

                        if (taskToPostpone == null) 
                            response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                        else
                        {
                            // this is a hack. delete task range properly!
                            response = PostponeTask(taskToPostpone, taskList, postponeTime); 
                            if (!response.IsSuccessful()) return response;
                        }
                    }
                }
            }
            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
        }
    }
}
