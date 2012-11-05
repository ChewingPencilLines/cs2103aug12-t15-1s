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
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.MODIFY, CommandType.POSTPONE };

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

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (attrb.commandType != CommandType.INVALID)
            {
                CommandType cmd1 = attrb.commandType;
                CommandType cmd2 = commandType;
                if (cmd1 == CommandType.DONE || cmd2 == CommandType.DONE)
                {
                    if (cmd1 == CommandType.DELETE || cmd1 == CommandType.SEARCH)
                        attrb.sortType = SortType.DONE_STATE;
                }
                else throw new MultipleCommandsException();
            }
            else
            {
                attrb.commandType = Value;
            }
        }

        internal override bool RequiresIndexRange()
        {
            if (indexRangeableCommandTypes.Contains(Value))
                return true;
            else
                return false;
        }

        internal override bool RequiresTimeRange()
        {
            if (this.GetType() == typeof(TokenCommand))
            {
                TokenCommand token = (TokenCommand)this;
                if (timeRangeableCommandTypes.Contains(token.Value))
                    return true;
            }
            return false;
        }

    }
}
