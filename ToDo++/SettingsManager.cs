using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToDo
{
    public class SettingsList
    {
        public bool loadOnStartup;
        public bool startMinimized;

        public List<string> customKeywords_ADD;
        public List<string> customKeywords_DELETE;
        public List<string> customKeywords_UPDATE;
        public List<string> customKeywords_UNDO;
        public List<string> customKeywords_REDO;

        public SettingsList()
        {
            loadOnStartup = true;
            startMinimized = true;
            customKeywords_ADD = new List<string>();
            customKeywords_DELETE = new List<string>();
            customKeywords_UPDATE = new List<string>();
            customKeywords_UNDO = new List<string>();
            customKeywords_REDO = new List<string>();
        }
    }

    public enum Commands { ADD = 1, DELETE, UPDATE, UNDO, REDO, NONE };

    public class SettingsManager
    {
        string fileName;
        private SettingsList settingsList;

        #region CommandEnumManager

        public Commands StringToCommand(string commandString)
        {
            switch (commandString)
            {
                case "ADD":
                    return Commands.ADD;
                case "DELETE":
                    return Commands.DELETE;
                case "UPDATE":
                    return Commands.UPDATE;
                case "UNDO":
                    return Commands.UNDO;
                case "REDO":
                    return Commands.REDO;
            }

            return Commands.NONE;
        }

        public string CommandToString(Commands commandInput)
        {
            switch (commandInput)
            {
                case Commands.ADD:
                    return "ADD";
                case Commands.DELETE:
                    return "DELETE";
                case Commands.UPDATE:
                    return "UPDATE";
                case Commands.UNDO:
                    return "UNDO";
                case Commands.REDO:
                    return "REDO";
            }

            return "NONE";
        }

        #endregion

        public SettingsManager()
        {
            fileName = "TEST.xml";
            settingsList = new SettingsList();

            /* Used for Writing test data to xml before running*/
            //SetUpCommands();
            //WriteToFile("TEST.xml");

            OpenFile();
        }

        public SettingsManager(SettingsManager passedSettingsManager)
        {
            fileName = "TEST.xml";
            settingsList = new SettingsList();
            Clone(passedSettingsManager);
        }

        #region StartupMinimizedStatus


        public void ToggleLoadOnStartup(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.loadOnStartup = true;
            else
                settingsList.loadOnStartup = false;

            WriteToFile();
        }

        public void ToggleStartMinimized(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.startMinimized = true;
            else
                settingsList.startMinimized = false;

            WriteToFile();
        }

        public bool GetLoadOnStartupStatus()
        {
            return settingsList.loadOnStartup;
        }

        public bool GetStartMinimizedStatus()
        {
            return settingsList.startMinimized;
        }

#endregion

        public void AddCommand(string newCommand, Commands commandType)
        {
            switch (commandType)
            {
                case Commands.ADD:
                    settingsList.customKeywords_ADD.Add(newCommand);
                    break;
                case Commands.DELETE:
                    settingsList.customKeywords_DELETE.Add(newCommand);
                    break;
                case Commands.UPDATE:
                    settingsList.customKeywords_UPDATE.Add(newCommand);
                    break;
                case Commands.UNDO:
                    settingsList.customKeywords_UNDO.Add(newCommand);
                    break;
                case Commands.REDO:
                    settingsList.customKeywords_REDO.Add(newCommand);
                    break;
            }
        }

        public void RemoveCommand(string commandToRemove, Commands commandType)
        {
            switch (commandType)
            {
                case Commands.ADD:
                    settingsList.customKeywords_ADD.Remove(commandToRemove);
                    break;
                case Commands.DELETE:
                    settingsList.customKeywords_DELETE.Remove(commandToRemove);
                    break;
                case Commands.UPDATE:
                    settingsList.customKeywords_UPDATE.Remove(commandToRemove);
                    break;
                case Commands.UNDO:
                    settingsList.customKeywords_UNDO.Remove(commandToRemove);
                    break;
                case Commands.REDO:
                    settingsList.customKeywords_REDO.Remove(commandToRemove);
                    break;
            }
        }

        public List<string> GetCommand(Commands commandType)
        {
            List<string> getCommands = new List<string>();
            switch (commandType)
            {
                case Commands.ADD:
                    getCommands = settingsList.customKeywords_ADD;
                    break;
                case Commands.DELETE:
                    getCommands = settingsList.customKeywords_DELETE;
                    break;
                case Commands.UPDATE:
                    getCommands = settingsList.customKeywords_UPDATE;
                    break;
                case Commands.UNDO:
                    getCommands = settingsList.customKeywords_UNDO;
                    break;
                case Commands.REDO:
                    getCommands = settingsList.customKeywords_REDO;
                    break;
            }

            return getCommands;
        }

        //Just a Test Function
        public void SetUpCommands()
        {
            ToggleLoadOnStartup(true);
            ToggleStartMinimized(true);
            AddCommand("+", Commands.ADD);
            AddCommand("-", Commands.DELETE);
        }

        public Commands CheckIfCommandExists(string userCommand)
        {
            foreach (string compare in settingsList.customKeywords_ADD)
                if (userCommand == compare)
                    return Commands.ADD;
            foreach (string compare in settingsList.customKeywords_DELETE)
                if (userCommand == compare)
                    return Commands.DELETE;
            foreach (string compare in settingsList.customKeywords_UPDATE)
                if (userCommand == compare)
                    return Commands.UPDATE;
            foreach (string compare in settingsList.customKeywords_UNDO)
                if (userCommand == compare)
                    return Commands.UNDO;
            foreach (string compare in settingsList.customKeywords_REDO)
                if (userCommand == compare)
                    return Commands.REDO;

            return Commands.NONE;
        }

        public void WriteToFile()
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                fileName);
            writer.Serialize(file, settingsList);
            file.Close();
        }

        public void OpenFile()
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));

            System.IO.StreamReader file = new System.IO.StreamReader(
                fileName);
            settingsList = (SettingsList)writer.Deserialize(file);

            file.Close();
        }

        public void Clone(SettingsManager passedSettingsManager)
        {
            this.settingsList.loadOnStartup = passedSettingsManager.settingsList.loadOnStartup;
            this.settingsList.startMinimized = passedSettingsManager.settingsList.startMinimized;
            this.settingsList.customKeywords_ADD = passedSettingsManager.settingsList.customKeywords_ADD;
            this.settingsList.customKeywords_DELETE = passedSettingsManager.settingsList.customKeywords_DELETE;
            this.settingsList.customKeywords_REDO = passedSettingsManager.settingsList.customKeywords_REDO;
            this.settingsList.customKeywords_UNDO = passedSettingsManager.settingsList.customKeywords_UNDO;
            this.settingsList.customKeywords_UPDATE = passedSettingsManager.settingsList.customKeywords_UPDATE;
        }

        public void CopyUpdatedCommandsFrom(SettingsManager passedSettingsManager)
        {
            passedSettingsManager.settingsList.customKeywords_ADD = this.settingsList.customKeywords_ADD;
            passedSettingsManager.settingsList.customKeywords_DELETE = this.settingsList.customKeywords_DELETE;
            passedSettingsManager.settingsList.customKeywords_REDO = this.settingsList.customKeywords_REDO;
            passedSettingsManager.settingsList.customKeywords_UNDO = this.settingsList.customKeywords_UNDO;
            passedSettingsManager.settingsList.customKeywords_UPDATE = this.settingsList.customKeywords_UPDATE;
        }

    }
}
