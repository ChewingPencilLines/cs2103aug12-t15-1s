using System.Collections.Generic;

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

        public SettingInformation()
        {
            misc = new MiscSettings(false, false, false, 9);
            userCommandKeywords = StaticVariables.GetCommandKeywords();
            userContextKeywords = StaticVariables.GetContextKeywords();
        }

        public bool ContainsCommandKeyword(string userKeyword, CommandType commandType)
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
