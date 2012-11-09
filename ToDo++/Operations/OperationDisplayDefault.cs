using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class OperationDisplayDefault : Operation
    {
        const int MAX_TASKS = 10;

        public OperationDisplayDefault()
            : base(SortType.DEFAULT)
        { }

        public OperationDisplayDefault(SortType sortType)
            : base(sortType)
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            
            List<Task> mostRecentTasks =
                (from task in taskList
                 where task.IsWithinTime(isSpecific, DateTime.Today, DateTime.Today.AddDays(7))
                 select task).ToList();

            mostRecentTasks.Sort(Task.CompareByDateTime);

            if (mostRecentTasks.Count > MAX_TASKS)
                mostRecentTasks = mostRecentTasks.GetRange(0, MAX_TASKS);

            mostRecentTasks.AddRange(from task in taskList where task is TaskFloating select task);

            currentListedTasks = new List<Task>(mostRecentTasks);

            return new Response(Result.SUCCESS, SortType.DATE_TIME, this.GetType(), currentListedTasks);
        }
    }
}