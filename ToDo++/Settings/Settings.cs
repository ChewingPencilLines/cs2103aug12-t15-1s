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
        public Dictionary<CommandType, string> userCommandKeywords;
        public Dictionary<ContextType, string> userContextKeywords;

        public SettingsList()
        {
            misc = new MiscSettings(false, false, false, 8);
            userCommandKeywords = new Dictionary<CommandType, string>();
            userContextKeywords = new Dictionary<ContextType, string>();
        }

        public bool ContainsCommandKeyword(string userKeyword,CommandType commandType)
        {
            string passed;
            if (userCommandKeywords.TryGetValue(commandType, out passed))
            {
                if (passed == userKeyword)
                    return true;
                else 
                    return false;
            }
            else
                return false;
        }

        public bool ContainsContextKeyword(string userKeyword, ContextType commandType)
        {
            string passed;
            if (userContextKeywords.TryGetValue(commandType, out passed))
            {
                if (passed == userKeyword)
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
                if (settingsList.ContainsCommandKeyword(newKeyword,commandType))
                    throw new RepeatCommandException("There is such a command in the list already");
                settingsList.userCommandKeywords.Add(commandType, newKeyword);
                //key then value
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveCommandKeyword(string keywordToRemove)
        {
            throw new NotImplementedException();
            //settingsList.userCommandKeywords.Remove(keywordToRemove);
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
                if(pair.Key==commandType)
                    getCommands.Add(pair.Value);
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
            settingsList.userContextKeywords.Add(contextType, newKeyword);
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveContextKeyword(string keywordToRemove)
        {
            throw new NotImplementedException();
            //settingsList.userContextKeywords.Remove(keywordToRemove);
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
                if (pair.Key == contextType)
                    getCommands.Add(pair.Value);
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
