using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Logic
    {
        OperationHandler operationHandler;
        List<Task> taskList;
  
        public Logic()
        { 
            operationHandler = new OperationHandler();
            taskList = new List<Task>();
        }

        public string ProcessCommand(string input)
        {
            Operation operation = ParseCommand(input);
            return ExecuteCommand(operation);
        }

        public bool UpdateSettings()
        {
            throw new NotImplementedException();
        }

        public string ExecuteCommand(Operation operation)
        {
            string response;
            response = operationHandler.Execute(operation, ref taskList);
            return response;
        }

        private Operation ParseCommand(string command)
        {
            if (command.Equals(null))
            {
                return null;
            }
            else
            {
                CommandParser CP = new CommandParser();
                Operation derivedOperation = CP.ParseOperation(command);
                return derivedOperation;
            }
        }
    }
 
}
