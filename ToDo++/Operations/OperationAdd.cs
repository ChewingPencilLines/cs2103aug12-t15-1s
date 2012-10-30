using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDo
{
    class OperationAdd : Operation
    {
        private Task newTask;

        internal OperationAdd(Task setTask)
        {
            newTask = setTask;
        }

        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            string response;

            if (newTask == null) return RESPONSE_ADD_FAILURE;

            response = AddTask(newTask, taskList, out successFlag);

            if (successFlag) TrackOperation();

            return response;
        }

        public override string Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return DeleteTask(task, taskList, out successFlag);
        }
    }
}
