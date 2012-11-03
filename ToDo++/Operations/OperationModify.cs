using System.Collections.Generic;

namespace ToDo
{
    class OperationModify : Operation
    {
        private int? oldIndex;
        private Task newTask;
 /*       
        public OperationModify(int Previous, Task Revised)
        {
            oldIndex = Previous - 1;
            newTask = Revised;
        }

        public OperationModify(Task Search)
        {
            newTask = Search;
        }

        public OperationModify()
        {
            oldIndex = null;
            newTask = null;
        }
*/
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
            this.storageIO = storageIO;
            Response response;
            List<Task> searchResults;

            if (oldIndex.HasValue == false && newTask == null)
            {
                currentListedTasks = taskList;
                return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            else if (oldIndex.HasValue == false && newTask != null)
            {
                searchResults = SearchForTasks(taskList, newTask.TaskName);
                currentListedTasks = searchResults;
                return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            else if (oldIndex.HasValue == true && (oldIndex < 0 || oldIndex > taskList.Count - 1))
            {
                if (newTask != null)
                {
                    searchResults = SearchForTasks(taskList, newTask.TaskName);
                    currentListedTasks = searchResults;
                    return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks);
                }
                else
                {
                    return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(),  currentListedTasks);
                }
            }
            else
            {
                if (oldIndex.Value >= 0 && oldIndex.Value <  currentListedTasks.Count)
                {
                    Task taskToModify =  currentListedTasks[oldIndex.Value];
                    if (taskToModify is TaskEvent && newTask is TaskFloating)
                    {
                        newTask = new TaskEvent(newTask.TaskName, ((TaskEvent)taskToModify).StartTime,
                            ((TaskEvent)taskToModify).EndTime, ((TaskEvent)taskToModify).isSpecific);
                    }
                    else if (taskToModify is TaskDeadline && newTask is TaskFloating)
                    {
                        newTask = new TaskDeadline(newTask.TaskName, ((TaskDeadline)taskToModify).EndTime,
                            ((TaskDeadline)taskToModify).isSpecific);
                    }
                    response = ModifyTask(taskToModify, newTask, taskList, out successFlag);
                }
                else
                    return new Response(Result.INVALID_TASK, Format.DEFAULT);
            }
            if (successFlag) TrackOperation();
            return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task taskToUndo = undoTask.Pop();
            Task previousTask = undoTask.Pop();
            redoTask.Push(taskToUndo);
            redoTask.Push(previousTask);
            return ModifyTask(taskToUndo, previousTask, taskList, out successFlag);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task taskToUndo = redoTask.Pop();
            Task previousTask = redoTask.Pop();
            undoTask.Push(taskToUndo);
            undoTask.Push(previousTask);
            return ModifyTask(taskToUndo, previousTask, taskList, out successFlag);
        }
    }
}
