using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ToDo
{
    public partial class Settings : Form
    {
        private SettingsManager settingsManager;        //Main settings manager in use
        private Commands currentCommand;                //Current Command Selected             
        private SettingsManager tempSettingsManager;    //A deep copy of settingsManager

        public Settings(SettingsManager setSettingsManager)
        {
            InitializeComponent();
            this.ShowIcon = false;
            settingsManager = setSettingsManager;

            DisableApplyButton();
            LoadCommandTab();
            LoadSettingsTab();
        }

        #region SettingsTab

        bool firstLoad = true;
        private void LoadSettingsTab()
        {
            minimisedCheckbox.Checked = settingsManager.GetStartMinimizedStatus();
            loadOnStartupCheckbox.Checked = settingsManager.GetLoadOnStartupStatus();
            firstLoad = false;
        }

        private void minimisedCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (firstLoad == false)
                EnableApplyButton();
        }

        private void loadOnStartupCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (firstLoad == false)
                EnableApplyButton();
        }

        #endregion

        #region FontTab

        /*
        private void LoadFontTab()
        {
            FontList();
        }

        private void FontList()
        {
            TreeNode treeNode = new TreeNode("Command Output");
            FontTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Command Input");
            FontTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Your Input");
            FontTree.Nodes.Add(treeNode);
        }
        
        private void FontTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        */

        #endregion

        #region CommandTab

        private void LoadCommandTab()
        {
            SetUpTempSettingsManager();
            CommandList();
            SelectFirstNode();
        }

        private void SetUpTempSettingsManager()
        {
            tempSettingsManager = settingsManager.CloneObj();
        }

        private void SelectFirstNode()
        {
            TreeNodeCollection nodes = CommandTree.Nodes;
            CommandTree.SelectedNode = nodes[0];
        }

        private void CommandList()
        {
            TreeNode treeNode = new TreeNode("ADD");
            CommandTree.Nodes.Add(treeNode);
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
            TreeNode treeNode = CommandTree.SelectedNode;
            commandPreview.Text = treeNode.Text;
            currentCommand = settingsManager.StringToCommand(treeNode.Text);

            UpdateDescriptionCommand();
            UpdateListOfCommands();
        }

        private void UpdateDescriptionCommand()
        {
            string description = "NONE";

            switch (currentCommand)
            {
                case Commands.ADD:
                    description = "Add Command\n\nEasily add floating, event or\ndeadline tasks\n";
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
            List<string> currentCommandList = tempSettingsManager.GetCommand(currentCommand);
            foreach (string item in currentCommandList)
                listOfCommands.Items.Add(item);
        }

        private void addUserCommandButton_Click(object sender, EventArgs e)
        {
            EnableApplyButton();
            tempSettingsManager.AddCommand(userCommand.Text, currentCommand);
            UpdateListOfCommands();
            ClearInputField();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            EnableApplyButton();
            tempSettingsManager.RemoveCommand(listOfCommands.SelectedItem.ToString(), currentCommand);
            UpdateListOfCommands();
            ClearInputField();
        }

        private void ClearInputField()
        {
            userCommand.Text = "";
        }

        #endregion

        #region UIButtons

        private void EnableApplyButton()
        {
            applyButton.Enabled = true;
        }

        private void DisableApplyButton()
        {
            applyButton.Enabled = false;
        }

        private void SetSettings()
        {
            #region ApplyChangesToSettingsTab

            if (firstLoad == false)
                settingsManager.ToggleStartMinimized(minimisedCheckbox.Checked);
            if (firstLoad == false)
                settingsManager.ToggleLoadOnStartup(loadOnStartupCheckbox.Checked);

            #endregion

            #region ApplyChangesToCommands

            settingsManager.CopyUpdatedCommandsFrom(tempSettingsManager);
            settingsManager.WriteToFile();

            #endregion
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            SetSettings();
            DisableApplyButton();
        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            SetSettings();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

















    }
}
