using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public class Logic
    {
        OperationHandler operationHandler;
        CommandParser commandParser;
        StringParser stringParser;
        Storage storage;
        List<Task> taskList;
  
        public Logic()
        {
            storage = new Storage("testfile.xml", "testsettings.xml");
            operationHandler = new OperationHandler(storage);
            stringParser = new StringParser();
            commandParser = new CommandParser(ref stringParser);
            taskList = storage.LoadTasksFromFile();
        }

        public string ProcessCommand(string input)
        {
            Operation operation = ParseCommand(input);
            return ExecuteCommand(operation);
        }

        /// <summary>
        /// This method updates settings to the various components using the specified SettingsList.
        /// </summary>
        /// <param name="settingsList"></param>
        /// <returns></returns>
        public bool UpdateSettings()
        {
            return true;
        }

        /// <summary>
        /// This method loads settings to the specified SettingsList from storage.
        /// </summary>
        /// <param name="settingsList"></param>
        /// <returns></returns>
        public void LoadSettings()
        {
            return;
        }

        private string ExecuteCommand(Operation operation)
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
