using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationAdd : Operation
    {
        private Task newTask;

        internal OperationAdd(Task setTask)
        {
            newTask = setTask;
        }

        internal Task NewTask
        {
            get { return newTask; }
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;

            if (newTask == null) return RESPONSE_ADD_FAILURE;
            response = opHandler.Add(newTask, taskList, out successFlag);
            if (successFlag) opHandler.TrackOperation(this);
            return response;
        }

    }
}
