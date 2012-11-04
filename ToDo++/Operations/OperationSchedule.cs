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
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            Response response = null;
            if (newTask == null)
            {
                //return RESPONSE_SCHEDULE_FAILURE;
                return new Response(Result.FAILURE,Format.DEFAULT, this.GetType(),  currentListedTasks);
            }
            //todo
            return response;// "stub";
        }
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            return DeleteTask(task, taskList);
        }
    }
}
