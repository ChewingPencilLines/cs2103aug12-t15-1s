using System.Collections.Generic;

namespace ToDo
{
    public class Settings
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        static SettingInformation settingInfo;

        public Settings()
        {
            InitializeSettings();
        }

        // ******************************************************************
        // Initialization and opening of settings file
        // ******************************************************************

        private void InitializeSettings()
        {
            settingInfo = new SettingInformation();
        }

        public void UpdateSettings(SettingInformation updatedInfo)
        {
            settingInfo = updatedInfo;
            CustomDictionary.UpdateDictionary(settingInfo.userCommandKeywords,settingInfo.userContextKeywords);
        }

        // ******************************************************************
        // Getters/Setters
        // ******************************************************************

        #region GettersSetters

        public void SetTextSize(int size) { settingInfo.misc.TextSize = size; EventHandlers.UpdateSettings(settingInfo); }
        public int GetTextSize() { return settingInfo.misc.TextSize; }
        public void SetLoadOnStartupStatus(bool status) { settingInfo.misc.LoadOnStartup = status; EventHandlers.UpdateSettings(settingInfo); }
        public bool GetLoadOnStartupStatus() { return settingInfo.misc.LoadOnStartup; }
        public void SetStartMinimized(bool status) { settingInfo.misc.StartMinimized = status; EventHandlers.UpdateSettings(settingInfo); }
        public bool GetStartMinimizeStatus() { return settingInfo.misc.StartMinimized; }
        public void SetStayOnTop(bool status) { settingInfo.misc.StayOnTop = status; EventHandlers.UpdateSettings(settingInfo); }
        public bool GetStayOnTopStatus() { return settingInfo.misc.StayOnTop; }
        public void SetFontSelection(string font) { settingInfo.misc.FontSelection = font; EventHandlers.UpdateSettings(settingInfo); }
        public string GetFontSelection() { return settingInfo.misc.FontSelection; }

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
            if (settingInfo.ContainsCommandKeyword(newKeyword, commandType))
                throw new RepeatCommandException("There is such a command in the list already");
            if (settingInfo.userContextKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            if (settingInfo.userCommandKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already ");
            settingInfo.userCommandKeywords.Add(newKeyword, commandType);

            EventHandlers.UpdateSettings(settingInfo);
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveCommandKeyword(string keywordToRemove)
        {
            if (keywordToRemove == "add" || keywordToRemove == "remove" || keywordToRemove == "display" || keywordToRemove == "sort"
                || keywordToRemove == "search" || keywordToRemove == "modify" || keywordToRemove == "undo" || keywordToRemove == "redo"
                || keywordToRemove == "done" || keywordToRemove == "postpone")
                throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
            settingInfo.userCommandKeywords.Remove(keywordToRemove);

            EventHandlers.UpdateSettings(settingInfo);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetCommandKeywordList(CommandType commandType)
        {
            List<string> getCommands = new List<string>();
            foreach(var pair in settingInfo.userCommandKeywords){
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
            if (settingInfo.ContainsContextKeyword(newKeyword, contextType))
                throw new RepeatCommandException("There is such a command in the list already");
            if (settingInfo.userContextKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            if (settingInfo.userCommandKeywords.ContainsKey(newKeyword))
                throw new RepeatCommandException("There is a repeat keyword already");
            settingInfo.userContextKeywords.Add(newKeyword, contextType);

            EventHandlers.UpdateSettings(settingInfo);
        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveContextKeyword(string keywordToRemove)
        {
            if (keywordToRemove == "on" || keywordToRemove == "from" || keywordToRemove == "to" || keywordToRemove == "-"
                || keywordToRemove == "this" || keywordToRemove == "next" || keywordToRemove == "following" )
                    throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
            settingInfo.userContextKeywords.Remove(keywordToRemove);

            EventHandlers.UpdateSettings(settingInfo);
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="contextType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetContextKeywordList(ContextType contextType)
        {
            List<string> getCommands = new List<string>();
            foreach (var pair in settingInfo.userContextKeywords)
            {
                if (pair.Value == contextType)
                    getCommands.Add(pair.Key);
            }

            return getCommands;
        }

        #endregion

        #endregion

    }
}
