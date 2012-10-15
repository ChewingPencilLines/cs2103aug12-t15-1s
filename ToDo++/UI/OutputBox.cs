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
        private Settings settings;

        /// <summary>
        /// Set the settings of the OutputBox Control, so interaction is possible
        /// </summary>
        /// <param name="passedSettings">Instance of settings passed in by pointer</param>
        public void SetSettings(Settings passedSettings) { settings = passedSettings; }

        /// <summary>
        /// Currently sets the Text Size of the OutputBox
        /// </summary>
        public void LoadSettingsIntoOutput()
        {
            this.SetOutputSize(settings.GetTextSize());
        }

        #region TextSizeControl

        /// <summary>
        /// Sets the Text Size of the OutputBox directly
        /// </summary>
        /// <param name="size">Size of Text in int</param>
        public void SetOutputSize(int size)
        {
            this.SelectAll();
            var family = this.SelectedText.ToString();
            this.SelectionFont = new Font(family, size);
            this.DeselectAll();
        }

        /// <summary>
        /// Decrease the Text Size by 1 unit, while modifying the settings
        /// </summary>
        public void DecreaseSizeOfOutput()
        {
            settings.DecreaseTextSize();
            this.SetOutputSize(settings.GetTextSize());
        }

        /// <summary>
        /// Increase the Text Size by 1 unit, while modifying the settings
        /// </summary>
        public void IncreaseSizeOfOutput()
        {
            settings.IncreaseTextSize();
            this.SetOutputSize(settings.GetTextSize());
        }

        #endregion

        #region FormattingControl

        /// <summary>
        /// Set Formatting of Text to be set into OutputBox
        /// </summary>
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

        /// <summary>
        /// This method displays both ToDo++ output and input with correct formatting
        /// </summary>
        /// <param name="userInput">What the User typed in the input box</param>
        /// <param name="systemOutput">What ToDo++ returns as an output</param>
        public void DisplayCommand(string userInput,string systemOutput)
        {
            SetFormat(Color.Blue, "User: ", 8);
            SetFormat(Color.Black, userInput, 8);
            SetFormat(Color.Red, "\n", 8);
            SetFormat(Color.Red, "ToDo++: ", 8);
            SetFormat(Color.Black, systemOutput, 8);
            SetFormat(Color.Red, "\n", 8);
            this.ScrollToCaret();
        }

    }
}
