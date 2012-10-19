using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class PopUpForm : Form
    {
        public PopUpForm()
        {
            InitializeComponent();
        }

        public string userInput { get { return userInputBox.Text; } set { userInputBox.Text = userInput; } }
        public string title { get { return titleLabel.Text; } set { titleLabel.Text = title; } }
        public string subTitle { get { return subtitleLabel.Text; } set { subtitleLabel.Text = subTitle; } }

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

        public void SetTitle(string title,string subTitle)
        {
            titleLabel.Text = title;
            subtitleLabel.Text = subTitle;
        }

        public bool UserEnteredData()
        {
            if (userInput == "")
                return false;
            else
                return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            userInputBox.Text = "";
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
