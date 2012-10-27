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
        CommandType selectedCommand;
        ContextType selectedContext;

        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadCommandList();
            LoadContextList();
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
            contextTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("ON");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("FROM");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("TO");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("-");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("THIS");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("NEXT");
            contextTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("FOLLOWING");
            contextTree.Nodes.Add(treeNode);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
            CustomMessageBox.Show("Add Command", "Enter your new command here");
            if (CustomMessageBox.ValidData())
                addedFlexiCommands.Items.Add(CustomMessageBox.GetInput());
             
        }

        private CommandType ConvertStringToCommand(string command)
        {
            switch (command)
            {
                case "ADD":
                    return CommandType.ADD;
                case "DELETE":
                    return CommandType.DELETE;
                case "DISPLAY":
                    return CommandType.DISPLAY;
                case "SORT":
                    return CommandType.SORT;
                case "SEARCH":
                    return CommandType.SEARCH;
                case "MODIFY":
                    return CommandType.MODIFY;
                case "UNDO":
                    return CommandType.UNDO;
                case "REDO":
                    return CommandType.REDO;
                case "DONE":
                    return CommandType.DONE;
                case "POSTPONE":
                    return CommandType.POSTPONE;
                case "EXIT":
                    return CommandType.EXIT;
            }

            return CommandType.INVALID;
        }

        //private void ContextType ConvertStringToContext(string context)
        //{
        //    switch(context)
        //    {
        //        case "aa":
        //            return ContextType.STARTTIME;
        //    }
        //}

        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

    }
}
