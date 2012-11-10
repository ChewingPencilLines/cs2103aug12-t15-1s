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
            Logger.Info("Created a date token object", "TokenDate::TokenDate");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            switch (attrb.currentMode)
            {
                case ContextType.STARTTIME:
                    Logger.Info("currentMode is STARTTIME", "ConfigureGenerator::TokenDate");
                    attrb.StartDateOnly = date;
                    attrb.isSpecific.StartDate = isSpecific;
                    Logger.Info("Updated StartDateOnly and its specificity.", "ConfigureGenerator::TokenDate");
                    // @ivan-todo: WarnUser if already determined startDate
                    break;
                case ContextType.ENDTIME:
                    Logger.Info("currentMode is ENDTIME", "ConfigureGenerator::TokenDate");
                    attrb.SetConditionalEndDate(date, isSpecific);
                    Logger.Info("Successfully set conditional end date.", "ConfigureGenerator::TokenDate");
                    break;
                case ContextType.DEADLINE:
                    Logger.Info("currentMode is DEADLINE", "ConfigureGenerator::TokenDate");
                    attrb.EndDateOnly = date;
                    attrb.isSpecific.EndDate = isSpecific;
                    Logger.Info("Updated EndDateOnly and its specificity.", "ConfigureGenerator::TokenDate");
                    break;
                default:
                    Logger.Error("Fell through currentMode switch statement.", "ConfigureGenerator::TokenDate");
                    Debug.Assert(false, "Fell through switch statement in ConfigureGenerator, TokenDate!");
                    break;
            }
        }
    }
}
