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

namespace ToDo__
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Set Formatting for your Text
        void AppendText(RichTextBox box, Color color, string text)
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

        //Append the RTB
        void DisplayCommand(string text)
        {
            AppendText(outputText, Color.Red, "ToDo++: ");
            AppendText(outputText, Color.Black, text);
            AppendText(outputText, Color.Red, "\n");
            inputText.Text = "";
        }

        //Button Clicked
        private void button1_Click(object sender, EventArgs e)
        {
            DisplayCommand(inputText.Text);
        }

        //Press Enter
        private void inputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DisplayCommand(inputText.Text);
            }
        }

        //Minise to Tray with ovverride for shortcutkey
        private void minimiseToTray(bool shortCutPressed)
        {
            notifyIcon1.BalloonTipTitle = "Minimize to Tray App";
            notifyIcon1.BalloonTipText = "You have successfully minimized your form.";

            if (FormWindowState.Minimized == this.WindowState || shortCutPressed==true)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        //When we hit minimize, it goes to tray, you can choose to remove this
        private void frmMain_Resize(object sender, EventArgs e)
        {
            //minimiseToTray(false);
        }

        //Double click the tray icon and it pops back up (Need a way to have a shortcut invoke the app again)
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            minimiseToTray(false);
        }

        //Pressing ALT+Q Will Minimize the app to the tray
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Q))
            {
                System.Diagnostics.Trace.WriteLine("Pressed");
                minimiseToTray(true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Paramters to load when Form Opens.
        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
