using System;
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
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (attrb.taskName == null)
            {
                attrb.taskName = literal;
                Logger.Info("Updated taskName.", "UpdateAttributes::TokenLiteral");
            }
        }
    }
}
