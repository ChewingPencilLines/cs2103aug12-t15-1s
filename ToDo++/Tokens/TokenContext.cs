﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenContext : Token
    {
        ContextType contextType;
        internal ContextType Value
        {
            get { return contextType; }
        }
        internal TokenContext(int position, ContextType val)
            : base(position)
        {
            contextType = val;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (Value == ContextType.CURRENT ||
                Value == ContextType.NEXT ||
                Value == ContextType.FOLLOWING
                )
                attrb.currentSpecifier = contextType;
            else attrb.currentSpecifier = contextType;
        }
    }
}
