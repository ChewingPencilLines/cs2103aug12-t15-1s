using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenCommand : Token
    {
        public const int START_INDEX = 0;
        public const int END_INDEX = 1;
        public const int RANGE = 2;
        CommandType commandType;
        int[] taskIndex;

        internal CommandType Value
        {
            get { return commandType; }
        }
        internal int[] TaskIndex
        {
            get { return taskIndex; }
        }

        internal TokenCommand(int position, CommandType val, int[] taskIndex = null)
            : base(position)
        {
            commandType = val;
            this.taskIndex = taskIndex;
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
                if (CustomDictionary.IsIndexableCommandType(commandType))
                {
                    attrb.taskIndex = TaskIndex;
                }
            }
        }
    }
}
