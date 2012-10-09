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
    public partial class Menu : UserControl
    {
        private SettingsManager settingsManager;

        public void SetSettingsManager(SettingsManager passedSettingsManager) { settingsManager=passedSettingsManager; }
        public Menu() { InitializeComponent(); }

        public void LoadSettingsIntoMenu()
        {
            loadOnStartupToolStripMenuItem.Checked = settingsManager.GetLoadOnStartupStatus();
            startMinimizedToolStripMenuItem.Checked = settingsManager.GetStartMinimizedStatus();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings(settingsManager);
            settingsForm.ShowDialog();
        }

        private void startMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertMinimizedToolStrip();
            settingsManager.ToggleStartMinimized(startMinimizedToolStripMenuItem.Checked);
        }

        private void invertMinimizedToolStrip()
        {
            if (startMinimizedToolStripMenuItem.Checked == true)
                startMinimizedToolStripMenuItem.Checked = false;
            else
                startMinimizedToolStripMenuItem.Checked = true;
        }

        private void invertStartupToolStrip()
        {
            if (loadOnStartupToolStripMenuItem.Checked == true)
                loadOnStartupToolStripMenuItem.Checked = false;
            else
                loadOnStartupToolStripMenuItem.Checked = true;
        }

        private void loadOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertStartupToolStrip();
            settingsManager.ToggleLoadOnStartup(loadOnStartupToolStripMenuItem.Checked);
        }

        private void menuStrip1_MouseEnter(object sender, EventArgs e)
        {
            LoadSettingsIntoMenu();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
