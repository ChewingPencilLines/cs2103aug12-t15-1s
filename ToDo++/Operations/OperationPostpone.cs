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
        private DateTimeSpecificity isSpecific;

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
                this.index = indexRange[TokenRange.START_INDEX] - 1;
                this.endindex = indexRange[TokenRange.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
        }

        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            string response;
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
                    response = RESPONSE_POSTPONE_FAILURE;
                }
                else if (searchResults.Count == 1)
                {
                    response = PostponeTask(searchResults[0], taskList, postponeTime, out successFlag);
                }
                else response = GenerateDisplayString(searchResults);
            }
            else if (index < 0 || index > lastListedTasks.Count - 1)
            {
                return RESPONSE_INVALID_TASK_INDEX;
            }
            else
            {
                if (endindex == index)
                {
                    Task taskToPostpone = lastListedTasks[index.Value];
                    if (taskToPostpone == null)
                        return RESPONSE_POSTPONE_FAILURE;
                    else
                        response = PostponeTask(taskToPostpone, taskList, postponeTime, out successFlag);
                }
                else if (endindex < 0 || endindex > lastListedTasks.Count - 1)
                {
                    return RESPONSE_INVALID_TASK_INDEX;
                }
                else
                {
                    response = null;
                    for (int? i = index; i <= endindex; i++)
                    {
                        Task taskToPostpone = lastListedTasks[i.Value];
                        if (taskToPostpone == null) response += RESPONSE_POSTPONE_FAILURE;
                        else
                            response += PostponeTask(taskToPostpone, taskList, postponeTime, out successFlag);
                        response += '\n';
                    }
                }
            }

            if (successFlag) TrackOperation();
            return response;
        }
    }
}
