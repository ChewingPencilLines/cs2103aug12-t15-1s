using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            EventHandlers.UpdateSettingsHandler += UpdateSettings;

            stringParser = new StringParser();
            commandParser = new CommandParser(ref stringParser);

            taskList = storage.LoadTasksFromFile();
            if (taskList == null) PromptUser_CreateNewTaskFile();

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

        private void PromptUser_CreateNewTaskFile()
        {
            CustomMessageBox.Show("Error!", "Task storage file seems corrupted. Error reading from it! Create new file?");
            DialogResult dialogResult = MessageBox.Show("Sure", "Create new task file?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                storage.CreateNewTaskFile();
            }
            else if (dialogResult == DialogResult.No)
            {
                AlertBox.Show("Exiting application..");
                Application.Exit();
            }
        }

        /// <summary>
        /// This method writes current settings to file
        /// </summary>
        /// <param name="settingsList"></param>
        /// <returns></returns>
        public bool UpdateSettingsFile(SettingInformation settings)
        {
            return storage.WriteSettingsToFile(settings);
        }

        private void UpdateSettings(object sender, EventArgs args)
        {
            UpdateSettingsFile((SettingInformation)sender);
        }

    } 
}
