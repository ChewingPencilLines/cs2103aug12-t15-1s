using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationDisplay : Operation
    {
        public OperationDisplay()
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
          //  Response response;
            // int numOfMatches;
            // return Search(out numOfMatches, taskList, "");            
            //response = GenerateDisplayString(taskList);
            return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), taskList);
        }
    }
}