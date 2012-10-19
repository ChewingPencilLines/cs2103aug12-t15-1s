using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Logic
    {
        OperationHandler operationHandler;
        CommandParser commandParser;
        StringParser stringParser;
        Storage storage;
        List<Task> taskList;
  
        public Logic()
        {
            storage = new Storage("tasklist.xml", "settingstest.xml");
            operationHandler = new OperationHandler(ref storage);
            stringParser = new StringParser();
            commandParser = new CommandParser(ref stringParser); 
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
                Operation derivedOperation = commandParser.ParseOperation(command);
                return derivedOperation;
            }
        }
    }
 
}
