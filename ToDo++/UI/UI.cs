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

        // ******************************************************************
        // Constructors.
        // ******************************************************************

        #region Constructor

        private Hotkeys.GlobalHotkey ghk;       //Global Hotkey to Minimize to System Tray
        SettingsManager mainSettingsManager;    //Settings Manager stores all settings data, including Flexi-Commands
        Logic logic;                            //Instance of Logic that handles Data structure and File Operations

        /// <summary>
        /// Creates a new instance of the Main Program (UI) and loads the various Classes
        /// </summary>
        public UI()
        {
            InitializeComponent();
            PrepareSystemTray();                //Loads Code to place App in System Tray
            PrepareSettingsManager();           //Loads initial Settings of App and applies the settings
            PrepareMenu();                      //Loads the menu strip
            PrepareOutputBox();                 //Loads Output Box
            PrepareLogic();                     //Creates instance of Logic to be used by Text Processing
        }

        #endregion

        // ******************************************************************
        // Win32 Functions
        // ******************************************************************

        #region Win32Functions

        /// <summary>
        /// Code for placing App in System Tray
        /// </summary>
        #region SystemTray

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

        /// <summary>
        /// Registers the App with the Registry to open on Startup
        /// </summary>
        #region RegisterToOpenOnStartup

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

        #endregion

        // ******************************************************************
        // Prepare Settings Manager
        // ******************************************************************

        #region PrepareSettingsManager

        /// <summary>
        /// Creates an Instance of Settings Manager
        /// </summary>
        public void PrepareSettingsManager()
        {
            mainSettingsManager = new SettingsManager();
            mainSettingsManager.PushCommands();
            MinimiseToTrayWhenChecked();
            RegisterLoadOnStartupWhenChecked();
        }

        /// <summary>
        /// Minimizes App to System tray if true
        /// </summary>
        public void MinimiseToTrayWhenChecked()
        {
            if (mainSettingsManager.GetStartMinimizedStatus() == true)
                MinimiseMaximiseTray();
        }

        /// <summary>
        /// Sets the Load on Startup Status
        /// </summary>
        public void RegisterLoadOnStartupWhenChecked()
        {
            if (mainSettingsManager.GetLoadOnStartupStatus() == true)
                RegisterInStartup(true);
            else
                RegisterInStartup(false);
        }

        #endregion

        // ******************************************************************
        // Prepare the Menu Bar
        // ******************************************************************

        #region PrepareMenu

        /// <summary>
        /// Prepare the Menu Bar. Pass an instance of settings manager into it so it can interact with it
        /// </summary>
        public void PrepareMenu()
        {
            menuStrip.SetSettingsManager(mainSettingsManager);
            menuStrip.LoadSettingsIntoMenu();
        }

        #endregion

        // ******************************************************************
        // Prepare the Output Box
        // ******************************************************************

        #region PrepareOutputBox

        /// <summary>
        /// Prepare the Output Box. Pass an instance of settings manager into it so it can interact with it
        /// </summary>
        public void PrepareOutputBox()
        {
            outputBox.SetSettingsManager(mainSettingsManager);
            outputBox.LoadSettingsIntoOutput();
        }

        #endregion

        // ******************************************************************
        // Code for creating an instance of Logic goes here
        // ******************************************************************

        #region PrepareLogic

        public void PrepareLogic()
        {          

            logic = new Logic();
        }

        #endregion

        // ******************************************************************
        // Code that interacts with logic and returns an output goes here
        // ******************************************************************

        #region TextInput

        /// <summary>
        /// Passes an the user text to Logic, which processes it and returns an output to be displayed
        /// </summary>
        private void ProcessText()
        {
            string input = textBox_input.Text;
            string output=logic.ProcessCommand(input);

            outputBox.DisplayCommand(input,output);
            outputBox.SetOutputSize(mainSettingsManager.GetTextSize());
            textBox_input.Clear();
        }

        /// <summary>
        /// When Go Button Clicked
        /// </summary>
        private void button_go_Click(object sender, EventArgs e)
        {
            ProcessText();
        }

        /// <summary>
        /// When Enter Button Pressed
        /// </summary>
        private void textBox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ProcessText();
            }
        }

        #endregion

        // ******************************************************************
        // Keyboard Commands
        // ******************************************************************

        #region KeyboardCommands

        /// <summary>
        /// Holds all Shortcut Keys
        /// </summary>
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

        /// <summary>
        /// Exit the Application
        /// </summary>
        public static void Exit()
        {
            Application.Exit();
        }

        /// <summary>
        /// Increase size of Text
        /// </summary>
        private void increaseSizeButton_Click(object sender, EventArgs e)
        {
            outputBox.IncreaseSizeOfOutput();
        }

        /// <summary>
        /// Decrease size of text
        /// </summary>
        private void decreaseSizeButton_Click(object sender, EventArgs e)
        {
            outputBox.DecreaseSizeOfOutput();
        }



    }
}
