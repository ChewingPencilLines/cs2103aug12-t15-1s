using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenCommand : Token
    {

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
                throw new MultipleCommandsException();
            }
            else
            {
                attrb.commandType = Value;
            }
        }

    }
}
