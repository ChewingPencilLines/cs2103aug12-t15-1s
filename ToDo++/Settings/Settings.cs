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
            textSize = 9;
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

    public static class Settings
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        static string fileName = "Settings.xml";
        static SettingsList settingsList;

        static Settings()
        {
            InitializeSettings();
        }

        // ******************************************************************
        // Initialization and opening of settings file
        // ******************************************************************

        private static void InitializeSettings()
        {
            settingsList = new SettingsList();
            OpenFile();
        }

        // ******************************************************************
        // I/O Operations
        // ******************************************************************

        #region IOFileOperations

        private static void OpenFile()
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
                AlertBox.Show("Settings File Not Found, new file will be created");
                //MessageBox.Show("Settings File Not Found, new file will be created");
                WriteToFile();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("There was an error with the Settings File, a new file will be created");
                WriteToFile();
            }

        }

        private static void WriteToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
            writer.Serialize(file, settingsList);
            file.Close();
        }

        #endregion

        // ******************************************************************
        // Getters/Setters
        // ******************************************************************

        #region GettersSetters
        internal static int textSize
        {
            get { return settingsList.textSize; }
            set
            {
                if ((textSize < 5) || (textSize > 14))
                    throw new TextSizeOutOfRangeException("Text Size Out of Range");
                settingsList.textSize = textSize;
                WriteToFile();
            }
        }

        internal static bool loadOnStartup { get { return settingsList.loadOnStartup; } set { settingsList.loadOnStartup = loadOnStartup; WriteToFile(); } }
        internal static bool startMinimized { get { return settingsList.startMinimized; } set { settingsList.startMinimized = startMinimized; WriteToFile(); } }

        #endregion;

        // ******************************************************************
        // Keyword Operations
        // ******************************************************************

        #region KeywordOperations

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newCommand">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        internal static void AddCommand(string newCommand, CommandType commandType)
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
        internal static void RemoveCommand(string commandToRemove, CommandType commandType)
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
        internal static List<string> GetCommandList(CommandType commandType)
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
        /// Pushes new set of FlexiCommands into the StringParser
        /// </summary>
        internal static void AddCommandsToStringParser()
        {
            StringParser.ResetCommandKeywords();
            foreach (string userCommand in GetCommandList(CommandType.ADD))
                StringParser.AddUserCommand(userCommand, CommandType.ADD);
            foreach (string userCommand in GetCommandList(CommandType.DELETE))
                StringParser.AddUserCommand(userCommand, CommandType.DELETE);
            foreach (string userCommand in GetCommandList(CommandType.MODIFY))
                StringParser.AddUserCommand(userCommand, CommandType.MODIFY);
            foreach (string userCommand in GetCommandList(CommandType.UNDO))
                StringParser.AddUserCommand(userCommand, CommandType.UNDO);
            foreach (string userCommand in GetCommandList(CommandType.REDO))
                StringParser.AddUserCommand(userCommand, CommandType.REDO);
        }

        #endregion

    }
}
