using System.Collections.Generic;
using System;

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
            CustomDictionary.UpdateDictionary(settingInfo.userCommandKeywords, settingInfo.userContextKeywords, settingInfo.userTimeRangeKeywordsType, settingInfo.userTimeRangeType, settingInfo.userTimeRangeKeywordsStartTime, settingInfo.userTimeRangeKeywordsEndTime);
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

        private bool ContainsRepeatKeywords(string newKeyword)
        {
            if (settingInfo.userContextKeywords.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userCommandKeywords.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userTimeRangeKeywordsType.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userTimeRangeType.ContainsKey(newKeyword))
                return false;

            return true;
        }

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newKeyword">New Command that is to be added</param>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void AddFlexiKeyword(string newKeyword, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType commandType = (CommandType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, commandType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userCommandKeywords.Add(newKeyword, commandType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType contexType = (ContextType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, contexType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userContextKeywords.Add(newKeyword, contexType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType timeRangeKeywordsType = (TimeRangeKeywordsType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, timeRangeKeywordsType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userTimeRangeKeywordsType.Add(newKeyword, timeRangeKeywordsType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType timeRangeType = (TimeRangeType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, timeRangeType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userTimeRangeType.Add(newKeyword, timeRangeType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }
            }

        }

        /// <summary>
        /// This method removes the specified command
        /// </summary>
        /// <param name="commandString">Specify to which CommandType it is being added to</param>
        public void RemoveFlexiKeyword(string keywordToRemove, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        if (keywordToRemove == "add" || keywordToRemove == "remove" || keywordToRemove == "display" || keywordToRemove == "sort"
                        || keywordToRemove == "search" || keywordToRemove == "modify" || keywordToRemove == "undo" || keywordToRemove == "redo"
                        || keywordToRemove == "done" || keywordToRemove == "undone" || keywordToRemove == "postpone")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userCommandKeywords.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.ContextType":
                    {
                        if (keywordToRemove == "by" || keywordToRemove == "on" || keywordToRemove == "from" || keywordToRemove == "to"
                        || keywordToRemove == "-" || keywordToRemove == "this" || keywordToRemove == "next" || keywordToRemove == "nxt"
                        || keywordToRemove == "following")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userContextKeywords.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        if (keywordToRemove == "morning" || keywordToRemove == "morn" || keywordToRemove == "afternoon" || keywordToRemove == "evening"
                        || keywordToRemove == "night")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userTimeRangeKeywordsType.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeType":
                    {
                        if (keywordToRemove == "hours" || keywordToRemove == "hour" || keywordToRemove == "hrs" || keywordToRemove == "hr"
                        || keywordToRemove == "days" || keywordToRemove == "day" || keywordToRemove == "month" || keywordToRemove == "months"
                        || keywordToRemove == "mnth" || keywordToRemove == "mths")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userTimeRangeType.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }
            }
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="commandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetFlexiKeywordList(Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType commandType = (CommandType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userCommandKeywords)
                        {
                            if (pair.Value == commandType)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType contextType = (ContextType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userContextKeywords)
                        {
                            if (pair.Value == contextType)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType timeRangeKeyword = (TimeRangeKeywordsType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userTimeRangeKeywordsType)
                        {
                            if (pair.Value == timeRangeKeyword)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType timeRange = (TimeRangeType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userTimeRangeType)
                        {
                            if (pair.Value == timeRange)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }
            }

            return null;
        }

        #endregion

        #region TimeDictionary

        public void SetTimeRange(TimeRangeKeywordsType timeRange, int startTime, int endTime)
        {
            settingInfo.userTimeRangeKeywordsStartTime[timeRange] = startTime;
            settingInfo.userTimeRangeKeywordsEndTime[timeRange] = endTime;
            EventHandlers.UpdateSettings(settingInfo);
        }

        public int GetStartTime(TimeRangeKeywordsType timeRange) { return settingInfo.userTimeRangeKeywordsStartTime[timeRange]; }
        public int GetEndTime(TimeRangeKeywordsType timeRange) { return settingInfo.userTimeRangeKeywordsEndTime[timeRange]; }

        #endregion


        #endregion

    }
}
