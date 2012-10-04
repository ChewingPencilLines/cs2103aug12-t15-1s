using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            command = Console.ReadLine();
            Logic a = new Logic();
            string feedback;
            feedback = a.ProcessCommand(command);
        }
    }
}
