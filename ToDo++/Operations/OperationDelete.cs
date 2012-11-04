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

        // @ivan -> alice: not good enough. needs to be able to delete by range of index / dates.
        // work on letting it search by dates first. i will get range of index detected soon.
        // i will also catch the "all" keyword, letting u allow all search hits to be deleted immediately.

        private int startIndex;
        private int endIndex;
        private bool hasIndex;
        private bool isAll;
        private string taskName;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;

        public OperationDelete(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll)
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
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response = null;
            List<Task> searchResults;
            
            // todo: should differentiate between nothing to delete vs. invalid index range
            if (currentListedTasks.Count == 0)
                return new Response(Result.INVALID_COMMAND, Format.DEFAULT);
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, Format.DEFAULT);

            if (!hasIndex && !isAll)
            {
                searchResults = SearchForTasks(taskList, taskName, isSpecific, isSpecific.StartTime && isSpecific.EndTime, startTime, endTime);
                if (searchResults.Count == 0)
                {
                    //check substring
                    searchResults = SearchForTasks(taskList, taskName, isSpecific);
                    if (searchResults.Count == 0)
                        response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                    else
                    {
                        currentListedTasks = new List<Task>(searchResults);
                        response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                    }
                }
                else if (searchResults.Count == 1)
                {
                    response = DeleteTask(searchResults[0], taskList);
                }
                else
                {
                    currentListedTasks = new List<Task>(searchResults);
                    response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                }
            }
            else if (isAll)
            {
                for (int i = currentListedTasks.Count-1; i >= 0; i--)
                {
                    Task task = currentListedTasks[i];
                    if (task == null)
                        response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                    else
                    {
                        response = DeleteTask(task, taskList);
                        if (!response.IsSuccessful()) return response;
                    }
                }
            }
            else
            {
                if (endIndex == startIndex)
                {
                    Task taskToDelete = currentListedTasks[startIndex];
                    if (taskToDelete == null)
                        // invalid task, already deleted
                        response = new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
                    else response = DeleteTask(taskToDelete, taskList);
                }
                else
                {
                    response = new Response(Result.INVALID_TASK, Format.DEFAULT);
                    for (int i = endIndex; i >= startIndex; i--)
                    {
                        Task taskToDelete = currentListedTasks[i];
                        if (taskToDelete == null)
                            response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType(), currentListedTasks);
                        else
                        {
                            // this is a hack. delete task range properly!
                            response = DeleteTask(taskToDelete, taskList);
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

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            redoTask.Push(task);
            return AddTask(task, taskList);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            return AddTask(task, taskList);
        }
    }
}
