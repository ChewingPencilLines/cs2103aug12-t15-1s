using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace ToDo
{
    public partial class HelpControl : UserControl
    {
        private UI ui;
        int currImage = 1;
        bool fadeLock=false;
        bool firstLoad;

        public HelpControl()
        {
            InitializeComponent();
            transpControl.Opacity = 0;
            transpControl.BringToFront();
        }

        public void SetUI(UI ui,bool firstLoad)
        {
            this.ui = ui;
            this.firstLoad = firstLoad;
            LoadRelaventHelpPanel();
        }

        private void LoadRelaventHelpPanel()
        {
            if (!firstLoad)
                customPanelControl.SelectedIndex = 1;
        }

        private void GenerateNextImage()
        {
            if (currImage == 11)
            {
                ui.SwitchToTaskListViewPanel();
                customPanelControl.SelectedIndex = 1;
            }


            currImage += 1;
            string fileName = string.Format("helpFrame{0}", currImage);

            switch (currImage)
            {
                case 2:
                    pictureBox.Image = Properties.Resources.help0002;
                    break;

                case 3:
                    pictureBox.Image = Properties.Resources.help0003;
                    break;

                case 4:
                    pictureBox.Image = Properties.Resources.help0004;
                    break;

                case 5:
                    pictureBox.Image = Properties.Resources.help0005;
                    break;

                case 6:
                    pictureBox.Image = Properties.Resources.help0006;
                    break;

                case 7:
                    pictureBox.Image = Properties.Resources.help0007;
                    break;

                case 8:
                    pictureBox.Image = Properties.Resources.help0008;
                    break;

                case 9:
                    pictureBox.Image = Properties.Resources.help0009;
                    break;

                case 10:
                    pictureBox.Image = Properties.Resources.help0010;
                    break;

                case 11:
                    pictureBox.Image = Properties.Resources.help0011;
                    break;
            }
            //pictureBox.Image = Properties.Resources.helpFrame2;
        }


        private void transpControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(!fadeLock)
                StartFadeInFadeOut();
        }

        private void StartFadeInFadeOut()
        {
            fadeLock = true;
            fadeInTimer.Enabled = true;
        }

        private void fadeInTimer_Tick(object sender, EventArgs e)
        {
            transpControl.Opacity += 5;
            if (transpControl.Opacity >= 100)
            {
                GenerateNextImage();
                fadeInTimer.Enabled = false;
                fadeOutTimer.Enabled = true;
            }
        }


        private void fadeOutTimer_Tick(object sender, EventArgs e)
        {
            transpControl.Opacity -= 5;
            if (transpControl.Opacity <= 0)
            {
                fadeInTimer.Enabled = false;
                fadeOutTimer.Enabled = false;
                fadeLock = false;
            }
        }

        private void introButton_Click(object sender, EventArgs e)
        {
            customPanelControl.SelectedIndex = 0;
        }
    }
}
