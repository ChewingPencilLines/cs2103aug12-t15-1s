using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ToDo
{
    class TaskListView:ListView
    {
        public void PopulateListView()
        {
            List<Task> displayList = new List<Task>();
            TaskEvent addTask = new TaskEvent("test task", DateTime.Now, DateTime.Now, new DateTimeSpecificity());

            List<ListViewGroup> groups = new List<ListViewGroup>();
            groups.Add(new ListViewGroup(DateTime.Today.DayOfWeek.ToString()));
            this.Groups.Add(groups[0]);

            ListViewItem taskItem = new ListViewItem(addTask.TaskName, groups[0]);
            taskItem.SubItems.Add(addTask.StartTime.ToString(), Color.Chocolate, Color.White, new System.Drawing.Font("Arial", 10));
            taskItem.SubItems.Add(addTask.EndTime.ToString());

            this.Items.Add(taskItem);
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }
    }
}
