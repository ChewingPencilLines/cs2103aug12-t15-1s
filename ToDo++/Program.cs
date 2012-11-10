﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.Info("Starting Application...", "Main");
            Logic logic = new Logic();
            Application.Run(new UI(logic));
            Logger.Info("Application terminated!\r\n", "Main");         
        }

    }
}
