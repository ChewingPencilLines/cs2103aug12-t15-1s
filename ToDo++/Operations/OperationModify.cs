using System.Collections.Generic;
using System;

namespace ToDo
{
    class OperationModify : Operation
    {
        private int? oldIndex;
        private Task newTask;

        public OperationModify(int[] indexRange, Task newTask)
        {
            if (indexRange == null) this.oldIndex = null;
            else this.oldIndex = indexRange[TokenIndexRange.START_INDEX] - 1;
            if (newTask.TaskName == null) this.newTask = null;
            else this.newTask = newTask;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            /*
             *  when modify, if user key in nothing or only index or only task details
             *  after the commandtype, then all tasks will be shown.
             *  only when user input full information will modify operated.
             */
            SetMembers(taskList, storageIO);

            Response response;
            List<Task> searchResults;
            DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            if (oldIndex.HasValue == false && newTask == null)
            {
                currentListedTasks = new List<Task>(taskList);
                response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            else if (oldIndex.HasValue == false && newTask != null)
            {
                searchResults = SearchForTasks(newTask.TaskName, isSpecific);
                currentListedTasks = searchResults;
                response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            else if (oldIndex.HasValue == true && (oldIndex < 0 || oldIndex > taskList.Count - 1))
            {
                if (newTask != null)
                {
                    searchResults = SearchForTasks(newTask.TaskName, isSpecific);
                    currentListedTasks = new List<Task>(searchResults);
                    response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                }
                else
                {
                    response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
                }
            }
            else
            {
                if (oldIndex.Value >= 0 && oldIndex.Value <  currentListedTasks.Count)
                {
                    Task taskToModify =  currentListedTasks[oldIndex.Value];
                    if (taskToModify is TaskEvent && newTask is TaskFloating)
                    {
                        newTask = new TaskEvent(newTask.TaskName, ((TaskEvent)taskToModify).StartDateTime,
                            ((TaskEvent)taskToModify).EndDateTime, ((TaskEvent)taskToModify).isSpecific);
                    }
                    else if (taskToModify is TaskDeadline && newTask is TaskFloating)
                    {
                        newTask = new TaskDeadline(newTask.TaskName, ((TaskDeadline)taskToModify).EndDateTime,
                            ((TaskDeadline)taskToModify).isSpecific);
                    }
                    response = ModifyTask(taskToModify, newTask);
                }
                else
                    response = new Response(Result.INVALID_TASK, Format.DEFAULT);
            }

            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
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
