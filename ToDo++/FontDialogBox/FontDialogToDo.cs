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
        bool confirmData;

        public FontDialogToDo()
        {
            InitializeComponent();

            this.fontSelection.SelectedIndexChanged += m_comboBox_SelectedIndexChanged;
            this.sizeSelection.RemoveDuplicate();
            InitializeOptions("Arial Black", 8, Color.Cyan);

            SetFormattingForPreview();
        }

        #region Getters

        public int GetSize() { return (int)this.sizeSelection.SelectedItem; }
        public string GetFont() { return this.fontSelection.publicFont.GetName(0); }
        public Color GetColor() { return this.colorSelection.SelectedColor; }

        #endregion

        #region FormattingInitializationFunctions

        public void InitializeOptions(string font, int size, Color color)
        {
            this.sizeSelection.SelectedItem = size;
            this.fontSelection.SelectedFontFamily = new FontFamily(font);
            this.colorSelection.SelectedColor = color;
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

        #endregion

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

        public bool CheckValidData()
        {
            if (confirmData == true)
                return true;
            else
                return false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            confirmData = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            confirmData = false;
            this.Close();
        }


    }
}
