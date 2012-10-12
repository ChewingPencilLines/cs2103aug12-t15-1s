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
using Microsoft.Win32;

namespace ToDo
{
    public partial class UI : Form
    {

        private Hotkeys.GlobalHotkey ghk;
        SettingsManager mainSettingsManager;

        public UI()
        {
            InitializeComponent();
            PrepareSystemTray();                //Loads Code to place App in System Tray
            PrepareSettingsManager();           //Loads initial Settings of App and applies the settings
            PrepareMenu();                      //Loads the menu strip
            PrepareOutputBox();                 //Loads Output Box
        }

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

        //Calling this Minimizes or Maximizes the application into system tray depending on state
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

        //De registers the hot-keys when the application closes
        private void UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
                MessageBox.Show("Hotkeys failed to unregister!");
        }

        #endregion

        #region PrepareSettingsManager

        public void PrepareSettingsManager()
        {
            mainSettingsManager = new SettingsManager();
            MinimiseToTrayWhenChecked();
            RegisterLoadOnStartupWhenChecked();
        }

        //Checks if App needs to be minimized to tray initially
        public void MinimiseToTrayWhenChecked()
        {
            if (mainSettingsManager.GetStartMinimizedStatus() == true)
                MinimiseMaximiseTray();
        }

        public void RegisterLoadOnStartupWhenChecked()
        {
            if (mainSettingsManager.GetLoadOnStartupStatus() == true)
                RegisterInStartup(true);
            else
                RegisterInStartup(false);
        }

        private void RegisterInStartup(bool isChecked)
        {
            if (mainSettingsManager.GetLoadOnStartupStatus() == true)
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (isChecked)
                {
                    registryKey.SetValue("ApplicationName", Application.ExecutablePath);
                }
                else
                {
                    registryKey.DeleteValue("ApplicationName");
                }
            }
        }

        #endregion

        #region PrepareMenu

        public void PrepareMenu()
        {
            menuStrip.SetSettingsManager(mainSettingsManager);
            menuStrip.LoadSettingsIntoMenu();
        }

        #endregion

        #region PrepareOutputBox

        public void PrepareOutputBox()
        {
            outputBox.SetSettingsManager(mainSettingsManager);
            outputBox.LoadSettingsIntoOutput();
        }

        #endregion

        #region TextInput

        private void ProcessText()
        {
            outputBox.DisplayCommand(textBox_input.Text);
            outputBox.SetOutputSize(mainSettingsManager.GetTextSize());
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

        #region KeyboardCommands

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Q))
            {
                Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        //Exit Command. Choose to Popup options before exiting
        public static void Exit()
        {
            Application.Exit();
        }

        private void increaseSizeButton_Click(object sender, EventArgs e)
        {
            outputBox.IncreaseSizeOfOutput();
        }

        private void decreaseSizeButton_Click(object sender, EventArgs e)
        {
            outputBox.DecreaseSizeOfOutput();
        }



    }
}
