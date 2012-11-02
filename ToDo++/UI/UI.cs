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
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;


namespace ToDo
{
    public partial class UI : Form
    {
        

        // ******************************************************************
        // Constructors.
        // ******************************************************************

        #region Constructor

        private Hotkeys.GlobalHotkey ghk;       //Global Hotkey to Minimize to System Tray
        Logic logic;                            //Instance of Logic that handles Data structure and File Operations

        /// <summary>
        /// Creates a new instance of the Main Program (UI) and loads the various Classes
        /// </summary>
        public UI(Logic logic)
        {
            InitializeComponent();
            InitializeLogic(logic);               //Sets logic            
            InitializeSystemTray();               //Loads Code to place App in System Tray
            InitializeSettings();                 //Sets the correct settings to ToDo++ at the start
            InitializeMenu();                     //Loads the Menu
            InitializeOutputBox();                //Loads Output Box    
            InitializeEventHandlers();            //Adds Event Handlers
            InitializePreferencesPanel();
            this.ActiveControl = textInput;
            //this.customPanelControl.SelectedIndex = 2;
            this.taskListViewControl.PopulateListView();

          
        }

        private void ShowTool()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            Point x = new Point(100, 100);

            IWin32Window win = this;
            toolTip1.Show("String", win, x);
            
            toolTip1.Hide(win);
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

        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 1;
        const int HTCAPTION = 2;

        private void InitializeSystemTray()
        {
            ghk = new Hotkeys.GlobalHotkey(Constants.ALT, Keys.Q, this);
            ghk.Register();
            notifyIcon_taskBar.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    /*
                    if (m.Result == (IntPtr)HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }*/
                    int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                    int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                    Point pt = PointToClient(new Point(x, y));
                    Size clientSize = ClientSize;
                    if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                        return;
                    }
                    break;

                case Hotkeys.Constants.WM_HOTKEY_MSG_ID:
                    MinimiseMaximiseTray();
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            //DrawGripper(e);
        }

