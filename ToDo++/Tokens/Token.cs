using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public abstract class Token
    {
        private int position;
        internal Token(int position)
        {
            this.position = position;
        }  
        internal int Position
        {
            get { return position; }           
        }

        internal abstract void UpdateAttributes(OperationGenerator attrb);
    }
}