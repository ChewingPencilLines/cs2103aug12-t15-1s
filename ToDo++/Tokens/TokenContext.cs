using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenContext : Token
    {
        ContextType contextType;

        internal TokenContext(int position, ContextType val)
            : base(position)
        {
            contextType = val;
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (contextType == ContextType.CURRENT ||
                contextType == ContextType.NEXT ||
                contextType == ContextType.FOLLOWING
                )
            {
                attrb.currentSpecifier = contextType;
                Logger.Info("Updated currentSpecifier.", "UpdateAttributes::TokenContext");
            }
            else
            {
                attrb.currentMode = contextType;
                Logger.Info("Updated currentMode.", "UpdateAttributes::TokenContext");
            }
        }
    }
}
