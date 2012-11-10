﻿using System;
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
        bool isCollapsed = false;
        bool isSettings = false;
        bool isHelp = false;

        public TopMenuControl()
        {
            InitializeComponent();
            SetButtonControlsToTransparent();
        }
        
        /// <summary>
        /// Intialize with UI
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public void InitializeWithUI(UI ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// Disable Button Color Change when mouse over
        /// </summary>
        private void SetButtonControlsToTransparent()
        {
            questionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            questionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            settingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            settingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            updownButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            updownButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            minButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            minButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
        }

        /// <summary>
        /// Changes the image of the Collapse Button respectively
        /// </summary>
        /// <param name="isCollasped"></param>
        public void SetUpDownButton(bool isCollasped)
        {
            if (isCollasped)
                updownButton.Image = Properties.Resources.downButton;
            else
                updownButton.Image = Properties.Resources.upButton;
        }

        #region ButtonEventHandlers

        //Go to Settings Event Handler
        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (isHelp)
            {
                ui.SwitchToSettingsPanel();
                isHelp = false;
                return;
            }

            isSettings = true;
            isHelp = false;

            ui.ToggleToDoSettingsPanel();
            if (isCollapsed == true)
                ui.ToggleCollapsedState();
            isCollapsed = false;
        }

        //CollapseExpand Event Handler
        private void updownButton_Click(object sender, EventArgs e)
        {
            if (isCollapsed == false)
            {
                ui.ToggleCollapsedState();
                isCollapsed = true;
            }
            else
            {
                ui.ToggleCollapsedState();
                isCollapsed = false;
            }
        }

        //Minimize to Tray Event Handler
        private void minButton_Click(object sender, EventArgs e)
        {
            ui.MinimiseMaximiseTray();
        }

        //Exit Event Handler
        private void closeButton_Click(object sender, EventArgs e)
        {
            ui.Exit();
        }

        private void questionButton_Click(object sender, EventArgs e)
        {
            if (isSettings)
            {
                ui.SwitchToHelpPanel();
                isSettings = false;
                return;
            }

            isHelp = true;
            isSettings = false;

            ui.ToggleHelpToDoPanel();
            if (isCollapsed == true)
                ui.ToggleCollapsedState();
            isCollapsed = false;
        }

        #endregion




    }
}
