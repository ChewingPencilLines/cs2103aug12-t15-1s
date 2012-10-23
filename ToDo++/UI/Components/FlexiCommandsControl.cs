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
    public partial class FlexiCommandsControl : UserControl
    {
        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadCommandList();
        }

        private void LoadCommandList()
        {
            TreeNode treeNode = new TreeNode("ADD");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DELETE");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("MODIFY");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("UNDO");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("REDO");
            commandTree.Nodes.Add(treeNode);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
            CustomMessageBox.Show("Add Command", "Enter your new command here");
            if (CustomMessageBox.ValidData())
                addedFlexiCommands.Items.Add(CustomMessageBox.GetInput());
             
        }
    }
}
