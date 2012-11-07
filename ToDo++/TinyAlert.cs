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
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace ToDo
{
    public partial class TinyAlert : Form
    {
        UI ui;
        int timing=3;

        public void SetUI(UI ui)
        {
            this.ui = ui;
        }

        public void SetTiming(int time)
        {
            this.timing = time;
        }

        public void Dismiss()
        {
            timerFadeIn.Enabled = false;//start the Fade In Effect
            timerFadeOut.Enabled = true;
        }
        
        public void ShowDisplay()
        {
            int size=System.Windows.Forms.TextRenderer.MeasureText(tinyAlertLabel.Text, new Font(tinyAlertLabel.Font.FontFamily, tinyAlertLabel.Font.Size, tinyAlertLabel.Font.Style)).Width;
            this.Width = size+20;
            this.Show();
            StartFader();
            //StartDrop();
        }

        public void SetColorText(Color backColor,Color textColor,string text)
        {
            this.BackColor = backColor;
            this.tinyAlertLabel.Text = text;
            this.tinyAlertLabel.ForeColor = textColor;
        }

        public TinyAlert()
        {
            InitializeComponent();
            this.Opacity = 100;
            System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); 
        }

        /// <summary>
        /// Allows resizing of borderless form
        /// </summary>
        #region Resizing
        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HT_CAPTION = 0x2;

        //[DllImportAttribute("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd,
        //                 int Msg, int wParam, int lParam);
        //[DllImportAttribute("user32.dll")]
        //public static extern bool ReleaseCapture();
        //private void UI_MouseDown(object sender, MouseEventArgs e)
        //{

        //}

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

        /*
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
         * */

        #endregion

        #region Sizing

        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private const int WM_GETMINMAXINFO = 0x0024;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WINDOWPOSCHANGING)
            {
                WindowPos windowPos = (WindowPos)m.GetLParam(typeof(WindowPos));

                // Make changes to windowPos

                // Then marshal the changes back to the message
                Marshal.StructureToPtr(windowPos, m.LParam, true);
            }

            base.WndProc(ref m);

            // Make changes to WM_GETMINMAXINFO after it has been handled by the underlying
            // WndProc, so we only need to repopulate the minimum size constraints
            if (m.Msg == WM_GETMINMAXINFO)
            {
                MinMaxInfo minMaxInfo = (MinMaxInfo)m.GetLParam(typeof(MinMaxInfo));
                minMaxInfo.ptMinTrackSize.x = this.MinimumSize.Width;
                minMaxInfo.ptMinTrackSize.y = this.MinimumSize.Height;
                Marshal.StructureToPtr(minMaxInfo, m.LParam, true);
            }
        }

        struct WindowPos
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int width;
            public int height;
            public uint flags;
        }

        struct POINT
        {
            public int x;
            public int y;
        }

        struct MinMaxInfo
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        #endregion



        private void displayForm_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        double i = 0.1;
        private void StartFader()
        {
            this.Opacity = i;//set the opacity of the form to 0.1 when form load
            timerFadeIn.Enabled = true;//start the Fade In Effect
            timerFadeOut.Enabled = false;
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.05;
            if (i >= timing)
            {//if form is full visible we execute the Fade Out Effect
                this.Opacity = 1;
                timerFadeIn.Enabled = false;//stop the Fade In Effect
                timerFadeOut.Enabled = true;//start the Fade Out Effect
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

                //NOTE: THIS CODE BLOCK HERE MIGHT BE USEFUL IF YOU WANT TO
                //      USE SPLASHSCREEN FORM FOR YOUR APPLICATION AFTER THE 
                //      SPLASHSCREEN FORM INVISIBLE YOU CAN CLOSE IT AND OPEN
                //      THE MAIN FORM OF YOUR APPLICATION HERE...
            }
            this.Opacity = i;
        }

        /*
        #region DropCode

        int m;
        private void StartDrop()
        {
            m = ui.Bottom-50;

            this.Location = new Point(this.Right, m);
            timerDrop.Enabled = true;//start the drop
            timerUp.Enabled = false;
        }

        private void timerDrop_Tick(object sender, EventArgs e)
        {
            m += 1;
            if (m >= ui.Bottom)
            {
                timerDrop.Enabled = false;
            }
            this.Location = new Point(ui.Right, ui.Bottom+m);
        }

        private void timerUp_Tick(object sender, EventArgs e)
        {
            m -= 1;
            if (m >= ui.Bottom)
            {
                timerDrop.Enabled = false;
            }
            this.Location = new Point(ui.Right, ui.Bottom + m);
        }

        #endregion
         * */
    }
}
