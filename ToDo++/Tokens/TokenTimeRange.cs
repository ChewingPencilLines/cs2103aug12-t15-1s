using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenTimeRange : Token
    {
        TimeRangeKeywordsType timeRange;
        TimeRangeType timeRangeType;
        int index;

        internal int Value
        {
            get { return index; }
        }
        internal TimeRangeType Type
        {
            get { return timeRangeType; }
        }
        internal TimeRangeKeywordsType Range
        {
            get { return timeRange; }
        }
        internal TokenTimeRange(int position, int val, TimeRangeType type, TimeRangeKeywordsType range)
            : base(position)
        {
            index = val;
            timeRangeType = type;
            timeRange = range;
        }

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (index != null)
            {
                attrb.rangeIndexes[0] = index;
            }
            attrb.timeRangeType = timeRangeType;
            attrb.timeRange = timeRange;
        }
    }
}
