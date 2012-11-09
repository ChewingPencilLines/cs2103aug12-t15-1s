using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDo
{
    class InputBox:TextBox
    {
        int currentIndex=0;
        List<string> commandsEntered = new List<string>();

        /// <summary>
        /// Adds a command entry to the input box
        /// </summary>
        /// <param name="commandEntered"></param>
        public void AddToList(string commandEntered)
        {
            commandsEntered.Add(commandEntered);
            currentIndex = commandsEntered.Count;
        }

        /// <summary>
        /// Goes to previous command
        /// </summary>
        public void UpdateWithPrevCommand()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                this.Text = commandsEntered[currentIndex];
                this.SelectionStart = this.Text.Length;
            }
        }

        /// <summary>
        /// Goes to next command
        /// </summary>
        public void UpdateWithNextCommand()
        {
            if (currentIndex < (commandsEntered.Count-1))
            {
                currentIndex++;
                this.Text = commandsEntered[currentIndex];
                this.SelectionStart = this.Text.Length;
            }
        }
    }
}
