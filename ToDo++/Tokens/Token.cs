using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public abstract class Token
    {
        static private List<CommandType> indexRangeableCommandTypes
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.MODIFY, CommandType.POSTPONE };

        static private List<CommandType> timeRangeableCommandTypes
            = new List<CommandType> { CommandType.SCHEDULE, CommandType.POSTPONE};

        private int position;
        internal Token(int position)
        {
            this.position = position;
        }  
        internal int Position
        {
            get { return position; }           
        }

        internal abstract void UpdateAttributes(OperationAttributes attrb);

        internal bool RequiresIndexRange()
        {
            if(this.GetType() == typeof(TokenCommand))
            {
                TokenCommand token = (TokenCommand)this;
                if (indexRangeableCommandTypes.Contains(token.Value))
                    return true;
            }
            return false;
        }

        internal bool RequiresTimeRange()
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