using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public class Logic
    {
        CommandParser commandParser;
        StringParser stringParser;
        Settings mainSettings;

        public Settings MainSettings
        {
            get { return mainSettings; }
            set { mainSettings = value; }
        }
        Storage storage;
        List<Task> taskList;
  
        public Logic()
        {
            mainSettings = new Settings();
            storage = new Storage("testfile.xml", "testsettings.xml");
            mainSettings.UpdateSettings(storage.LoadSettingsFromFile());
            stringParser = new StringParser();
            commandParser = new CommandParser(ref stringParser);
            taskList = storage.LoadTasksFromFile();
        }

        public string ProcessCommand(string input)
        {
            Operation operation = null;
            try
            {
                operation = ParseCommand(input);
            }
            catch (InvalidDateTimeException e)
            {
                AlertBox.Show(e.Message);
            }
            if (operation == null) return Operation.REPONSE_INVALID_COMMAND;
            else return ExecuteCommand(operation);
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

        private string ExecuteCommand(Operation operation)
        {
            string response;
            response = operation.Execute(taskList, storage);
            return response;
        }

        /// <summary>
        /// This method writes current settings to file
        /// </summary>
        /// <param name="settingsList"></param>
        /// <returns></returns>
        public bool UpdateSettingsFile(SettingsList settings)
        {
            return storage.WriteSettingsToFile(settings);
        }

    } 
}
