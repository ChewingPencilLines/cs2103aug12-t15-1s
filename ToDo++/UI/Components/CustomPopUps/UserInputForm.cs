using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class UserInputForm : Form
    {
        public UserInputForm()
        {
            InitializeComponent();
        }

        // ******************************************************************
        // Getters and Setters to get and set properties of this Pop Up
        // ******************************************************************

        #region GettersSetters

        public string userInput { get { return userInputBox.Text; } set { userInputBox.Text = userInput; } }
        public string title { get { return titleLabel.Text; } set { titleLabel.Text = title; } }
        public string subTitle { get { return subtitleLabel.Text; } set { subtitleLabel.Text = subTitle; } }

        #endregion

        // ******************************************************************
        // Win32 Code to make this form draggable
        // ******************************************************************

        #region MakeDraggable

        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 1;
        const int HTCAPTION = 2;

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0084) // WM_NCHITTEST
                msg.Result = (IntPtr)2; // HTCAPTION
            else
                base.WndProc(ref msg);
        }

        #endregion

        // ******************************************************************
        // Public Methods to Get Set User Input, Title, Subtitle
        // ******************************************************************

        #region PublicMethods

        /// <summary>
        /// Sets Title and Subtitle of PopUp
        /// </summary>
        /// <param name="title">Specify the title</param>
        /// <param name="subtitle">Specify the subtitle</param>
        public void SetTitle(string title,string subTitle)
        {
            titleLabel.Text = title;
            subtitleLabel.Text = subTitle;
        }

        /// <summary>
        /// Sets User Input prior to displaying
        /// </summary>
        /// <param name="input">Specify the a preset user input</param>
        public void SetUserInput(string input)
        {
            userInputBox.Text = input;
        }

        /// <summary>
        /// Checks for Valid Data
        /// </summary>
        /// <returns>Boolean of whether there was valid data or not</returns>
        public bool UserEnteredData()
        {
            if (userInput == "")
                return false;
            else
                return true;
        }

        #endregion

        // ******************************************************************
        // Event Handlers for keyboard/button inputs
        // ******************************************************************

        #region EventHandlers

        private void cancelButton_Click(object sender, EventArgs e)
        {
            userInputBox.Text = "";
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                this.Close();
                return true;
            }
            else if (keyData == (Keys.Escape))
            {
                userInputBox.Text = "";
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion
    }
}
