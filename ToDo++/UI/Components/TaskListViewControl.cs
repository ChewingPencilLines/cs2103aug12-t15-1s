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
using Hotkeys;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using BrightIdeasSoftware;
namespace ToDo
{
    class TaskListViewControl:ObjectListView
    {
        public TaskListViewControl()
        {
        }

        public void UpdateDisplay(Response response)
        {
            List<Task> tasks = response.TasksToBeDisplayed;

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
                    this.SetObjects(tasks);
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
            } //if
        }

    }
}
