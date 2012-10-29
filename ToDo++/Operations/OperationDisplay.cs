using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationDisplay : Operation
    {
        public OperationDisplay()
        { }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;
            // int numOfMatches;
            // return Search(out numOfMatches, taskList, "");            
            response = GenerateDisplayString(taskList);
            return response;
        }

        public override string Undo(List<Task> taskList, Storage storageXML)
        {
            return null;
        }
    }
}