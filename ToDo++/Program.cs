using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            string command, feedback;
            Logic AI = new Logic();

            command = Console.ReadLine();       
            feedback = AI.ProcessCommand(command);
            Console.WriteLine(feedback);
        }
    }
}
