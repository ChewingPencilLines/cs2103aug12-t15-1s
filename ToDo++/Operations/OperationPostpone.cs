using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationPostpone : Operation
    {
        private int? index;
        private int? endindex;
        private string taskName;
        private DateTime? oldTime = null, postponeTime = null;
        private DateTimeSpecificity isSpecific = new DateTimeSpecificity();

        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime, DateTime? postponeTime, DateTimeSpecificity isSpecific)
        {
            this.oldTime = startTime;
            this.postponeTime = postponeTime;

            //alter for only one date in command as the postpone date
            if ((indexRange != null || taskName != null) && postponeTime == null && startTime != null)
            {
                this.oldTime = postponeTime;
                this.postponeTime = startTime;
                this.isSpecific.StartTime = isSpecific.EndTime;
                this.isSpecific.EndTime = isSpecific.StartTime;
            }
            this.isSpecific = isSpecific;
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenIndexRange.START_INDEX] - 1;
                this.endindex = indexRange[TokenIndexRange.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response;
            List<Task> searchResults;
            if (index == null)
            {
                if (oldTime == null)
                {
                    searchResults = SearchForTasks(taskList, taskName);
                    //filter floating tasks
                    searchResults = (from task in searchResults
                                     where (task is TaskEvent || task is TaskDeadline)
                                     select task).ToList();
                }
                else
                {
                    searchResults = SearchForTasks(taskList, taskName, isSpecific.StartTime, oldTime);
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
                    currentListedTasks = searchResults;
                    response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                }
            }
            else if (index < 0 || index > currentListedTasks.Count - 1)
            {
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            }
            else
            {
                if (endindex == index)
                {
                    Task taskToPostpone = currentListedTasks[index.Value];
                    if (taskToPostpone == null)
                        response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                    else
                        response = PostponeTask(taskToPostpone, taskList, postponeTime);
                }
                else if (endindex < 0 || endindex > currentListedTasks.Count - 1)
                {
                    return new Response(Result.INVALID_TASK, Format.DEFAULT);
                }
                else
                {
                    response = null;
                    for (int? i = index; i <= endindex; i++)
                    {
                        Task taskToPostpone = currentListedTasks[i.Value];

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

            if (response.IsSuccessful()) TrackOperation();
            return response;
        }
    }
}
