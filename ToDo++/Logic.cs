using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Logic
    {
        OperationHandler operationHandler; 
       
        public Logic()
        {
            operationHandler = new OperationHandler();
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
            if (operation == null)
            {
                return null;
            }
            else
            {
                Result response;
                response = operationHandler.Execute(operation);
                return response.ToString();
            }
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
                return CP.ParseOperation(command);
            }
        }
    }
}
