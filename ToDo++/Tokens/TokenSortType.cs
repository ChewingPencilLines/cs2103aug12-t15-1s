﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenSortType : Token
    {
        SortType sortType;
        
        internal TokenSortType(int position, SortType val)
            : base(position)
        {
            sortType = val;
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            attrb.sortType = sortType;
            Logger.Info("Updated sortType.", "UpdateAttributes::TokenSortType");
        }

    }
}
