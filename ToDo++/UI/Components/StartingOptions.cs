using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class StartingOptions : UserControl
    {
        bool firstLoad = false;
        Settings settings;

        public StartingOptions()
        {
            InitializeComponent();
        }

        public void InitializeStartingOptions(Settings settings)
        {
            this.settings = settings;
            InitializeCheckBoxes();
        }

        private void InitializeCheckBoxes()
        {
            minimisedCheckbox.Checked = settings.GetStartMinimizeStatus();
            loadOnStartupCheckbox.Checked = settings.GetLoadOnStartupStatus();
            stayOnTopCheckBox.Checked = settings.GetStayOnTopStatus();
            firstLoad = true;
        }

        private void minimisedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
                settings.SetStartMinimized(minimisedCheckbox.Checked);
        }

        private void loadOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
                settings.SetLoadOnStartupStatus(loadOnStartupCheckbox.Checked);
        }

        private void stayOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
            {
                settings.SetStayOnTop(stayOnTopCheckBox.Checked);
                EventHandlers.StayOnTop(stayOnTopCheckBox.Checked);
            }
        }

    }
}
