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
        bool MouseIsOverDisplayList;

        /// <summary>
        /// Creates a new instance of the Main Program (UI) and loads the various Classes
        /// </summary>
        public UI(Logic logic)
        {
            IntializeTinyAlert();
            InitializeComponent();
            InitializeLogic(logic);               //Sets logic            
            InitializeSystemTray();               //Loads Code to place App in System Tray
            InitializeSettings();                 //Sets the correct settings to ToDo++ at the start
            InitializeMenu();                     //Loads the Menu
            InitializeOutputBox();                //Loads Output Box    
            InitializeEventHandlers();            //Adds Event Handlers
            InitializePreferencesPanel();
            IntializeTopMenu();
            InitializeTaskListView();
            this.ActiveControl = textInput;
            this.MouseWheel += new MouseEventHandler(ScrollIfOverDisplay);
        }

        #endregion

        private void IntializeTinyAlert()
        {
            TinyAlertView.SetUI(this);
            TinyAlertView.SetTiming(5);
        }

        private void InitializeTaskListView()
        {
            taskListViewControl.Initialize();
            taskListViewControl.UpdateDisplay(logic.GetDefaultView());
        }

        private void IntializeTopMenu()
        {
            topMenuControl.InitializeWithUI(this);
        }

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
        public void MinimiseMaximiseTray()
        {
            notifyIcon_taskBar.BalloonTipTitle = "ToDo++";
            notifyIcon_taskBar.BalloonTipText = "Hit Alt+Q to bring it up";

            //If Window is Open
            if (notifyIcon_taskBar.Visible == false)
            {
                StartFadeOut();
                TinyAlertView.DismissEarly();
                notifyIcon_taskBar.Visible = true;
                notifyIcon_taskBar.ShowBalloonTip(500);
            }
            //If Window is in tray
            else
            {
                notifyIcon_taskBar.Visible = false;
                StartFadeIn();
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
        /// Creates Shadow
        /// </summary>
        #region Shadow

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
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

        double i = 1;

        public void StartFadeOut()
        {
            timerFadeIn.Enabled = false;
            timerFadeOut.Enabled = true;
        }

        public void StartFadeIn()
        {
            this.Show();
            timerFadeIn.Enabled = true;//start the Fade In Effect
            timerFadeOut.Enabled = false;
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.05;
            if (i >= 1)
            {//if form is full visible we execute the Fade Out Effect
                this.Opacity = 1;
                timerFadeIn.Enabled = false;//stop the Fade In Effect
                //timerFadeOut.Enabled = true;//start the Fade Out Effect
                return;
            }
            this.Opacity = i;
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            i -= 0.05;
            if (i <= 0.01)
            {//if form is invisible we execute the Fade In Effect again
                this.Opacity = 0.0;
                //timerFadeIn.Enabled = true;//start the Fade In Effect
                timerFadeOut.Enabled = false;//stop the Fade Out Effect
                this.Hide();
                return;
            }
            this.Opacity = i;
        }



        #endregion

        /// <summary>
        /// Collapse Expand
        /// </summary>
        #region CollapseExpand

        int collapseExpandState = 0;
        int setHeight;
        int prevHeight;

        public void StartCollapserExpander()
        {
            if (collapseExpandState == 0)
            {
                setHeight = this.Height;
                prevHeight = this.Height;
                timerCollpaser.Enabled = true;
                timerExpander.Enabled = false;
                collapseExpandState = 1;
            }
            else
            {
                setHeight = 60;
                timerCollpaser.Enabled = false;
                timerExpander.Enabled = true;
                this.MaximumSize = new System.Drawing.Size(1000, 1000);
                collapseExpandState = 0;
            }
        }

        private void timerCollpaser_Tick(object sender, EventArgs e)
        {
            setHeight -= 20;
            if (setHeight <= 60)
            {
                timerCollpaser.Enabled = false;
                this.MaximumSize = new System.Drawing.Size(1000, 60);
            }
            this.Height = setHeight;
        }

        private void timerExpander_Tick(object sender, EventArgs e)
        {
            setHeight += 20;
            if (setHeight >= prevHeight)
            {
                timerExpander.Enabled = false;
            }
            this.Height = setHeight;
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

        public void SwitchToSettingsPanel()
        {
            if (collapseExpandState == 1)
            {
                StartCollapserExpander();
            }
            this.customPanelControl.SelectedIndex = 1;
        }

        public void SwitchToToDoPanel()
        {
            if (collapseExpandState == 1)
            {
                StartCollapserExpander();
            }
            this.customPanelControl.SelectedIndex = 2;
        }

        #endregion

        // ******************************************************************
        // Prepare Preferences Panel
        // ******************************************************************

        #region PreparePrefereces

        private void InitializePreferencesPanel()
        {
            preferencesPanel.InitializeWithSettings(logic.MainSettings);
        }

        #endregion

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

        int selected = 0;/*
        private void preferencesButton_Click(object sender, EventArgs e)
        {
            if (selected == 0)
            {
                preferencesButton.Text = "ToDo";
                SwitchToSettingsPanel();
                selected = 1;
            }
            else
            {
                preferencesButton.Text = "Preferences";
                SwitchToToDoPanel();
                selected = 0;
            }
        }*/

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
            Response output = logic.ProcessCommand(input);
            taskListViewControl.UpdateDisplay(output);
            if (output.IsSuccessful())
                TinyAlertView.Show(TinyAlertView.StateTinyAlert.SUCCESS, output.FeedbackString);
            else 
                TinyAlertView.Show(TinyAlertView.StateTinyAlert.FAILURE, output.FeedbackString);
            textInput.Clear();
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
                //TaskDisplayTestDriver();
            }
        }

        private void TaskDisplayTestDriver()
        {
            List<Task> displayList = new List<Task>();
            Task addTask;
            addTask = new TaskEvent("test task", DateTime.Now, DateTime.Now, new DateTimeSpecificity());
            displayList.Add(addTask);
            addTask = new TaskEvent("test task 2", DateTime.Now, DateTime.Now, new DateTimeSpecificity());
            displayList.Add(addTask);
            addTask = new TaskEvent("this is a super long test task. who writes this sort of tasks??", new DateTime(2012, 12, 31), new DateTime(2013, 1, 1), new DateTimeSpecificity());            
            displayList.Add(addTask);
            addTask = new TaskDeadline("deaddddline is near. =[", new DateTime(2013, 1, 1), new DateTimeSpecificity());
            addTask.DoneState = true;
            displayList.Add(addTask);            
            addTask = new TaskFloating("floating task test");
            displayList.Add(addTask);
            Response testResponse = new Response(Result.SUCCESS, Format.NAME, typeof(OperationAdd), displayList);
            taskListViewControl.UpdateDisplay(testResponse);
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
        // Event Handlers
        // ******************************************************************

        #region EventHandlers

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

        /// <summary>
        /// Exit the Application
        /// </summary>
        public void Exit()
        {
            Application.Exit();
        }

        private void outputBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void UI_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            //TinyAlertView.Location = new Point(this.Right, this.Bottom - 10);
            TinyAlertView.SetLocation();
        }

        private void taskListViewControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskListViewControl.SelectedItem = null;
        }

        private void taskListViewControl_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            // Row index should not change even if doing a column sort.
            e.Item.SubItems[1].Text = "[" + (e.RowIndex+1).ToString() + "]";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TinyAlertView.Show(TinyAlertView.StateTinyAlert.WARNING,"Added this item to List");
        }

        private void UI_Move(object sender, EventArgs e)
        {
            TinyAlertView.SetLocation();
        }

        private void taskListViewControl_BeforeSorting(object sender, BrightIdeasSoftware.BeforeSortingEventArgs e)
        {
            e.GroupByOrder = SortOrder.None;
        }

        private void taskListViewControl_MouseEnter(object sender, EventArgs e)
        {
            MouseIsOverDisplayList = true;
            taskListViewControl.Focus();
        }   

        private void taskListViewControl_MouseLeave(object sender, EventArgs e)
        {
            MouseIsOverDisplayList = false;
        }

        private void SelectTextInput(object sender, KeyPressEventArgs e)
        {            
            textInput.Text += e.KeyChar;
            textInput.Focus();
            textInput.DeselectAll();
            textInput.Select(textInput.TextLength, 0);
            e.Handled = true;
        }

        private void ScrollIfOverDisplay(object sender, MouseEventArgs e)
        {
            if (MouseIsOverDisplayList)
                taskListViewControl.Focus();
        }
    }
}
