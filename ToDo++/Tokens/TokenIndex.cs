using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenIndex : Token
    {
        string index;
        internal string Value
        {
            get { return index; }
        }
        internal TokenIndex(int position, string val)
            : base(position)
        {
            index = val;
        }
    }
}
