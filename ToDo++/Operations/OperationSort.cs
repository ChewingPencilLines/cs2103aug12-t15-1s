using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationSort : Operation
    {
        public OperationSort()
        { }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            opHandler.TrackOperation(this);
            string response;
            List<Task> sortedTasks = (from task in opHandler.LastListedTasks
                               orderby task.TaskName
                               select task).ToList();
            response = opHandler.DisplayAll(sortedTasks);
            return response;
        }
    }
}
