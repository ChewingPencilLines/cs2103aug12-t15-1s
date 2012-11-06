using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenIndexRange : Token
    {
        public const int START_INDEX = 0;
        public const int END_INDEX = 1;
        public const int RANGE = 2;
        int[] indexes;
        bool isAll;

        internal bool IsAll
        {
            get { return isAll; }
        }
        internal int[] Value
        {
            get { return indexes; }
        }
        internal TokenIndexRange(int position, int[] val, bool isAll)
            : base(position)
        {
            indexes = val;
            this.isAll = isAll;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (indexes != null)
            {
                attrb.taskRangeIndex = indexes;
            }

            attrb.rangeIsAll = isAll;
        }
    }
}
