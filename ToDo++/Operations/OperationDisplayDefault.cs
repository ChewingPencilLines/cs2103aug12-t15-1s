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
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            this.storageIO = storageIO;
            List<Task> mostRecentTasks =
                (from task in taskList
                 where task.IsWithinTime(isSpecific, DateTime.Today, DateTime.Today.AddDays(7))
                 select task).ToList();
            mostRecentTasks.Sort(Task.CompareByDateTime);
            if (mostRecentTasks.Count > MAX_TASKS)
                mostRecentTasks = mostRecentTasks.GetRange(0, MAX_TASKS);

            mostRecentTasks.AddRange(from task in taskList where task is TaskFloating select task);

            currentListedTasks = mostRecentTasks;

            return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
        }
    }
}