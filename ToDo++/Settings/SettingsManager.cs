using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace ToDo
{
    public class SettingsList
    {
        public bool loadOnStartup;
        public bool startMinimized;

        public int textSize;

        public List<string> customKeywords_ADD;
        public List<string> customKeywords_DELETE;
        public List<string> customKeywords_UPDATE;
        public List<string> customKeywords_UNDO;
        public List<string> customKeywords_REDO;

        public SettingsList()
        {
            loadOnStartup = false;
            startMinimized = false;
            textSize = 12;
            customKeywords_ADD = new List<string>();
            customKeywords_DELETE = new List<string>();
            customKeywords_UPDATE = new List<string>();
            customKeywords_UNDO = new List<string>();
            customKeywords_REDO = new List<string>();
        }

        public void ClearAll()
        {
            loadOnStartup = false;
            startMinimized = false;
            textSize = 12;
            customKeywords_ADD.Clear();
            customKeywords_DELETE.Clear();
            customKeywords_REDO.Clear();
            customKeywords_UNDO.Clear();
            customKeywords_UPDATE.Clear();
        }
    }

    public enum Commands { ADD = 1, DELETE, UPDATE, UNDO, REDO, NONE };

    public class SettingsManager
    {
        private string fileName = "Settings.xml";
        private SettingsList settingsList;

        public SettingsManager()
        {
            settingsList = new SettingsList();

            /* Used for Writing test data to xml before running*/
            //SetUpCommands();
            //WriteToFile();

            OpenFile();
        }

        #region CommandFunctions

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

        #region TestFunctionToLoadData

        //Just a Test Function
        public void SetUpCommands()
        {
            ToggleLoadOnStartup(true);
            ToggleStartMinimized(true);
            AddCommand("+", Commands.ADD);
            AddCommand("-", Commands.DELETE);
            SetTextSize(12);
        }

        #endregion

        #region TextSize

        public void SetTextSize(int size)
        {
            settingsList.textSize = size;
        }

        public int GetTextSize()
        {
            return settingsList.textSize;
        }

        public void IncreaseTextSize()
        {
            try
            {
                if ((settingsList.textSize + 1) > 14)
                    throw new TextSizeOutOfRange("Text Size Too Big");
                settingsList.textSize++;
                WriteToFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DecreaseTextSize()
        {
            try
            {
                if ((settingsList.textSize - 1) < 5)
                    throw new TextSizeOutOfRange("Text Size Too Small");
                settingsList.textSize--;
                WriteToFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        #endregion

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

        #region CommandModifications

        public void AddCommand(string newCommand, Commands commandType)
        {
            try
            {
                switch (commandType)
                {
                    case Commands.ADD:
                        if (settingsList.customKeywords_ADD.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the ADD list already");
                        settingsList.customKeywords_ADD.Add(newCommand);
                        break;
                    case Commands.DELETE:
                        if (settingsList.customKeywords_DELETE.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the DELETE list already");
                        settingsList.customKeywords_DELETE.Add(newCommand);
                        break;
                    case Commands.UPDATE:
                        if (settingsList.customKeywords_REDO.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the REDO list already");
                        settingsList.customKeywords_UPDATE.Add(newCommand);
                        break;
                    case Commands.UNDO:
                        if (settingsList.customKeywords_UNDO.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the UNDO list already");
                        settingsList.customKeywords_UNDO.Add(newCommand);
                        break;
                    case Commands.REDO:
                        if (settingsList.customKeywords_UPDATE.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the UPDATE list already");
                        settingsList.customKeywords_REDO.Add(newCommand);
                        break;
                }
            }
            catch (RepeatCommandException e)
            {
                MessageBox.Show(e.Message);
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

        #endregion

        #region FileOperations

        public void WriteToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
            writer.Serialize(file, settingsList);
            file.Close();
        }

        public void OpenFile()
        {
            System.IO.StreamReader file;

            try
            {
                file = new System.IO.StreamReader(fileName);
                System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
                settingsList = (SettingsList)writer.Deserialize(file);
                file.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Settings File Not Found, new file will be created");
                WriteToFile();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("There was an error with the Settings File, a new file will be created");
                WriteToFile();
            }

        }

        #endregion

        #region CloningOperations

        public void CopyUpdatedCommandsFrom(SettingsManager passedSettingsManager)
        {
            this.settingsList.customKeywords_ADD = passedSettingsManager.settingsList.customKeywords_ADD;
            this.settingsList.customKeywords_DELETE = passedSettingsManager.settingsList.customKeywords_DELETE;
            this.settingsList.customKeywords_REDO = passedSettingsManager.settingsList.customKeywords_REDO;
            this.settingsList.customKeywords_UNDO = passedSettingsManager.settingsList.customKeywords_UNDO;
            this.settingsList.customKeywords_UPDATE = passedSettingsManager.settingsList.customKeywords_UPDATE;
        }

        public SettingsManager CloneObj()
        {
            SettingsManager p = new SettingsManager();
            p.settingsList.ClearAll();

            p.settingsList.loadOnStartup = this.settingsList.loadOnStartup;
            p.settingsList.startMinimized = this.settingsList.startMinimized;

            p.settingsList.textSize = this.settingsList.textSize;

            foreach (string item in this.settingsList.customKeywords_ADD)
                p.settingsList.customKeywords_ADD.Add(item);
            foreach (string item in this.settingsList.customKeywords_DELETE)
                p.settingsList.customKeywords_DELETE.Add(item);
            foreach (string item in this.settingsList.customKeywords_REDO)
                p.settingsList.customKeywords_REDO.Add(item);
            foreach (string item in this.settingsList.customKeywords_UNDO)
                p.settingsList.customKeywords_UNDO.Add(item);
            foreach (string item in this.settingsList.customKeywords_UPDATE)
                p.settingsList.customKeywords_UPDATE.Add(item);

            return p;
        }

        #endregion
    }
}
