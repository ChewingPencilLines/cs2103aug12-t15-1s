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
        private Settings settings;

        /// <summary>
        /// Set the settings of the OutputBox Control, so interaction is possible
        /// </summary>
        /// <param name="passedSettings">Instance of settings passed in by pointer</param>
        public void SetSettings(Settings passedSettings) { settings = passedSettings; }
        public Menu() { InitializeComponent(); }

        /// <summary>
        /// Currently loads the check status of Load On Startup and Get Minimized Status
        /// </summary>
        public void LoadSettingsIntoMenu()
        {
            loadOnStartupToolStripMenuItem.Checked = settings.GetLoadOnStartupStatus();
            startMinimizedToolStripMenuItem.Checked = settings.GetStartMinimizedStatus();
        }

        #region MenuEventHandlers

        /// <summary>
        /// Opens an instance of the Settings Form
        /// </summary>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsUI settingsUI = new SettingsUI(settings);
            settingsUI.ShowDialog();
        }

        /// <summary>
        /// Check the Start Minimized Option
        /// </summary>
        private void startMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertMinimizedToolStrip();
            settings.ToggleStartMinimized(startMinimizedToolStripMenuItem.Checked);
        }

        /// <summary>
        /// Check the Load on Startup Option
        /// </summary>
        private void loadOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertStartupToolStrip();
            settings.ToggleLoadOnStartup(loadOnStartupToolStripMenuItem.Checked);
        }

        /// <summary>
        /// Code to enable disable the Tick on the Menu Item
        /// </summary>
        private void invertMinimizedToolStrip()
        {
            if (startMinimizedToolStripMenuItem.Checked == true)
                startMinimizedToolStripMenuItem.Checked = false;
            else
                startMinimizedToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// Code to enable disable the Tick on the Menu Item
        /// </summary>
        private void invertStartupToolStrip()
        {
            if (loadOnStartupToolStripMenuItem.Checked == true)
                loadOnStartupToolStripMenuItem.Checked = false;
            else
                loadOnStartupToolStripMenuItem.Checked = true;
        }

        private void menuStrip1_MouseEnter(object sender, EventArgs e)
        {
            LoadSettingsIntoMenu();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


    }
}
