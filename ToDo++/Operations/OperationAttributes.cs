using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationAttributes
    {
        CommandType commandType = new CommandType();
        ContextType currentMode = new ContextType();
        ContextType currentSpecifier = new ContextType();
        TimeSpan? startTime = null, endTime = null;
        DateTime? startDate = null, endDate = null;
        DayOfWeek? startDay = null, endDay = null;
        DateTime? startCombined = null, endCombined = null;
        DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        string taskName = null;
        int[] taskIndex = null;
    }
}
