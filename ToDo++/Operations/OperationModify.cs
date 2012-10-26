using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationModify : Operation
    {
        private int? oldIndex;
        private Task newTask;

        public int? OldIndex
        {
            get { return oldIndex; }
        }

        public Task NewTask
        {
            get { return newTask; }
        }

        public OperationModify(int Previous, Task Revised)
        {
            oldIndex = Previous - 1;
            newTask = Revised;
        }

        public OperationModify(Task Search)
        {
            newTask = Search;
        }

        public OperationModify()
        {
            oldIndex = null;
            newTask = null;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            /*
             *  when modify, if user key in nothing or only index or only task details
             *  after the commandtype, then all tasks will be shown.
             *  only when user input full information will modify operated.
             */
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;
            if (oldIndex.HasValue == false && newTask == null)
            {
                response = opHandler.DisplayAll(taskList);
            }
            else if (oldIndex.HasValue == false && newTask != null)
            {
                int numberOfMatches;
                response = opHandler.Search(out numberOfMatches, taskList, newTask.TaskName);
            }
            else if (oldIndex.HasValue == true && (oldIndex < 0 || oldIndex > taskList.Count - 1))
            {
                if (newTask != null)
                {
                    int numberOfMatches;
                    response = opHandler.Search(out numberOfMatches, taskList, newTask.TaskName);
                }
                else
                {
                    response = opHandler.DisplayAll(taskList);
                }
            }
            else
            {
                Task taskToModify = opHandler.LastListedTasks[oldIndex.Value];
                response = opHandler.Modify(ref taskToModify, newTask, ref taskList, out successFlag);
            }
            if (successFlag) opHandler.TrackOperation(this);
            return response;
        }
    }
}
