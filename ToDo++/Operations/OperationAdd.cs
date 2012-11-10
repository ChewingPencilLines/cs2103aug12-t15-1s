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
        /// Executes the operation and adds it to the operation history.
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
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
                AddToOperationHistory();
            }
            return response;
        }

        /// <summary>
        /// Undo this operation.
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = DeleteTask(newTask);
            return response;
        }

        /// <summary>
        /// Redo this operation.
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = AddTask(newTask);
            return response;
        }
    }
}
