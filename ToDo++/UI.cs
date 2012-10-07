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
            MinimiseToTrayWhenChecked();
        }

        public void MinimiseToTrayWhenChecked()
        {
            if (mainSettingsManager.GetStartMinimizedStatus() == true)
            {
                MinimiseMaximiseTray();
            }
        }

        public void SetSettingsInUI()
        {
            loadOnStartupToolStripMenuItem.Checked = mainSettingsManager.GetLoadOnStartupStatus();
            startMinimizedToolStripMenuItem.Checked = mainSettingsManager.GetStartMinimizedStatus();
            richTextBox_output.SetOutputSize(mainSettingsManager.GetTextSize());
        }

        public void IncreaseSizeOfOutput()
        {
            mainSettingsManager.IncreaseTextSize();
            richTextBox_output.SetOutputSize(mainSettingsManager.GetTextSize());
        }

        public void DecreaseSizeOfOutput()
        {
            mainSettingsManager.DecreaseTextSize();
            richTextBox_output.SetOutputSize(mainSettingsManager.GetTextSize());
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

        private void toDoToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            SetSettingsInUI();
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
            if (notifyIcon_taskBar.Visible == false)
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

        private void ProcessText()
        {
            richTextBox_output.DisplayCommand(textBox_input.Text);
            richTextBox_output.SetOutputSize(mainSettingsManager.GetTextSize());
            textBox_input.Clear();
        }

        //Go Button Clicked
        private void button_go_Click(object sender, EventArgs e)
        {
            ProcessText();
        }

        //Enter Pressed while inputbox is in focus
        private void textBox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ProcessText();
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
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings(mainSettingsManager);
            settingsForm.ShowDialog();
        }

        private void increaseSizeButton_Click(object sender, EventArgs e)
        {
            IncreaseSizeOfOutput();
        }

        private void decreaseSizeButton_Click(object sender, EventArgs e)
        {
            DecreaseSizeOfOutput();
        }



    }
}
