using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace ToDo
{
    public class SettingInformation
    {
        //DEFAULT SETTING VALUES TO BE LOADED
        private const bool defaultLoadOnStartup = false;
        private const bool defaultStartMinimized = false;
        private const bool defaultStayOnTop = false;
        private const int defaultTextSize = 9;
        private const string defaultFont = "Arial";
        private const Color defaultTaskDoneColor = Color.Green;
        private const Color defaultTaskMissedDeadlineColor = Color.Red;
        private const Color defaultTaskNearingDeadlineColor = Color.OrangeRed;
        private const Color defaultTaskOverColor = Color.MediumVioletRed;

        public struct MiscSettings
        {
            private bool loadOnStartup;
            private bool startMinimized;
            private bool stayOnTop;
            private int textSize;
            private string fontSelection;
            private Color taskDoneColor;
            private Color taskMissedDeadlineColor;
            private Color taskNearingDeadlineColor;
            private Color taskOverColor;

            public bool LoadOnStartup { get { return loadOnStartup; } set { loadOnStartup = value; } }
            public bool StartMinimized { get { return startMinimized; } set { startMinimized = value; } }
            public bool StayOnTop { get { return stayOnTop; } set { stayOnTop = value; } }
            public int TextSize { get { return textSize; } set { textSize = value; } }
            public string FontSelection { get { return fontSelection; } set { fontSelection = value; } }
            public Color TaskDoneColor { get { return taskDoneColor; } set { taskDoneColor = value; } }
            public Color TaskMissedDeadlineColor { get { return taskMissedDeadlineColor; } set { taskMissedDeadlineColor = value; } }
            public Color TaskNearingDeadlineColor { get { return taskNearingDeadlineColor; } set { taskNearingDeadlineColor = value; } }
            public Color TaskOverColor { get { return taskOverColor; } set { taskOverColor = value; } }

            public MiscSettings(bool _loadOnStartup, bool _startMinimized, bool _stayOnTop, int _textSize, string _fontSelection,
                                Color _taskDoneColor, Color _taskMissedDeadlineColor, Color _taskNearingDeadlineColor, Color _taskOverColor)
            {
                loadOnStartup = _loadOnStartup;
                startMinimized = _startMinimized;
                stayOnTop = _stayOnTop;
                textSize = _textSize;
                fontSelection = _fontSelection;
                taskDoneColor = _taskDoneColor;
                taskMissedDeadlineColor = _taskMissedDeadlineColor;
                taskNearingDeadlineColor = _taskNearingDeadlineColor;
                taskOverColor = _taskOverColor;
            }
        }

        public MiscSettings misc;
        public Dictionary<string, CommandType> userCommandKeywords;
        public Dictionary<string, ContextType> userContextKeywords;
        public Dictionary<string, TimeRangeKeywordsType> userTimeRangeKeywordsType;
        public Dictionary<string, TimeRangeType> userTimeRangeType;
        public Dictionary<TimeRangeKeywordsType, int> userTimeRangeKeywordsStartTime;
        public Dictionary<TimeRangeKeywordsType, int> userTimeRangeKeywordsEndTime;

        public SettingInformation()
        {
            misc = new MiscSettings(defaultLoadOnStartup, defaultStartMinimized, defaultStayOnTop, defaultTextSize, defaultFont,
                                    defaultTaskDoneColor,defaultTaskMissedDeadlineColor,defaultTaskNearingDeadlineColor,defaultTaskOverColor);
            userCommandKeywords = CustomDictionary.GetCommandKeywords();
            userContextKeywords = CustomDictionary.GetContextKeywords();
            userTimeRangeKeywordsType = CustomDictionary.GetTimeRangeKeywordKeywords();
            userTimeRangeType = CustomDictionary.GetTimeRangeKeywords();
            userTimeRangeKeywordsStartTime = CustomDictionary.GetTimeRangeStartTime();
            userTimeRangeKeywordsEndTime = CustomDictionary.GetTimeRangeEndTime();
        }

        public bool ContainsFlexiCommandKeyword(string userKeyword, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType passed;
                        if (userCommandKeywords.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (CommandType)flexiCommandType)
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType passed;
                        if (userContextKeywords.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (ContextType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType passed;
                        if (userTimeRangeKeywordsType.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (TimeRangeKeywordsType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType passed;
                        if (userTimeRangeType.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (TimeRangeType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }
            }


            return false;
        }

        public string ToXML()
        {
            string serializedXML = this.Serialize();
            return serializedXML;
        }

        static SettingInformation GenerateSettingInfoFromXML(string xml)
        {
            return xml.Deserialize<SettingInformation>();
        }
    }
}
