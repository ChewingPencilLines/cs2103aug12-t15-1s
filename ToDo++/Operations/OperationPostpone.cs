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

        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime, DateTime? postponeTime)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenCommand.START_INDEX] - 1;
                this.endindex = indexRange[TokenCommand.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
            this.oldTime = startTime;
            this.postponeTime = postponeTime;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;

            List<Task> searchResults;
            if (index == null)
            {
                searchResults = SearchForTasks(taskList, taskName, false, oldTime);
                if (searchResults.Count == 0)
                {
                    //check substring
                    searchResults = SearchForTasks(taskList, taskName, false, oldTime);
                    if (searchResults.Count == 0)
                        response = RESPONSE_POSTPONE_FAILURE;
                    else response = GenerateDisplayString(searchResults);
                }
                else if (searchResults.Count == 1)
                {
                   // response = DeleteTask(searchResults[0], taskList, out successFlag);
                    throw new NotImplementedException();
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
                        throw new NotImplementedException();
                       // response = DeleteTask(taskToPostpone, taskList, out successFlag);
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
                            throw new NotImplementedException();
                            //response += DeleteTask(taskToDelete, taskList, out successFlag);
                        response += '\n';
                    }
                }
            }

            if (successFlag) TrackOperation();
            return response;
        }
    }
}
