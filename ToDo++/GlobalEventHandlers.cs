﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public static class EventHandlers
    {
        public static event EventHandler StayOnTopHandler;
        public static void StayOnTop(bool status){ StayOnTopHandler(status, EventArgs.Empty);}

        
    }
}
