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
                    attrb.StartTimeOnly = time;
                    attrb.isSpecific.StartTime = specific;
                    break;
                case ContextType.ENDTIME:
                    attrb.SetConditionalEndTime(time, specific);
                    break;
                case ContextType.DEADLINE:
                    attrb.EndTimeOnly = time;
                    attrb.isSpecific.EndTime = specific;
                    break;
                default:
                    Debug.Assert(false, "Fell through switch statement in UpdateAttributes, TokenTime!");
                    break;
            }
        }
    }
}
