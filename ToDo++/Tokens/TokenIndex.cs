using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenIndex : Token
    {
        int[] index;
        bool isAll;

        internal bool IsAll
        {
            get { return isAll; }
            set { isAll = value; }
        }
        internal int[] Value
        {
            get { return index; }
        }
        internal TokenIndex(int position, int[] val, bool isAll)
            : base(position)
        {
            index = val;
            this.isAll = isAll;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            throw new NotImplementedException();
        }
    }
}
