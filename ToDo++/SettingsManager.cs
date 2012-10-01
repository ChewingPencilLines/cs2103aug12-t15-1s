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

        public List<string> addList;
        public List<string> deleteList;
        public List<string> updateList;
        public List<string> undoList;
        public List<string> redoList;

        public SettingsList()
        {
            loadOnStartup = true;
            startMinimized = true;
            addList = new List<string>();
            deleteList = new List<string>();
            updateList = new List<string>();
            undoList = new List<string>();
            redoList = new List<string>();
        }
    }

    public enum Commands { ADD = 1, DELETE, UPDATE, UNDO, REDO, NONE };

    public class SettingsManager
    {
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
            settingsList = new SettingsList();

            /* Used for Writing test data to xml before running*/
            //SetUpCommands();
            //WriteToFile("TEST.xml");

            OpenFile("TEST.xml");
        }

        #region StartupMinimizedStatus


        public void ToggleLoadOnStartup(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.loadOnStartup = true;
            else
                settingsList.loadOnStartup = false;

            WriteToFile("TEST.xml");
        }

        public void ToggleStartMinimized(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.startMinimized = true;
            else
                settingsList.startMinimized = false;

            WriteToFile("TEST.xml");
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
                    settingsList.addList.Add(newCommand);
                    break;
                case Commands.DELETE:
                    settingsList.deleteList.Add(newCommand);
                    break;
                case Commands.UPDATE:
                    settingsList.updateList.Add(newCommand);
                    break;
                case Commands.UNDO:
                    settingsList.undoList.Add(newCommand);
                    break;
                case Commands.REDO:
                    settingsList.redoList.Add(newCommand);
                    break;
            }

            WriteToFile("TEST.xml");
        }

        public void RemoveCommand(string commandToRemove, Commands commandType)
        {
            switch (commandType)
            {
                case Commands.ADD:
                    settingsList.addList.Remove(commandToRemove);
                    break;
                case Commands.DELETE:
                    settingsList.deleteList.Remove(commandToRemove);
                    break;
                case Commands.UPDATE:
                    settingsList.updateList.Remove(commandToRemove);
                    break;
                case Commands.UNDO:
                    settingsList.undoList.Remove(commandToRemove);
                    break;
                case Commands.REDO:
                    settingsList.redoList.Remove(commandToRemove);
                    break;
            }

            WriteToFile("TEST.xml");
        }

        public List<string> GetCommand(Commands commandType)
        {
            List<string> getCommands = new List<string>();
            switch (commandType)
            {
                case Commands.ADD:
                    getCommands = settingsList.addList;
                    break;
                case Commands.DELETE:
                    getCommands = settingsList.deleteList;
                    break;
                case Commands.UPDATE:
                    getCommands = settingsList.updateList;
                    break;
                case Commands.UNDO:
                    getCommands = settingsList.undoList;
                    break;
                case Commands.REDO:
                    getCommands = settingsList.redoList;
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
            foreach (string compare in settingsList.addList)
                if (userCommand == compare)
                    return Commands.ADD;
            foreach (string compare in settingsList.deleteList)
                if (userCommand == compare)
                    return Commands.DELETE;
            foreach (string compare in settingsList.updateList)
                if (userCommand == compare)
                    return Commands.UPDATE;
            foreach (string compare in settingsList.undoList)
                if (userCommand == compare)
                    return Commands.UNDO;
            foreach (string compare in settingsList.redoList)
                if (userCommand == compare)
                    return Commands.REDO;

            return Commands.NONE;
        }

        public void WriteToFile(string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                fileName,true);
            writer.Serialize(file, settingsList);
            file.Close();
        }

        public void OpenFile(string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));

            System.IO.StreamReader file = new System.IO.StreamReader(
                fileName,true);
            settingsList = (SettingsList)writer.Deserialize(file);

            file.Close();
        }

    }
}
