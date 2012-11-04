using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using BrightIdeasSoftware;
namespace ToDo
{
    class TaskListViewControl : ObjectListView
    {
        List<Task> displayedTasks;
        OLVColumn defaultCol;

        public TaskListViewControl()
        {
            displayedTasks = null;
        }

        internal void Initialize()
        {
            EmptyListMsg = "You have no tasks in your ToDo++ list.\r\nClick on the ? icon above to find out how to get started!";
            this.RowHeight = 32;
            this.ShowItemToolTips = true;
            this.HeaderStyle = ColumnHeaderStyle.None;

            this.AllColumns.Find(e => e.AspectName == "DoneState").AspectToStringConverter = delegate(object state)
            {
                if ((bool)state == true) return "[DONE]";
                else return String.Empty;
            };

            defaultCol = this.AllColumns.Find(e => e.AspectName == "TaskName");
            this.AlwaysGroupByColumn = defaultCol;
            defaultCol.WordWrap = true;
            SetGroupingByDateTime();
        }

        public void UpdateDisplay(Response response)
        {
            displayedTasks = response.TasksToBeDisplayed;  
            if (displayedTasks == null) return;

            switch (response.FormatType)
            {
                case Format.DEFAULT:
                    displayedTasks.Sort(Task.CompareByDateTime);
                    SetGroupingByDateTime();
                    //this.AutoResizeColumns();
                    break;
                case Format.NAME:
                    displayedTasks.Sort(Task.CompareByName);
                    defaultCol.UseInitialLetterForGroup = true;
                    defaultCol.GroupKeyGetter = null;
                    defaultCol.GroupKeyToTitleConverter = null;
                    break;
                case Format.DONE_STATE:
                    break;
                default:
                    Trace.Fail("Some case in UpdateDisplay in TaskListViewControl was not accounted for..!");
                    break;
            }            
            this.SetObjects(displayedTasks);

            Task reorderedTask = null;
            List<Task> reorderedList = new List<Task>();
            for (int i = 0; i < this.Items.Count; i++ )
            {
                reorderedTask = (Task)this.GetNthItemInDisplayOrder(i).RowObject;
                reorderedList.Add(reorderedTask);
            }
            displayedTasks = reorderedList;
        }

        /// <summary>
        /// Capture CTRL+ to prevent resize of all columns.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 107 && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        private void TaskListViewControl_FormatRow(object sender, FormatRowEventArgs e)
        {
            e.Item.SubItems[1].Text = e.RowIndex.ToString();
        }


        // 
        // Grouping Delegates
        //

        #region Group By Date/Time
        private void SetGroupingByDateTime()
        {
            defaultCol.GroupKeyGetter = GroupKeyByDateTime;
            defaultCol.GroupKeyToTitleConverter = GenerateGroupFromKeyDateTime;
        }

        private object GroupKeyByDateTime(object task)
        {
            if (task is TaskFloating)
            {
                return null;
            }
            else if (task is TaskEvent)
            {
                TaskEvent checkTask = (TaskEvent)task;

                if (checkTask.isSpecific.StartDate.Day == false)
                    return null;
                else
                    return checkTask.StartTime.Date;
            }
            else if (task is TaskDeadline)
            {
                TaskDeadline checkTask = (TaskDeadline)task;

                if (checkTask.isSpecific.EndDate.Day == false)
                    return null;
                return checkTask.EndTime.Date;
            }
            else
            {
                Trace.Fail("TaskListViewControl failed to initialize: Object is not a task object!");
                return null;
            }
        }

        private string GenerateGroupFromKeyDateTime(object groupKey)
        {
            if (groupKey == null)
                return "Other Tasks";
            DateTime date = (DateTime)groupKey;
            if (date == DateTime.Now.Date)
                return "Today";
            else if (date > DateTime.Now.Date.AddDays(6))
                return date.ToString("D");
            else
                return date.DayOfWeek.ToString();
        }
        #endregion
    }
}
