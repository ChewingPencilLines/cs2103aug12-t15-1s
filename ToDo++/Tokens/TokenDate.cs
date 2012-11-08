﻿using System;
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
                    // @ivan-todo: WarnUser if already determined startDate
                    break;
                case ContextType.ENDTIME:
                    attrb.SetConditionalEndDate(date, isSpecific);
                    break;
                case ContextType.DEADLINE:
                    attrb.EndDateOnly = date;
                    attrb.isSpecific.EndDate = isSpecific;
                    break;
                default:
                    Debug.Assert(false, "Fell through switch statement in UpdateAttributes, TokenDate!");
                    break;
            }
        }
    }
}
