using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ToDo
{
    class TaskListViewControl : ObjectListView
    {
        const string MESSAGE_EMPTY_LIST = "You have no tasks in your ToDo++ list.\r\nClick on the ? icon above to find out how to get started!";
        const string MESSAGE_STYLE_DONE = "[DONE]";
        const string COL_NAME_TASK_NAME = "TaskName";
        const string COL_NAME_DONE_STATE = "DoneState";        

        List<Task> displayedTasks;
        OLVColumn defaultCol;
        private Settings settings;

        public TaskListViewControl()
        {
            displayedTasks = null;
        }

        internal void InitializeWithSettings(Settings settings)
        {
            this.settings = settings;
            this.SetFormatting(settings.GetTextSize(), settings.GetFontSelection());

            EmptyListMsg = MESSAGE_EMPTY_LIST;
            this.RowHeight = 32;
            this.ShowItemToolTips = true;
            this.HeaderStyle = ColumnHeaderStyle.None;

            this.AllColumns.Find(e => e.AspectName == COL_NAME_DONE_STATE).AspectToStringConverter = delegate(object state)
            {
                if ((bool)state == true) return MESSAGE_STYLE_DONE;
                else return String.Empty;
            };

            defaultCol = this.AllColumns.Find(e => e.AspectName == COL_NAME_TASK_NAME);
            this.AlwaysGroupByColumn = defaultCol;
            defaultCol.WordWrap = true;
            SetGroupingByDateTime();
        }

        public List<Task> UpdateDisplay(Response response)
        {                        
            if (response.TasksToBeDisplayed == null) return displayedTasks; // don't update display list.

            displayedTasks = response.TasksToBeDisplayed;  

            switch (response.FormatType)
            {
                case Format.DEFAULT:
                    break;
                case Format.NAME:
                    displayedTasks.Sort(Task.CompareByName);
                    SetGroupingByName();
                    break;
                case Format.DATE_TIME:
                    displayedTasks.Sort(Task.CompareByDateTime);
                    SetGroupingByDateTime();
                    break;
                case Format.DONE_STATE:
                    break;
                default:
                    Trace.Fail("Some case in UpdateDisplay in TaskListViewControl was not accounted for..!");
                    break;
            }            

            this.SetObjects(displayedTasks);
            
            List<Task> reorderedList = GenerateReorderedList();

            return reorderedList;
        }

        private void SetGroupingByName()
        {
            defaultCol.UseInitialLetterForGroup = true;
            defaultCol.GroupKeyGetter = null;
            defaultCol.GroupKeyToTitleConverter = null;
        }

        private List<Task> GenerateReorderedList()
        {
            Task reorderedTask = null;
            List<Task> reorderedList = new List<Task>();
            for (int i = 0; i < this.Items.Count; i++)
            {
                reorderedTask = (Task)this.GetNthItemInDisplayOrder(i).RowObject;
                reorderedList.Add(reorderedTask);
            }
            displayedTasks = reorderedList;
            return reorderedList;
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

        private void SetFormatting(int size,string fontName)
        {
            Font x = new Font(fontName, size, FontStyle.Regular);
            this.Font = x;
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
                    return checkTask.StartDateTime.Date;
            }
            else if (task is TaskDeadline)
            {
                TaskDeadline checkTask = (TaskDeadline)task;

                if (checkTask.isSpecific.EndDate.Day == false)
                    return null;
                return checkTask.EndDateTime.Date;
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
