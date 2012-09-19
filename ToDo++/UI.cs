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

        public UI()
        {
            InitializeComponent();
            textBox_input.Focus();
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
            SetFormat(richTextBox_output, Color.Blue,"Username: ");
            SetFormat(richTextBox_output, Color.Black,userInput);
            SetFormat(richTextBox_output, Color.Red, "\n");
            SetFormat(richTextBox_output, Color.Red, "ToDo++: ");
            SetFormat(richTextBox_output, Color.Black, "aa");
            SetFormat(richTextBox_output, Color.Red, "\n");
            textBox_input.Text = "";
            richTextBox_output.ScrollToCaret();
        }

        //Press Enter
        private void inputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DisplayCommand(textBox_input.Text);
            }
        }

        //Go Button Clicked
        private void button_go_Click(object sender, EventArgs e)
        {
            DisplayCommand(textBox_input.Text);
        }

        //Minimize to Tray with over-ride for short cut
        private void MinimiseToTray(bool shortCutPressed)
        {
            notifyIcon_taskBar.BalloonTipTitle = "Minimize to Tray App";
            notifyIcon_taskBar.BalloonTipText = "You have successfully minimized your app.";

            if (FormWindowState.Minimized == this.WindowState || shortCutPressed==true)
            {
                notifyIcon_taskBar.Visible = true;
                notifyIcon_taskBar.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon_taskBar.Visible = false;
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
