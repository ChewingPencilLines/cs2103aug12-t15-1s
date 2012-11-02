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
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }
        private void RefreshTaskListView(Response response)
        {
            List<Task> tasks = response.TasksToBeDisplayed;

            switch (response.FormatType)
            {
                case Format.DEFAULT:
                    /* Do not delete.
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
                        if (!groupExists)
                        {
                            this.Groups.Add(groupToAdd);
                        }

                        // Add item
                        ListViewItem taskItem = new ListViewItem(task.TaskName, groupToAdd);
                        taskItem.SubItems.Add("asd", Color.Chocolate, Color.White, new System.Drawing.Font("Arial", 10));
                        taskItem.SubItems.Add("dsa".ToString());
                        this.Items.Add(taskItem);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
