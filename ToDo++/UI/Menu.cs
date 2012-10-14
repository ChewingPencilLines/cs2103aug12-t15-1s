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

        /// <summary>
        /// Set the settingsManager of the Menu Control, so interaction is possible
        /// </summary>
        /// <param name="setSettingsManager">Instance of MainSettingsManager passed in by pointer</param>
        public void SetSettingsManager(SettingsManager passedSettingsManager) { settingsManager=passedSettingsManager; }
        public Menu() { InitializeComponent(); }

        /// <summary>
        /// Currently loads the check status of Load On Startup and Get Minimized Status
        /// </summary>
        public void LoadSettingsIntoMenu()
        {
            loadOnStartupToolStripMenuItem.Checked = settingsManager.GetLoadOnStartupStatus();
            startMinimizedToolStripMenuItem.Checked = settingsManager.GetStartMinimizedStatus();
        }

        #region MenuEventHandlers

        /// <summary>
        /// Opens an instance of the Settings Form
        /// </summary>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings(settingsManager);
            settingsForm.ShowDialog();
        }

        /// <summary>
        /// Check the Start Minimized Option
        /// </summary>
        private void startMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertMinimizedToolStrip();
            settingsManager.ToggleStartMinimized(startMinimizedToolStripMenuItem.Checked);
        }

        /// <summary>
        /// Check the Load on Startup Option
        /// </summary>
        private void loadOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invertStartupToolStrip();
            settingsManager.ToggleLoadOnStartup(loadOnStartupToolStripMenuItem.Checked);
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
