using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDo
{
    public class OperationAdd : Operation
    {
        private Task newTask;
        public OperationAdd(Task setTask)
        {
            newTask = setTask;
        }
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response;

            if (newTask == null)
            {
                return new Response(Result.FAILURE, Format.DEFAULT, this.GetType(),  currentListedTasks);
               // return RESPONSE_ADD_FAILURE;
            }
            response = AddTask(newTask, taskList);
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
            return DeleteTask(task, taskList);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            return DeleteTask(task, taskList);
        }
    }
}
