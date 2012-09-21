/**
 * @Alice Jiang
 * Logic part
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo__
{

    class Logic
    {
        string[] commandStack;
        int top;
        string[] Instruction;

        public Logic()
        {
            //initialize
            commandStack = new string[100];
            top = -1;
            //for test
            string test = readCommand();
            string[] testl = new string[4];
            string[] testc = new string[4];
            testl[1] = "add"; testl[2] = "buy milk"; //decompose command in fact
            getInstruction(testl);
            testc = setInstruction();
            Console.WriteLine(testc[1]+" "+testc[2]);//display instruction passed to crud
            showResult("added task buy milk");
        }

        //this function can be used by logic to get raw command
        public string readCommand()
        {
            string command;
            command = Console.ReadLine();
            commandStack[++top] = command; //restore past command.
            return command;
        }

        //the function pass inst from logic to crud unit, used by logic
        public void getInstruction(string[] inst)
        {
            Instruction = inst;
            //for this string array, the first parameter is key word like 'add'
            //the second parameter is task
            //the third & fourth be the start and end time

            //shall we create a new structure for it? I'm not sure.
        }

        //the function pass inst from logic to crud unit, used by crud
        public string[] setInstruction()
        {
            return Instruction;
        }

        //show feedback from crud,used by crud
        public void showResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
