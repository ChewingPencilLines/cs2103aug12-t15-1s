﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenLiteral : Token
    {
        string literal;

        internal TokenLiteral(int position, string val)
            : base(position)
        {
            literal = val;
            Logger.Info("Created a literal token", "TokenLiteral::TokenLiteral");
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (attrb.taskName == null)
            {
                attrb.taskName = literal;
                Logger.Info("Found task name", "UpdateAttributes::TokenLiteral");
            }
            else
            {
                attrb.taskName += " " + literal;
                Logger.Info("Task name already defined but more literals present. Appending to task name.", "UpdateAttributes::TokenLiteral");
            }
        }
    }
}
