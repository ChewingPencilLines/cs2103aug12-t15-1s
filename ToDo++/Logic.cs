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
            //string command = ValidateCommand(input);
            Operation operation = ParseCommand(input);
            return ExecuteCommand(operation);
        }

        public bool UpdateSettings()
        {
            throw new NotImplementedException();
        }

        private string ExecuteCommand(Operation operation)
        {
            if (operation.Equals(null))
            {
                return null;
            }
            else
            {
                Responses response = operationHandler.ExecuteOperation(operation);
                return null;
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

        /*
        private string ValidateCommand(string command)
        {
            if (!command.Equals(null) & command.Length == 0)
            {
                return null;
            }
            else
            {
                CommandStack.Push(command);
                return command;
            }
        } */
    }
}
