using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FlexiCommandsControl : UserControl
    {
        Settings settings;
        CommandType selectedCommand;
        ContextType selectedContext;
        SelectedType selectedType;
        List<string> selectedFlexiCommands;

        public void InitializeFlexiCommands(Settings settings)
        {
            this.settings = settings;
            this.selectedFlexiCommands=new List<string>();
        }

        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadCommandList();
            LoadContextList();
        }

        #region ConversionStringToEnum

        public enum SelectedType { CommandSelected = 1, ContextSelected };

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

        private ContextType ConvertStringToContext(string context)
        {
            switch (context)
            {
                case "ON/FROM":
                    return ContextType.STARTTIME;
                case "TO":
                    return ContextType.ENDTIME;
                case "-":
                    return ContextType.ENDTIME;
                case "THIS":
                    return ContextType.CURRENT;
                case "NEXT":
                    return ContextType.NEXT;
                case "FOLLOWING":
                    return ContextType.FOLLOWING;
            }

            return ContextType.DEADLINE;
        }

        #endregion

        #region LoadTreeLists

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
            TreeNode treeNode = new TreeNode("ON/FROM");
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

        #endregion

        #region EventHandlersForButtons

        private void addButton_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Show("Add Command", "Enter your new command here");
            if (CustomMessageBox.ValidData())
                AddFlexiCommandToSettings(CustomMessageBox.GetInput());

            UpdateFlexiCommandList();
        }

        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.contextTree.SelectedNode = null;
            this.selectedType = SelectedType.CommandSelected;
            string selected = commandTree.SelectedNode.Text;
            this.selectedCommand = ConvertStringToCommand(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void contextTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.ContextSelected;
            string selected = contextTree.SelectedNode.Text;
            this.selectedContext = ConvertStringToContext(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveFlexiCommandFromSettings(this.listedFlexiCommands.SelectedItem.ToString());
            UpdateFlexiCommandList();
        }

        #endregion

        private void UpdateFlexiCommandList()
        {
            listedFlexiCommands.Items.Clear();
            if (this.selectedType == SelectedType.CommandSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string command in settings.GetCommandKeywordList(this.selectedCommand))
                    this.selectedFlexiCommands.Add(command);
            }
            else if(this.selectedType==SelectedType.ContextSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetContextKeywordList(this.selectedContext))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }

            foreach (string flexiCommand in this.selectedFlexiCommands)
                listedFlexiCommands.Items.Add(flexiCommand);
        }

        private void AddFlexiCommandToSettings(string flexiCommand)
        {
            try
            {
                if (this.selectedType == SelectedType.CommandSelected)
                    settings.AddCommandKeyword(flexiCommand, this.selectedCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.AddContextKeyword(flexiCommand, this.selectedContext);
            }
            catch (RepeatCommandException e)
            {
                AlertBox.Show(e.Message);
            }

        }

        private void RemoveFlexiCommandFromSettings(string flexiCommand)
        {
            try
            {
                if (this.selectedType == SelectedType.CommandSelected)
                    settings.RemoveCommandKeyword(flexiCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.RemoveContextKeyword(flexiCommand);
            }
            catch (InvalidDeleteFlexiException e)
            {
                AlertBox.Show(e.Message);
            }
        }

        private void UpdateDescription()
        {
            string title="";
            string description="";

            if (this.selectedType == SelectedType.CommandSelected)
            {
                title = commandTree.SelectedNode.Text;
                switch (selectedCommand)
                {
                    case CommandType.ADD:
                        description = "This is a description for ADD Command";
                        break;

                    default:
                        description = "Not Implemented Exception";
                        break;
                }
            }
            else if (this.selectedType == SelectedType.ContextSelected)
            {
                title = contextTree.SelectedNode.Text;
                switch (selectedContext)
                {
                    case ContextType.CURRENT:
                        description = "This is a description for Current Command";
                        break;

                    default:
                        description = "Not Implemented Exception";
                        break;
                }
            }

            titleLabel.Text = title;
            descriptionLabel.Text = description;
        }

        private void PushFlexiCommandsToStringParser()
        {

        }



    }
}
