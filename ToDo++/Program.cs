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
            Application.Run(new UI());
            
            /*
             * this is test for logic function add, display and delete
             * to see the result, uncommend below and commend above
             */
           
            /*
            Logic a = new Logic();
            Task t=new TaskFloating("alice");
            OperationAdd b = new OperationAdd(t);
            string feedback = a.ExecuteCommand(b);
            Console.WriteLine(feedback);
            OperationSearch c = new OperationSearch("");
            feedback = a.ExecuteCommand(c);
            Console.WriteLine(feedback);
            OperationDelete d = new OperationDelete(1);
            feedback = a.ExecuteCommand(d);
            Console.WriteLine(feedback);
            //MessageBox.Show(feedback);
            */
        }

        //define taskList as a global variable to store tasks.
        public static TaskList taskList = new TaskList();

    }
}
