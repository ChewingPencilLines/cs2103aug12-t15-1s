using System.Collections.Generic;
using System;

namespace ToDo
{
    class OperationModify : Operation
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
        #endregion

        #region Task Tracking
        private Task oldTask;
        private Task newTask;
        #endregion

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        public OperationModify(string taskName, int[] indexRange, DateTime? startTime,
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

        /*
        *  If a user keys in nothing or only index or only task details
        *  after the commandtype, then all tasks will be shown.
        *  only when user input an index + information will a task be modified.
        */
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            if (!hasIndex)
            {
                // search for task
                SetMembers(taskList, storageIO);
                List<Task> searchResults = SearchForTasks(taskName, isSpecific, false, startTime, endTime, searchType);
                currentListedTasks = new List<Task>(searchResults);

                string[] criteria;
                SetArgumentsForFeedbackString(out criteria, taskName, startTime, endTime, searchType);

                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, criteria);
            }
            else
            {                
                response = CheckIfIndexesAreValid(startIndex, endIndex);
                if (response != null) return response;

                if (startIndex != endIndex || isAll == true)
                {
                    return new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType());
                }

                oldTask = currentListedTasks[startIndex];
                if (taskName == null || taskName == String.Empty)
                {
                    // copy over taskName from indexed task if didn't specify a name
                    taskName = oldTask.TaskName;
                }
                else if (startTime == null && endTime == null)
                {
                    // copy over all times from indexed task
                    oldTask.CopyDateTimes(ref startTime, ref endTime, ref isSpecific);
                }
                newTask = Task.GenerateNewTask(taskName, startTime, endTime, isSpecific);

                response = ModifyTask(oldTask, newTask);
            }

            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
        }

        protected Response ModifyTask(Task taskToModify, Task newTask)
        {
            Response response = null;
            response = DeleteTask(taskToModify);
            if (response.IsSuccessful()) response = AddTask(newTask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationModify), currentListedTasks);
            else
                return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = ModifyTask(newTask, oldTask);
            return response;
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = ModifyTask(oldTask, newTask);
            return response;
        }
    }
}
