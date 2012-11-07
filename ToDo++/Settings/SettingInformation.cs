﻿using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace ToDo
{
    public class SettingInformation
    {
        public struct MiscSettings
        {
            private bool loadOnStartup;
            private bool startMinimized;
            private bool stayOnTop;
            private int textSize;
            private string fontSelection;
            private Color taskDoneColor;
            private Color taskDeadlineColor;
            private Color taskDeadlineDayColor;
            private Color taskEventColor;

            public bool LoadOnStartup { get { return loadOnStartup; } set { loadOnStartup = value; } }
            public bool StartMinimized { get { return startMinimized; } set { startMinimized = value; } }
            public bool StayOnTop { get { return stayOnTop; } set { stayOnTop = value; } }
            public int TextSize { get { return textSize; } set { textSize = value; } }
            public string FontSelection { get { return fontSelection; } set { fontSelection = value; } }
            public Color TaskDoneColor { get { return taskDoneColor; } set { taskDoneColor = value; } }
            public Color TaskDeadlineColor { get { return taskDeadlineColor; } set { taskDeadlineColor = value; } }
            public Color TaskDeadlineDayColor { get { return taskDeadlineDayColor; } set { taskDeadlineDayColor = value; } }
            public Color TaskEventColor { get { return taskEventColor; } set { taskEventColor = value; } }

            public MiscSettings(bool _loadOnStartup, bool _startMinimized, bool _stayOnTop, int _textSize, string _fontSelection,
                                Color _taskDoneColor, Color _taskDeadlineColor, Color _taskDeadlineDayColor, Color _taskEventColor)
            {
                loadOnStartup = _loadOnStartup;
                startMinimized = _startMinimized;
                stayOnTop = _stayOnTop;
                textSize = _textSize;
                fontSelection = _fontSelection;
                taskDoneColor = _taskDoneColor;
                taskDeadlineColor = _taskDeadlineColor;
                taskDeadlineDayColor = _taskDeadlineDayColor;
                taskEventColor = _taskEventColor;
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
            misc = new MiscSettings(false, false, false, 9, "Arial",Color.Green,Color.Red,Color.OrangeRed,Color.MediumVioletRed);
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
