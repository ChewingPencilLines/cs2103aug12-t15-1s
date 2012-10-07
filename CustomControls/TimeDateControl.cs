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
        private Timer timer;

        public TimeDateControl()
        {
            InitializeComponent();
            SetDate();
            Clock();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
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
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void SetDate()
        {
            DateTime dt = DateTime.Now;
            dateObject.Text = dt.ToShortDateString();
        }
    }
}
