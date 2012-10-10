using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class DateTimeControl : UserControl
    {
        public DateTimeControl()
        {
            InitializeComponent();
            SetDate();
            Clock();
            TransparentControl();
        }

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

        public void Clock()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            time.BackColor = Color.Transparent;
            time.Text = DateTime.Now.ToLongTimeString();
        }

        private void SetDate()
        {
            DateTime dt = DateTime.Now;
            date.Text = dt.ToShortDateString();
        }
    }
}
