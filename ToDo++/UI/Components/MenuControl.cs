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
    public partial class MenuControl : UserControl
    {
        private UI ui;

        public void InitializeMenuControl(UI ui) { this.ui = ui; }
        public MenuControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.DrawRectangle(new Pen(this.BackColor, 5), this.ClientRectangle);
        }

        int selected = 0;
        private void preferencesButton_Click(object sender, EventArgs e)
        {
            if (selected == 0)
            {
                preferencesButton.Text = "ToDo";
                ui.SwitchToSettingsPanel();
                selected = 1;
            }
            else
            {
                preferencesButton.Text = "Preferences";
                ui.SwitchToToDoPanel();
                selected = 0;
            }
        }


    }
}
