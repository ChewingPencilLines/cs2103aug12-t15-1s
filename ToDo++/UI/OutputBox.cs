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
    class OutputBox: RichTextBox
    {
        private SettingsManager settingsManager;

        public void SetSettingsManager(SettingsManager passedSettingsManager) { settingsManager = passedSettingsManager; }

        public void LoadSettingsIntoOutput()
        {
            this.SetOutputSize(settingsManager.GetTextSize());
        }

        #region TextSizeControl

        public void SetOutputSize(int size)
        {
            this.SelectAll();
            var family = this.SelectedText.ToString();
            this.SelectionFont = new Font(family, size);
            this.DeselectAll();
        }

        public void DecreaseSizeOfOutput()
        {
            settingsManager.DecreaseTextSize();
            this.SetOutputSize(settingsManager.GetTextSize());
        }

        public void IncreaseSizeOfOutput()
        {
            settingsManager.IncreaseTextSize();
            this.SetOutputSize(settingsManager.GetTextSize());
        }

        #endregion

        #region FormattingControl

        //Set Formatting for your Text
        public void SetFormat(Color color, string text, int size)
        {
            RichTextBox box = this;
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            box.SelectionFont = new Font("Tahoma", size);
            box.SelectionLength = 0;
        }

        #endregion

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

    }
}
