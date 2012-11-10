using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDo
{
    public class OperationAdd : Operation
    {
        private Task newTask;

        /// <summary>
        /// Derived constructor to create an Add Operation.
        /// </summary>
        /// <param name="addTask">The task to add when this Operation is executed.</param>
        /// <param name="sortType">The type of sorting to use on the displayed task after executing this Operation.</param>
        /// <returns></returns>
        public OperationAdd(Task addTask, SortType sortType)
            : base(sortType)
        {
            newTask = addTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskList"></param>
        /// <param name="storageIO"></param>
        /// <returns></returns>
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
