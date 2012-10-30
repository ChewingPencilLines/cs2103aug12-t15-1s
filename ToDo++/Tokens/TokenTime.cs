using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    public class TokenTime : Token
    {
        TimeSpan time;
        Boolean specific;
        internal TimeSpan Value
        {
            get { return time; }
        }
        internal Boolean IsSpecific
        {
            get { return specific; }
        }
        internal TokenTime(int position, TimeSpan val, Boolean specific_flag)
            : base(position)
        {
            time = val;
            specific = specific_flag;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            switch (attrb.currentMode)
            {
                case ContextType.STARTTIME:
                    attrb.StartTime = Value;
                    attrb.isSpecific.StartTime = IsSpecific;
                    break;
                case ContextType.ENDTIME:
                    attrb.EndTime = Value;
                    attrb.isSpecific.EndTime = IsSpecific;
                    break;
                case ContextType.DEADLINE:
                    attrb.EndTime = Value;
                    attrb.isSpecific.EndTime = IsSpecific;
                    break;
                default:
                    Debug.Assert(false, "Fell through switch statement in GenerateOperation, TokenTime case!");
                    break;
            }
        }
    }
}
