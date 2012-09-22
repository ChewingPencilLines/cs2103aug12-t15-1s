using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Logic
    {
        private Stack<string> CommandStack = new Stack<string>();

        public Logic()
        {
            string command;
            command = GetCommand();
            if (!command.Equals(null))
            {
                CommandStack.Push(command);
                CommandParser CP = new CommandParser();
                Operation OP = CP.ParseOperation(command);
            }
            
        }

        public string GetCommand()
        {
            string command = ReadCommand();
            if (!command.Equals(null) & command.Length == 0)
            {
                return null;
            }
            return command;
        }

        //seperate console part for test
        public string ReadCommand()
        {
            string command;
            command = Console.ReadLine();
            return command;
        }

    }
}
