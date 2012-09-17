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
    public partial class UI : Form
    {
        private Processing processingClass;

        public UI()
        {
            InitializeComponent();
            processingClass = new Processing();
            inputText.Focus();
        }

        //Set Formatting for your Text
        private void SetFormat(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            // could set box.SelectionBackColor, box.SelectionFont, etc...
            box.SelectionLength = 0; // clear
        }

        //Append the Output Window
        void DisplayCommand(string userInput)
        {
            SetFormat(outputText, Color.Blue,"Username: ");
            SetFormat(outputText, Color.Black,userInput);
            SetFormat(outputText, Color.Red, "\n");
            SetFormat(outputText, Color.Red, "ToDo++: ");
            SetFormat(outputText, Color.Black, processingClass.returnOutput(userInput));
            SetFormat(outputText, Color.Red, "\n");
            inputText.Text = "";
            outputText.ScrollToCaret();
        }

        //Press Enter
        private void inputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DisplayCommand(inputText.Text);
            }
        }

        //Go Button Clicked
        private void Go_Click(object sender, EventArgs e)
        {
            DisplayCommand(inputText.Text);
        }

        //Minimize to Tray with over-ride for short cut
        private void MinimiseToTray(bool shortCutPressed)
        {
            notifyIcon.BalloonTipTitle = "Minimize to Tray App";
            notifyIcon.BalloonTipText = "You have successfully minimized your app.";

            if (FormWindowState.Minimized == this.WindowState || shortCutPressed==true)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        //Double click the tray icon and it pops back up (Need a way to have a shortcut invoke the app again)
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            MinimiseToTray(false);
        }

        //Pressing ALT+Q Will Minimize the app to the tray
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Q))
            {
                System.Diagnostics.Trace.WriteLine("Pressed");
                MinimiseToTray(true);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Q))
            {
                Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Exit Command. Choose to Popup options before exiting
        public static void Exit()
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

    }
}
