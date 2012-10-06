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
using Hotkeys;

namespace ToDo
{
    public partial class UI : Form
    {

        private Hotkeys.GlobalHotkey ghk;
        SettingsManager mainSettingsManager;

        public UI()
        {
            InitializeComponent();
            PrepareSystemTray();
            PrepareSettingsManager();
        }

        #region PrepareSettingsManager
        
        public void PrepareSettingsManager()
        {
            mainSettingsManager = new SettingsManager();
            SetSettingsInUI();
        }

        public void SetSettingsInUI()
        {
            loadOnStartupToolStripMenuItem.Checked = mainSettingsManager.GetLoadOnStartupStatus();
            startMinimizedToolStripMenuItem.Checked = mainSettingsManager.GetStartMinimizedStatus();
        }

        private void invertMinimizedToolStrip()
        {
            if (startMinimizedToolStripMenuItem.Checked == true)
                startMinimizedToolStripMenuItem.Checked = false;
            else
                startMinimizedToolStripMenuItem.Checked = true;
        }

        private void invertStartupToolStrip()
        {
            if (loadOnStartupToolStripMenuItem.Checked == true)
                loadOnStartupToolStripMenuItem.Checked = false;
            else
                loadOnStartupToolStripMenuItem.Checked = true;
        }

        private void startMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertMinimizedToolStrip();
            mainSettingsManager.ToggleStartMinimized(startMinimizedToolStripMenuItem.Checked);
        }

        private void loadOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertStartupToolStrip();
            mainSettingsManager.ToggleLoadOnStartup(loadOnStartupToolStripMenuItem.Checked);
        }

        #endregion

        #region MinimizeMaximizeSystemTrayHotKey

        private void PrepareSystemTray()
        {
            ghk = new Hotkeys.GlobalHotkey(Constants.ALT, Keys.Q, this);
            ghk.Register();
            notifyIcon_taskBar.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
                MinimiseMaximiseTray();
            base.WndProc(ref m);
        }

        //Calling this Minimises or Maximizes the application into system tray depending on state
        private void MinimiseMaximiseTray()
        {
            notifyIcon_taskBar.BalloonTipTitle = "ToDo++";
            notifyIcon_taskBar.BalloonTipText = "Hit Alt+Q to bring it up";

            //If Window is Open
            if (FormWindowState.Normal == this.WindowState && notifyIcon_taskBar.Visible == false)
            {
                this.Hide();
                notifyIcon_taskBar.Visible = true;
                notifyIcon_taskBar.ShowBalloonTip(500);
            }
            //If Window is in tray
            else
            {
                notifyIcon_taskBar.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        //Double click the tray icon and it pops back up
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MinimiseMaximiseTray();
        }

        //Deregisters the hot-keys when the application closes
        private void UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
                MessageBox.Show("Hotkeys failed to unregister!");
        }

        #endregion

        #region TextBoxInputFormatting

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
            SetFormat(richTextBox_output, Color.Blue, "Username: ");
            SetFormat(richTextBox_output, Color.Black, userInput);
            SetFormat(richTextBox_output, Color.Red, "\n");
            SetFormat(richTextBox_output, Color.Red, "ToDo++: ");
            SetFormat(richTextBox_output, Color.Black, "aa");
            SetFormat(richTextBox_output, Color.Red, "\n");
            textBox_input.Text = "";
            richTextBox_output.ScrollToCaret();
        }

        //Go Button Clicked
        private void button_go_Click(object sender, EventArgs e)
        {
            DisplayCommand(textBox_input.Text);
        }

        //Enter Pressed while inputbox is in focus
        private void textBox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DisplayCommand(textBox_input.Text);
            }
        }

        #endregion

        //You can add keyboard commands here
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
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

        //Open up settings page
        private void settingsClicked(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings(mainSettingsManager);
            settingsForm.Show();
        }

    }
}
