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

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            switch (attrb.currentMode)
            {
                case ContextType.STARTTIME:
                    attrb.StartTimeOnly = time;
                    attrb.isSpecific.StartTime = specific;
                    Logger.Info("Updated StartTimeOnly and its specificity.", "UpdateAttributes::TokenTime");
                    break;
                case ContextType.ENDTIME:
                    attrb.SetConditionalEndTime(time, specific);
                    Logger.Info("Successfully set conditional end time.", "UpdateAttributes::TokenTime");
                    break;
                case ContextType.DEADLINE:
                    attrb.EndTimeOnly = time;
                    attrb.isSpecific.EndTime = specific;
                    Logger.Info("Updated EndTimeOnly and its specificity.", "UpdateAttributes::TokenTime");
                    break;
                default:
                    Logger.Warning("Fell through switch statement.", "UpdateAttributes::TokenTime");
                    Debug.Assert(false, "Fell through switch statement in UpdateAttributes, TokenTime!");
                    break;
            }
        }
    }
}
