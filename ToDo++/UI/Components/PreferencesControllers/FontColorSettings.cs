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
    public partial class FontColorSettings : UserControl
    {
        Settings settings;

        public void InitializeFontColorControl(Settings settings)
        {
            this.settings = settings;
        }

        public FontColorSettings()
        {
            InitializeComponent();
        }


        private void textSizeButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), Color.White);
            FontBox.Show(true, true, false);
            if (FontBox.ConfirmHit())
            {
                settings.SetTextSize(FontBox.GetSize());
                settings.SetFontSelection(FontBox.GetFont());
                EventHandlers.UpdateUI();
            }
        }

        private void taskDoneColorButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), settings.GetTaskDoneColor());
            FontBox.Show(false, false, true);
            if (FontBox.ConfirmHit())
            {
                settings.SetTaskDoneColor(FontBox.GetColor());
                EventHandlers.UpdateUI();
            }
        }
    }
}
