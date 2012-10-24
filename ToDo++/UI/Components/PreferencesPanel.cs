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
    public partial class PreferencesPanel : UserControl
    {
        public PreferencesPanel(Settings settings)
        {
            InitializeComponent();
            startingOptions.InitializeStartingOptions(settings);
            flexiCommandsControl.InitializeFlexiCommands(settings);
        }
    }
}
