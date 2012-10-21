using System;
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
            Task task = new TaskDeadline("testTask",DateTime.Now);
            Storage IO = new Storage("a", "b");
            IO.CreateNewTaskFile("notused.xml");
            IO.AddTask(task,"id");
            Application.Run(new UI());             
        }

    }
}
