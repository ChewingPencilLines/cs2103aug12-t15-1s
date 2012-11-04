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
    public partial class TopMenuControl : UserControl
    {

        private UI ui;

        /// <summary>
        /// Currently sets the Text Size of the OutputBox
        /// </summary>
        public void InitializeWithUI(UI ui)
        {
            this.ui = ui;
        }


        public TopMenuControl()
        {
            InitializeComponent();
        }

        int state = 0;
        private void updownButton_Click(object sender, EventArgs e)
        {
            if (state == 0)
            {
                ui.StartCollapserExpander();
                updownButton.Image = Properties.Resources.downButton;
                state = 1;
            }
            else
            {
                ui.StartCollapserExpander();
                updownButton.Image = Properties.Resources.upButton;
                state = 0;
            }

            
            
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            ui.MinimiseMaximiseTray();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            ui.Exit();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            ui.SwitchToSettingsPanel();
        }
    }
}
