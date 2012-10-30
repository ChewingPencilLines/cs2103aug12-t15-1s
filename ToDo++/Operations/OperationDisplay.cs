using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationDisplay : Operation
    {
        public OperationDisplay()
        { }

        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            string response;
            // int numOfMatches;
            // return Search(out numOfMatches, taskList, "");            
            response = GenerateDisplayString(taskList);
            return response;
        }
    }
}