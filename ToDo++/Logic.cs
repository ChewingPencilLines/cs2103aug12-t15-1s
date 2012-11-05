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
            while (taskList == null)
            {
                PromptUser_CreateNewTaskFile();
                taskList = storage.LoadTasksFromFile();
            }
        }

        public Response ProcessCommand(string input)
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
            catch (MultipleCommandsException e)
            {
                AlertBox.Show(@"Invalid input.\r\n
                            Multiple commands were entered that could not be resolved.\r\n
                            Use delimiting characters if reserved keywords are required in your task name.");
            }
            if (operation == null)
            {
                return new Response(Result.INVALID_COMMAND);
            }
            else
            {
                return ExecuteCommand(operation);
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
                Operation derivedOperation = commandParser.ParseOperation(command);
                return derivedOperation;
            }
        }

        private Response ExecuteCommand(Operation operation)
        {
            Response response;
            response = operation.Execute(taskList, storage);
            return response;
        }

        private void PromptUser_CreateNewTaskFile()
        {
            UserInputBox.Show("Error!", "Task storage file seems corrupted. Error reading from it!\r\n Create new file?");
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


        internal Response GetDefaultView()
        {
            return new OperationDisplayDefault().Execute(taskList, storage);
        }

        internal void UpdateLastDisplayedTasksList(List<Task> displayedList)
        {
            Operation.UpdateCurrentListedTasks(displayedList);
        }
    } 
}
