using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ToDo
{


    public partial class FlexiCommandsControl : UserControl
    {
        Settings settings;
        CommandType selectedCommand;
        ContextType selectedContext;
        SelectedType selectedType;
        TimeRangeKeywordsType selectedTimeRangeKeywordType;
        TimeRangeType selectedTimeRangeType;
        List<string> selectedFlexiCommands;

        public void InitializeFlexiCommands(Settings settings)
        {
            this.settings = settings;
            this.selectedFlexiCommands=new List<string>();
        }

        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadContextList();
            LoadTimeKeywordRangeList();
            LoadTimeRangeList();
            LoadCommandList();
            //this.rangeController.Enabled = false;
            this.rangeController.RangeMaximum = 10;
            this.rangeController.RangeMinimum = 5;
        }


        #region ConversionStringToEnum

        public enum SelectedType { CommandSelected = 1, ContextSelected, TimeRangeKeywordsSelected, TimeRangeSelected };

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
                case "SCHEDULE":
                    return CommandType.SCHEDULE;
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

        private TimeRangeKeywordsType ConvertStringToTimeRangeKeyword(string context)
        {
            switch (context)
            {
                case "MORNING":
                    return TimeRangeKeywordsType.MORNING;
                case "AFTERNOON":
                    return TimeRangeKeywordsType.AFTERNOON;
                case "EVENING":
                    return TimeRangeKeywordsType.EVENING;
                case "NIGHT":
                    return TimeRangeKeywordsType.NIGHT;
            }

            return TimeRangeKeywordsType.NONE;
        }

        private TimeRangeType ConvertStringToTimeRange(string context)
        {
            switch (context)
            {
                case "DAY":
                    return TimeRangeType.DAY;
                case "HOUR":
                    return TimeRangeType.HOUR;
                case "MONTH":
                    return TimeRangeType.MONTH;
            }

            return TimeRangeType.DEFAULT;
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

        private void LoadTimeKeywordRangeList()
        {
            timeRangeKeywordTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("MORNING");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("AFTERNOON");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("EVENING");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("NIGHT");
            timeRangeKeywordTree.Nodes.Add(treeNode);
        }

        private void LoadTimeRangeList()
        {
            timeRangeTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("HOUR");
            timeRangeTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DAY");
            timeRangeTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("MONTH");
            timeRangeTree.Nodes.Add(treeNode);
        }

        #endregion

        #region EventHandlersForButtons

        private void addButton_MouseDown(object sender, MouseEventArgs e)
        {
            addButton.SetMouseDown();
        }

        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            addButton.SetMouseUp();
            ShowUserInputBox();
        }

        private void removeButton_MouseDown(object sender, MouseEventArgs e)
        {
            removeButton.SetMouseDown();
        }

        private void removeButton_MouseUp(object sender, MouseEventArgs e)
        {
            removeButton.SetMouseUp();
            RemoveFlexiCommandFromSettings(this.listedFlexiCommands.SelectedItem.ToString());
            UpdateFlexiCommandList();
        }

        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.contextTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = false;

            this.selectedType = SelectedType.CommandSelected;
            string selected = commandTree.SelectedNode.Text;
            this.selectedCommand = ConvertStringToCommand(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void contextTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.commandTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = false;

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.ContextSelected;
            string selected = contextTree.SelectedNode.Text;
            this.selectedContext = ConvertStringToContext(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void timeRangeKeywordTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.commandTree.SelectedNode = null;
            this.contextTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = true;

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.TimeRangeKeywordsSelected;
            string selected = timeRangeKeywordTree.SelectedNode.Text;
            this.selectedTimeRangeKeywordType = ConvertStringToTimeRangeKeyword(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
            UpdateTimeRangeUI();
        }

        private void timeRangeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.contextTree.SelectedNode = null;
            this.commandTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.rangeController.Enabled = false;

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.TimeRangeSelected;
            string selected = timeRangeTree.SelectedNode.Text;
            this.selectedTimeRangeType = ConvertStringToTimeRange(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void commandTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowUserInputBox();
        }

        private void contextTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowUserInputBox();
        }

        #endregion

        private void ShowUserInputBox()
        {
            UserInputBox.Show("Add Command", "Enter your new command here");
            if (UserInputBox.ValidData())
                AddFlexiCommandToSettings(UserInputBox.GetInput());

            UpdateFlexiCommandList();
        }

        private void UpdateFlexiCommandList()
        {
            listedFlexiCommands.Items.Clear();
            if (this.selectedType == SelectedType.CommandSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string command in settings.GetFlexiKeywordList(this.selectedCommand))
                    this.selectedFlexiCommands.Add(command);
            }
            else if(this.selectedType==SelectedType.ContextSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedContext))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }
            else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedTimeRangeKeywordType))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }
            else if (this.selectedType == SelectedType.TimeRangeSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedTimeRangeType))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }

            foreach (string flexiCommand in this.selectedFlexiCommands)
                listedFlexiCommands.Items.Add(flexiCommand);
        }

        private void UpdateTimeRangeUI()
        {

            MessageBox.Show(this.settings.GetStartTime(selectedTimeRangeKeywordType).ToString());
            if (this.selectedTimeRangeKeywordType == TimeRangeKeywordsType.NIGHT)
            {
                this.rangeController.RangeMaximum = this.settings.GetStartTime(selectedTimeRangeKeywordType);
                this.rangeController.RangeMinimum = this.settings.GetEndTime(selectedTimeRangeKeywordType);
            }
            else
            {
                this.rangeController.RangeMinimum = this.settings.GetStartTime(selectedTimeRangeKeywordType);
                this.rangeController.RangeMaximum = this.settings.GetEndTime(selectedTimeRangeKeywordType);
            }
        }

        private void AddFlexiCommandToSettings(string flexiCommand)
        {
            try
            {
                if (this.selectedType == SelectedType.CommandSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedContext);
                else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedTimeRangeKeywordType);
                else if (this.selectedType == SelectedType.TimeRangeSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedTimeRangeType);
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
                    settings.RemoveFlexiKeyword(flexiCommand, selectedCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.RemoveFlexiKeyword(flexiCommand,selectedContext);
                else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
                    settings.RemoveFlexiKeyword(flexiCommand, selectedTimeRangeKeywordType);
                else if (this.selectedType == SelectedType.TimeRangeSelected)
                    settings.RemoveFlexiKeyword(flexiCommand, selectedTimeRangeType);
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

        private void rangeController_RangeChanged(object sender, EventArgs e)
        {

        }



    }
}
