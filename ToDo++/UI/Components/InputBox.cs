using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    class InputBox:TextBox
    {
        int currentIndex=0;
        List<string> commandsEntered = new List<string>();

        public void AddToList(string commandEntered)
        {
            commandsEntered.Add(commandEntered);
            currentIndex = commandsEntered.Count;
        }

        public void UpdateWithPrevCommand()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                this.Text = commandsEntered[currentIndex];
            }
        }

        public void UpdateWithNextCommand()
        {
            if (currentIndex < (commandsEntered.Count-1))
            {
                currentIndex++;
                this.Text = commandsEntered[currentIndex];
            }
        }
    }
}
