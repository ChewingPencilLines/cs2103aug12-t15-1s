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
    }
}
