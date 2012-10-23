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
        public struct MiscSettings
        {
            private bool loadOnStartup;
            private bool startMinimized;
            private bool stayOnTop;
            private int textSize;

            public bool LoadOnStartup { get { return loadOnStartup; } set { loadOnStartup = value; } }
            public bool StartMinimized { get { return startMinimized; } set { startMinimized = value; } }
            public bool StayOnTop { get { return stayOnTop; } set { stayOnTop = value; } }
            public int TextSize { get { return textSize; } set { textSize = value; } }

            public MiscSettings(bool _loadOnStartup, bool _startMinimized, bool _stayOnTop, int _textSize)
            {
                loadOnStartup = _loadOnStartup;
                startMinimized = _startMinimized;
                stayOnTop = _stayOnTop;
                textSize = _textSize;
            }
        }

        public MiscSettings misc;
        public Dictionary<string, CommandType> userCommandKeywords;
        public Dictionary<string, CommandType> userContextKeywords;

        public SettingsList()
        {
            misc = new MiscSettings(false, false, false, 8);
            userCommandKeywords = new Dictionary<string, CommandType>();
            userContextKeywords = new Dictionary<string, CommandType>();
        }

        public bool ContainsCommandKeyword(string userCommand,CommandType commandType)
        {
            if (userCommandKeywords.ContainsKey(userCommand) && userCommandKeywords.ContainsValue(commandType))
                return true;
            else
                return false;
        }

        public bool ContainsContextKeyword(string userCommand, CommandType commandType)
        {
            if (userContextKeywords.ContainsKey(userCommand) && userContextKeywords.ContainsValue(commandType))
                return true;
            else
                return false;
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
        }

        // ******************************************************************
        // Getters/Setters
        // ******************************************************************

        #region GettersSetters

        internal static void SetTextSize(int size) { settingsList.misc.TextSize = size; }
        internal static int GetTextSize() { return settingsList.misc.TextSize; }
        internal static void SetLoadOnStartupStatus(bool status) { settingsList.misc.LoadOnStartup = status; }
        internal static bool GetLoadOnStartupStatus() { return settingsList.misc.LoadOnStartup; }
        internal static void SetStartMinimized(bool status) { settingsList.misc.StartMinimized = status; }
        internal static bool GetStartMinimizeStatus() { return settingsList.misc.StartMinimized; }
        internal static void SetStayOnTop(bool status) { settingsList.misc.StayOnTop = status; }
        internal static bool GetStayOnTopStatus() { return settingsList.misc.StayOnTop; }

        #endregion;

        // ******************************************************************
        // Keyword Operations
        // ******************************************************************

        #region KeywordOperations

        #region CommandKeywords

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newCommand">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        internal static void AddCommandKeyword(string newCommand, CommandType commandType)
        {
            try
            {
                if (settingsList.ContainsCommandKeyword(newCommand,commandType))
                    throw new RepeatCommandException("There is such a command in the list already");
                settingsList.userCommandKeywords.Add(newCommand, commandType);
            }
            catch (RepeatCommandException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        internal static void RemoveCommandKeyword(string commandToRemove)
        {
            settingsList.userCommandKeywords.Remove(commandToRemove);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        internal static List<string> GetCommandKeywordList(CommandType commandType)
        {
            List<string> getCommands = new List<string>();
            foreach(var pair in settingsList.userCommandKeywords){
                if(pair.Value==commandType)
                    getCommands.Add(pair.Key);
            }

            return getCommands;
        }

        #endregion

        #region ContextKeywords

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newCommand">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        internal static void AddContextKeyword(string newCommand, CommandType commandType)
        {
            try
            {
                if (settingsList.ContainsContextKeyword(newCommand, commandType))
                    throw new RepeatCommandException("There is such a command in the list already");
                settingsList.userContextKeywords.Add(newCommand, commandType);
            }
            catch (RepeatCommandException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        internal static void RemoveContextKeyword(string commandToRemove)
        {
            settingsList.userContextKeywords.Remove(commandToRemove);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        internal static List<string> GetContextKeywordList(CommandType commandType)
        {
            List<string> getCommands = new List<string>();
            foreach (var pair in settingsList.userContextKeywords)
            {
                if (pair.Value == commandType)
                    getCommands.Add(pair.Key);
            }

            return getCommands;
        }

        #endregion

        /// <summary>
        /// Pushes new set of FlexiCommands into the StringParser
        /// </summary>
        internal static void AddCommandsToStringParser()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
