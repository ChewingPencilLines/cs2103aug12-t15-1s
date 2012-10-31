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

        //public string alertText { get { return alertLabel.Text; } set { alertLabel.Text = alertText; } }

        public void SetAlertText(string alertText)
        {
            alertLabel.Text = alertText;
        }

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

        private void okButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
