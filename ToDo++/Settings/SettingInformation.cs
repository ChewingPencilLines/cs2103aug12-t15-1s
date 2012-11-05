﻿using System.Collections.Generic;
using System;
using System.Windows.Forms;

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
            
            public bool LoadOnStartup { get { return loadOnStartup; } set { loadOnStartup = value; } }
            public bool StartMinimized { get { return startMinimized; } set { startMinimized = value; } }
            public bool StayOnTop { get { return stayOnTop; } set { stayOnTop = value; } }
            public int TextSize { get { return textSize; } set { textSize = value; } }
            public string FontSelection { get { return fontSelection; } set { fontSelection = value; } }

            public MiscSettings(bool _loadOnStartup, bool _startMinimized, bool _stayOnTop, int _textSize,string _fontSelection)
            {
                loadOnStartup = _loadOnStartup;
                startMinimized = _startMinimized;
                stayOnTop = _stayOnTop;
                textSize = _textSize;
                fontSelection = _fontSelection;
            }
        }

        public MiscSettings misc;
        public Dictionary<string, CommandType> userCommandKeywords;
        public Dictionary<string, ContextType> userContextKeywords;
        public Dictionary<string, TimeRangeKeywordsType> userTimeRangeKeywordsType;
        public Dictionary<string, TimeRangeType> userTimeRangeType;

        public SettingInformation()
        {
            misc = new MiscSettings(false, false, false, 9,"Arial");
            userCommandKeywords = CustomDictionary.GetCommandKeywords();
            userContextKeywords = CustomDictionary.GetContextKeywords();
            userTimeRangeKeywordsType = CustomDictionary.GetTimeRangeKeywordKeywords();
            userTimeRangeType = CustomDictionary.GetTimeRangeKeywords();
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
