using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FontDialogToDo : Form
    {
        bool fontSelectionEnable; 
        bool sizeSelectionEnable;
        bool colorSelectionEnable;

        public FontDialogToDo()
        {
            InitializeComponent();

            this.fontSelection.SelectedIndexChanged += m_comboBox_SelectedIndexChanged;
            this.sizeSelection.RemoveDuplicate();
            this.sizeSelection.SelectedItem = 8;
            this.fontSelection.SelectedFontFamily = new FontFamily("Arial Black");
            this.colorSelection.SelectedColor = Color.Cyan;

            SetFormattingForPreview();
        }

        public void EnableDisableControls(bool font, bool size, bool color)
        {
            this.fontSelectionEnable = font;
            this.sizeSelectionEnable = size;
            this.colorSelectionEnable = color;

            this.fontSelection.Enabled = this.fontSelectionEnable;
            this.sizeSelection.Enabled = this.sizeSelectionEnable;
            this.colorSelection.Enabled = this.colorSelectionEnable;
        }

        private void SetFormattingForPreview()
        {
            int size = Convert.ToInt32(sizeSelection.SelectedItem.ToString());
            FontFamily temp = fontSelection.publicFont;
            string fontName = temp.GetName(0);

            try
            {
                Font x = new Font(fontName, size, FontStyle.Regular);
                previewLabel.Font = x;
                previewLabel.ForeColor = colorSelection.SelectedColor;
            }
            catch (Exception e)
            {
                AlertBox.Show("This font can't be used");
            }
        }

        #region EventHandlersForOptions

        private void sizeComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetFormattingForPreview();
        }

        private void colorSelection_ColorChanged(object sender, ColorComboTestApp.ColorChangeArgs e)
        {
            SetFormattingForPreview();
        }

        private void m_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontSelection.publicFont = fontSelection.SelectedFontFamily;
            SetFormattingForPreview();
        }

        #endregion


    }
}
