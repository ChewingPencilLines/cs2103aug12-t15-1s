using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class OperationSchedule : Operation
    {
        private Task newTask;
        public OperationSchedule(Task setTask)
        {
            newTask = setTask;
        }
        public override string Execute(List<Task> taskList, Storage storageIO)
        {
            string response;
            if (newTask == null)
            {
                return RESPONSE_SCHEDULE_FAILURE;
            }
            //todo
            return "stub";
        }
        public override string Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return DeleteTask(task, taskList, out successFlag);
        }
    }
}
