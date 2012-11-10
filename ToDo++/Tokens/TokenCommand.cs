using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenCommand : Token
    {
        static private List<CommandType> indexRangeableCommandTypes
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.UNDONE, CommandType.MODIFY, CommandType.POSTPONE, CommandType.SEARCH };

        static private List<CommandType> timeRangeableCommandTypes
            = new List<CommandType> { CommandType.SCHEDULE, CommandType.POSTPONE };

        CommandType commandType;

        internal CommandType Value
        {
            get { return commandType; }
        }

        internal TokenCommand(int position, CommandType val)
            : base(position)
        {
            commandType = val;
            Logger.Info("Created a command token object", "TokenCommand::TokenCommand");
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (attrb.commandType != CommandType.INVALID)
            {
                if (Value == CommandType.DONE)
                {
                    attrb.searchType = SearchType.DONE;
                    Logger.Info("Updated SearchType to DONE.", "UpdateAttributes::TokenCommand");
                }
                else if (Value == CommandType.UNDONE)
                {
                    attrb.searchType = SearchType.UNDONE;
                    Logger.Info("Updated SearchType to UNDONE.", "UpdateAttributes::TokenCommand");
                }
                else if (attrb.commandType == CommandType.SORT)
                {
                    attrb.commandType = Value;
                    Logger.Info("Resolved multiple commands to not use Sort as command (lower priority)", "UpdateAttributes::TokenCommand");
                }
                else if (Value == CommandType.SORT)
                {
                    Logger.Info("Resolved multiple commands to not use Sort as command (lower priority)", "UpdateAttributes::TokenCommand");
                }
                else
                {
                    Logger.Error("Multiple commands detected", "UpdateAttributes::TokenCommand");
                    throw new MultipleCommandsException();
                }
            }
            else
            {
                attrb.commandType = Value;
                Logger.Info("commandType is INVALID", "UpdateAttributes::TokenCommand");
            }
        }

        internal bool RequiresIndexRange()
        {
            if (indexRangeableCommandTypes.Contains(Value))
                return true;
            else
                return false;
        }

        internal bool RequiresTimeRange()
        {
            if (timeRangeableCommandTypes.Contains(Value))
                return true;
            else
                return false;
        }

    }
}
