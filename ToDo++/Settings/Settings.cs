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
    // ******************************************************************
    // Class Containing all Settings Information
    // ******************************************************************

    #region SettingsList

    public class SettingsList
    {
        public bool loadOnStartup;
        public bool startMinimized;

        public int textSize;

        public List<string> customKeywords_ADD;
        public List<string> customKeywords_DELETE;
        public List<string> customKeywords_MODIFY;
        public List<string> customKeywords_UNDO;
        public List<string> customKeywords_REDO;

        public SettingsList()
        {
            loadOnStartup = false;
            startMinimized = false;
            textSize = 12;
            customKeywords_ADD = new List<string>();
            customKeywords_DELETE = new List<string>();
            customKeywords_MODIFY = new List<string>();
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
            customKeywords_MODIFY.Clear();
        }
    }

    #endregion

    public class Settings
    {
        // ******************************************************************
        //Constructor-Contains instances SettingsList and FileName
        // ******************************************************************

        #region Constructor

        private string fileName = "Settings.xml";
        private SettingsList settingsList;

        public Settings()
        {
            settingsList = new SettingsList();
            OpenFile();
        }

        #endregion

        // ******************************************************************
        //Functions to covert CommandType Enum between string and CommandType
        // ******************************************************************

        #region CommandFunctions

        /// <summary>
        /// This method converts string to a CommandType
        /// It can be used by other functions if neccessary (Used by Settings Class)
        /// </summary>
        /// <param name="commandString">Pass in a string that matches CommandType</param>
        /// <returns>The CommandType that matches the string passed in</returns>
        public CommandType StringToCommand(string commandString)
        {
            switch (commandString)
            {
                case "ADD":
                    return CommandType.ADD;
                case "DELETE":
                    return CommandType.DELETE;
                case "MODIFY":
                    return CommandType.MODIFY;
                case "UNDO":
                    return CommandType.UNDO;
                case "REDO":
                    return CommandType.REDO;
            }

            return CommandType.INVALID;
        }

        /// <summary>
        /// This method converts CommandType to a string
        /// It can be used by other functions if neccessary
        /// </summary>
        /// <param name="commandInput">Pass in a valid CommandType</param>
        /// <returns>Returns the Command in string format</returns>
        public string CommandToString(CommandType commandInput)
        {
            switch (commandInput)
            {
                case CommandType.ADD:
                    return "ADD";
                case CommandType.DELETE:
                    return "DELETE";
                case CommandType.MODIFY:
                    return "MODIFY";
                case CommandType.UNDO:
                    return "UNDO";
                case CommandType.REDO:
                    return "REDO";
            }

            return "INVALID";
        }

        #endregion

        // ******************************************************************
        //Functions that Get/Set/Increase/Decrease Text size of OutputBox
        // ******************************************************************

        #region TextSize

        /// <summary>
        /// This method sets the Text Size of the OutputBox directly
        /// </summary>
        /// <param name="size">Pass in a valid size</param>
        public void SetTextSize(int size)
        {
            if ((size < 5) || (size > 14))
                throw new TextSizeOutOfRangeException("Text Size Out of Range");
            settingsList.textSize = size;
        }

        /// <summary>
        /// Returns the Current OutputBox Text Size
        /// </summary>
        /// <returns>An int of the OutputBox Text Size</returns>
        public int GetTextSize()
        {
            return settingsList.textSize;
        }

        /// <summary>
        /// Function call to increase the text size directly
        /// </summary>
        public void IncreaseTextSize()
        {
            try
            {
                if ((settingsList.textSize + 1) > 14)
                    throw new TextSizeOutOfRangeException("Text Size Out of Range");
                settingsList.textSize++;
                WriteToFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Function call to decrease the text size directly
        /// </summary>
        public void DecreaseTextSize()
        {
            try
            {
                if ((settingsList.textSize - 1) < 5)
                    throw new TextSizeOutOfRangeException("Text Size Too Small");
                settingsList.textSize--;
                WriteToFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        #endregion

        // ******************************************************************
        //Functions that Set the LoadOnStartup and StartMinimized Statuses
        // ******************************************************************

        #region StartupMinimizedStatus

        /// <summary>
        /// Directly Sets the LoadOnStartup status
        /// Directly writes to File
        /// </summary>
        /// <param name="checkedStatus">Sets LoadOnStartup to true or false</param>
        public void ToggleLoadOnStartup(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.loadOnStartup = true;
            else
                settingsList.loadOnStartup = false;

            WriteToFile();
        }

        /// <summary>
        /// Directly Sets the LoadOnStartup status
        /// Directly writes to File
        /// </summary>
        /// <param name="checkedStatus">Sets LoadOnStartup to true or false</param>
        public void ToggleStartMinimized(bool checkedStatus)
        {
            if (checkedStatus)
                settingsList.startMinimized = true;
            else
                settingsList.startMinimized = false;

            WriteToFile();
        }

        /// <summary>
        /// Returns The LoadOnStartup Status as a bool
        /// </summary>
        /// <returns>The LoadOnStartup Status</returns>
        public bool GetLoadOnStartupStatus()
        {
            return settingsList.loadOnStartup;
        }

        /// <summary>
        /// Returns The GetMinimized Status as a bool
        /// </summary>
        /// <returns>The GetMinimized Status</returns>
        public bool GetStartMinimizedStatus()
        {
            return settingsList.startMinimized;
        }

        #endregion

        // ******************************************************************
        //Functions that Modify the list Of User Commands
        // ******************************************************************

        #region CommandModifications

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newCommand">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void AddCommand(string newCommand, CommandType commandType)
        {
            try
            {
                switch (commandType)
                {
                    case CommandType.ADD:
                        if (settingsList.customKeywords_ADD.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the ADD list already");
                        settingsList.customKeywords_ADD.Add(newCommand);
                        break;
                    case CommandType.DELETE:
                        if (settingsList.customKeywords_DELETE.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the DELETE list already");
                        settingsList.customKeywords_DELETE.Add(newCommand);
                        break;
                    case CommandType.MODIFY:
                        if (settingsList.customKeywords_MODIFY.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the MODIFY list already");
                        settingsList.customKeywords_MODIFY.Add(newCommand);
                        break;
                    case CommandType.UNDO:
                        if (settingsList.customKeywords_UNDO.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the UNDO list already");
                        settingsList.customKeywords_UNDO.Add(newCommand);
                        break;
                    case CommandType.REDO:
                        if (settingsList.customKeywords_REDO.Contains(newCommand))
                            throw new RepeatCommandException("There is such a command in the REDO list already");
                        settingsList.customKeywords_REDO.Add(newCommand);
                        break;
                }
            }
            catch (RepeatCommandException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandToRemove">Exact String of the Command to be removed</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveCommand(string commandToRemove, CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.ADD:
                    settingsList.customKeywords_ADD.Remove(commandToRemove);
                    break;
                case CommandType.DELETE:
                    settingsList.customKeywords_DELETE.Remove(commandToRemove);
                    break;
                case CommandType.MODIFY:
                    settingsList.customKeywords_MODIFY.Remove(commandToRemove);
                    break;
                case CommandType.UNDO:
                    settingsList.customKeywords_UNDO.Remove(commandToRemove);
                    break;
                case CommandType.REDO:
                    settingsList.customKeywords_REDO.Remove(commandToRemove);
                    break;
            }
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetCommandList(CommandType commandType)
        {
            List<string> getCommands = new List<string>();
            switch (commandType)
            {
                case CommandType.ADD:
                    getCommands = settingsList.customKeywords_ADD;
                    break;
                case CommandType.DELETE:
                    getCommands = settingsList.customKeywords_DELETE;
                    break;
                case CommandType.MODIFY:
                    getCommands = settingsList.customKeywords_MODIFY;
                    break;
                case CommandType.UNDO:
                    getCommands = settingsList.customKeywords_UNDO;
                    break;
                case CommandType.REDO:
                    getCommands = settingsList.customKeywords_REDO;
                    break;
            }

            return getCommands;
        }

        /// <summary>
        /// Function to check if a command exists
        /// </summary>
        /// <param name="userCommand">specify exact string of command you wish to check</param>
        /// <returns>Returns the CommandType of that userCommand if userCommand is found</returns>
        public CommandType CheckIfCommandExists(string userCommand)
        {
            foreach (string compare in settingsList.customKeywords_ADD)
                if (userCommand == compare)
                    return CommandType.ADD;
            foreach (string compare in settingsList.customKeywords_DELETE)
                if (userCommand == compare)
                    return CommandType.DELETE;
            foreach (string compare in settingsList.customKeywords_MODIFY)
                if (userCommand == compare)
                    return CommandType.MODIFY;
            foreach (string compare in settingsList.customKeywords_UNDO)
                if (userCommand == compare)
                    return CommandType.UNDO;
            foreach (string compare in settingsList.customKeywords_REDO)
                if (userCommand == compare)
                    return CommandType.REDO;

            return CommandType.INVALID;
        }

        /// <summary>
        /// Pushes new set of FlexiCommands into the StringParser
        /// </summary>
        public void PushCommands()
        {
            StringParser.ResetCommandKeywords();
            foreach (string userCommand in this.GetCommandList(CommandType.ADD))
                StringParser.AddUserCommand(userCommand, CommandType.ADD);
            foreach (string userCommand in this.GetCommandList(CommandType.DELETE))
                StringParser.AddUserCommand(userCommand, CommandType.DELETE);
            foreach (string userCommand in this.GetCommandList(CommandType.MODIFY))
                StringParser.AddUserCommand(userCommand, CommandType.MODIFY);
            foreach (string userCommand in this.GetCommandList(CommandType.UNDO))
                StringParser.AddUserCommand(userCommand, CommandType.UNDO);
            foreach (string userCommand in this.GetCommandList(CommandType.REDO))
                StringParser.AddUserCommand(userCommand, CommandType.REDO);
        }

        #endregion

        // ******************************************************************
        //Functions that Open/Write/Create Settings File
        // ******************************************************************

        #region FileOperations

        /// <summary>
        /// Writes all of SettingsList to an XML File
        /// </summary>
        public void WriteToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
            writer.Serialize(file, settingsList);
            file.Close();
        }

        /// <summary>
        /// Opens the Settings XML File, and loads all data into SettingsList
        /// Handles Settings File Corruption/Non-Existent errors
        /// </summary>
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

        // ******************************************************************
        //Functions to Copy/Clone Settings objects
        // ******************************************************************

        #region CloningOperations

        /// <summary>
        /// Updates this object with all commands from the passed instance of Settings
        /// This is used in Settings Class
        /// </summary>
        /// <param name="passedSettings">Updates this object's commands with the instance of Settings passed in</param>
        public void CopyUpdatedCommandsFrom(Settings passedSettings)
        {
            this.settingsList.customKeywords_ADD = passedSettings.settingsList.customKeywords_ADD;
            this.settingsList.customKeywords_DELETE = passedSettings.settingsList.customKeywords_DELETE;
            this.settingsList.customKeywords_REDO = passedSettings.settingsList.customKeywords_REDO;
            this.settingsList.customKeywords_UNDO = passedSettings.settingsList.customKeywords_UNDO;
            this.settingsList.customKeywords_MODIFY = passedSettings.settingsList.customKeywords_MODIFY;
        }

        /// <summary>
        /// Clones an instance of Settings
        /// </summary>
        /// <returns>A deep copy of the Settings object to be cloned</returns>
        public Settings CloneObj()
        {
            Settings p = new Settings();
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
            foreach (string item in this.settingsList.customKeywords_MODIFY)
                p.settingsList.customKeywords_MODIFY.Add(item);

            return p;
        }

        #endregion
    }
}
