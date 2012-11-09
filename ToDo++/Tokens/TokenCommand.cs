﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenCommand : Token
    {
        static private List<CommandType> indexRangeableCommandTypes
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.UNDONE, CommandType.MODIFY, CommandType.POSTPONE };

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
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (attrb.commandType != CommandType.INVALID)
            {
                if (Value == CommandType.DONE)
                {
                    attrb.searchDone = SearchType.DONE;
                    Logger.Info("Updated searchDone to DONE.", "UpdateAttributes::TokenCommand");
                }
                else if (Value == CommandType.UNDONE)
                {
                    attrb.searchDone = SearchType.UNDONE;
                    Logger.Info("Updated searchDone to UNDONE.", "UpdateAttributes::TokenCommand");
                }
                else throw new MultipleCommandsException();
            }
            else
            {
                attrb.commandType = Value;
                Logger.Info("commandType is INVALID", "UpdateAttributes::TokenCommand");
            }
        }

        internal override bool RequiresIndexRange()
        {
            if (indexRangeableCommandTypes.Contains(Value))
            {
                Logger.Info("command type requires index range", "RequiresIndexRange::TokenCommand");
                return true;
            }
            else
            {
                Logger.Info("command type does not require index range", "RequiresIndexRange::TokenCommand");
                return false;
            }
        }

        internal override bool RequiresTimeRange()
        {
            if (this.GetType() == typeof(TokenCommand))
            {
                TokenCommand token = (TokenCommand)this;
                if (timeRangeableCommandTypes.Contains(token.Value))
                {
                    Logger.Info("command type requires time range", "RequiresTimeRange::TokenCommand");
                    return true;
                }
            }
            Logger.Info("command type does not require time range", "RequiresTimeRange::TokenCommand");
            return false;
        }

    }
}
