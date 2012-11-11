﻿using System;
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
            Logger.Info("Created an time range token object", "TokenTimeRange::TokenTimeRange");
        }
        internal TokenTimeRange(int position, TimeRangeKeywordsType range)
            : base(position)
        {
            index = 0;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRange = range;
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            bool multipleTaskDurations = false;
            if (index != 0)
            {
                if (attrb.timeRangeIndex == 0)
                {
                    attrb.timeRangeIndex = index;
                }
                else
                {
                    multipleTaskDurations = true;
                    Logger.Warning("Attempted to update timeRangeIndex again", "ConfigureGenerator::TokenTimeRange");
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
                    multipleTaskDurations = true;
                    Logger.Warning("Attempted to update timeRangeType again", "ConfigureGenerator::TokenTimeRange");
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
            if (multipleTaskDurations)
            {
                Logger.Warning("Multiple task durations were detected.", "ConfigureGenerator::TokenTimeRange");
                AlertBox.Show("Multiple task durations specified. Only the first is accepted.");
            }
        }
    }
}