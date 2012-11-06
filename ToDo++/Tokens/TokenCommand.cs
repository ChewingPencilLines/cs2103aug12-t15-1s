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

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (attrb.commandType != CommandType.INVALID)
            {
                if (Value == CommandType.DONE)
                {
                    attrb.searchDone = SearchType.DONE;
                    attrb.sortType = SortType.DONE_STATE;
                }
                else if (Value == CommandType.UNDONE)
                    attrb.searchDone = SearchType.UNDONE;
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
