using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class OperationSort : Operation
    {
        public OperationSort()
        { }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;

            List<Task> sortedTasks = (from task in lastListedTasks
                               orderby task.TaskName
                               select task).ToList();
            response = GenerateDisplayString(sortedTasks);

            TrackOperation();

            return response;
        }
    }
}
