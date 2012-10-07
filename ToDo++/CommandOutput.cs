using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToDo
{
    class CommandOutput: RichTextBox
    {
        //Set Formatting for your Text
        public void SetFormat(Color color, string text, int size)
        {
            RichTextBox box = this;
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            box.SelectionFont = new Font("Tahoma", size);
            // could set box.SelectionBackColor, box.SelectionFont, etc...
            box.SelectionLength = 0; // clear
        }

        //Append the Output Window
        public void DisplayCommand(string userInput)
        {
            SetFormat(Color.Blue, "Username: ", 8);
            SetFormat(Color.Black, userInput, 8);
            SetFormat(Color.Red, "\n", 8);
            SetFormat(Color.Red, "ToDo++: ", 8);
            SetFormat(Color.Black, "aa", 8);
            SetFormat(Color.Red, "\n", 8);
            this.ScrollToCaret();
        }

        public void SetOutputSize(int size)
        {
            this.SelectAll();
            var family = this.SelectedText.ToString();
            //var style = this.SelectionFont.Style;
            this.SelectionFont = new Font(family, size);
            this.DeselectAll();
        }
    }
}
