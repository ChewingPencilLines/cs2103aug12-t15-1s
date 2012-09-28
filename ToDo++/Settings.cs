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
    public partial class Settings : Form
    {
        private Commands currentCommand;
        private SettingsManager settingsManager;

        public Settings()
        {
            InitializeComponent();
            this.ShowIcon = false;
            settingsManager = new SettingsManager();
            LoadCommandTab();
        }

        #region CommandTab

        private void LoadCommandTab()
        {
            CommandList();
        }

        private void CommandList()
        {
            TreeNode treeNode = new TreeNode("ADD");
            CommandTree.Nodes.Add(treeNode);
            CommandTree.SelectedNode = treeNode;
            treeNode = new TreeNode("DELETE");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("UPDATE");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("UNDO");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("REDO");
            CommandTree.Nodes.Add(treeNode);
        }

       private void CommandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode treeNode = CommandTree.SelectedNode;
                commandPreview.Text = treeNode.Text;
                currentCommand = settingsManager.StringToCommand(treeNode.Text);

                UpdateDescriptionCommand();
                UpdateListOfCommands();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

       private void UpdateDescriptionCommand()
       {
           string description="NONE";

           switch (currentCommand)
           {
               case Commands.ADD:
                   description = "Add Command\nDescription";
                   break;
               case Commands.DELETE:
                   description = "Delete Command\nDescription";
                   break;
               case Commands.UPDATE:
                   description = "Update Command\nDescription";
                   break;
               case Commands.UNDO:
                   description = "Undo Command\nDescription";
                   break;
               case Commands.REDO:
                   description = "Redo Command\nDescription";
                   break;
           }

           commandDescription.Text = description;
       }

       private void UpdateListOfCommands()
       {
           listOfCommands.Items.Clear();
           List<string> currentCommandList = settingsManager.GetCommand(currentCommand);
           foreach (string item in currentCommandList)
               listOfCommands.Items.Add(item);
       }

       private void addUserCommandButton_Click(object sender, EventArgs e)
       {
           settingsManager.AddCommand(userCommand.Text, currentCommand);
           UpdateListOfCommands();
       }

        #endregion

        #region FontTab

        private void FontList()
        {
            TreeNode treeNode = new TreeNode("Command Output");
            FontTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Command Input");
            FontTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Your Input");
            FontTree.Nodes.Add(treeNode);
        }

        private void FontTree_MouseClick(object sender, MouseEventArgs e)
        {
            //TreeNode node = FontTree.SelectedNode;
            //if (node.Text == "System Output")
            //{
            //    MessageBox.Show(string.Format("You selected: {0}", node.Text));
            //}
        }

        private void FontTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = FontTree.SelectedNode;
            if (node.Text == "Command Output")
            {
                MessageBox.Show(string.Format("You selected: {0}", node.Text));
            }
        }

        #endregion


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
