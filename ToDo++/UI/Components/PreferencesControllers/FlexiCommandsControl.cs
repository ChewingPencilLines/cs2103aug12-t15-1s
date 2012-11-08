using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

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
            this.rangeController.Enabled = false;
        }


        #region ConversionStringToEnum

        public enum SelectedType { CommandSelected = 1, ContextSelected, TimeRangeKeywordsSelected, TimeRangeSelected };

        private CommandType ConvertStringToCommand(string command)
        {
            return (CommandType)Enum.Parse(typeof(CommandType), command);
        }

        private ContextType ConvertStringToContext(string context)
        {
            return (ContextType)Enum.Parse(typeof(ContextType), context);
        }

        private TimeRangeKeywordsType ConvertStringToTimeRangeKeyword(string rangeKeyword)
        {
            return (TimeRangeKeywordsType)Enum.Parse(typeof(TimeRangeKeywordsType), rangeKeyword);
        }

        private TimeRangeType ConvertStringToTimeRange(string timeRange)
        {
            return (TimeRangeType)Enum.Parse(typeof(TimeRangeType), timeRange);
        }

        #endregion

        #region LoadTreeLists

        private void LoadCommandList()
        {
            commandTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            foreach (string commandType in Enum.GetNames(typeof(CommandType)))
            {
                if (!((commandType == "INVALID") || (commandType == "EXIT")))
                {
                    treeNode = new TreeNode(commandType);
                    commandTree.Nodes.Add(treeNode);
                }
            }
        }

        private void LoadContextList()
        {
            contextTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            foreach (string contextType in Enum.GetNames(typeof(ContextType)))
            {
                if (!((contextType == "INVALID") || (contextType == "EXIT")))
                {
                    treeNode = new TreeNode(contextType);
                    contextTree.Nodes.Add(treeNode);
                }
            }
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
            if (this.selectedTimeRangeKeywordType == TimeRangeKeywordsType.NIGHT)
            {
                this.rangeController.InnerColor = Color.Gray;
                this.rangeController.RangeMaximum = this.settings.GetStartTime(selectedTimeRangeKeywordType);
                this.rangeController.RangeMinimum = this.settings.GetEndTime(selectedTimeRangeKeywordType);
            }
            else
            {
                this.rangeController.InnerColor = Color.Black;
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
            descriptionLabel.Text = "";

            if (this.selectedType == SelectedType.CommandSelected)
            {
                title = commandTree.SelectedNode.Text;
                switch (selectedCommand)
                {
                    case CommandType.ADD:
                        SetFormat(Color.Brown, "Floating:\n", 9);
                        SetFormat(Color.Black, "Enter \"add [task name]\"\n", 9);
                        SetFormat(Color.Gray, "eg. add finish project\n", 9);
                        SetFormat(Color.Brown, "Event:\n", 9);
                        SetFormat(Color.Black, "Enter \"add [task name] [start time] {end time} {day/date}\"\n", 9);
                        SetFormat(Color.Gray, "eg. add check todo++ in 2 hours\n", 9);
                        SetFormat(Color.Gray, "eg. add max birthday 4pm tomorrow \n", 9);
                        SetFormat(Color.Gray, "eg. add team meeting 2pm-4pm next wed \n", 9);
                        SetFormat(Color.Brown, "Deadline:\n", 9);
                        SetFormat(Color.Black, "Enter \"add [task name] by [deadline]\" \n", 9);
                        SetFormat(Color.Gray, "eg. add do cs2103 CE2 by saturday midnight \n", 9);
                        break;

                    case CommandType.DELETE:
                        SetFormat(Color.Brown, "Delete Tasks:\n", 9);
                        SetFormat(Color.Black, "Enter \"delete [task name/ID]\"\n", 9);
                        SetFormat(Color.Gray, "eg. delete 3\n", 9);
                        SetFormat(Color.Gray, "eg. delete task\n", 9);
                        SetFormat(Color.Gray, "eg. delete 1-3\n", 9);
                        SetFormat(Color.Brown, "Delete from Schedule:\n", 9);
                        SetFormat(Color.Black, "Enter \"delete [day/date] {before/after} {time}\"\n", 9);
                        SetFormat(Color.Gray, "eg. remove Sunday after 1500hrs\n", 9);
                        SetFormat(Color.Gray, "eg. delete 31 December after 10pm \n", 9);
                        break;

                    case CommandType.DISPLAY:
                        SetFormat(Color.Brown, "Display All Tasks:\n", 9);
                        SetFormat(Color.Black, "Enter \"display\"\n", 9);
                        SetFormat(Color.Brown, "Within Dates:\n", 9);
                        SetFormat(Color.Black, "Enter \"display [day/date] {before/after} {time}\"\n", 9);
                        SetFormat(Color.Gray, "eg. display 06/09/2012\n", 9);
                        SetFormat(Color.Gray, "eg. display Sunday after 1500hrs\n", 9);
                        SetFormat(Color.Gray, "eg. display 6 sept before 10pm\n", 9);
                        SetFormat(Color.Gray, "eg. display next Saturday\n", 9);
                        SetFormat(Color.Gray, "eg. display tomorrow\n", 9);
                        SetFormat(Color.Brown, "Searching Task Names:\n", 9);
                        SetFormat(Color.Black, "Enter \"display [name]\"\n", 9);
                        SetFormat(Color.Gray, "eg. display buy milk\n", 9);
                        SetFormat(Color.Brown, "Searching Task Types:\n", 9);
                        SetFormat(Color.Black, "Enter \"display [type]\"\n", 9);
                        SetFormat(Color.Gray, "eg. display event\n", 9);
                        SetFormat(Color.Gray, "eg. display deadline\n", 9);
                        SetFormat(Color.Gray, "eg. display floating\n", 9);
                        break;

                    case CommandType.DONE:
                        SetFormat(Color.Brown, "Task Done:\n", 9);
                        SetFormat(Color.Black, "Enter \"done [day/date] {before/after} {time}\"\n", 9);
                        SetFormat(Color.Gray, "eg. done task\n", 9);
                        SetFormat(Color.Gray, "eg. done 3\n", 9);
                        SetFormat(Color.Gray, "eg. done 1-3\n", 9);
                        SetFormat(Color.Gray, "eg. done all\n", 9);
                        break;

                    case CommandType.MODIFY:
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
            else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
            {
                title = timeRangeKeywordTree.SelectedNode.Text;
                titleLabel.Text = title;
                switch (selectedTimeRangeKeywordType)
                {
                    default:
                        UpdateTimeRangeDescription();
                        return;
                }
            }

            titleLabel.Text = title;
        }

        private void UpdateTimeRangeDescription()
        {
            descriptionLabel.Clear();
            SetFormat(Color.Black, "MORNING: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.MORNING).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.MORNING).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "AFTERNOON: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.AFTERNOON).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.AFTERNOON).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "EVENING: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.EVENING).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.EVENING).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "NIGHT: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.NIGHT).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.NIGHT).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
        }

        #region FormattingControl

        /// <summary>
        /// Set Formatting of Text to be set into OutputBox
        /// </summary>
        public void SetFormat(Color color, string text, int size)
        {
            RichTextBox box = descriptionLabel;
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            box.SelectionFont = new Font("Tahoma", size,FontStyle.Regular);
            box.SelectionLength = 0;
        }

        #endregion

        private void rangeController_RangeChanged(object sender, EventArgs e)
        {
            if (this.selectedTimeRangeKeywordType == TimeRangeKeywordsType.NIGHT)
            {
                settings.SetTimeRange(selectedTimeRangeKeywordType, rangeController.RangeMaximum, rangeController.RangeMinimum);
                UpdateDescription();
            }
            else
            {
                settings.SetTimeRange(selectedTimeRangeKeywordType, rangeController.RangeMinimum, rangeController.RangeMaximum);
                UpdateDescription();
            }
        }

        private void flatTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flatTabControl1.SelectedIndex == 0)
            {
                commandTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = flatTabControl1.Height-70;
                descriptionLabel.Size = tempSize;
            }
            else if (flatTabControl1.SelectedIndex == 1)
            {
                contextTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = flatTabControl1.Height-70;
                descriptionLabel.Size = tempSize;
            }
            else if (flatTabControl1.SelectedIndex == 2)
            {
                timeRangeKeywordTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = 93;
                descriptionLabel.Size = tempSize;
            }
        }





    }
}
