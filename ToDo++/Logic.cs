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

        }

        public string ProcessCommand(string input)
        {
            string command = ValidateCommand(input);
            Operation op = DecomposeCommand(command);
            return ExecuteCommand(op);
        }

        private string ExecuteCommand(Operation OP)
        {
            if (OP.Equals(null))
            {
                return null;
            }
            else
            {
                Execute exe = new Execute(OP);
                return exe.result;
            }
        }

        private Operation DecomposeCommand(string command)
        {
            if (command.Equals(null))
            {
                return null;
            }
            else
            {
                CommandParser CP = new CommandParser();
                return CP.ParseOperation(command);
            }
        }

        private string ValidateCommand(string command)
        {
            if (!command.Equals(null) & command.Length == 0)
            {
                return null;
            }
            else
            {
                CommandStack.Push(command);
                return command;
            }
        }

    }
}
