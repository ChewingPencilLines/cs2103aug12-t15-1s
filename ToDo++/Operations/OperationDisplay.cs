using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationDisplay : Operation
    {
        public OperationDisplay()
        { }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;
            // int numOfMatches;
            // return opHandler.Search(out numOfMatches, taskList, "");            
            response = opHandler.DisplayAll(taskList);
            return response;
        }
    }
}