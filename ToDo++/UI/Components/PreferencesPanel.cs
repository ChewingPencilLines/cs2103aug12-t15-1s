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

        private Settings settings;

        /// <summary>
        /// Currently sets the Text Size of the OutputBox
        /// </summary>
        public void InitializeWithSettings(Settings settings)
        {
            this.settings = settings;
            startingOptions.InitializeStartingOptions(settings);
            flexiCommandsControl.InitializeFlexiCommands(settings);
            LoadPreferencesTree();
        }

        public PreferencesPanel()
        {
            InitializeComponent();
        }

        private void LoadPreferencesTree()
        {
            preferencesTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("Starting Options");
            preferencesTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("FlexiCommands");
            preferencesTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Font Options");
            preferencesTree.Nodes.Add(treeNode);
        }

        private void preferencesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int selectedNodeText = e.Node.Index;
            switch (selectedNodeText)
            {
                case 0:
                    preferencesSelector.SelectedIndex = 0;
                    break;

                case 1:
                    preferencesSelector.SelectedIndex = 1;
                    break;
            }
        }
    }
}
