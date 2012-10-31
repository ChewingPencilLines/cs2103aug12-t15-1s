using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public abstract class Token
    {
        static private List<CommandType> rangeableCommandTypes
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.MODIFY, CommandType.POSTPONE };

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

        internal bool RequiresRange()
        {
            if(this.GetType() == typeof(TokenCommand))
            {
                TokenCommand token = (TokenCommand)this;
                if (rangeableCommandTypes.Contains(token.Value))
                    return true;
            }
            return false;
        }
    }
}