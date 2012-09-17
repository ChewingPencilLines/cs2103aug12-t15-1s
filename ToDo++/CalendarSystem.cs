using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ToDo
{
    class CalendarSystem
    {
        Calendar systemCalendar = CultureInfo.InvariantCulture.Calendar;    //Make use of Calendars functions
        DateTime myDT = new DateTime(2002, 4, 3, new GregorianCalendar());

    }
}
