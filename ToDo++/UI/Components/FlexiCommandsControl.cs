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
        Settings settings;

        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadCommandList();
            //LoadContextList();
            //nextButton.FlatAppearance.BorderColor = Color.SteelBlue;
            //prevButton.FlatAppearance.BorderColor = Color.SteelBlue;
        }

        public void InitializeFlexiCommands(Settings settings)
        {
            this.settings = settings;
        }

        private void LoadCommandList()
        {
            commandTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("ADD");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DELETE");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DISPLAY");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("SORT");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("SEARCH");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("MODIFY");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("UNDO");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("REDO");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DONE");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("POSTPONE");
            commandTree.Nodes.Add(treeNode);
        }

        private void LoadContextList()
        {
            commandTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("ON");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("FROM");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("TO");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("-");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("THIS");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("NEXT");
            commandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("FOLLOWING");
            commandTree.Nodes.Add(treeNode);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
            CustomMessageBox.Show("Add Command", "Enter your new command here");
            if (CustomMessageBox.ValidData())
                addedFlexiCommands.Items.Add(CustomMessageBox.GetInput());
             
        }

        private void nextList_Click(object sender, EventArgs e)
        {
            LoadContextList();
        }
    }
}
