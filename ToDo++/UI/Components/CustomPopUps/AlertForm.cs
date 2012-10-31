using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method sets the alert text that is to be displayed
        /// </summary>
        /// <param name="alertText">Alert Text to be displayed</param>
        public void SetAlertText(string alertText)
        {
            alertLabel.Text = alertText;
        }

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
        // Event Handlers for closing Alert
        // ******************************************************************

        #region EventHandlers&KeyboardCommands

        //Okay Button to close Alert
        private void okButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //Hit Enter to close Alert
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion
    }
}
