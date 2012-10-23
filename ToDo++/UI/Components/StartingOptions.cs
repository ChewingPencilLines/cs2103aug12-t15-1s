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
    public partial class StartingOptions : UserControl
    {
        public event EventHandler UIStayOnTopChangedHandler;

        bool firstLoad = false;
        public StartingOptions()
        {
            InitializeComponent();
            InitializeCheckBoxes();
        }

        private void InitializeCheckBoxes()
        {
            //minimisedCheckbox.Checked = Settings.GetStartMinimizeStatus();
            //loadOnStartupCheckbox.Checked = Settings.GetLoadOnStartupStatus();
            //stayOnTopCheckBox.Checked = Settings.GetStayOnTopStatus();
            firstLoad = true;
        }

        private void minimisedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //if (firstLoad == true)
                //Settings.SetStartMinimized(minimisedCheckbox.Checked);
        }

        private void loadOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //if (firstLoad == true)
                //Settings.SetLoadOnStartupStatus(loadOnStartupCheckbox.Checked);
        }

        private void stayOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
            {
                //Settings.SetStayOnTop(stayOnTopCheckBox.Checked);
                OnStayOnTopChanged(stayOnTopCheckBox.Checked);
            }
        }

        protected void OnStayOnTopChanged(bool x)
        {
            var handler = UIStayOnTopChangedHandler;
            if (handler != null)
            {
                handler(x, EventArgs.Empty);
            }
        }
    }
}
