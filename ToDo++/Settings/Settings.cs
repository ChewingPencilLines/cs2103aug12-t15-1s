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
        public Dictionary<string, ContextType> userContextKeywords;

        public SettingsList()
        {
            misc = new MiscSettings(false, false, false, 8);
            userCommandKeywords = new Dictionary<string, CommandType>();
            userContextKeywords = new Dictionary<string, ContextType>();
        }

        public bool ContainsCommandKeyword(string userKeyword,CommandType commandType)
        {
            CommandType passed;
            if (userCommandKeywords.TryGetValue(userKeyword, out passed))
            {
                if (passed == commandType)
                    return true;
                else 
                    return false;
            }
            else
                return false;
        }

        public bool ContainsContextKeyword(string userKeyword, ContextType commandType)
        {
            ContextType passed;
            if (userContextKeywords.TryGetValue(userKeyword, out passed))
            {
                if (passed == commandType)
                    return true;
                else return false;
            }
            else
                return false;
        }
    }

    #endregion

    public class Settings
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        static SettingsList settingsList;

        public Settings()
        {
            InitializeSettings();
        }

        // ******************************************************************
        // Initialization and opening of settings file
        // ******************************************************************

        private void InitializeSettings()
        {
            settingsList = new SettingsList();
        }

        public void UpdateSettings(SettingsList updatedList)
        {
            settingsList.misc = updatedList.misc;
        }

        // ******************************************************************
        // Getters/Setters
        // ******************************************************************

        #region GettersSetters

        public void SetTextSize(int size) { settingsList.misc.TextSize = size; }
        public int GetTextSize() { return settingsList.misc.TextSize; }
        public void SetLoadOnStartupStatus(bool status) { settingsList.misc.LoadOnStartup = status; }
        public bool GetLoadOnStartupStatus() { return settingsList.misc.LoadOnStartup; }
        public void SetStartMinimized(bool status) { settingsList.misc.StartMinimized = status; }
        public bool GetStartMinimizeStatus() { return settingsList.misc.StartMinimized; }
        public void SetStayOnTop(bool status) { settingsList.misc.StayOnTop = status; }
        public bool GetStayOnTopStatus() { return settingsList.misc.StayOnTop; }

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
        /// <param name="newKeyword">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void AddCommandKeyword(string newKeyword, CommandType commandType)
        {
            if (settingsList.ContainsCommandKeyword(newKeyword, commandType))
                throw new RepeatCommandException("There is such a command in the list already");
            if (settingsList.userContextKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            if (settingsList.userCommandKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            settingsList.userCommandKeywords.Add(newKeyword, commandType);
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveCommandKeyword(string keywordToRemove)
        {
            settingsList.userCommandKeywords.Remove(keywordToRemove);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetCommandKeywordList(CommandType commandType)
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
        /// <param name="newKeyword">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void AddContextKeyword(string newKeyword, ContextType contextType)
        {
            if (settingsList.ContainsContextKeyword(newKeyword, contextType))
                throw new RepeatCommandException("There is such a command in the list already");
            if (settingsList.userContextKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            if (settingsList.userCommandKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            settingsList.userContextKeywords.Add(newKeyword, contextType);
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveContextKeyword(string keywordToRemove)
        {
            settingsList.userContextKeywords.Remove(keywordToRemove);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="contextType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetContextKeywordList(ContextType contextType)
        {
            List<string> getCommands = new List<string>();
            foreach (var pair in settingsList.userContextKeywords)
            {
                if (pair.Value == contextType)
                    getCommands.Add(pair.Key);
            }

            return getCommands;
        }

        #endregion

        /// <summary>
        /// Pushes new set of FlexiCommands into the StringParser
        /// </summary>
        public void AddCommandsToStringParser()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
