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
        internal TokenTimeRange(int position, int val, TimeRangeType type)
            : base(position)
        {
            index = val;
            timeRangeType = type;
            timeRange = TimeRangeKeywordsType.NONE;
        }
        internal TokenTimeRange(int position, TimeRangeKeywordsType range)
            : base(position)
        {
            index = 0;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRange = range;
        }

        internal override void UpdateAttributes(OperationGenerator attrb)
        {
            if (index != 0 && attrb.timeRangeIndex == 0)
            {
                attrb.timeRangeIndex = index;
            }
            if (timeRangeType != TimeRangeType.DEFAULT)
            {
                if (attrb.timeRangeType == TimeRangeType.DEFAULT)
                {
                    attrb.timeRangeType = timeRangeType;
                }
                else
                {
                    AlertBox.Show("Multiple task durations specified. Only the first is accepted.");
                }
            }
            if (timeRange != TimeRangeKeywordsType.NONE)
            {
                if (attrb.timeRangeOne == TimeRangeKeywordsType.NONE)
                {
                    attrb.timeRangeOne = timeRange;
                }
                else if (attrb.timeRangeTwo == TimeRangeKeywordsType.NONE
                    || attrb.timeRangeTwo <= timeRange)
                {
                    attrb.timeRangeTwo = timeRange;
                }
            }
        }
    }
}