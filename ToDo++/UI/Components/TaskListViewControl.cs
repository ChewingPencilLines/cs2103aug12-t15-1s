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

namespace ToDo
{
    class TaskListViewControl:ListView
    {
        public TaskListViewControl()
        {
            //this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;            
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

                    var groupNames = from task in tasks
                                     where !(task is TaskFloating)
                                     select task.GetDay().ToString();

                    foreach (Task task in tasks)
                    {
                        string groupName;
                        if (!(task is TaskFloating))
                        {
                            // sort by group
                            groupName = task.GetDay().ToString();
                        }
                        else groupName = "Floating";

                        ListViewGroup groupToAdd = new ListViewGroup(groupName);

                        // check if group exists already
                        bool groupExists = false;
                        foreach (ListViewGroup group in this.Groups)
                        {
                            if (group.Header == groupName)
                            {
                                groupExists = true;
                                groupToAdd = group;
                                break;
                            }
                        }
                        // add if it doesnt
                        if (!groupExists)
                        {
                            this.Groups.Add(groupToAdd);
                        }

                        // Add item
                        ListViewItem taskItem = new ListViewItem(task.TaskName, groupToAdd);
                        taskItem.SubItems.Add(task.GetTimeString());
                        if(task.DoneState == true)
                        taskItem.SubItems.Add("[DONE]".ToString());
                        this.Items.Add(taskItem);
                    }
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
