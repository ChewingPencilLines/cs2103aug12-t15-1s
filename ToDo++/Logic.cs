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
                string response;
                response = operationHandler.Execute(operation);
                return response;
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
        private string TranslateResult(Result r)
        {
            string print;
            switch (r.ToString())
            {
                case "ADD_SUCCESS":
                    print = "Added task successfully.";
                    break;
                case "DELETE_SUCCESS":
                    print = "Deleted task successfully.";
                    break;
                case "MODIFY_SUCCESS":
                    print = "Modified task successfully.";
                    break;
                case "UNDO_SUCCESS":
                    print = "Undone task successfully.";
                    break;
                case "SEARCH_SUCCESS":
                    print = "The result you search appear above";
                    break;
                case "ERROR":
                    print = "Command failed.";
                    break;
                default:
                    print = "Command failed.";
                    break;
            }
            return print;
        }*/
    }
 
}
