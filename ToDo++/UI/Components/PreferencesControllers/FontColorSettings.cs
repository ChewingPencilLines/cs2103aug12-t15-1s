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
            InitializePreviewBox();
        }

        public FontColorSettings()
        {
            InitializeComponent();
        }

        private void InitializePreviewBox()
        {
            previewBox.InitializeWithSettings(this.settings);
            AddPreviewTextToPreviewBox();
        }

        private void AddPreviewTextToPreviewBox()
        {
            previewBox.DisplayCommand("Buy Milk by 2pm", "Added \"Buy Milk\" successfully!");
        }

        private void textSizeButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions("Arial Black", settings.GetTextSize(), Color.Yellow);
            FontBox.Show(false, true, false);
            int size = FontBox.GetSize();
            if (FontBox.ConfirmHit())
            {
                settings.SetTextSize(size);
                previewBox.SetOutputSize(settings.GetTextSize());
                EventHandlers.UpdateOutputBoxSettings();
            }
        }
    }
}
