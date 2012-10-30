using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    public class TokenDate : Token
    {
        Specificity isSpecific;
        DateTime dateTime;
        internal Specificity IsSpecific
        {
            get { return isSpecific; }
            set { isSpecific = value; }
        }
        internal DateTime Value
        {
            get { return dateTime; }
        }

        internal TokenDate(int position, DateTime date, Specificity isSpecific)
            : base(position)
        {
            dateTime = date;
            this.isSpecific = isSpecific;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            switch (attrb.currentMode)
            {
                case ContextType.STARTTIME:
                    attrb.startDate = Value;
                    // @ivan-todo: WarnUser if already determined startDate
                    break;
                case ContextType.ENDTIME:
                    attrb.endDate = Value;
                    break;
                case ContextType.DEADLINE:
                    attrb.endDate = Value;
                    break;
                default:
                    Debug.Assert(false, "Fell through switch statement in GenerateOperation, TokenDay case!");
                    break;
            }
        }
    }
}
