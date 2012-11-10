using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDo
{
    public class OperationAdd : Operation
    {
        private Task newTask;
        public OperationAdd(Task setTask, SortType sortType)
            : base(sortType)
        {
            newTask = setTask;
        }
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response;
            if (newTask == null)
            {
                return new Response(Result.FAILURE, sortType, this.GetType());
            }
            response = AddTask(newTask);
            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = DeleteTask(newTask);
            return response;
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = AddTask(newTask);
            return response;
        }
    }
}
