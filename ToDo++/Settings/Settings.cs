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

        // ******************************************************************
        // Constructors.
        // ******************************************************************

        #region Constructor

        private SettingsManager settingsManager;        //Main instance of settingsManager
        private CommandType currentCommand;                //Current Command Selected             
        private SettingsManager tempSettingsManager;    //A deep copy of settingsManager

        /// <summary>
        /// Creates a new instance of the Settings UI and loads the various Tabs
        /// </summary>
        /// <param name="setSettingsManager">Instance of MainSettingsManager passed in by pointer</param>
        public Settings(SettingsManager setSettingsManager)
        {
            InitializeComponent();
            this.ShowIcon = false;                      //No icon for the Settings User Interface
            settingsManager = setSettingsManager;       //MainSettingsManager passed by pointer to settingsManager

            DisableApplyButton();                       //Only making changes will enable the Apply Button
            LoadPersonalSettingsTab();                  //Load Personal Settings Tab (Tab 1)     
            LoadFlexiCommandTab();                      //Load Flexi Command Tab (Tab 2)              
        }

        #endregion

        // ******************************************************************
        // Personal Settings Tab
        // ******************************************************************

        #region PersonalSettingsTab

        bool firstLoad = true;

        /// <summary>
        /// Gets and sets status of the checkboxes. Ensures that a check box change won't call CheckStateChanged the first time
        /// </summary>
        private void LoadPersonalSettingsTab()
        {
            minimisedCheckbox.Checked = settingsManager.GetStartMinimizedStatus();
            loadOnStartupCheckbox.Checked = settingsManager.GetLoadOnStartupStatus();
            firstLoad = false;
        }

        /// <summary>
        /// Changing the state of Start Mimized Checkbox enables the apply button
        /// </summary>
        private void minimisedCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (firstLoad == false)
                EnableApplyButton();
        }

        /// <summary>
        /// Changing the state of Load on Startup Checkbox enables the apply button
        /// </summary>
        private void loadOnStartupCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (firstLoad == false)
                EnableApplyButton();
        }

        #endregion

        // ******************************************************************
        // Font Tab (Not implemented yet)
        // ******************************************************************

        #region FontTab

        /// <summary>
        /// Allows changing of coloring/fonts
        /// </summary>
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

        // ******************************************************************
        // Flexi Command Tab
        // ******************************************************************

        #region FlexiCommandTab

        /// <summary>
        /// Loads the Flexi-Command Tab Elements
        /// </summary>
        private void LoadFlexiCommandTab()
        {
            SetUpTempSettingsManager();
            CommandList();
            SelectFirstNode();
        }

        /// <summary>
        /// Flexi Command Tab uses a cloned instance of Settings Manager (tempSettingsManager). Only
        /// when apply is hit. Then those changes are copied over to the actual settings Manager permanently
        /// </summary>
        private void SetUpTempSettingsManager()
        {
            tempSettingsManager = settingsManager.CloneObj();
        }

        /// <summary>
        /// Select the ADD Command First
        /// </summary>
        private void SelectFirstNode()
        {
            TreeNodeCollection nodes = CommandTree.Nodes;
            CommandTree.SelectedNode = nodes[0];
        }

        /// <summary>
        /// Loads all Commands into the Command Tree Element
        /// </summary>
        private void CommandList()
        {
            TreeNode treeNode = new TreeNode("ADD");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DELETE");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("MODIFY");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("UNDO");
            CommandTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("REDO");
            CommandTree.Nodes.Add(treeNode);
        }

        /// <summary>
        /// Event handler for once a new command is selected from the Command Tree
        /// </summary>
        private void CommandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = CommandTree.SelectedNode;
            commandPreview.Text = treeNode.Text;
            currentCommand = settingsManager.StringToCommand(treeNode.Text);

            UpdateDescriptionCommand();
            UpdateListOfCommands();
        }

        /// <summary>
        /// Updates the Command Description Element (when new Command is selected from Command Tree)
        /// </summary>
        private void UpdateDescriptionCommand()
        {
            string description = "Command\n\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            switch (currentCommand)
            {
                case CommandType.ADD:
                    //description = "Add Command\nDescription";
                    break;
                case CommandType.DELETE:
                    //description = "Delete Command\nDescription";
                    break;
                case CommandType.MODIFY:
                    //description = "Update Command\nDescription";
                    break;
                case CommandType.UNDO:
                    //description = "Undo Command\nDescription";
                    break;
                case CommandType.REDO:
                    //description = "Redo Command\nDescription";
                    break;
            }

            commandDescription.Text = description;
        }

        /// <summary>
        /// Update the List of added user commands (when new Command is selected from Command Tree)
        /// </summary>
        private void UpdateListOfCommands()
        {
            listOfCommands.Items.Clear();
            List<string> currentCommandList = tempSettingsManager.GetCommandList(currentCommand);
            foreach (string item in currentCommandList)
                listOfCommands.Items.Add(item);
        }

        /// <summary>
        /// Add Button Hit (Command in Text Box is added to the list of user commands)
        /// </summary>
        private void addUserCommandButton_Click(object sender, EventArgs e)
        {
            EnableApplyButton();
            tempSettingsManager.AddCommand(userCommandInput.Text, currentCommand);
            UpdateListOfCommands();
            ClearInputField();
        }

        /// <summary>
        /// Remove Button Hit (Removes selected Command from the list)
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableApplyButton();
                tempSettingsManager.RemoveCommand(listOfCommands.SelectedItem.ToString(), currentCommand);
                UpdateListOfCommands();
                ClearInputField();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You have selected nothing to remove");
            }

        }

        private void ClearInputField()
        {
            userCommandInput.Text = "";
        }

        #endregion

        // ******************************************************************
        // Apply, OK and Cancel Button Handlers
        // ******************************************************************

        #region UIButtons

        private void EnableApplyButton()
        {
            applyButton.Enabled = true;
        }

        private void DisableApplyButton()
        {
            applyButton.Enabled = false;
        }

        /// <summary>
        /// Sets settingsManager with all the latest changes in the Settings User Interface
        /// </summary>
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

            #region PushCommandsToStringParser
            settingsManager.PushCommands();

            #endregion
        }

        /// <summary>
        /// Apply Button Hit (Sets Settings and Disables the Apply Button)
        /// </summary>
        private void applyButton_Click(object sender, EventArgs e)
        {
            SetSettings();
            DisableApplyButton();
        }

        /// <summary>
        /// Ok Button Hit (Sets Settings and Closes Settings)
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if(applyButton.Enabled==true)
                SetSettings();
            this.Close();
        }

        /// <summary>
        /// Cancel Button Hit (Closes Settings)
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion



















    }
}
