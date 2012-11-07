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

        internal override void UpdateAttributes(OperationAttributes attrb)
        {
            if (index != 0)
            {
                if (attrb.timeRangeIndex == 0)
                {
                    attrb.timeRangeIndex = index;
                }
                else
                {
                    // todo: warn user of multiple inputs
                    // can consider extending functionality in the future to take in multiple time spans i.e. 4 months 6 days
                }
            }
            if (timeRangeType != TimeRangeType.DEFAULT)
            {
                if (attrb.timeRangeType == TimeRangeType.DEFAULT)
                {
                    attrb.timeRangeType = timeRangeType;
                }
                else
                {
                    // todo: warn user of multiple inputs
                    // can consider extending functionality in the future to take in multiple time spans i.e. 4 months 6 days
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
                else
                {
                    // todo: reject non-consecutive inputs
                    // also reject more than double inputs for time ranges i.e. morning afternoon evening
                    // can consider extending functionality in the future to take in multiple consecutives
                }
            }
        }
    }
}