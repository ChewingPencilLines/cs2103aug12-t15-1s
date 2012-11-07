﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace ToDo
{
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();
            System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); 
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

        /// <summary>
        /// Creates rounded edge
        /// </summary>
        /// 
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
        /// Creates Shadow (DISABLED)
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
