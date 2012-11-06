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
        bool collaspedState = false;
        bool isShowingSettings = false;
        
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
       
        private void updownButton_Click(object sender, EventArgs e)
        {
            if (collaspedState == false)
            {
                ui.ToggleCollapsedState();
                collaspedState = true;
            }
            else
            {
                ui.ToggleCollapsedState();                
                collaspedState = false;
            }
        }

        public void SetUpDownButton(bool isCollasped)
        {
            if(isCollasped)
                updownButton.Image = Properties.Resources.downButton;
            else
                updownButton.Image = Properties.Resources.upButton;
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
            if (isShowingSettings == false)
            {
                ui.SwitchToSettingsPanel();
                if (collaspedState == true)
                    ui.ToggleCollapsedState();
                isShowingSettings = true;
                collaspedState = false;
            }
            else
            {
                ui.SwitchToToDoPanel();
                isShowingSettings = false;
            }
        }
    }
}