        public void DrawGripper(PaintEventArgs e)
        {
            if (VisualStyleRenderer.IsElementDefined(
                VisualStyleElement.Status.Gripper.Normal))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
                Rectangle rectangle1 = new Rectangle((Width) - 18, (Height) - 20, 20, 20);
                renderer.DrawBackground(e.Graphics, rectangle1);
            }
        }

        //Calling this Minimizes or Maximizes the application into system tray depending on state
        private void MinimiseMaximiseTray()
        {
            notifyIcon_taskBar.BalloonTipTitle = "ToDo++";
            notifyIcon_taskBar.BalloonTipText = "Hit Alt+Q to bring it up";

            //If Window is Open
            if (notifyIcon_taskBar.Visible == false)
            {
                FadeOut();
                notifyIcon_taskBar.Visible = true;
                notifyIcon_taskBar.ShowBalloonTip(500);
            }
            //If Window is in tray
            else
            {
                FadeIn();
                notifyIcon_taskBar.Visible = false;
                //this.WindowState = FormWindowState.Normal;
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
            if (isChecked == true)
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

        /// <summary>
        /// Allows resizing of borderless form
        /// </summary>
        #region Resizing

        private void UI_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void UI_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);            
        }

        #endregion

        /// <summary>
        /// Creates rounded edge
        /// </summary>
        #region Rounded Edge
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        #endregion

        /// <summary>
        /// Shadow Effect
        /// </summary>
        #region BadShadow

        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion

        /// <summary>
        /// Form Fade In and Out Timers
        /// </summary>
        #region FormFadeInOut

        Timer timer1 = new Timer();
        Timer timer2 = new Timer();

        private void IntializeTimers()
        {
            timer1 = new Timer();
            timer2 = new Timer();
        }

        private void FadeOut()
        {
            timer1.Start();
            timer1.Tick += new EventHandler(timerTickFadeOut);
            timer1.Interval = 15;
        }

        private void FadeIn()
        {
            this.Show();
            this.Opacity = 0;
            timer2.Start();
            timer2.Tick += new EventHandler(timerTickFadeIn);
            timer2.Interval = 15;
        }

        void timerTickFadeOut(object sender, EventArgs e)
        {
            this.Opacity -= 0.07;

            if (this.Opacity <= 0)
            {
                this.Hide();
                timer1.Stop();
                //timer1.Dispose();
            }
        }

        void timerTickFadeIn(object sender, EventArgs e)
        {
            this.Opacity += 0.07;

            if (this.Opacity >= 100)
            {
                timer2.Stop();
                //timer2.Dispose();
            }
        }

        #endregion
        
        #endregion

        // ******************************************************************
        // Prepare Settings Manager
        // ******************************************************************

        #region PrepareSettings

        /// <summary>
        /// Creates an Instance of Settings Manager
        /// </summary>
        private void InitializeSettings()
        {
            MinimiseToTrayWhenChecked();
            RegisterLoadOnStartupWhenChecked();
        }

        /// <summary>
        /// Minimizes App to System tray if true
        /// </summary>
        private void MinimiseToTrayWhenChecked()
        {
            if (logic.MainSettings.GetStartMinimizeStatus() == true)
                MinimiseMaximiseTray();
        }

        /// <summary>
        /// Sets the Load on Startup Status
        /// </summary>
        private void RegisterLoadOnStartupWhenChecked()
        {
            if (logic.MainSettings.GetLoadOnStartupStatus() == true)
                RegisterInStartup(true);
            else
                RegisterInStartup(false);
        }

        #endregion

        // ******************************************************************
        // Switch Between Panels (Preferences and ToDo++)
        // ******************************************************************

        #region PanelSwitching

        public void SwitchToConsolePanel()
        {
            this.customPanelControl.SelectedIndex = 2;
        }

        public void SwitchToTaskListPanel()
        {
            this.customPanelControl.SelectedIndex = 0;
        }

        public void SwitchToPreferences()
        {
            this.customPanelControl.SelectedIndex = 1;
        }

        #endregion

        // ******************************************************************
        // Enables the Custom Scrollbar used in Preferences
        // ******************************************************************

        private void InitializePreferencesPanel()
        {
            preferencesPanel.InitializeWithSettings(logic.MainSettings);
        }

        // ******************************************************************
        // Prepare the Menu Bar
        // ******************************************************************

        #region PrepareMenu

        /// <summary>
        /// Prepare the Menu Bar. Pass an instance of settings manager into it so it can interact with it
        /// </summary>
        private void InitializeMenu()
        {

        }



        #endregion

        // ******************************************************************
        // Prepare the Output Box
        // ******************************************************************

        #region PrepareOutputBox

        /// <summary>
        /// Prepare the Output Box. Pass an instance of settings manager into it so it can interact with it
        /// </summary>
        private void InitializeOutputBox()
        {
            outputBox.InitializeWithSettings(logic.MainSettings);
        }

        #endregion

        // ******************************************************************
        // Set logic
        // ******************************************************************

        #region PrepareLogic

        private void InitializeLogic(Logic logic)
        {
            this.logic = logic;
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
            string input = textInput.Text;
            string output = logic.ProcessCommand(input);
            //outputBox.SetOutputSize(logic.MainSettings.GetTextSize());
            outputBox.DisplayCommand(input, output);
            textInput.Clear();
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
                e.Handled = true;
                textInput.AddToList(textInput.Text);
                ProcessText();
            }

        }

        /// <summary>
        /// When Up/Down Keys Pressed
        /// </summary>
        private void textInput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            if (e.KeyCode == Keys.Up)
            {
                textInput.UpdateWithPrevCommand();
            }

            if (e.KeyCode == Keys.Down)
            {
                textInput.UpdateWithNextCommand();
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

        // ******************************************************************
        // Event Handlers for UI and Non-UI elements
        // ******************************************************************

        #region EventHandlers

        #region NonUIHandlers

        /// <summary>
        /// Adds Event Handlers relating to UI here
        /// </summary>
        private void InitializeEventHandlers()
        {
            EventHandlers.StayOnTopHandler += SetStayOnTop;
            EventHandlers.UpdateOutputBoxSettingsHandler += UpdateOutputBoxSettings;
        }

        /// <summary>
        /// When event received, Form always stays on Top
        /// </summary>
        private void SetStayOnTop(object sender, EventArgs args)
        {
            bool onTop = Convert.ToBoolean(sender);
            this.TopMost = onTop;
            UserInputBox.OnTop(onTop);
            FontBox.OnTop(onTop);
            AlertBox.OnTop(onTop);
        }

        /// <summary>
        /// When event received, OutputBox updates itself with the latest settings
        /// </summary>
        private void UpdateOutputBoxSettings(object sender, EventArgs args)
        {
            outputBox.InitializeWithSettings(logic.MainSettings);
        }

        #endregion

        /*
        #region ButtonEvents

        private void loadButton_MouseDown(object sender, MouseEventArgs e)
        {
            loadButton.SetMouseDown();
        }


        private void loadButton_MouseUp(object sender, MouseEventArgs e)
        {
            loadButton.SetMouseUp();
        }

        private void consoleButon_MouseDown(object sender, MouseEventArgs e)
        {
            consoleButon.SetMouseDown();
            //this.customPanelControl.SelectedIndex = 2;
        }

        int selected = 0;
        private void consoleButon_MouseUp(object sender, MouseEventArgs e)
        {
            consoleButon.SetMouseUp();
            if (selected == 0)
            {
                consoleButon.ButtonText = "TaskList";
                SwitchToConsolePanel();
                selected = 1;
            }
            else
            {
                consoleButon.ButtonText = "Console";
                SwitchToTaskListPanel();
                selected = 0;
            }
        }

        private void preferencesButton_MouseDown(object sender, MouseEventArgs e)
        {
            preferencesButton.SetMouseDown();
        }

        private void preferencesButton_MouseUp(object sender, MouseEventArgs e)
        {
            preferencesButton.SetMouseUp();
            SwitchToPreferences();
        }

        #endregion
         * */

        #endregion

        /// <summary>
        /// Exit the Application
        /// </summary>
        public static void Exit()
        {
            Application.Exit();
        }






    }
}
