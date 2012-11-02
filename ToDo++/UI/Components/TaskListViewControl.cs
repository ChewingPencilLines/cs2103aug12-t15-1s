using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using BrightIdeasSoftware;
namespace ToDo
{
    class TaskListViewControl:ObjectListView
    {
        List<Task> displayedTasks;
        public TaskListViewControl()
        {
            displayedTasks = null;
        }

        internal void Initialize()
        {
            EmptyListMsg = "You have no tasks in your ToDo++ list.\r\nClick on the ? icon above to find out how to get started!";
            this.RowHeight = 32;   
            this.HeaderStyle = ColumnHeaderStyle.None;
            OLVColumn defaultCol = this.AllColumns.Find(e => e.AspectName == "TaskName");            
            this.AlwaysGroupByColumn = defaultCol;
            defaultCol.WordWrap = true;
            defaultCol.GroupKeyGetter = delegate(object task)
            {
                if (task is TaskFloating)
                {
                    return null;
                }
                else if (task is TaskEvent)
                {
                    return ((TaskEvent)task).StartTime;
                }
                else if (task is TaskEvent)
                {
                    return ((TaskEvent)task).EndTime;
                }
                else
                {
                    Trace.Fail("object is not a task object!");
                    return null;
                }
            };
            defaultCol.GroupKeyToTitleConverter = delegate(object groupKey)
            {
                DateTime date = (DateTime)groupKey;
                if (date.Date > DateTime.Now.Date.AddDays(6))
                    return date.ToString("D");
                else
                    return date.DayOfWeek.ToString();
            };
            this.AllColumns.Find(e => e.AspectName == "DoneState").AspectToStringConverter = delegate(object state)
            {
                if ((bool)state == true) return "[DONE]";
                else return String.Empty;
            };
        }

        public void UpdateDisplay(Response response)
        {
            displayedTasks = response.TasksToBeDisplayed;

            switch (response.FormatType)
            {
                case Format.DEFAULT:
                    /* Do not delete. Move to logic class for setting default screen.
                    List<Task> mostRecentTasks = 
                        (from task in tasks                                     
                        where task.IsWithinTime(DateTime.Today, DateTime.Today.AddDays(7))
                        select task).ToList();
                    mostRecentTasks.Sort(Task.CompareByDateTime);
                    // 10 = MAX_TASKS
                    mostRecentTasks = mostRecentTasks.GetRange(0, 10);
                     */
                    displayedTasks.Sort(Task.CompareByDateTime);
                    this.SetObjects(displayedTasks);                    
                    //this.AutoResizeColumns();
                    break;                  
                default:
                    break;
            }
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
    }
}
