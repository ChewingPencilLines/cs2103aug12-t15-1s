using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class Logic
    {
        private Stack<string> CommandStack = new Stack<string>();

        public Logic(string command)
        {
            command = GetCommand(command);
            Operation OP = DecomposeCommand(command);
            ExecuteCommand(OP);
        }

        public string ExecuteCommand(Operation OP)
        {
            string result = "";
            //@todo: pass op to crud part and get result;
            return result;
        }

        public Operation DecomposeCommand(string command)
        {
            if (!command.Equals(null))
            {
                CommandStack.Push(command);
                CommandParser CP = new CommandParser();
                return CP.ParseOperation(command);
            }
            return null;
        }

        //get command from direct CLI input/GUI input
        public string GetCommand(string command)
        {
            if (!command.Equals(null) & command.Length == 0)
            {
                return null;
            }
            return command;
        }

    }
}
