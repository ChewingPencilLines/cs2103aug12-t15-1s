using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class TimeDateControl : UserControl
    {
        
        #region Transparency

        public void TransparentControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //Graphics g = pevent.Graphics;
            //g.DrawRectangle(Pens.Black, this.ClientRectangle);
        }


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // don't call the base class
            base.OnPaintBackground(pevent);
        }


        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        #endregion
        
        private Timer timer;

        public TimeDateControl()
        {
            InitializeComponent();
            SetDate();
            Clock();
            TransparentControl();
        }

        public void Clock()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            timeLabel.BackColor = Color.Transparent;
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void SetDate()
        {
            DateTime dt = DateTime.Now;
            dateObject.Text = dt.ToShortDateString();
        }
    }
}
