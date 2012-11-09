using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    public class TokenDate : Token
    {
        Specificity isSpecific;
        DateTime date;

        internal Specificity IsSpecific
        {
            set { isSpecific = value; }
        }


        internal TokenDate(int position, DateTime date, Specificity isSpecific)
            : base(position)
        {
            this.date = date;
            this.isSpecific = isSpecific;
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            switch (attrb.currentMode)
            {
                case ContextType.STARTTIME:
                    attrb.StartDateOnly = date;
                    attrb.isSpecific.StartDate = isSpecific;
                    Logger.Info("Updated StartDateOnly and its specificity.", "UpdateAttributes::TokenDate");
                    // @ivan-todo: WarnUser if already determined startDate
                    break;
                case ContextType.ENDTIME:
                    attrb.SetConditionalEndDate(date, isSpecific);
                    Logger.Info("Successfully set conditional end date.", "UpdateAttributes::TokenDate");
                    break;
                case ContextType.DEADLINE:
                    attrb.EndDateOnly = date;
                    attrb.isSpecific.EndDate = isSpecific;
                    Logger.Info("Updated EndDateOnly and its specificity.", "UpdateAttributes::TokenDate");
                    break;
                default:
                    Logger.Warning("Fell through switch statement.", "UpdateAttributes::TokenDate");
                    Debug.Assert(false, "Fell through switch statement in UpdateAttributes, TokenDate!");
                    break;
            }
        }
    }
}
