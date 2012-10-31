using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationDelete : Operation
    {

        // @ivan -> alice: not good enough. needs to be able to delete by range of index / dates.
        // work on letting it search by dates first. i will get range of index detected soon.
        // i will also catch the "all" keyword, letting u allow all search hits to be deleted immediately.

        private int? index;
        private int? endindex;
        private string taskName;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;

        public OperationDelete(string taskName, int[] indexRange, DateTime? startTime, DateTime? endTime, DateTimeSpecificity isSpecific)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenCommand.START_INDEX] - 1;
                this.endindex = indexRange[TokenCommand.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
        }

        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            string response;

            List<Task> searchResults;
            if (index == null)
            {
                searchResults = SearchForTasks(taskList, taskName, isSpecific.StartTime && isSpecific.EndTime, startTime, endTime);
                if (searchResults.Count == 0)
                {
                    //check substring
                    searchResults = SearchForTasks(taskList, taskName);
                    if (searchResults.Count == 0)
                        response = RESPONSE_DELETE_FAILURE;
                    else response = GenerateDisplayString(searchResults);
                }
                else if (searchResults.Count == 1)
                {
                    response = DeleteTask(searchResults[0], taskList, out successFlag);
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
                    Task taskToDelete = lastListedTasks[index.Value];
                    if (taskToDelete == null)
                        return RESPONSE_DELETE_ALREADY;
                    else response = DeleteTask(taskToDelete, taskList, out successFlag);
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
                        Task taskToDelete = lastListedTasks[i.Value];
                        if (taskToDelete == null) response += RESPONSE_DELETE_ALREADY;
                        else response += DeleteTask(taskToDelete, taskList, out successFlag);
                        response += '\n';
                    }
                }
            }

            if (successFlag) TrackOperation();
            return response;
        }

        public override string Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return AddTask(task, taskList, out successFlag);
        }
    }
}
